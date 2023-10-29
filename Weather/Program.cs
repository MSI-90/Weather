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

            //app.UseExceptionHandler("/Error/{0}");
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "Error",
                pattern: "Error/Message/{message}",
                defaults: new { controller = "Error", action = "Error" });

            app.Run();
        }
    }
}