using HttpWebServer.Interfaces;
using Microsoft.Win32;
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
            ,Label port, Label ipAddress, Label protocol, Label defaultDocument, Label websitePath, Label hostType, Label siteIdLabel)
        {
            informatioGrid.Visibility = System.Windows.Visibility.Visible;
            
            var allSites = this._engine.GetAllBindings();
            var element = allSites[siteId];
            headerLebel.Content = element.WebsiteName;
            port.Content = element.Port;
            ipAddress.Content = element.IP;
            protocol.Content = element.Protocol.ToString();
            defaultDocument.Content = element.DefaultDocument;
            websitePath.Content = element.WebSiteServerPath;
            hostType.Content = element.HostType.ToString();
            siteIdLabel.Content = siteId;
        }
        public void LoadDefaultDocumentLabelOnChange(Label defaultDocument, string serverPath)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".html";
            fileDialog.Filter = "HTML Start Index (*.html)|*.html";
            var notPathStringLength = "Server path: ".Length;
            var path = serverPath;
            fileDialog.InitialDirectory = path;
            fileDialog.ShowDialog();
            defaultDocument.Content = fileDialog.SafeFileName;
            
        }    
        public void LoadServerPathOnChange(Label serverPathLabel)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            serverPathLabel.Content = dialog.SelectedPath;
        }
    }
}
