using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MS2.Data;
using MS2.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS2.Controllers
{
    public class UserDataController : Controller
    {
        private readonly ILogger<UserDataController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ISiteRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserDataController(ILogger<UserDataController> logger, IEmailSender sender, ISiteRepository repo, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _emailSender = sender;
            _repository = repo;
            _userManager = userManager;
        }
        public async Task<IActionResult> AllOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            IEnumerable<Order> ordersByUser = _repository.GetOrdersByUserId(user.Id);

            return View(ordersByUser);
        }
    }
}
