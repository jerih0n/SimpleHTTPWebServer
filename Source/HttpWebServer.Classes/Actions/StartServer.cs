using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWebServer.Interfaces;

namespace HttpWebServer.Classes.Actions
{
    public class StartServer : Action
    {
        public StartServer(IHttpServer engineInstance) : base(engineInstance)
        {

        }

        public override string GetResponse()
        {
            throw new NotImplementedException();
        }

        public override bool PerformAction(string input)
        {
            IHttpServer server = this.GetServer;
            return server.Start();
        }
    }
}
