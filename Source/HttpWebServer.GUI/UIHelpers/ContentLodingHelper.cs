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
        private const int _maxElementsInServerLogListBox = 100;
        public ContentLodingHelper(IHttpEngine engine)
        {
            this._engine = engine;
        }
        /// <summary>
        /// Load list of all binging in the list box in Website tab
        /// </summary>
        /// <param name="websiteTabListBox"></param>
        public  void LoadWebsiteBindingsForWebsiteTab(ListBox websiteTabListBox)
        {
            
            websiteTabListBox.Items.Clear();
            var allbindngs = this._engine.GetAllBindings();
            foreach (var element in allbindngs)
            {
                var message = string.Format("{0}       {1}"
                    , element.Value.Id
                    , element.Value.WebsiteName);
                websiteTabListBox.Items.Add(message);

            }
        }
        
        /// <summary>
        /// Load the LAN ip address in Binding tab
        /// </summary>
        /// <param name="ipLabel"></param>
        public void LoadLANIPAddress(Label ipLabel)
        {
            ipLabel.Content = string.Format("LAN IP - {0}", this._engine.LocalIpAddress);
        }
        /// <summary>
        /// Load information on Selec for a selected website binding in Website tab
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="informatioGrid"></param>
        /// <param name="headerLebel"></param>
        /// <param name="port"></param>
        /// <param name="ipAddress"></param>
        /// <param name="protocol"></param>
        /// <param name="defaultDocument"></param>
        /// <param name="websitePath"></param>
        /// <param name="hostType"></param>
        /// <param name="siteIdLabel"></param>
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
        /// <summary>
        /// Load the new selected default document on change in Website tab
        /// </summary>
        /// <param name="defaultDocument"></param>
        /// <param name="serverPath"></param>
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
        /// <summary>
        /// Load the new Server path on change in Website Tab
        /// </summary>
        /// <param name="serverPathLabel"></param>
        public void LoadServerPathOnChange(Label serverPathLabel)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            serverPathLabel.Content = dialog.SelectedPath;
        }
        /// <summary>
        /// Load the selected server path in the input path field on Bindings Tab
        /// </summary>
        /// <param name="pathTextBox"></param>
        public void LoadServerPathOnSelect(TextBox pathTextBox)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            pathTextBox.Text = dialog.SelectedPath;

        }
        /// <summary>
        /// Unload all the UI controls in the Binding tab
        /// </summary>
        /// <param name="siteName"></param>
        /// <param name="port"></param>
        /// <param name="path"></param>
        /// <param name="error"></param>
        /// <param name="success"></param>
        public void UnloadBindingControls(TextBox siteName, TextBox port, TextBox path, Label error, Label success )
        {
            siteName.Text = string.Empty;
            port.Text = string.Empty;
            path.Text = string.Empty;
            error.Content = string.Empty;
            success.Content = string.Empty;
        }
        /// <summary>
        /// Make the visibility of website information grid hidden in the Website tab
        /// </summary>
        /// <param name="informationGrid"></param>
        public void HideInformationGrid(Grid informationGrid)
        {
            informationGrid.Visibility = System.Windows.Visibility.Hidden;
        }
        public void LoadWebsitesOnServerTab(ListBox serverTabListBox)
        {
            var allWebsites = this._engine.GetAllBindings();
            foreach(var element in allWebsites)
            {
                string message = string.Format("{0} {1} {2} {3} {4}",
                    element.Value.WebsiteName,element.Value.Port, 
                    element.Value.IP, element.Value.DefaultDocument, 
                    element.Value.Protocol);
                serverTabListBox.Items.Add(message);
            }

        }
        public void UnloadWebsitesOnServerTab(ListBox serverTabListBox)
        {
            serverTabListBox.Items.Clear();
        }
        public void AddNewItemToServeRecentNodesList(ListBox serverRecentNodesList,string newElement)
        {
            //Search and clear the newElement list if the list items are too may 
            if(serverRecentNodesList.Items.Count == _maxElementsInServerLogListBox)
            {
                serverRecentNodesList.Items.Clear();
            }
            serverRecentNodesList.Items.Add(newElement);
        }
      
    }
}
