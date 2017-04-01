using HttpWebServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HttpWebServer.GUI.UIHelpers
{
    public  class ContentLodingHelper
    {
        private IHttpEngine _engine;
        public ContentLodingHelper(IHttpEngine engine)
        {
            this._engine = engine;
        }
        public  void LoadWebsiteBindingsForWebsiteTab(ListBox websiteTabListBox)
        {
            var allbindngs = this._engine.GetAllBindings();
            foreach (var element in allbindngs)
            {
                var message = string.Format("{0}  {1}"
                    ,element.Value.Id
                    ,element.Value.WebsiteName);
                websiteTabListBox.Items.Add(message);
            }

        }
        public void LoadLANIPAddress(Label ipLabel)
        {
            ipLabel.Content = string.Format("LAN IP - {0}", this._engine.LocalIpAddress);
        }
        public void LoadWebsiteInformation(int siteId, Label headerLebel)
        {

        }
            
    }
}
