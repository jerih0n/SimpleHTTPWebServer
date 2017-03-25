

namespace HttpWebServer.Classes.Models
{
    using HttpWebServer.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class ModelFactory
    {
        public IValidatable GetProperModel(AllPoperties allPoroperties)
        {
            switch(allPoroperties.ButtonName)
            {
                case "createNewBinding": return
                        new WebsiteBinding(allPoroperties.WebSiteName, 
                        allPoroperties.Hosting, allPoroperties.Port, 
                        allPoroperties.Protocol, 
                        allPoroperties.WebSitePath);
                default: return null;
            }
        }

    }
}
