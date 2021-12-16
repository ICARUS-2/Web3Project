using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS2.Data;
using MS2.Data.Entities;
using MS2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MS2.Models;

namespace MS2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly ISiteRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        public OrdersController(ISiteRepository repository, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _repository = repository;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _repository.GetAllProducts();
        }

        // POST api/<OrderController>
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<OrderEntry>> Post([FromBody] OrderParser entry)
        {
            Order order = entry.ParseOrder(_repository);

            order.UserId = (User.Identity.IsAuthenticated) ? _userManager.GetUserId(User) : "";

            _repository.InsertOrder(order);
            _repository.SaveAll();

            if (User.Identity.Name != null)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                string message = order.ToHtmlText();

                await _emailSender.SendEmailAsync(user.Email, $"Order Report", message);
            } 

            return CreatedAtAction("GetSamurai", new { id = order.OrderNumber }, order);
        }

        [HttpGet("/SalesBreakdown")]
        public IActionResult SalesBreakdown(string period)
        {
            string[] split = period.Split(' ');

            DateTime start = DateTime.Parse(split[1]);

            List<Order> orders = null;

            switch (split[0])
            {
                case "Day":
                    orders = _repository.GetOrdersByDate(start).ToList();
                    break;

                case "Week":
                    orders = _repository.GetOrdersByDateRange(start, start.AddDays(7)).ToList();
                    break;

                case "Month":
                    orders = _repository.GetOrdersByDateRange(start, start.AddMonths(1)).ToList();
                    break;

                case "Year":
                    orders = _repository.GetOrdersByDateRange(start, start.AddYears(1)).ToList();
                    break;
            }

            List<SalesBreakdownViewModel> vmList = new List<SalesBreakdownViewModel>();

            // Populate a list of viewmodels with empty values for each product
            foreach (Product p in _repository.GetAllProducts())
            {
                SalesBreakdownViewModel newVm = new SalesBreakdownViewModel();
                newVm.Product = p;
                vmList.Add(newVm);
            }

            // Add up the amounts for each product in each order
            foreach (Order o in orders)
            {
                foreach (OrderEntry entry in o.Items)
                {
                    string name = entry.Product.ItemName;

                    switch (entry.Size)
                    {
                        case "Small":
                            vmList.Find((vm) => vm.Product.ItemName == name).SmallUnits += entry.Quantity;
                            break;

                        case "Medium":
                            vmList.Find((vm) => vm.Product.ItemName == name).MediumUnits += entry.Quantity;
                            break;

                        case "Large":
                            vmList.Find((vm) => vm.Product.ItemName == name).LargeUnits += entry.Quantity;
                            break;
                    }
                }
            }

            ViewData["Period"] = period.Remove(0, split[0].Length + 1);
            return View(vmList);
        }

        [HttpGet("/OrdersByPeriod")]
        public IActionResult OrdersByPeriod(string period)
        {
            ViewData["Period"] = period;
            return View();
        }
    }
}
