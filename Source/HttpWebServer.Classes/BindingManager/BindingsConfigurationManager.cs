

namespace HttpWebServer.Classes.BindingManager
{
    using HttpWebServer.Classes.XMLModels;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;
    using HttpWebServer.Shared.DataTransfer;
    using HttpWebServer.Shared;
    /// <summary>
    /// Class works with XML serialization/deserialization of XML. Implemetns Singelton. GetInstance
    /// </summary>
    public class BindingsConfigurationManager
    {
        private XmlSerializer _serializer;
        private string _directory;
        private const string BindingConfigFileName = "Bindings.xml";
        private static BindingsConfigurationManager _instance;
        private bool isChanged = true;
        private Dictionary<int, WebsiteBingingParameters> _allWebsitesKeyPort;
        protected BindingsConfigurationManager()
        {
            this._serializer = new XmlSerializer(typeof(Bindings));
            this._directory = Environment.CurrentDirectory + "/ServerConfig/" + BindingConfigFileName;
            this._allWebsitesKeyPort = new Dictionary<int, WebsiteBingingParameters>();

        }
        /// <summary>
        /// Get Dictionary of all website bindings. key in the dictionary is the PORT. Any changes to the XML struct should be added here!
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, WebsiteBingingParameters> GerAllBindedWebSites()
        {
            if(!isChanged)
            {
                return this._allWebsitesKeyPort;
            }
            FileStream fileStream;
            try
            {
                fileStream = new FileStream(this._directory, FileMode.Open);
            }
            catch(IOException e) // Binding File does not exist. Create Default one! TODO:
            {
                
                fileStream = new FileStream(this._directory, FileMode.Open);
            }
                       
            Bindings allWebsiteBindings = (Bindings)this._serializer.Deserialize(fileStream);
            foreach(var element in allWebsiteBindings.AllBindings)
            {
                WebsiteBingingParameters siteParams = new WebsiteBingingParameters();
                siteParams.IP = element.IPAddress;
                siteParams.Port = element.Port;
                siteParams.WebsiteName = element.WebSiteName;
                siteParams.WebSiteServerPath = element.ServerPath;
                if (element.Protocol == ConstantBindingProperties.HTTPProtocol)
                {
                    siteParams.Protocol = Shared.Enums.Protocol.HTTP;
                }
                if(element.Protocol == ConstantBindingProperties.None)
                {
                    siteParams.Protocol = Shared.Enums.Protocol.None;
                }
                if(element.HostType == ConstantBindingProperties.LANIP)
                {
                    siteParams.HostType = Shared.Enums.HostType.LANIpAddress;
                }
                if(element.HostType == ConstantBindingProperties.Local)
                {
                    siteParams.HostType = Shared.Enums.HostType.LocalHost;
                }
                else
                {
                    siteParams.HostType = Shared.Enums.HostType.None;
                }
                this._allWebsitesKeyPort.Add(siteParams.Port, siteParams);
            }
            return this._allWebsitesKeyPort;
        }
        /// <summary>
        /// Get already created  instance of the class(Singelton pattern) 
        /// </summary>
        /// <returns></returns>
        public static BindingsConfigurationManager Instace()
        {
            if(_instance == null)
            {
                _instance = new BindingsConfigurationManager();
            }
            return _instance;
        }
        
    }
}
