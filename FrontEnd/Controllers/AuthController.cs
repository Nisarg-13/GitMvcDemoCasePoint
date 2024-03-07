using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Models;
using FrontEnd.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Controllers
{
    // [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthRepositories _authRepositories;

        public AuthController(ILogger<AuthController> logger, IAuthRepositories authRepositories)
        {
            _logger = logger;
            _authRepositories = authRepositories;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AuthModel login)
        {
            if (ModelState.IsValid)
            {
                _authRepositories.Login(login);
                return RedirectToAction("EmpDisplay", "Employee");
            }
            return View(login);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AuthModel register)
        {
            if (_authRepositories.IsEmailExists(register.c_email))
            {
                ViewBag.Message = "Email already exists";
                return View();
            }
            _authRepositories.Register(register);
            return RedirectToAction("Login");

            // return View(register);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}