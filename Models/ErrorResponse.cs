namespace rss_feed.Models {
    public class ErrorResponse {
        public string ErrorText { get; set; }

        public ErrorResponse() {
        }

        public ErrorResponse(string errorText) {
            ErrorText = errorText;
        }
    }
}