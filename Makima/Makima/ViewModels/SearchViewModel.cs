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
        private NyaaViewModel _nyaa;
        public NyaaViewModel Nyaa
        {
            get { return _nyaa; }
            set { SetProperty(ref _nyaa, value); }
        }
        public DelegateCommand SearchCommand { get; set; }
        public DelegateCommand DownloadCommand { get; set; }

        private string _searchName;
        public string SearchName
        {
            get { return _searchName; }
            set { SetProperty(ref _searchName, value); }
        }

        public SearchViewModel()
        {
            Load();
        }

        private void Load()
        {
            Nyaa = new NyaaViewModel();
            SearchCommand = new DelegateCommand(Search);
            DownloadCommand = new DelegateCommand(Download);

            Nyaa.New();
        }

        private void Search(object data)
        {
            Nyaa.Search(SearchName);
        }

        private void Download(object data)
        {
            Nyaa.DownloadTorrent(data.ToString());
        }
    }
}
