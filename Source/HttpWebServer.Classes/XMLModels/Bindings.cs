

namespace HttpWebServer.Classes.XMLModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [XmlRoot("Bindings")]
    public class Bindings
    {
        [XmlElement("websiteBinding")]
        public List<BindingParameters> AllBindings { get; set; }
    }
    
    public class BindingParameters
    {
        [XmlElement("webSiteName")]
        public string WebSiteName { get; set; }
        [XmlElement("port")]
        public int Port { get; set; }
        [XmlElement("serverPath")]
        public string ServerPath { get; set; }
        [XmlElement("IPAddress")]
        public string IPAddress { get; set; }
        [XmlElement("hostType")]
        public string HostType { get; set; }
        [XmlElement("protocol")]
        public string Protocol { get; set; }
    }
}
