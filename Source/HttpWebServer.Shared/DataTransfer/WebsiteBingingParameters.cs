using HttpWebServer.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer.Shared.DataTransfer
{
    public class WebsiteBingingParameters
    {
        public string WebsiteName { get; set; }
        public string WebSiteServerPath { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public HostType HostType { get; set; }
        public Protocol Protocol { get; set; }
    }
}
