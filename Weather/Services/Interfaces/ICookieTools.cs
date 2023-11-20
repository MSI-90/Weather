namespace Weather.Services.Interfaces
{
    public interface ICookieTools
    {
        List<string> LastWeather {  get; set; }
        IEnumerable<string> GetLastCookie();
        void SetOnce(string value);
        void AddCity(List<string> list, string[] array, string cookie);
    }
}
