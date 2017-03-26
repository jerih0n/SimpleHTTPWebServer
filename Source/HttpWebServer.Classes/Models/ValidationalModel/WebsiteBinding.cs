

namespace HttpWebServer.Classes.Models
{
    using System;
    using HttpWebServer.Shared.Enums;
    using System.Text.RegularExpressions;
    using Interfaces;
    using HttpWebServer.Shared.DataTransfer;

    
    public class WebsiteBinding : IValidatable
    {
        private HTTPValidationResult _validationResult;
        //validation messages
        private const string Success = "New website binding is successfuly created";
        private const string WebsiteNameExist = "Website with that name already exist";
        private const string PortIsNotIntege = "Port must be a integer number";
        private const string InvalidPort = "Invalid port number! Port must be an integer number between 0 and 65535";
        private const string PathDoesNotExist = "Directory Path does not exist!";
        public WebsiteBinding(string websiteName, HostType hostType, string port, Protocol protocol, string webSidePath )
        {
            this.WebSiteName = websiteName;
            this.HostType = hostType;
            this.Port = port;
            this.Protocol = protocol;
            this.WebsitePath = WebsitePath;

            this._validationResult = new HTTPValidationResult()
            {
                IsValid = false,
                Message = "",
                InputForHTTPServerClass = "error"
            };
        }
        public string WebSiteName { get; set; }
        public HostType HostType { get; set; }
        public string Port { get; set; }
        public Protocol Protocol { get; set; }
        public string WebsitePath { get; set; }

        public HTTPValidationResult Validate()
        {
            //TODO : // website port  must be unique
            //TODO : // nameShouldBeUnique
            //TODO : // port should be an integer and between 0 and 65535
            //TODO : // website path shoud exist
            return this._validationResult;
        }
    }
}
