using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWebServer.Interfaces;

namespace HttpWebServer.Classes.Actions
{
    public class StartServerFormDefaultPort : Action
    {
        public StartServerFormDefaultPort(IHttpServer engineInstance) : base(engineInstance)
        {

        }
        public override bool PerformAction(string input)
        {
            IHttpServer server = this.GetServer;
            return server.Start();
        }
    }
}
