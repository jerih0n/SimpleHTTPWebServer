using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer.Interfaces
{
    public interface IHttpServer
    {
        int GetPort { get; }
        bool IsCurrenInstanceOfTheServerRunning { get; }
        bool Start();
        
        

    }
}
