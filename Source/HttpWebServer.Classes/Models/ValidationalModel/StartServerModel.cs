using HttpWebServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWebServer.Shared.DataTransfer;

namespace HttpWebServer.Classes.Models.ValidationalModel
{
    public class StartServerModel : IValidatable
    {
        private int _port;
        public StartServerModel(int port)
        {
            this._port = port;
        }
        public HTTPValidationResult Validate()
        {
            var result = new HTTPValidationResult()
            {
                IsValid = false,
                Message = ""
            };
         
            //check if the server is not started yet
            IHttpEngine engine = Engine.Engine.Instance();
            var isServerAlreadyRun = engine.AllRuningServers().ContainsKey(this._port);
            if(isServerAlreadyRun)
            {
                result.Message = string.Format("Server at port {0} already running",this._port);
                return result;
            }
            result.Message = "OK";
            result.IsValid = true;
            result.HTTPServerClassCommand = Shared.Enums.ServerCommandsEnums.StartServerOnCustomPort;
            result.InputForHTTPServerClass = this._port.ToString();
            return result;
        }
        
    }
}
