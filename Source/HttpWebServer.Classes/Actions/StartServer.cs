using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWebServer.Interfaces;
using HttpWebServer.Shared;

namespace HttpWebServer.Classes.Actions
{
    public class StartServer : Action
    {
        private string _message;
        public StartServer(IHttpServer engineInstance) : base(engineInstance)
        {
            this._message = null;
        }

        public override string GetResponse()
        {
            return this._message;
        }

        public override bool PerformAction(string input)
        {
            IHttpEngine engineInstace = Engine.Engine.Instance();
            int selectedServerId = int.Parse(input);
            var serverIformation = engineInstace.GetAllBindings().Where(x => x.Key == selectedServerId)
                .Select(y => y.Value).FirstOrDefault();
            if(serverIformation == null)
            {
                return false;
            }
            var serverPort = serverIformation.Port;
            IHttpServer server = new HttpServer.HttpServer(serverPort);
            server.Start(); // the server is now running. Notification should be fired
            engineInstace.AddNewRunningServer(serverPort, server);
            this._message = ServerOutput.ServerIsRunning + serverPort;
            return true;
        }
    }
}
