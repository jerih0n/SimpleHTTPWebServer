

namespace HttpWebServer.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public static class ServerOutput
    {
        public static string Help = "Available Commands: \n-help - show all supported commands\n-start -server - start HTTP Server on default port 8080\n-start -server -{port number} - start HTTP Server on given port\n-exit - exit the Http Server console\n-stop - stop the Http web Server\n";
        public static string Exit = "exit";
        public static string SomethingGoesWrong = "Somethign goes wrong. Please try again";
        public static string PortNotValid = "Invalid port! Port number must be between 0 and 65535";
        public static string PortMustBeNumber = "Port must be a number between 0 and 65535";
        public static string ActionInProgress = "Not finished Action";
        public static string ServerIsRunning = "New Server is started at port ";

    }
}
