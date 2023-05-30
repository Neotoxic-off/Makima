using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Makima.ViewModels
{
    public class SearchViewModel: BaseViewModel
    {
        public NyaaViewModel Nyaa { get; set; }

        public SearchViewModel()
        {
            Nyaa = new NyaaViewModel();
            Nyaa.New();
        }
    }
}
