using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWebServer.Interfaces;

namespace HttpWebServer.Classes.Actions
{
    public class StartServerFromCustomPort : Action
    {
        public StartServerFromCustomPort(IHttpServer engineInstance) : base(engineInstance)
        {

        }
        public override bool PerformAction(string input)
        {
            throw new NotImplementedException();
        }
    }
}
