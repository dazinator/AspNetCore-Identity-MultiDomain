using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sample.Data;
using Sample.Models;
using Sample.Services;

namespace Sample
{
    public class Startup
    {
        private CookieAuthenticationOptions cookieAuthOptions;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            var cookieName = "myCookieName";

            services.AddIdentity<ApplicationUser, IdentityRole>(a =>
            {
                //a.Cookies.ApplicationCookie.Events = new CustomCookieAuthenticationEvents();
                // a.Cookies.ApplicationCookie.TicketDataFormat = skyConnectCookieFormat;
                a.Cookies.ApplicationCookie.CookieName = cookieName;
                a.Cookies.ApplicationCookie.LoginPath = "/Account/Login";
                // a.Cookies.ApplicationCookie.AccessDeniedPath = accessDeniedPath;
                a.Cookies.ApplicationCookie.AutomaticChallenge = true;
                a.Cookies.ApplicationCookie.AutomaticAuthenticate = true;
                a.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromMinutes(120);
                // a.Cookies.ApplicationCookie.AuthenticationScheme = 
                cookieAuthOptions = a.Cookies.ApplicationCookie;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // allow cookie to be used on domains foo and bar.
            cookieAuthOptions.CookieDomain = ".foo.com";
            app.UseCookieAuthentication(cookieAuthOptions);

            cookieAuthOptions.CookieDomain = ".bar.com";
            app.UseCookieAuthentication(cookieAuthOptions);

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
