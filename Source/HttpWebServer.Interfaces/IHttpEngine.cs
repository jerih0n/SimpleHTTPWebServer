using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer.Interfaces
{
    public interface IHttpEngine
    {
        bool IsServerRunning { get; }
        string TakeUserInput(string inputCommand);
        
        
    }
}
