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
                var message = string.Format("{0}       {1}"
                    , element.Value.Id
                    , element.Value.WebsiteName);
                websiteTabListBox.Items.Add(message);

            }
        }
        public void LoadLANIPAddress(Label ipLabel)
        {
            ipLabel.Content = string.Format("LAN IP - {0}", this._engine.LocalIpAddress);
        }
        public void LoadWebsiteInformation(int siteId,Grid informatioGrid, Label headerLebel
            ,Label port, Label ipAddress, Label protocol, Label defaultDocument, Label websitePath, Label hostType)
        {
            informatioGrid.Visibility = System.Windows.Visibility.Visible;
            
            var allSites = this._engine.GetAllBindings();
            var element = allSites[siteId];
            headerLebel.Content = element.WebsiteName;
            port.Content = "Port: "+element.Port;
            ipAddress.Content = "IP Address:" + element.IP;
            protocol.Content = "Protocol: " + element.Protocol.ToString();
            defaultDocument.Content = "Default doc.: " + element.DefaultDocument;
            websitePath.Content = "Server path: " + element.WebSiteServerPath;
            hostType.Content = "Host type: " + element.HostType.ToString();

            
        }
            
    }
}
