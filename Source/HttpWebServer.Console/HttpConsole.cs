

namespace HttpWebServer.Console
{
    using HttpWebServer.Classes.Engine;
    using HttpWebServer.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HttpWebServer.Shared;
    class HttpConsole
    {
        public static IHttpEngine _serverEngine;
        
        static void Main()
        {
            //Instance of server's valid commands;
            string starsSrtring = new string('*', Console.WindowWidth);
            _serverEngine = Engine.Instance();
            
            Console.WriteLine(starsSrtring);
            Console.WriteLine("This is basic http web server v1.0. Server is created by Jerihon GitHub:");
            Console.WriteLine("https://github.com/jerih0n");
            Console.WriteLine("If you need help - enter -help in order to see the available commands");
            Console.WriteLine(starsSrtring);
            while (true)
            {
                string input = Console.ReadLine();
                string response = _serverEngine.TakeUserInput(input);
                Console.WriteLine(response);
                if(response == "exit")
                {
                    break;
                }
            }
        }
    }
}
