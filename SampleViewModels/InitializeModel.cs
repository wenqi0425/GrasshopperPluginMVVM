using Microsoft.Web.WebView2.Core;

using SampleMVVM;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Web.WebView2.Wpf;

namespace SampleViewModels
{
    public class InitializeModel /*: BaseDispatcherModel*/
    {
        private string _htmlPath;
        private readonly Dispatcher _dispatcher;
        private WebView2 _webView;

        // The directory where the HTML file used for the UI.
        private string Directory
            => _dispatcher.Invoke(() => Path.GetDirectoryName(_htmlPath));

        // A special "temp" folder should be created in the Grasshopper/Libraries directory.
        private string ExecutingLocation
            => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\temp";

        public InitializeModel(string htmlPath, Dispatcher dispatcher)
        {
            _htmlPath = htmlPath;
            _dispatcher = dispatcher;
        }

        public async void InitializeWebView(DockPanel docker)
        {
            // clear everything in the WPF dock panel container
            docker.Children.Clear();
            docker.Children.Add(_webView);

            // initialize the webview 2 instance
            try
            {
                _webView.CoreWebView2InitializationCompleted += OnWebViewInitializationCompleted;
                CoreWebView2Environment env = await CoreWebView2Environment.CreateAsync(null, ExecutingLocation);
                await _webView.EnsureCoreWebView2Async(env);

                //_webView.WebMessageReceived += OnWebViewInteraction;
                //_webView.NavigationCompleted += OnWebViewNavigationCompleted;
                //InitializeDevToolsProtocolHelper();
                //SubscribeToDocumentUpdated();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnWebViewInitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            if (_webView?.CoreWebView2 == null) return;
            _webView.Source = new Uri(_htmlPath);
            _webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(
                Properties.Resources.AddDocumentClickListener);
            _webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(Properties.Resources
                .QueryInputElementsInDOM);
            _webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(Properties.Resources.SetValuesInDom);
        }
    }
}
