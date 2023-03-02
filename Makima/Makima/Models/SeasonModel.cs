using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Makima.Models
{
    public class SeasonModel: BaseModel
    {
        private string _id;
        public string ID
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private ObservableCollection<EpisodeModel> _episodes;
        public ObservableCollection<EpisodeModel> Episodes
        {
            get { return _episodes; }
            set { SetProperty(ref _episodes, value); }
        }
        private ObservableCollection<EpisodeModel> _episodes_watched;
        public ObservableCollection<EpisodeModel> EpisodesWatched
        {
            get { return _episodes_watched; }
            set { SetProperty(ref _episodes_watched, value); }
        }
    }
}
