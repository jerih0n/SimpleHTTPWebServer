

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
    public class Engine : IHttpEngine
    {
        private static IHttpEngine _instance;
        private bool _isServerRunning = false;
        private IHttpServer _server = null;
        private EngineActionsFactory _factoryMethod;
        private const int LastPortNumber = 65535;
        private BindingManager.BindingsConfigurationManager _bindingManager;
        
        /// <summary>
        /// Singelton Pattern inplemented for the Engine
        /// </summary>
        protected Engine()
        {
            // we are declaring a IHttpServer class running on the default port
            this._server = new HttpServer();
            this._bindingManager = BindingManager.BindingsConfigurationManager.Instace();
            this._bindingManager.GerAllBindedWebSites();
            this._factoryMethod = new EngineActionsFactory(this._server);
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

        private string ProcessUserInput(ServerCommandsEnums serverCommand, string input)
        {
            if (ServerCommandsEnums.StartServerOnCustomPort == serverCommand)
            {
                //initializing the instance of the HTTP server on port
                try
                {
                    var newPort = int.Parse(input);
                    if (newPort <= 0 || newPort > LastPortNumber)
                    {
                        return ServerOutput.PortNotValid;
                    }

                }
                catch (InvalidCastException)
                {
                    return ServerOutput.PortMustBeNumber;
                }
            }
            var action = this._factoryMethod.GetRequiredActionClass(serverCommand);
            var resul = action.PerformAction(input);

            return "";
        }

        public static IHttpEngine Instance()
        {
            if (_instance == null)
            {
                _instance = new Engine();
            }
            return _instance;
        }
        public bool IsServerRunning
        {
            get
            {
                return this._isServerRunning;
            }
        }
    

    }

}
