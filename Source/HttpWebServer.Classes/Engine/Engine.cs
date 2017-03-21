

namespace HttpWebServer.Classes.Engine
{
    using HttpWebServer.Interfaces;
    using HttpWebServer.Shared;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HttpServer;
    using System.Net.Sockets;
    public class Engine : IHttpEngine
    {
        private static IHttpEngine _instance;
        private bool _isServerRunning = false;
        private IHttpServer _server = null;
        private  SortedDictionary<string, string> _allCommands;
        /// <summary>
        /// Singelton Pattern inplemented for the Engine
        /// </summary>
        protected Engine()
        {
            ValidCommandsSingleton singletonInstance = ValidCommandsSingleton.Innstance();
            this._allCommands = singletonInstance.GetAllCommands();
        }
        /// <summary>
        /// Accepts an input from the user and proccess it. This is the only visible method of Engine class
        /// </summary>
        /// <param name="inputCommand"></param>
        /// <returns></returns>
        public string TakeUserInput(string inputCommand)
        {
            string result = this.ProcessUserInput(inputCommand);
           
            return result;
        }

        internal string ProcessUserInput(string inputCommand)
        {
            if (this._allCommands.ContainsKey(inputCommand))
            {
                var response = this._allCommands[inputCommand];

                return response;
            }
            else
            {
                //Commands is not valid
                return "Command " + inputCommand + " is not valid";
            }
            throw new NotImplementedException();
        }

        public static IHttpEngine Instance()
        {
            if(_instance == null)
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
        #region Server Actions
        //Methods connected with the HttpWebServer
        #region HTTPWebServerActions
        private bool StartServer()
        {
            if (this._isServerRunning == false)
            {
                return false;
            }
            this._server = new HttpServer();
            
            this._isServerRunning = true;
            return true;
        }
        private bool StartServer(int port)
        {
            if(this._isServerRunning ==  false)
            {
                return false;
            }
            this._server = new HttpServer();
            this._isServerRunning = true;
            return true;
        }
        #endregion

        #endregion

        #region CommandInterpretator
        private bool CommandIterpretator(string command)
        {
            switch(command)
            {
                case "exit": return true;
                case "-help": return true;
                case "-start -server":
                    bool result = this.StartServer();
                    return result;
                default: return true; 
            }
        }
        #endregion
    }

}
