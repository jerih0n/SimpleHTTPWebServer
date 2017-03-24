using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer.Shared.Enums
{
    public enum ServerCommandsEnums
    {
        Help = 0,
        Exit = 1,
        StartServerOnDefaultPort = 2,
        StartServerOnCustomPort = 3,
        StopServer = 4
    }
}
