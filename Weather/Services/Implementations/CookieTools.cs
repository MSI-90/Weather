using Microsoft.AspNetCore.Mvc.Formatters;
using Weather.Services.Interfaces;

namespace Weather.Services.Implementations
{
    internal class CookieTools : ICookieTools
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public List<string> LastWeather { get; set; }
        internal string[] elementsFromCookie { get; private set; }
        private const string _key = "city";
        private const int _numberOfCityes = 4;
        public CookieTools(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            LastWeather = GetLastCookie().ToList();
        }
        public IEnumerable<string> GetLastCookie()
        {
            var context = _contextAccessor.HttpContext;
            if (context != null)
            {
                if (context.Request.Cookies.ContainsKey(_key))
                {
                    context.Request.Cookies.TryGetValue(_key, out string? cookieValue);
                    string[] array = cookieValue?.Split("|") ?? new string[0];
                    if (array.Count() > 0)
                        array.ToList();

                    return array;
                }
            }

            return new List<string>();
        }
        public void SetOnce(string value)
        {
            var context = _contextAccessor.HttpContext;
            if (context != null)
            {
                CookieOptions options = new()
                {
                    SameSite = SameSiteMode.Strict,
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddMonths(1)
                };
                if (!string.IsNullOrEmpty(value))
                {
                    if (!context.Request.Cookies.ContainsKey(_key))
                    {
                        context.Response.Cookies.Append(_key, value, options);
                        LastWeather.Add(value);
                    }
                    else
                    {
                        if (context.Request.Cookies.TryGetValue(_key, out var cookie))
                        {
                            elementsFromCookie = cookie.Split("|", StringSplitOptions.TrimEntries);
                            elementsFromCookie = elementsFromCookie.Where(e => !string.IsNullOrEmpty(e)).ToArray();

                            if (!elementsFromCookie.Contains(value) && elementsFromCookie.Length < _numberOfCityes)
                            {
                                string newValue = cookie + "|" + value;
                                context.Response.Cookies.Append(_key, newValue, options);
                            }

                            if (!elementsFromCookie.Contains(value) && elementsFromCookie.Length == _numberOfCityes)
                            {
                                List<string> arr = new List<string>();
                                foreach (var item in elementsFromCookie)
                                    arr.Add(item);

                                arr.RemoveAt(0);
                                arr.Add(value);

                                string newValue = arr[0]+"|";
                                for (int i = 1; i < arr.Count; i++)
                                {
                                    elementsFromCookie[i] = arr[i];
                                    newValue += arr[i] + "|";
                                }

                                context.Response.Cookies.Append(_key, newValue, options);
                            }

                            AddCity(LastWeather, elementsFromCookie, cookie);
                        }
                    }
                }
            }
        }
        public void AddCity(List<string> list, string[] array, string cookie)
        {
            //array = !string.IsNullOrEmpty(cookie) ? cookie.Split("|") : Array.Empty<string>();
            if (array.Length > 0)
            {
                list.Clear();
                list = array.ToList();
            }
        }
    }
}
