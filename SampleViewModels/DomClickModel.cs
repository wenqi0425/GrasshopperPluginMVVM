using Microsoft.Web.WebView2.Wpf;

using SampleMVVM;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleViewModels
{
    public class DomClickModel : BaseDomClickModel
    {
        private DomClickModel _domClickModel;

        public string ClickType => _domClickModel.type;
        public string ClickTargetId => _domClickModel.targetId;
        public string ClickTargetName => _domClickModel.targetName;        
    }
}
