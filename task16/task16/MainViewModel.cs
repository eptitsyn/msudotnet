using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task16
{
    class MainViewModel
    {
        MainModel model { get; set; };
        public MainViewModel()
        {
            model = new MainModel();
        }
    }
}
