﻿using HttpWebServer.Shared;

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
                case ServerCommandsEnums.Exit: return null;
                case ServerCommandsEnums.Help: return null;
                case ServerCommandsEnums.StartServerOnDefaultPort: return new StartServerFormDefaultPort(this._httpServer);
                case ServerCommandsEnums.StartServerOnCustomPort: return new StartServerFromCustomPort(this._httpServer);
            }
            return null;
        }

            
    }
}
