using SampleViewModels;

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

namespace SampleWebview2
{
    /// <summary>
    /// Interaction logic for RhinoPluginWindow.xaml
    /// </summary>
    public partial class RhinoPluginWindow : Window
    {
        private DomInputModel _domInputModel;

        public List<string> InputValues => _domInputModel.InputValues;
        public List<string> InputIds => _domInputModel.InputIds;
        public List<string> InputNames => _domInputModel.InputNames;
        public List<string> InputTypes => _domInputModel.InputTypes;


        public RhinoPluginWindow()
        {
            InitializeComponent();
        }
    }
}
