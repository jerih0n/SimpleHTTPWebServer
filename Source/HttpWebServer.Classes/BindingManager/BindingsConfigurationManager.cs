

namespace HttpWebServer.Classes.BindingManager
{
    using HttpWebServer.Classes.XMLModels;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;
    using HttpWebServer.Shared.DataTransfer;
    using HttpWebServer.Shared;
    using System.Xml;

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
        private Dictionary<string, WebsiteBingingParameters> _allWebsitesKeyWebsiteName;
        private Dictionary<string, WebsiteBingingParameters> _allWebsitesKeyPath;
        private const string ServerConfigDirectoryName = "/ServerConfig/";
        protected BindingsConfigurationManager()
        {
            this._serializer = new XmlSerializer(typeof(Bindings));
            this._directory = Environment.CurrentDirectory + ServerConfigDirectoryName + BindingConfigFileName;
            this._allWebsitesKeyPort = new Dictionary<int, WebsiteBingingParameters>();
            this._allWebsitesKeyPath = new Dictionary<string, WebsiteBingingParameters>();
            this._allWebsitesKeyWebsiteName = new Dictionary<string, WebsiteBingingParameters>();

        }
        /// <summary>
        /// Set three Dictionaries of all website bindings. key in the dictionaries are PORT, Name and Path. Any changes to the XML struct should be added here!
        /// </summary>
        /// <returns></returns>
        public void InitiateBindings()
        {
            if(!isChanged)
            {
                return;
            }
            FileStream fileStream;
            try
            {
                fileStream = new FileStream(this._directory, FileMode.Open);
            }
            catch(IOException e) // Binding File or the Directory does not exist;
            {
                
                //Directory is created
                var configDirectoryPath = Environment.CurrentDirectory + ServerConfigDirectoryName;
                if(!Directory.Exists(configDirectoryPath))
                {
                    Directory.CreateDirectory(configDirectoryPath);
                    //create new defaultBingingConfigFile
                    this.CreateDefaultXMLBindingFile(configDirectoryPath);
                }
                else
                {
                    this.CreateDefaultXMLBindingFile(configDirectoryPath);
                }
                //creat new defaultBindingConfig file;
                
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
                this._allWebsitesKeyWebsiteName.Add(siteParams.WebsiteName, siteParams);
                this._allWebsitesKeyPath.Add(siteParams.WebSiteServerPath, siteParams);
            }
            return;
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
        /// <summary>
        /// Return Map of all websites bindings with port as key. Fast search by port number
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, WebsiteBingingParameters> GetBindingsWithPortAsAKey()
        {
            return this._allWebsitesKeyPort;
        }
        /// <summary>
        /// Return Map of all websites bindings with website name as key. Fast search by website name
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, WebsiteBingingParameters> GetBindingsWithNameAsKey()
        {
            return this._allWebsitesKeyWebsiteName;
        }
        /// <summary>
        /// Return Map of all websites bindings with website path as key. Fast search by website path
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, WebsiteBingingParameters> GetBindingsWithPatheAsKey()
        {
            return this._allWebsitesKeyWebsiteName;
        }
        /// <summary>
        /// Invoke after every new binding. Re Initiate the collections 
        /// </summary>
        public void ReInitiate()
        {
            this.isChanged = true;
            this.InitiateBindings();
        }
        public bool AddNewBinding(string serverName, string hostingType, int port,  string protocol, string path)
        {
            this.isChanged = true;
            return true;
        }
        private void CreateDefaultXMLBindingFile(string directory)
        {
            var defaultBindingParameter = new BindingParameters()
            {
                WebSiteName = "DefaultTemplate",
                Port = 8080,
                ServerPath = "",
                IPAddress = "127.0.0.1",
                Protocol = "HTTP",
                HostType = "local"
            };
            var defaultXMLModel = new Bindings()
            {
                AllBindings = new List<BindingParameters>()
                {
                    defaultBindingParameter
                }
            };
            var serializer = new XmlSerializer(defaultXMLModel.GetType());
            using(var writter = XmlWriter.Create(directory+BindingConfigFileName))
            {
                serializer.Serialize(writter, defaultXMLModel);
            }

        }   
    }
}
