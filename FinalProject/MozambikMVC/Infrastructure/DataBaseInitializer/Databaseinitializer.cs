using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mozambik.Data;
using Mozambik.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MozambikMVC.Infrastructure.DataBaseInitializer
{
    public static class Databaseinitializer
    {
        public static void Seed(IServiceScope scope)
        {
            using (var _maneger = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>())
            {
                var roleManeger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
                var _configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                if (!roleManeger.Roles.Any())
                {
                    var roles = _configuration["Roles"].Split(",");
                    foreach (var role in roles)
                    {
                        IdentityRole<int> identityRole = new IdentityRole<int> { Name = role };
                        var roleResponse = roleManeger.CreateAsync(identityRole).GetAwaiter().GetResult();
                        if (roleResponse.Succeeded)
                        {
                            if (!_maneger.Users.Any())
                            {
                                var user = CreateUser(_configuration);
                                var userResult = _maneger.CreateAsync(user, _configuration["Admin:Password"]).GetAwaiter().GetResult();
                                if (userResult.Succeeded)
                                {
                                    _maneger.AddToRoleAsync(user, roles[0]).GetAwaiter().GetResult();
                                }
                            }

                        }
                    }
                }
            }
            using (var db = scope.ServiceProvider.GetRequiredService<ProductDbContext>())
            {
                if (!db.Abouts.Any())
                {
                    var about = new About
                    {
                        Title = "Haqqımızda",
                        Description = @"Azərbaycanda məşhur olan FROM.AE internet mağazası Yaxın və Orta Şərq ölkələrində və Azərbaycan da daxil olmaqla MDB məkanında bu gün ən məşhur elektrik və məişət texnikası mağazalarından biri hesab olunur.

İnternet mağaza bu statusa maksimal dərəcədə geniş çeşidlər",
                        Address = "Bakı",
                        Email = "bayramova@gmail.com",
                        PhoneNumber = "+994-(55)-670-84-29",

                    };
                    db.Abouts.Add(about);
                    db.SaveChanges();
                }
            }

        }

        private static AppUser CreateUser(IConfiguration _configuration)
        {
            return new AppUser
            {
                Name = _configuration["Admin:Name"],
                Surname = _configuration["Admin:Surname"],
                UserName = _configuration["Admin:Name"] + "_" + _configuration["Admin:Surname"],
                Address = _configuration["Admin:Address"],
                Email = _configuration["Admin:Email"],
                PhoneNumber = "+994-670-84-29",

            };
        }
    }
}
