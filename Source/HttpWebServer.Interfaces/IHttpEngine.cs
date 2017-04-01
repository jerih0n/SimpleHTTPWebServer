
namespace HttpWebServer.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HttpWebServer.Shared.Enums;
    using HttpWebServer.Shared.DataTransfer;

    public interface IHttpEngine
    {
        bool IsServerRunning { get; }
        string TakeUserInput(ServerCommandsEnums serverCommand, string input);
        string LocalIpAddress { get; }
        string LocalHostIp { get; }
        Dictionary<int, WebsiteBingingParameters> GetAllBindings();
        
    }
}
