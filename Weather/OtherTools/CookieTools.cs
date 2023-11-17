using System.Drawing.Text;
using System.Net;

namespace Weather.OtherTools
{
    internal static class CookieTools
    {
        internal static List<string> LastWeather { get; private set; } = new List<string>();
        internal static string[] elementsFromCookie { get; private set; }
        private const string _key = "city";
        private const int _numberOfCityes = 4;
        internal static void SetOnce(HttpContext? context, string value)
        {
            if (context != null)
            {
                CookieOptions options = new()
                {
                    //SameSite = SameSiteMode.Strict,
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddMonths(1)
                };

                if (!context.Request.Cookies.ContainsKey(_key))
                {
                    context.Response.Cookies.Append(_key, value, options);
                    
                }
                else
                {
                    if (context.Request.Cookies.TryGetValue(_key, out var cookie))
                    {
                        elementsFromCookie = cookie.Split("|");

                        if (!elementsFromCookie.Contains(value) && elementsFromCookie.Length < _numberOfCityes)
                        {
                            string newValue = cookie + "|" + value;
                            context.Response.Cookies.Append(_key, newValue, options);
                        }

                        AddCity(LastWeather, elementsFromCookie, cookie);
                    }
                }
            }
        }
        private static void AddCity(List<string> list, string[] array, string cookie)
        {
            array = !string.IsNullOrEmpty(cookie) ? cookie.Split("|") : new string[0];
            if(array.Length > 0)
            {
                foreach (string value in array)
                {
                    if (!list.Contains(value))
                    {
                        list.Add(value);
                    }
                }
            }
        }
    }
}
