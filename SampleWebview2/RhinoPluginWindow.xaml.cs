using SampleViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
using SampleMVVM;

namespace SampleWebview2
{
    /// <summary>
    /// Interaction logic for RhinoPluginWindow.xaml
    /// </summary>
    public partial class RhinoPluginWindow : Window
    {
        private DomInputModel _domInputModel;
        private InitializeModel _dispatcher;


        public List<string> InputValues => _domInputModel.InputValues;
        public List<string> InputIds => _domInputModel.InputIds;
        public List<string> InputNames => _domInputModel.InputNames;
        public List<string> InputTypes => _domInputModel.InputTypes;


        public RhinoPluginWindow(string htmlPath)
        {
            InitializeComponent();
            _dispatcher = new InitializeModel(htmlPath, Dispatcher);
            //_dispatcher.InitializeWebView(Docker);
            //_dispatcher.SubscribeToHtmlChanged();
        }
    }
}
