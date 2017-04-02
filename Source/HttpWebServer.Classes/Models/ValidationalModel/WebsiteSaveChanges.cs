using HttpWebServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWebServer.Shared.DataTransfer;

namespace HttpWebServer.Classes.Models.ValidationalModel
{
    public class WebsiteSaveChanges : IValidatable
    {
        private const string Success = "Website information successfuly updated!";
        
        public WebsiteSaveChanges(AllPoperties allProperties)
        {
            this.Id = allProperties.Id.ToString();
            this.WebSiteName = allProperties.WebSiteNameIformation;
            this.HostType = allProperties.HostingIformation;
            this.Port = allProperties.PortIformation;
            this.Protocol = allProperties.ProtocolIformation;
            this.WebsitePath = allProperties.WebSitePathIformation;
            this.DefaultDocument = allProperties.DefaultFileIformation;
            this.IpAddress = allProperties.IpAddressIformation;

        }
        public string Id { get; set; }
        public string WebSiteName { get; set; }
        public string HostType { get; set; }
        public string Port { get; set; }
        public string Protocol { get; set; }
        public string WebsitePath { get; set; }
        public string IpAddress { get; set; }
        public string DefaultDocument { get; set; }

        public HTTPValidationResult Validate()
        {
            HTTPValidationResult result = new HTTPValidationResult()
            {
                IsValid = true,
                Message = Success,
                HTTPServerClassCommand = Shared.Enums.ServerCommandsEnums.UpdateExistingBinding,
                InputForHTTPServerClass = string.Format("{0}*{1}*{2}*{3}*{4}*{5}*{6}*{7}",
                this.WebSiteName
                ,this.HostType,
                this.Port, 
                this.IpAddress, 
                this.Protocol,
                this.WebsitePath
                ,this.DefaultDocument,
                this.Id)
        };
            
            return result;
        }
    }
}
