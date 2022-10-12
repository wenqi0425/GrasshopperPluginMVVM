using SampleUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDesktop
{
    class Program
    {
        static void Main(string[] args)
        {
            var myMainWindow = new MainView();
            myMainWindow.ShowDialog();
        }
    }
}
