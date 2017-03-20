

namespace HttpWebServer.Classes.Engine
{
    using HttpWebServer.Interfaces;
    using HttpWebServer.Shared;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Engine : IHttpEngine
    {
        private static IHttpEngine _instance;
        private bool _isServerRunning = false;
        public bool IsServerRunning
        {
            get
            {
                return this._isServerRunning;
            }
        }
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
    }
}
