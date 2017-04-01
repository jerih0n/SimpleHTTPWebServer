

namespace HttpWebServer.Classes.Models
{
    using HttpWebServer.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HttpWebServer.Shared;
    public class ModelFactory
    {
        public IValidatable GetProperModel(AllPoperties allPoroperties)
        {
            //Refactor with Dictionary
            
            switch(allPoroperties.ButtonName)
            {
                case ActionButtonName.AddNewBinding: return
                        new WebsiteBinding(allPoroperties.WebSiteName, 
                        allPoroperties.Hosting, allPoroperties.Port,
                        allPoroperties.IpAddress,
                        allPoroperties.Protocol, 
                        allPoroperties.WebSitePath);              
                default: return null;
            }
        }

    }
}
