using System.Net;

namespace Weather.OtherTools
{
    internal static class CookieTools
    { 
        internal static Queue<string> LastWeather {  get; set; } = new Queue<string>();
        internal const string key = "city";
        internal static void SetOnce(HttpContext? context, string value)
        {
            if (context != null)
            {
                CookieOptions options = new();
                options.HttpOnly = true;
                options.Secure = true;
                options.Expires = DateTimeOffset.UtcNow.AddMonths(1);

                if (!context.Request.Cookies.ContainsKey(key))
                    context.Response.Cookies.Append(key, value, options);
                else
                {
                    if (context.Request.Cookies.TryGetValue(key, out var cookie))
                        value = cookie + "|" + value;
                }

                context.Response.Cookies.Append(key, value, options);
            }
        }

    }
}
