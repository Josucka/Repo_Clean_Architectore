﻿using Clean.Architecture.Domain.Account;
using Clean.Architecture.WebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Clean.Architecture.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticate _authenticate;

        public AccountController(IAuthenticate authenticate)
        {
            _authenticate = authenticate;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model) 
        {
            var result = await _authenticate.RegisterUser(model.Email, model.Password);
            if (result)
                return RedirectToAction("/");
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid register attempt. (Password must be long).");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _authenticate.Authenticate(model.Email, model.Password);
            if (result)
            {
                if (string.IsNullOrEmpty(model.ReturnUrl))
                    return RedirectToAction("Index", "Home");
                return RedirectToAction(model.ReturnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt. (password must be strong).");
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _authenticate.Logout();   
            return RedirectToAction("/Account/Login");
        }
    }
}
