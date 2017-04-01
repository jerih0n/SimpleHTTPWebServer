

namespace HttpWebServer.Classes.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HttpWebServer.Interfaces;
    public class SaveNewWebsiteBinding : Action
    {
        
        public SaveNewWebsiteBinding(IHttpServer engineInstance) : base(engineInstance)
        {
        }

        public override string GetResponse()
        {
            throw new NotImplementedException();
        }
        //Input pattern {name}:{hostType}:{port}:{protocol}:{path}
        public override bool PerformAction(string input)
        {
            var inputParams = input.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
            var bindingManager = BindingManager.BindingsConfigurationManager.Instace();
            var result = bindingManager
                .AddNewBinding(inputParams[0],
                inputParams[1], int.Parse(inputParams[2]),
                inputParams[3], inputParams[4],inputParams[5]);
            return result;
        }
    }
}
