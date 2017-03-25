


namespace HttpWebServer.Shared.DataTransfer
{
    using System;
    public class HTTPValidationResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public string InputForHTTPServerClass { get; set; }
        public HttpWebServer.Shared.Enums.ServerCommandsEnums HTTPServerClassCommand { get; set; }
    }
}
