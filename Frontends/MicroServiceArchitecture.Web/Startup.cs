using MicroServiceArchitecture.Shared.Services;
using MicroServiceArchitecture.Web.Extensions;
using MicroServiceArchitecture.Web.Handler;
using MicroServiceArchitecture.Web.Helpers;
using MicroServiceArchitecture.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace MicroServiceArchitecture.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ResourceOwnerPasswordTokenHandler>();
            services.AddScoped<ClientCredentialTokenHandler>();
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();
            services.AddAccessTokenManagement();
            services.AddScoped<PhotoHelper, PhotoHelper>();

            services.Configure<ServiceApiSettings>(Configuration.GetSection("ServiceApiSettings"));
            services.Configure<ClientSettings>(Configuration.GetSection("ClientSettings"));

            services.AddHttpClientServices(Configuration);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opts =>
            {
                opts.LoginPath = "/Auth/SignIn";
                opts.ExpireTimeSpan = TimeSpan.FromDays(60);
                opts.SlidingExpiration = true;
                opts.Cookie.Name = "microservicewebcookie";
            });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
