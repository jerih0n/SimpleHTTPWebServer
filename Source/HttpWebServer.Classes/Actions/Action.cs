

namespace HttpWebServer.Classes.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HttpWebServer.Interfaces;
    using HttpWebServer.Shared;
    public abstract class Action
    {
        private IHttpServer _server;
        private string _response;
        public Action(IHttpServer engineInstance)
        {
            this._server = engineInstance;
            this._response = ServerOutput.ActionInProgress;
        }
        public abstract bool PerformAction(string input);
        public IHttpServer GetServer
        {
            get
            {
                return this._server;
            }
        }
        public abstract string GetResponse();
            
    }
}
