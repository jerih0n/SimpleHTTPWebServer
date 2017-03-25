using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWebServer.Interfaces;

namespace HttpWebServer.Classes.Actions
{
    public class NoAction : Action
    {
        public NoAction(IHttpServer engineInstance) : base(engineInstance)
        {

        }
        public override bool PerformAction(string input)
        {
            return true;
        }
        public override string GetResponse()
        {
            throw new NotImplementedException();
        }
    }
}
