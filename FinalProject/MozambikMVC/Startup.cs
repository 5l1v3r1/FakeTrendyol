using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Mozambik.Data;

namespace MozambikMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.ConfigureApplicationCookie(x =>
            {
                x.LoginPath = "/jungle/abouts/index";
                x.AccessDeniedPath = new PathString("/jungle/account/login");
                x.LogoutPath = new PathString("/jungle/account/login");
            }
                      
            ); 
            services.AddControllersWithViews(options=> {

                options.EnableEndpointRouting = false;
                //var policy = new AuthorizationPolicyBuilder().AddAuthenticationSchemes()
                //                                            .RequireAuthenticatedUser()
                //                                            .Build();
            });;
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddSession();

            services.AddDistributedMemoryCache();
            services.AddDbContext<ProductDbContext>(x => x.UseSqlServer(Configuration["Db"]));
            services.AddIdentity<AppUser, IdentityRole<int>>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ProductDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(),"Images")),
                RequestPath = new PathString("/Images"),
                
            });
            
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                       name: "Jungle",
                      pattern: "{area:exists}/{controller}/{action}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",    
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
