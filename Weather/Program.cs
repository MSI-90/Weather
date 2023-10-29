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

            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            app.UseExceptionHandler("/Error");
            //app.UseExceptionHandler("/Error/{0}");
            //app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseStatusCodePagesWithRedirects("/Error/{0}");
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}