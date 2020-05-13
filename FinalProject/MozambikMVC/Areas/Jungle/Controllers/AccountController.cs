using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mozambik.Data;
using MozambikMVC.Areas.Jungle.Models;

namespace MozambikMVC.Areas.Jungle.Controllers
{
    [Area("Jungle")]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _maneger;
        private readonly UserManager<AppUser> _userManeger;
        private readonly IPasswordValidator<AppUser> _pswValidator;
        public AccountController(SignInManager<AppUser> maneger
                                            , UserManager<AppUser> userManager
                                            , IPasswordValidator<AppUser> passwordValidator)
        {
            _pswValidator = passwordValidator;
            _userManeger = userManager;
            _maneger = maneger;
        }

        [Route("jungle/account/login")]
        [Route("account/login")]
        public IActionResult Login()
        {
            return View();
        }
        [Route("jungle/account/login")]
        [Route("account/login")]
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userManeger.FindByEmailAsync(model.Email).Result;
                    if (user != null)
                    {
                        var result = _pswValidator.ValidateAsync(_userManeger, user, model.Password);
                        //  var result = _maneger.CheckPasswordSignInAsync(user, model.Password, false).Result;
                        if (result.Result.Succeeded)
                        {
                            _maneger.SignInAsync(user, true).GetAwaiter().GetResult();
                            //HttpContext.User = _maneger.CreateUserPrincipalAsync(user).Result;
                        }
                    }

                    return RedirectToAction("Index", "Abouts", new { area = "Jungle" });

                }
                catch
                {
                    //throw new Exception("SignIN ugursuzdur");
                   
                }
            }
            return RedirectToAction("Login","Account",new { area = "Jungle" });
        }
        public IActionResult LogOut()
        {
            _maneger.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction("Login", "Account", new { area = "Jungle" });

        }
    }
}