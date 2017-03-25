

namespace HttpWebServer.Classes.Models
{
    using System;
    using HttpWebServer.Shared.Enums;
    using System.Text.RegularExpressions;
    using Interfaces;
    using HttpWebServer.Shared.DataTransfer;

    
    public class WebsiteBinding : IValidatable
    {
        private ValidationResult _validationResult;
        public WebsiteBinding(string websiteName, HostTypes hostType, string port, Protocol protocol, string webSidePath )
        {
            this.WebSiteName = websiteName;
            this.HostType = hostType;
            this.Port = port;
            this.Protocol = protocol;
            this.WebsitePath = WebsitePath;

            this._validationResult = new ValidationResult()
            {
                IsValid = false,
                Message = ""
            };
        }
        public string WebSiteName { get; set; }
        public HostTypes HostType { get; set; }
        public string Port { get; set; }
        public Protocol Protocol { get; set; }
        public string WebsitePath { get; set; }

        public ValidationResult Validate()
        {
            throw new NotImplementedException();
        }
    }
}
