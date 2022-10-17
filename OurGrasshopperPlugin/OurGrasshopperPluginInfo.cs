using Grasshopper;
using Grasshopper.Kernel;

using System;
using System.Drawing;

namespace OurGrasshopperPlugin
{
    public class OurGrasshopperPluginInfo : GH_AssemblyInfo
    {
        public override string Name => "OurGrasshopperPlugin";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "";

        public override Guid Id => new Guid("689C7B6B-0C90-4C0F-A3F8-872D418A174A");

        //Return a string identifying you or your company.
        public override string AuthorName => "Wenqi Huang @ Stykka";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "wenqi100@gmail.com | wenqi0425 on GitHub";
    }
}