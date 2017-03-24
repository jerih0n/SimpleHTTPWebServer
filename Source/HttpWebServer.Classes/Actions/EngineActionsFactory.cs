using HttpWebServer.Shared;

namespace HttpWebServer.Classes.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using HttpWebServer.Interfaces;
    using HttpWebServer.Shared.Enums;
    public class EngineActionsFactory
    {
        private IHttpServer _httpServer;
        public EngineActionsFactory(IHttpServer httpServer)
        {
            this._httpServer = httpServer;
        }


        public Action GetRequiredActionClass(ServerCommandsEnums command)
        {
           switch(command)
            {
                case ServerCommandsEnums.Exit: return new NoAction(this._httpServer);
                case ServerCommandsEnums.Help: return new NoAction(this._httpServer);
                case ServerCommandsEnums.StartServerOnDefaultPort: return new StartServer(this._httpServer);
                case ServerCommandsEnums.StartServerOnCustomPort: return new StartServer(this._httpServer);
            }
            return null;
        }

            
    }
}
