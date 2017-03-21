

namespace HttpWebServer.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public static class ConsoleResponses
    {
        public static string Help = "Available Commands: \n-help - show all supported commands\n-start -server - start HTTP Server on default port 8080\n-start -server -{port number} - start HTTP Server on given port\n-exit - exit the Http Server console\n-stop - stop the Http web Server\n";
        public static string Exit = "exit";
        
    }
}
