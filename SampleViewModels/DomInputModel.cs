using SampleMVVM;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleViewModels
{
    public class DomInputModel : BaseDomInputModel
    {
        private List<DomInputModel> _domInputModels;
        public List<string> InputValues 
            => _domInputModels?.Select(s => s.value).ToList();
        
        public List<string> InputIds 
            => _domInputModels?.Select(s => s.id).ToList();

        public List<string> InputNames 
            => _domInputModels?.Select(s => s.name).ToList();

        public List<string> InputTypes 
            => _domInputModels?.Select(s => s.type).ToList();
    }
}
