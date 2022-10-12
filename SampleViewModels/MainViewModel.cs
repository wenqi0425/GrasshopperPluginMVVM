using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleViewModels
{
    public class MainViewModel
    {
        public int Counter { get; set; }
        public MainViewModel()
        {
            this.Counter = 77;
        }
    }
}
