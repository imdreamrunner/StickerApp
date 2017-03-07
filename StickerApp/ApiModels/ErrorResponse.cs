namespace StickerApp.ApiModels
{
    public class ErrorResponse
    {
        public ErrorResponse(string error, string reason = null)
        {
            this.Error = error;
            this.Reason = reason;
        }

        public string Error { get; set; }
        public string Reason { get; set; }
    }
}