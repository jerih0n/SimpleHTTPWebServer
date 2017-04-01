

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
        public string WebSiteName { get; set; }
        public string WebSitePath { get; set; }
        public string Port { get; set; }
        public Protocol Protocol { get; set; }
        public HostType Hosting { get; set; }
        public string IpAddress { get; set; }

    }
}
