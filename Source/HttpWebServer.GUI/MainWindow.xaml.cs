

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

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IHttpEngine _engine;
        private ModelFactory _modelFactory;
        private ContentLodingHelper _loadingHelper;
        public MainWindow()
        {
            InitializeComponent();

            this._engine = Engine.Instance();
            this._modelFactory = new ModelFactory();
            this._loadingHelper = new ContentLodingHelper(this._engine);
            //All loading of UI elements
            #region Loading content
            this._loadingHelper.LoadWebsiteBindingsForWebsiteTab(websiteSectionListBox); 
            this._loadingHelper.LoadLANIPAddress(localIPaddressLabel);
            #endregion
        }

        
        /// <summary>
        /// Event handler for all actions invoked by clicking buttons that requires Engine to perform action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void onButtonClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button button = (System.Windows.Controls.Button)sender;
            var allProperties = this.GetAllProperties(button.Name);
            
            var correctModel = this._modelFactory.GetProperModel(allProperties);
            //invoke is valid
            HTTPValidationResult validationResult=  correctModel.Validate();
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
            errorMessageLabel.Visibility = Visibility.Hidden;
            successMessageLabel.Visibility = Visibility.Visible;
            successMessageLabel.Content = validationResult.Message;
            //reload the list items so new binding is added
            websiteSectionListBox.Items.Clear(); // clear the old items
            this._loadingHelper.LoadWebsiteBindingsForWebsiteTab(websiteSectionListBox);
            //TODO: FIX THE BUG !!!!
        }
        

        private void websiteSectionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
        }



        #region UI standalone methods
        //Add every new UI controler here!
        private AllPoperties GetAllProperties(string buttonName)
        {
            Protocol protocol = Protocol.None;
            HostType host = HostType.None;
            AllPoperties result = new AllPoperties();
            result.ButtonName = buttonName;
            if ((bool)serverHostRadioButton.IsChecked)
            {
                host = HostType.LANIpAddress;
                result.IpAddress = this._engine.LocalIpAddress;
            }
            if((bool)localHostRadioButton.IsChecked)
            {
                host = HostType.LocalHost;
                result.IpAddress = this._engine.LocalHostIp;

            }
            if((bool)protocolHTTPRadioButton.IsChecked)
            {
                protocol = Protocol.HTTP;
            }
            result.Protocol = protocol;
            result.Hosting = host;
            result.Port = portInputField.Text;
            result.WebSiteName = webServerNameField.Text;
            result.WebSitePath = physicalPathInputField.Text;
            result.DefaultFileIformation = (string)defaultDocumentInformation.Content;
            result.WebSitePathIformation = (string)websitePathInformation.Content;
            result.Id = (int)siteIdInfomration.Content;
            result.IpAddressIformation = (string)ipAddressInformation.Content;
            result.HostingIformation = (string)hostTypeInformation.Content;
            result.PortIformation = Convert.ToString((int)porftInfoemation.Content);
            result.ProtocolIformation = (string)protocolInfomration.Content;
            result.WebSiteNameIformation = (string)websiteTabSelectedItemName.Content;
            return result;
        }

        #endregion
        #region EventHandlers For UI ONLY changes
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
            this._loadingHelper.LoadServerPathOnChange(websitePathInformation);
        }
#endregion
    }
}
