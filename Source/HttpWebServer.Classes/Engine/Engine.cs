

namespace HttpWebServer.Classes.Engine
{
    using HttpWebServer.Interfaces;
    using HttpWebServer.Shared;
    using HttpWebServer.Shared.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HttpServer;
    using System.Net.Sockets;
    using System.Text.RegularExpressions;
    using HttpWebServer.Classes.Actions;
    using System.Net;
    using HttpWebServer.Shared.DataTransfer;

    public class Engine : IHttpEngine
    {
        private static IHttpEngine _instance;
        private bool _isServerRunning = false;
        private IHttpServer _server = null;
        private EngineActionsFactory _factoryMethod;
        private const int LastPortNumber = 65535;
        private string _localIpAddress;
        private const string localHostIp = "127.0.0.1";
        private BindingManager.BindingsConfigurationManager _bindingManager;

        public bool IsServerRunning
        {
            get
            {
                return this._isServerRunning;
            }
        }

        public string LocalIpAddress
        {
            get
            {
                return this._localIpAddress;
            }
        }

        public string LocalHostIp
        {
            get
            {
                return localHostIp;
            }
        }


        /// <summary>
        /// Singelton Pattern inplemented for the Engine
        /// </summary>
        protected Engine()
        {
            // we are declaring a IHttpServer class running on the default port
            this._server = new HttpServer();
            this._bindingManager = BindingManager.BindingsConfigurationManager.Instace();
            this._bindingManager.InitiateBindings();
            this._factoryMethod = new EngineActionsFactory(this._server);
            this._localIpAddress = this.GetLocalIpAddress();
        }
        /// <summary>
        /// Accepts an input from the user and proccess it. This is the only visible method of Engine class
        /// </summary>
        /// <param name="inputCommand"></param>
        /// <returns></returns>
        public string TakeUserInput(ServerCommandsEnums serverCommand, string input)
        {
            string result = this.ProcessUserInput(serverCommand, input);

            return result;
        }
        public static IHttpEngine Instance()
        {
            if (_instance == null)
            {
                _instance = new Engine();
            }
            return _instance;
        }
        public Dictionary<int, WebsiteBingingParameters> GetAllBindings()
        {

            return this._bindingManager.GetBindingsIdAsKey();
        }

        #region Private methods
        private string ProcessUserInput(ServerCommandsEnums serverCommand, string input)
        {
            var action = this._factoryMethod.GetRequiredActionClass(serverCommand);
            var resul = action.PerformAction(input);

            return "";
        }
        private string GetLocalIpAddress()
        {
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }
            return localIP;
        }
        #endregion
    }

}
