

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
    using HttpWebServer.Shared.Enums;
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
        private Dictionary<int, WebsiteBingingParameters> _allWebsitesWithIdAsKey;
        private const string ServerConfigDirectoryName = "/ServerConfig/";
        protected BindingsConfigurationManager()
        {
            this._serializer = new XmlSerializer(typeof(Bindings));
            this._directory = Environment.CurrentDirectory + ServerConfigDirectoryName + BindingConfigFileName;
            this._allWebsitesKeyPort = new Dictionary<int, WebsiteBingingParameters>();
            this._allWebsitesKeyWebsiteName = new Dictionary<string, WebsiteBingingParameters>();
            this._allWebsitesWithIdAsKey = new Dictionary<int, WebsiteBingingParameters>();

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
                siteParams.DefaultDocument = element.DefaultDocument;
                siteParams.Id = element.Id;
                siteParams.IP = element.IPAddress;
                siteParams.Port = element.Port;
                siteParams.WebsiteName = element.WebSiteName;
                siteParams.WebSiteServerPath = element.ServerPath;
                this.ConvertEnumToString(siteParams, element.Protocol, element.HostType);
                this.MadeNewRecordToAllDictionaries(siteParams);
                
            }
            fileStream.Close();
            this.isChanged = false;
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
        public Dictionary<int, WebsiteBingingParameters> GetBindingsIdAsKey()
        {
            return this._allWebsitesWithIdAsKey;
        }
        /// <summary>
        /// Invoke after every new binding. Re Initiate the collections 
        /// </summary>
        public void ReInitiate()
        {
            this.isChanged = true;
            this.InitiateBindings();
        }
        public bool AddNewBinding(string webSiteName, string hostingType, int port, string IPAddress,  string protocol, string path, string defaultDocument)
        {
            var bindingId = this._allWebsitesKeyPort.Count + 1;
            var newBInding = new BindingParameters()
            {
                Id = bindingId,
                IPAddress = IPAddress,
                HostType = hostingType,
                Port = port,
                Protocol = protocol,
                ServerPath = path,
                WebSiteName = webSiteName,
                DefaultDocument = defaultDocument
            };
            var fileStream = new FileStream(this._directory, FileMode.Open);           
            Bindings binding = (Bindings)this._serializer.Deserialize(fileStream);
            binding.AllBindings.Add(newBInding);
            var directory = Environment.CurrentDirectory + ServerConfigDirectoryName + BindingConfigFileName;
            var serializer = new XmlSerializer(binding.GetType());
            fileStream.Close();
            using (var writter = XmlWriter.Create(directory))
            {
                serializer.Serialize(writter, binding);
            }

            var newBindigParams = new WebsiteBingingParameters()
            {
                Id = newBInding.Id,
                DefaultDocument = newBInding.DefaultDocument,
                WebSiteServerPath = newBInding.ServerPath,
                Port = newBInding.Port,
                IP = newBInding.IPAddress,
                WebsiteName = newBInding.WebSiteName
            };
            this.ConvertEnumToString(newBindigParams, newBInding.Protocol, newBInding.HostType);
            this.MadeNewRecordToAllDictionaries(newBindigParams);
            return true;
        }
        public bool UpdateBindingInformation(string webSiteName, string hostingType, string port, string IPAddress, string protocol, string path, string defaultDocument, string id)
        {
            var siteId = int.Parse(id);

            var fileStream = new FileStream(this._directory, FileMode.Open);
            Bindings binding = (Bindings)this._serializer.Deserialize(fileStream);
            var givenBinding = binding.AllBindings.Find(x => x.Id == siteId);
            givenBinding.WebSiteName = webSiteName;
            givenBinding.ServerPath = path;
            givenBinding.Port = int.Parse(port);
            givenBinding.DefaultDocument = defaultDocument;
            givenBinding.HostType = hostingType;
            givenBinding.Protocol = protocol;
            givenBinding.IPAddress = IPAddress;
            var directory = Environment.CurrentDirectory + ServerConfigDirectoryName + BindingConfigFileName;
            var serializer = new XmlSerializer(binding.GetType());
            fileStream.Close();
            using (var writter = XmlWriter.Create(directory))
            {
                serializer.Serialize(writter, binding);
            }
            //update the information in the UI
            var laylerModel = this._allWebsitesWithIdAsKey[siteId];
            laylerModel.WebsiteName = webSiteName;
            laylerModel.WebSiteServerPath = path;
            laylerModel.Port = int.Parse(port);
            laylerModel.DefaultDocument = defaultDocument;
            laylerModel.IP = IPAddress;
            this.ConvertEnumToString(laylerModel, protocol, hostingType);
            return true;
        }
        private void CreateDefaultXMLBindingFile(string directory)
        {
            var bindingId = this._allWebsitesKeyPort.Count + 1;
            var defaultBindingParameter = new BindingParameters()
            {
                Id = bindingId,
                WebSiteName = "DefaultTemplate",
                Port = 8080,
                ServerPath = "",
                IPAddress = "127.0.0.1",
                Protocol = "HTTP",
                HostType = "local",
                DefaultDocument = "Not Selected"
                
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
        //Convert the Enums for Port and Protocol to string
        private void ConvertEnumToString(WebsiteBingingParameters parameter, string protocol, string hostType )
        {
            if (protocol == ConstantBindingProperties.HTTPProtocol)
            {
                parameter.Protocol = Shared.Enums.Protocol.HTTP;
            }
            if (hostType == ConstantBindingProperties.LANIP)
            {
                parameter.HostType = Shared.Enums.HostType.LANIpAddress;
            }
            if (hostType == ConstantBindingProperties.Local)
            {
                parameter.HostType = Shared.Enums.HostType.LocalHost;
            }
        }
        private void MadeNewRecordToAllDictionaries(WebsiteBingingParameters value )
        {
            this._allWebsitesKeyPort.Add(value.Port, value);
            this._allWebsitesKeyWebsiteName.Add(value.WebsiteName, value);
            this._allWebsitesWithIdAsKey.Add(value.Id, value);
        }
    }
}
