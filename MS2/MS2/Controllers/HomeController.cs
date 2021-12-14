using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
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
using MS2.Data.Entities;
using System.Collections;

namespace MS2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ISiteRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public HomeController(ILogger<HomeController> logger, IEmailSender sender, ISiteRepository repo, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _emailSender = sender;
            _repository = repo;
            _userManager = userManager;
            _signInManager = signInManager;
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
        public async Task<IActionResult> Menu()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var favs = _repository.GetFavsByUserId(user.Id).ToList();

                if (favs.Count == 0)
                {
                    favs.Add(new Favourite(user.Id, "NO FAVORITES"));
                }

                ViewData["favs"] = favs;


                return View(_repository.GetAllProducts());
            }
            else
            {
                ViewData["favs"] = new List<Favourite>();
                return View(_repository.GetAllProducts());
            }
        }

        [HttpGet("/Careers")]
        public IActionResult Careers()
        {
            return View(_repository.GetAllJobPostings());
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

        [HttpPost("/Menu")]
        public async Task<IActionResult> Menu(string productID)
        {
            bool isLoggedIn = _signInManager.IsSignedIn(User);

            if (isLoggedIn)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                // DID USER FAVORITE?
                var fav = _repository.DidUserFavorite(user.Id, productID);

                // If not, add fav. If so, remove fav.
                if (fav.Count() == 0) _repository.AddFavorite(user.Id, productID);
                else _repository.RemoveFav(fav.First());

                var favs = _repository.GetFavsByUserId(user.Id).ToList();

                if (favs.Count == 0)
                {
                    favs.Add(new Favourite(user.Id, "NO FAVORITES"));
                }

                ViewData["favs"] = favs;
            }
            else
            {
                ViewData["favs"] = new List<Favourite>();
            }
            return View(_repository.GetAllProducts());
        }

        public async Task<IActionResult> ShoppingCartAsync()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if(user != null)
                ViewBag.UserAddress = user.Address;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("/Favorites")]
        async public Task<IActionResult> Favorites()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var favs = _repository.GetFavsByUserId(user.Id).ToList();

                List<Product> list = new List<Product>();

                var prods = _repository.GetAllProducts().ToList();

                foreach (Favourite f in favs)
                {
                    Product p = prods.Where(p => p.Id.ToString() == f.ProductId).ToList()[0];
                    list.Add(p);
                }

                return View(list);
            }
            else
            {
                ViewData["favs"] = new List<Favourite>();
                return View(_repository.GetAllProducts());
            }
        }
    }
}
