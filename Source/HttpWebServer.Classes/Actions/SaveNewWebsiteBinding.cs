using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWebServer.Interfaces;

namespace HttpWebServer.Classes.Actions
{
    public class SaveNewWebsiteBinding : Action
    {
        public SaveNewWebsiteBinding(IHttpServer engineInstance) : base(engineInstance)
        {
        }

        public override string GetResponse()
        {
            throw new NotImplementedException();
        }

        public override bool PerformAction(string input)
        {
            throw new NotImplementedException();
        }
    }
}
