using System.Drawing.Text;
using System.Net;

namespace Weather.OtherTools
{
    internal static class CookieTools
    { 
        internal static Queue<string> LastWeather {  get; set; } = new Queue<string>();
        private const string _key = "city";
        private const int _numberOfCityes = 4;
        internal static void SetOnce(HttpContext? context, string value)
        {
            if (context != null)
            {
                CookieOptions options = new()
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddMonths(1)
                };

                if (!context.Request.Cookies.ContainsKey(_key))
                    context.Response.Cookies.Append(_key, value, options);
                else
                {
                    if (context.Request.Cookies.TryGetValue(_key, out var cookie))
                    {
                        string[] elementsFromCookie = cookie.Split("|");
                        bool isIsset = elementsFromCookie.Contains(value);

                        if (!isIsset && elementsFromCookie.Length < _numberOfCityes)
                        {
                            string newValue = cookie + "|" + value;
                            context.Response.Cookies.Append(_key, newValue, options);

                            foreach (string city in elementsFromCookie)
                            {
                                LastWeather.Enqueue(city);
                            }
                        }
                    } 
                }
            }
        }

    }
}
