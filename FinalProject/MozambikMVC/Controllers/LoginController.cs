using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mozambik.Data;
using MozambikMVC.Models;

namespace MozambikMVC.Controllers
{
    public class LoginController : Controller
    {
        private SignInManager<AppUser> _maneger;
        private UserManager<AppUser> _userManeger;
        private IPasswordValidator<AppUser> _pswValidator;
        public LoginController(SignInManager<AppUser> maneger
                                            ,UserManager<AppUser> userManager
                                            ,IPasswordValidator<AppUser> passwordValidator)
        {
            _pswValidator = passwordValidator;
            _userManeger = userManager;
            _maneger = maneger;
        }
        [HttpPost]
        public IActionResult SignUp(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new AppUser
                    {
                        Email = model.Email,
                        UserName = model.Name,
                    };
                    var result = _userManeger.CreateAsync(user, model.Password).Result;
                    if (result.Succeeded)
                    {
                        _userManeger.AddToRoleAsync(user, "User");
                        _maneger.SignInAsync(user,true);
                    }
                

                }
                catch
                {

                    throw new Exception("User yaradilmadi");
                }
               
               
            }
            return RedirectToAction("Index", "Home", new { Area = "null" });
        }
        [HttpPost]
        public IActionResult SignIn(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   var user =  _userManeger.FindByEmailAsync(model.Email).Result;
                    if (user!=null)
                    {
                        var result = _pswValidator.ValidateAsync(_userManeger, user, model.Password);
                        if (result.Result.Succeeded)
                        {
                            _maneger.SignInAsync(user, true).GetAwaiter().GetResult();
                        }
                    }

                }
                catch
                {

                    throw new Exception("SignIN ugursuzdur");
                }


            }
            return RedirectToAction("Index", "Home", new { Area = "null" });
        }
        public IActionResult SignOut()
        {
            _maneger.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction("Index", "Home", new { Area = "null" });
        }
    }
}