

namespace HttpWebServer.Classes.Models
{
    using HttpWebServer.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HttpWebServer.Shared;
    using HttpWebServer.Classes.Models.ValidationalModel;

    public class ModelFactory
    {
        public IValidatable GetProperModel(AllPoperties allPoroperties)
        {
            //Refactor with Dictionary

            switch (allPoroperties.ButtonName)
            {
                case ActionButtonName.AddNewBinding: return
                        new WebsiteBinding(allPoroperties);
                case ActionButtonName.WebSiteOptionsSaveChanges:
                    return new WebsiteSaveChanges(allPoroperties);  
                default: return null;
            }
        }

    }
}
