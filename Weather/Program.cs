using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Extensions;
using System.Reflection;
using Weather.Services;
using Weather.Services.Implementations;
using Weather.Services.Interfaces;

namespace Weather
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            //Add services to the container.

            //builder.Services.AddSession(options =>
            //{
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            //    options.IdleTimeout = TimeSpan.FromMinutes(1);
            //});

            builder.Services.AddMvc();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<ReadCityesFromFile>();
            builder.Services.AddTransient<ICookieTools, CookieTools>();
            builder.Services.AddTransient<IWeatherConnection, WeatherService>();
            builder.Services.AddTransient<ICitiesParseJsonFile,  RegionParseService>();

            builder.Services.AddBreadcrumbs(Assembly.GetExecutingAssembly(), options =>
            {
                options.TagName = "nav";
                options.TagClasses = "";
                options.OlClasses = "breadcrumb";
                options.LiClasses = "breadcrumb-item";
                options.ActiveLiClasses = "breadcrumb-item active";
            });


            var app = builder.Build();

            app.UseHsts();
            //app.UseExceptionHandler("/Error");
            app.UseStatusCodePagesWithRedirects("/status/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}