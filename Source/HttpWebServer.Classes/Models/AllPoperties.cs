

namespace HttpWebServer.Classes.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HttpWebServer.Shared.Enums;
    public class AllPoperties
    {
        
        public string ButtonName { get; set; }
        public int Id { get; set; }
        public string WebSiteName { get; set; }
        public string WebSitePath { get; set; }
        public string Port { get; set; }
        public Protocol Protocol { get; set; }
        public HostType Hosting { get; set; }
        public string IpAddress { get; set; }
        public string WebSiteNameIformation { get; set; }
        public string WebSitePathIformation { get; set; }
        public string PortIformation { get; set; }
        public string ProtocolIformation { get; set; }
        public string HostingIformation { get; set; }
        public string IpAddressIformation { get; set; }
        public string DefaultFileIformation { get; set; }

    }
}
