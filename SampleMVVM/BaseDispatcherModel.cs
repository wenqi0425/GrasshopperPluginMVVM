using Microsoft.Web.WebView2.Wpf;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SampleMVVM
{
    public class BaseDispatcherModel
    {
        private string _htmlPath;
        private readonly Dispatcher _dispatcher;

        // The directory where the HTML file used for the UI.
        private string Directory 
            => _dispatcher.Invoke(() => Path.GetDirectoryName(_htmlPath));

        // A special "temp" folder should be created in the Grasshopper/Libraries directory.
        private string ExecutingLocation 
            => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\temp";

        private WebView2 _webView;

        public BaseDispatcherModel(string htmlPath, Dispatcher dispatcher)
        {
            _htmlPath = htmlPath;
            _dispatcher = dispatcher;
        }
    }
}
