using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OdeToFoodCore.Data;
using OdeToFoodCore.Services;

namespace OdeToFoodCore
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            }).
            AddOpenIdConnect(options =>
            {                
                _configuration.Bind("AzureAd", options);
            })
            .AddCookie();

            services.AddSingleton<IGreeter, Greeter>();
            //services.AddSingleton<IRestaurantData, InMemoryRestaurantData>();
            services.AddDbContext<OdeToFoodDbContext>(
                options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IRestaurantData, SQLRestaurantData>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                                IHostingEnvironment env,
                                IGreeter greeter, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Use(next =>
            //{
            //    return async context =>
            //    {
            //        logger.LogInformation("Request Incoming");
            //        if (context.Request.Path.StartsWithSegments("/mym"))
            //        {
            //            await context.Response.WriteAsync("Hitt!!!");
            //            logger.LogInformation("Request handled");
            //        }
            //        else
            //        {
            //            await next(context);
            //            logger.LogInformation("Request outgoing");
            //        }
            //    };
            //});


            //app.UseWelcomePage(new WelcomePageOptions
            //{
            //    Path = "/wp"
            //});

            //app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseNodeModules(env.ContentRootPath);
            //app.UseFileServer();

            app.UseAuthentication();

            app.UseRewriter(new RewriteOptions()
                .AddRedirectToHttpsPermanent());

            app.UseMvc(ConfigureRoutes);

            //app.Run(async (context) =>
            //{
            //    var greeting = greeter.GetMessageOfTheDay();
            //    context.Response.ContentType = "text/plain";
            //    await context.Response.WriteAsync($"Not found");
            //});
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // /Home/Index
            routeBuilder.MapRoute("Default", 
                "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
