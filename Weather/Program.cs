using Microsoft.AspNetCore.Mvc.RazorPages;
using Weather.Services;
using Weather.Services.Interfaces;

namespace Weather
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddMvc();
            builder.Services.AddTransient<IWeatherConnection, WeatherService>();
            builder.Services.AddTransient<ICitiesParseJsonFile,  RegionParseService>();

            var app = builder.Build();

            app.UseExceptionHandler("/Error");
            app.UseStatusCodePagesWithRedirects("/Error/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}