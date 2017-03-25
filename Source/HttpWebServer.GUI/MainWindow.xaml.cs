

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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IHttpEngine _engine;
        private string _webSiteName;
        private IValidatable _dataTransferClass;
        private ModelFactory _modelFactory;
        
        public MainWindow()
        {
            this._engine = Engine.Instance();
            this._modelFactory = new ModelFactory();
            InitializeComponent();
        }

        public void onButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var allProperties = this.GetAllProperties();
            var correctModel = this._modelFactory.GetProperModel(allProperties);
            //invoke is valid
            HTTPValidationResult validationResult=  correctModel.Validate();
            if(!validationResult.IsValid)
            {
                //send message 
            }
            var output = this._engine.TakeUserInput(
                validationResult.HTTPServerClassCommand, 
                validationResult.InputForHTTPServerClass);


            
            
        }
        private AllPoperties GetAllProperties()
        {
            AllPoperties result = new AllPoperties();
            
            return result;
        }


    }
}
