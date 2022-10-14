using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using SampleWebview2;
using System.Threading;
using System.Windows.Threading;
using System.Drawing;
using SampleViewModels.Properties;

namespace OurGrasshopperPlugin
{
    public class HtmlUiComponent : GH_Component
    {
        private RhinoPluginWindow _plugWindow;

        private Thread _uiThread;
        private string _oldPath;

        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>

        public HtmlUiComponent()
          : base("Our Plugin Html Ui", "HTML",
            "Launch a UI Window from a HTML file.",
            "HTML", "Main")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            // Use the pManager object to register your input parameters.
            // You can often supply default values when creating parameters.
            // All parameters must have the correct access type. If you want 
            // to import lists or trees of values, modify the ParamAccess flag.
            pManager.AddTextParameter("HTML Path", "path", "Where to look for the HTML interface.",
                GH_ParamAccess.item);
            pManager.AddBooleanParameter("Show Window", "show", "Toggle for showing/hiding the interface window.",
                GH_ParamAccess.item, false);
            pManager.AddTextParameter("Title", "title", "The title name for the UI window.",
                GH_ParamAccess.item, "UI");

            // If you want to change properties of certain parameters, 
            // you can use the pManager instance to access them by index:
            //pManager[0].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            // Use the pManager object to register your output parameters.
            // Output parameters do not have default values, but they too must have the correct access type.
            pManager.AddTextParameter("Input Values", "vals", "Value of HTML Inputs", GH_ParamAccess.list);
            pManager.AddTextParameter("Input Ids", "ids", "Ids of HTML Inputs", GH_ParamAccess.list);
            pManager.AddTextParameter("Input Names", "names", "Names of HTML Inputs", GH_ParamAccess.list);
            pManager.AddTextParameter("Input Types", "types", "Types of HTML Inputs", GH_ParamAccess.list);
            pManager.AddGenericParameter("Web Window", "web", "Web Window Instance", GH_ParamAccess.item);

            // Sometimes you want to hide a specific parameter from the Rhino preview.
            // You can use the HideParameter() method as a quick way:
            //pManager.HideParameter(0);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess da)
        {
            // First, we need to retrieve all data from the input parameters.
            // We'll start by declaring variables and assigning them starting values.
            // get input from gh component inputs
            string path = null;
            bool show = false;
            string title = null;

            // get input
            if (!da.GetData(0, ref path)) return;
            if (!da.GetData<bool>(1, ref show)) return;
            da.GetData(2, ref title);

            if (_oldPath != path)
            {
                _plugWindow.Navigate(path);
                _oldPath = path;
            }

            da.SetDataList(0, _plugWindow.InputValues);
            da.SetDataList(1, _plugWindow.InputIds);
            da.SetDataList(2, _plugWindow.InputNames);
            da.SetDataList(3, _plugWindow.InputTypes);
            da.SetData(4, _plugWindow);

            LaunchWindow(path, title);

            GH_Document doc = OnPingDocument();
            doc?.ScheduleSolution(500, document => ExpireSolution(false));
        }

        private void LaunchWindow(string path, string title = "UI")
        {
            if (!(_uiThread is null) && _uiThread.IsAlive) return;
            _uiThread = new Thread(() =>
            {
                SynchronizationContext.SetSynchronizationContext(
                    new DispatcherSynchronizationContext(
                        Dispatcher.CurrentDispatcher));
                // The dialog becomes the owner responsible for disposing the objects given to it.
                _plugWindow = new RhinoPluginWindow(path);
                _plugWindow.Closed += _plugWindow_Closed;
                _plugWindow.Show();
                _plugWindow.Title = title;
                Dispatcher.Run();
            });

            _uiThread.SetApartmentState(ApartmentState.STA);
            _uiThread.IsBackground = true;
            _uiThread.Start();
        }

        private void _plugWindow_Closed(object sender, EventArgs e)
        {
            Dispatcher.CurrentDispatcher.InvokeShutdown();
        }

        

        /// <summary>
        /// The Exposure property controls where in the panel a component icon 
        /// will appear. There are seven possible locations (primary to septenary), 
        /// each of which can be combined with the GH_Exposure.obscure flag, which 
        /// ensures the component will only be visible on panel dropdowns.
        /// </summary>
        public override GH_Exposure Exposure => GH_Exposure.primary;

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// You can add image files to your project resources and access them like this:
        /// return Resources.IconForThisComponent;
        /// </summary>
        protected override Bitmap Icon => Resources.web_window;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("2C8DFB33-66E2-402E-BFF8-4C8623C6EAE2");
    }
}