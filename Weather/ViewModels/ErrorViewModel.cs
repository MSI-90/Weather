namespace Weather.ViewModels
{
    public class ErrorViewModel
    {
        public int? StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}