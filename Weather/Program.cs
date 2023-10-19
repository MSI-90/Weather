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
            builder.Services.AddTransient<IWeatherConnection, WeatherModelService>();
            builder.Services.AddTransient<IParseFromJsonFile,  CityesListParseService>();

            var app = builder.Build();


            //app.UseExceptionHandler("/Error");
            //app.UseStatusCodePages();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}