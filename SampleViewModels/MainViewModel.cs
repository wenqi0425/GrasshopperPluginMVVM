using SampleMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SampleViewModels
{
    public class MainViewModel : BaseViewModel
    {
        /* Nugets: Fody and PropertyChanged.Fody
        private int _counter;
              
        public int Counter 
        {
            get{ return _counter; }
            set
            {
                _counter = value;
                this.RaisePropertyChanged(nameof(this.Counter));                
            } 
        }
        */

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
