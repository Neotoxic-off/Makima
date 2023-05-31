using Makima.Models;
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
        private List<FilterModel> _filters;
        public List<FilterModel> Filters
        {
            get { return _filters; }
            set { SetProperty(ref _filters, value); }
        }
        private FilterModel _selectedFilters;
        public FilterModel SelectedFilters
        {
            get { return _selectedFilters; }
            set { SetProperty(ref _selectedFilters, value); }
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
            Filters = new List<FilterModel>()
            {
                new FilterModel("All categories", "0_0"),
                new FilterModel("Anime", "1_0"),
                new FilterModel("Anime music video", "1_1"),
                new FilterModel("English translated", "1_2"),
                new FilterModel("Non english translated", "1_3"),
                new FilterModel("Raw", "1_4"),

                new FilterModel("Audio", "2_0"),
                new FilterModel("Lossless", "2_1"),
                new FilterModel("Lossy", "2_2"),

                new FilterModel("Literature", "3_0"),
                new FilterModel("English translated", "3_1"),
                new FilterModel("Non english translated", "3_2"),
                new FilterModel("Raw", "3_2"),

                new FilterModel("Live action", "4_0"),
                new FilterModel("English translated", "4_1"),
                new FilterModel("Idol / promotional video", "4_2"),
                new FilterModel("Non english translated", "4_3"),
                new FilterModel("Raw", "4_4"),

                new FilterModel("Pictures", "5_0"),
                new FilterModel("Graphics", "5_1"),
                new FilterModel("Photos", "5_2"),

                new FilterModel("Software", "6_0"),
                new FilterModel("Applications", "6_1"),
                new FilterModel("Games", "6_2")
            };
            SelectedFilters = Filters[0];
        }

        private void Search(object data)
        {
            Nyaa.Search(SelectedFilters, SearchName);
        }

        private void Download(object data)
        {
            Nyaa.DownloadTorrent(data.ToString());
        }
    }
}
