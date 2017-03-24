

namespace HttpWebServer.Classes.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HttpWebServer.Interfaces;
    public abstract class Action
    {
        private IHttpServer _server;

        public Action(IHttpServer engineInstance)
        {
            this._server = engineInstance;
        }
        public abstract bool PerformAction(string input);
        public IHttpServer GetServer
        {
            get
            {
                return this._server;
            }
        }
            
    }
}
