


namespace HttpWebServer.Shared.DataTransfer
{
    using System;
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public string InputForHTTPServerClass { get; set; }

    }
}
