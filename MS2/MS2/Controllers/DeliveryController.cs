using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MS2.Data;
using MS2.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS2.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly ISiteRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeliveryController(ISiteRepository repo, UserManager<ApplicationUser> userMgr)
        {
            _repository = repo;
            _userManager = userMgr;
        }
        [Authorize(Roles = "Driver")]
        public IActionResult OpenOrders()
        {
            var allOrders = _repository.GetOrdersByStatus(OrderStatus.Ordered.ToString());

            return View(allOrders);
        }

        [Authorize(Roles = "Driver")]
        public async Task<IActionResult> CompletedOrders()
        {
            var user = await _userManager.GetUserAsync(User);

            var allOrders = _repository.GetOrdersByStatus(OrderStatus.Delivered.ToString());

            return View(allOrders.Where(o => o.DeliveryDriverId == user.Id).OrderByDescending(o => o.OrderDate));
        }

        [Authorize(Roles = "Driver")]
        [HttpPost]
        public async Task<IActionResult> AssignOrder(IFormCollection form)
        {
            var user = await _userManager.GetUserAsync(User);

            int orderNumber = Int32.Parse(form["OrderNumber"]);

            Order order = _repository.GetOrderByOrderNumber(orderNumber);
            order.DeliveryDriverId = user.Id;
            order.Status = OrderStatus.OutForDelivery.ToString();
            _repository.SaveAll();
            return Redirect("OpenOrders");
        }

        [Authorize( Roles = "Driver")]
        [HttpPost]
        public async Task<IActionResult> MarkAsComplete(IFormCollection form)
        {
            var user = await _userManager.GetUserAsync(User);

            int orderNumber = Int32.Parse(form["OrderNumber"]);

            Order order = _repository.GetOrderByOrderNumber(orderNumber);
            order.DeliveryDriverId = user.Id;
            order.Status = OrderStatus.Delivered.ToString();
            _repository.SaveAll();
            return Redirect("~/UserRoles/Dashboard");
        }
    }
}
