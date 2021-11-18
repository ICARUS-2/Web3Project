using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MS2.Data;
using MS2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using static MS2.Controllers.Captcha;

namespace MS2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ISiteRepository _repository;
       
        public HomeController(ILogger<HomeController> logger, IEmailSender sender, ISiteRepository repo)
        {
            _logger = logger;
            _emailSender = sender;
            _repository = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/About")]
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("/Order")]
        public IActionResult Order()
        {
            return View(_repository.GetAllProducts());
        }

        [HttpGet("/Menu")]
        public IActionResult Menu()
        {
            return View(_repository.GetAllProducts());
        }

        [HttpGet("/Careers")]
        public IActionResult Careers()
        {
            return View();
        }

        [HttpGet("Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("/Contact")]
        public async Task<IActionResult> Contact(IFormCollection form)
        {
            const string CAPTCHA_SECRET_KEY = "6Ld6gRQdAAAAAH11bYtZc99wK1_yNoUbk1vkDJZe";

            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(form["g-recaptcha-response"]))
                {
                    ViewData["Title"] = "Error!";
                    return View("Error");
                }
                ReCaptchaResponse reCaptchaResponse = VerifyCaptcha(CAPTCHA_SECRET_KEY, form["g-recaptcha-response"]);
                if (!reCaptchaResponse.success)
                {
                    ViewData["Title"] = "Error!";
                    return View("Error");
                }

                ContactModel contact = new ContactModel
                {
                    Timestamp = DateTime.Now,
                    Email = form["Email"],
                    Topic = form["Topic"],
                    Message = form["Message"],
                    FirstName = form["FirstName"],
                    LastName = form["LastName"]
                };

                //_repository.Contacts.Add(contact);
                await _emailSender.SendEmailAsync(contact.Email, contact.Topic, contact.Message);
                return View("Success", contact);
            }
            return View();
        }

        public IActionResult ShoppingCart()
        {
            return View(_repository.GetShoppingCartItems());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
