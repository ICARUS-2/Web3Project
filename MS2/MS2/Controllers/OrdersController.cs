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

namespace MS2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
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

            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user != null)
            {
                string message = order.ToHtmlText();

                await _emailSender.SendEmailAsync(user.Email, $"Order Report", message);
            }

            return CreatedAtAction("GetSamurai", new { id = order.OrderNumber }, order);
        }
    }
}
