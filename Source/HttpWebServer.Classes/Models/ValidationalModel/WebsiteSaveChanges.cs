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
            this.WebsitePath = allProperties.WebSitePathIformation ?? "Not Selected";
            this.DefaultDocument = allProperties.DefaultFileIformation ?? "Not Selected";
            this.IpAddress = allProperties.IpAddressIformation;

        }
        private string Id { get; set; }
        private string WebSiteName { get; set; }
        private string HostType { get; set; }
        private string Port { get; set; }
        private string Protocol { get; set; }
        private string WebsitePath { get; set; }
        private string IpAddress { get; set; }
        private string DefaultDocument { get; set; }

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
