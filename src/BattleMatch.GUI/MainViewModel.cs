using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimizer.GUI
{
    internal class MainViewModel
    {
        public readonly ObservableCollection<string> GoodMatchups;

        public MainViewModel()
        {
            GoodMatchups = new ObservableCollection<string> { "test1", "test2" };
        }
    }
}
