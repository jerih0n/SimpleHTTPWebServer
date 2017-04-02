using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWebServer.Interfaces;

namespace HttpWebServer.Classes.Actions
{
   public class UpdateExistingBinding : Action
    {
        public UpdateExistingBinding(IHttpServer engineInstance) : base(engineInstance)
        {
        }
        public override bool PerformAction(string input)
        {
            var inputParams = input.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
            var bindingManager = BindingManager.BindingsConfigurationManager.Instace();
           var result = bindingManager.UpdateBindingInformation(inputParams[0], inputParams[1]
               ,inputParams[2], inputParams[3], inputParams[4], inputParams[5], inputParams[6], inputParams[7]);
            return result;
        }
        public override string GetResponse()
        {
            throw new NotImplementedException();
        }
    }
}
