using SampleMVVM;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SampleViewModels
{
    public class MainViewModel
    {
        public int Counter { get; set; }
        public ICommand CountUpCommand { get; set; }
        public MainViewModel()
        {
            this.Counter = 0;
            this.CountUpCommand = new RelayCommand(this.CountUp);
        }

        public void CountUp()
        {
            this.Counter += 1;
        }
    }
}
