

namespace HttpWebServer.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using HttpWebServer.Classes.Engine;
    using HttpWebServer.Interfaces;
    using HttpWebServer.Classes.Models;
    using HttpWebServer.Shared.Enums;
    using HttpWebServer.Shared.DataTransfer;
    using System.Windows.Forms;
    using HttpWebServer.GUI.UIHelpers;
    using Shared;
    using HttpWebServer.Classes.Models.ValidationalModel;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IHttpEngine _engine;
        private ContentLodingHelper _loadingHelper;
        private bool _isWebsiteBindingListChange;
        private bool _isWebsiteServerListChange;
        public MainWindow()
        {
            InitializeComponent();

            this._engine = Engine.Instance();
            this._loadingHelper = new ContentLodingHelper(this._engine);
            this._isWebsiteBindingListChange = true;
            this._isWebsiteServerListChange = true;
            //All loading of UI elements
            this._loadingHelper.LoadLANIPAddress(localIPaddressLabel);
        }





        #region EventHandler from GUI
        private void websiteSectionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Exception is catched when the Items.Clear(); method is invoked by the UI helper class

            try
            {
                System.Windows.Controls.ListBox listBox = (System.Windows.Controls.ListBox)sender;
                var parts = listBox.SelectedItem.ToString()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int iteimId = int.Parse(parts[0]);
                this._loadingHelper.LoadWebsiteInformation(
                    iteimId, websiteBindingDetailedInformation, websiteTabSelectedItemName,
                    porftInfoemation, ipAddressInformation, protocolInfomration,
                    defaultDocumentInformation, websitePathInformation,
                    hostTypeInformation, siteIdInfomration);
                this._isWebsiteBindingListChange = false;
            }
            catch (NullReferenceException)
            {
                this._isWebsiteBindingListChange = false;
            }
        }
        private void changePathMenu_Click(object sender, RoutedEventArgs e)
        {
            this._loadingHelper.LoadServerPathOnChange(websitePathInformation);
        }

        private void changeDefaultDocument_Click(object sender, RoutedEventArgs e)
        {
            this._loadingHelper.LoadDefaultDocumentLabelOnChange(defaultDocumentInformation, (string)websitePathInformation.Content);
        }
        private void browseSitePath_Click(object sender, RoutedEventArgs e)
        {
            this._loadingHelper.LoadServerPathOnSelect(physicalPathInputField);
        }
        /// <summary>
        /// Creates the new bingins
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createNewBinding_Click(object sender, RoutedEventArgs e)
        {
            var properties = new AllPoperties()
            {
                WebSiteName = webServerNameField.Text,
                Port = portInputField.Text,
                WebSitePath = physicalPathInputField.Text,
                 
            };
            if ((bool)serverHostRadioButton.IsChecked)
            {
                properties.Hosting = HostType.LANIpAddress;
                properties.IpAddress = this._engine.LocalIpAddress;
            }
            if ((bool)localHostRadioButton.IsChecked)
            {
                properties.Hosting = HostType.LocalHost;
                properties.IpAddress = this._engine.LocalHostIp;

            }
            if ((bool)protocolHTTPRadioButton.IsChecked)
            {
                properties.Protocol = Protocol.HTTP;
            }
            
            var model = new WebsiteBinding(properties);
            var validationResult = model.Validate();
            if(!validationResult.IsValid)
            {
                successMessageLabel.Visibility = Visibility.Hidden;
                errorMessageLabel.Visibility = Visibility.Visible;
                errorMessageLabel.Content = validationResult.Message;
                return;               
            }
            var output = this._engine.TakeUserInput(
                    validationResult.HTTPServerClassCommand,
                    validationResult.InputForHTTPServerClass);
            successMessageLabel.Visibility = Visibility.Visible;
            errorMessageLabel.Visibility = Visibility.Hidden;
            successMessageLabel.Content = validationResult.Message;
            this._isWebsiteBindingListChange = true;
            this._isWebsiteServerListChange = true;
        }

        private void websiteOptionsSeveChanges_Click(object sender, RoutedEventArgs e)
        {
            var properties = new AllPoperties()
            {
                Id = (int)siteIdInfomration.Content,
                WebSiteNameIformation = (string)websiteTabSelectedItemName.Content,
                PortIformation = porftInfoemation.Content.ToString(),
                ProtocolIformation = (string)protocolInfomration.Content,
                HostingIformation = (string)hostTypeInformation.Content,
                IpAddressIformation = (string)ipAddressInformation.Content,
                DefaultFileIformation = (string)defaultDocumentInformation.Content,
                WebSitePathIformation = (string)websitePathInformation.Content
            };
            var model = new WebsiteSaveChanges(properties);
            var validationResult = model.Validate();
            if(!validationResult.IsValid)
            {
                saveChangesResult.Content = validationResult.Message;
                saveChangesResult.Foreground = new SolidColorBrush(Colors.Red);
                return;
            }
            saveChangesResult.Content = validationResult.Message;
            saveChangesResult.Foreground = new SolidColorBrush(Colors.Green);
            var serverResult = this._engine.TakeUserInput(
                validationResult.HTTPServerClassCommand,
                validationResult.InputForHTTPServerClass);
        }
        /// <summary>
        /// Load and/or unload UI elements and the code behind values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServeTabContainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ServerTab.IsSelected)
            {
               
                this._loadingHelper.HideInformationGrid(websiteBindingDetailedInformation);
                this._loadingHelper.UnloadBindingControls(webServerNameField,
                    portInputField, physicalPathInputField, errorMessageLabel, successMessageLabel);
                if(this._isWebsiteServerListChange)
                {
                    this._loadingHelper.UnloadWebsitesOnServerTab(serverTabList);
                    this._loadingHelper.LoadWebsitesOnServerTab(serverTabList);
                    this._isWebsiteServerListChange = false;
                }
                
                
            }
            else if(BindingTab.IsSelected)
            {
                this.selectedSiteId.Content = null;
                this._loadingHelper.HideInformationGrid(websiteBindingDetailedInformation);
            }
            else if(WebsitesTab.IsSelected)
            {
                this.selectedSiteId.Content = null;
                if (this._isWebsiteBindingListChange)
                {
                    this._loadingHelper.UnloadBindingControls(webServerNameField,
                    portInputField, physicalPathInputField, errorMessageLabel, successMessageLabel);
                    this._loadingHelper.LoadWebsiteBindingsForWebsiteTab(websiteSectionListBox);
                    this._isWebsiteBindingListChange = false;
                }
                
                
                //Unload all UI controls from the Binding tab
                
                
            }
            else if(SecurityTab.IsSelected)
            {
                this.selectedSiteId.Content = null;
                this._loadingHelper.HideInformationGrid(websiteBindingDetailedInformation);
                this._loadingHelper.UnloadBindingControls(webServerNameField,
                    portInputField, physicalPathInputField, errorMessageLabel, successMessageLabel);
                
            }
            else if(HelpTab.IsSelected)
            {
                this.selectedSiteId.Content = null;
                this._loadingHelper.HideInformationGrid(websiteBindingDetailedInformation);
                this._loadingHelper.UnloadBindingControls(webServerNameField,
                    portInputField, physicalPathInputField, errorMessageLabel, successMessageLabel);
                
            }
            else
            {
                return;
            }
        }
        private void serverTabList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ListBox listBox = (System.Windows.Controls.ListBox)sender;
            selectedSiteId.Content = listBox.SelectedIndex + 1;
        }
        private void startServer_Click(object sender, RoutedEventArgs e)
        {
            var validationModel = new StartServerModel((int)selectedSiteId.Content);
            var validationResult = validationModel.Validate();
            if(!validationResult.IsValid)
            {
                this._loadingHelper.AddNewItemToServeRecentNodesList(serverRequestResponeList, validationResult.Message);
                return;
            }
            var response = this._engine.TakeUserInput(validationResult.HTTPServerClassCommand, validationResult.InputForHTTPServerClass);
            this._loadingHelper.AddNewItemToServeRecentNodesList(serverRequestResponeList, response);
        }

        private void restartServer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void stopServer_Click(object sender, RoutedEventArgs e)
        {

        }
       
        private void sourceControlNav_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/jerih0n/SimpleHTTPWebServer");
        }

        private void websiteNav_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start("https://botevweb.wordpress.com/");
        }

        #endregion


    }
}
