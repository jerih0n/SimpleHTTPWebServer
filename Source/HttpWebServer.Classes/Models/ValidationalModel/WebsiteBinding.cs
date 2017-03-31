

namespace HttpWebServer.Classes.Models
{
    using System;
    using HttpWebServer.Shared.Enums;
    using System.Text.RegularExpressions;
    using Interfaces;
    using HttpWebServer.Shared.DataTransfer;
    using System.IO;

    public class WebsiteBinding : IValidatable
    {
        private HTTPValidationResult _validationResult;
        //validation messages
        private const string Success = "New website binding is successfuly created";
        private const int MaxPortNumber = 65535;
        private const string WebsiteNameExist = "Website with that name already exist";
        private const string PortIsNotUnique = "Port must be a unique";
        private const string InvalidPort = "Invalid port number! Port must be an integer number between 0 and 65535";
        private const string PathDoesNotExist = "Directory Path does not exist!";
        private const string PathAlreadyExist = "Website with that path already exist";
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
            //Port should be Unique and must be a integer number
            var bindingsByPort = BindingManager.BindingsConfigurationManager.Instace().GetBindingsWithPortAsAKey();
            try
            {
                var port = int.Parse(this.Port);
                if(bindingsByPort.ContainsKey(port))
                {
                    this._validationResult.Message = PortIsNotUnique;
                    return this._validationResult;
                }
                if(port <= 0 || port > MaxPortNumber)
                {
                    this._validationResult.Message = InvalidPort;
                    return this._validationResult;
                }
            }
            catch(Exception)
            {
                this._validationResult.Message = InvalidPort;
                return this._validationResult;
            }
            // name should be Unique
            var bindingByName = BindingManager.BindingsConfigurationManager.Instace().GetBindingsWithNameAsKey();
            if(bindingByName.ContainsKey(this.WebSiteName))
            {
                this._validationResult.Message = WebsiteNameExist;
                return this._validationResult;
            }
            //website path shoud exist and should be unique
            //if (!Directory.Exists(this.WebsitePath))
            //{
            //    this._validationResult.Message = PathDoesNotExist;
            //    return this._validationResult;
            //}
            //var bindingByPath = BindingManager.BindingsConfigurationManager.Instace().GetBindingsWithPatheAsKey();
            //if (bindingByPath.ContainsKey(this.WebsitePath))
            //{
            //    this._validationResult.Message = PathAlreadyExist;
            //    return this._validationResult;
            //}
            this._validationResult.Message = Success;
            this._validationResult.IsValid = true;
            this._validationResult.HTTPServerClassCommand = ServerCommandsEnums.SaveNewBinding;
            this._validationResult.InputForHTTPServerClass =
                string.Format("{0}:{1}:{2}:{3}:{4}", 
                this.WebSiteName, this.HostType, 
                this.Port, this.Protocol, 
                this.WebsitePath);
            return this._validationResult;
        }
    }
}
