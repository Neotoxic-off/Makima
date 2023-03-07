using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static Makima.ViewModels.BaseViewModel;

namespace Makima.Models
{
    public class SeriesModel: BaseModel
    {
        private bool _selected;
        public bool Selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }
        private SeasonModel _latest_season;
        public SeasonModel LatestSeason
        {
            get { return _latest_season; }
            set { SetProperty(ref _latest_season, value); }
        }
        private EpisodeModel _latest_episode;
        public EpisodeModel LatestEpisode
        {
            get { return _latest_episode; }
            set { SetProperty(ref _latest_episode, value); }
        }
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
        private ImageSource _splash;
        public ImageSource Splash
        {
            get { return _splash; }
            set { SetProperty(ref _splash, value); }
        }
        private ObservableCollection<SeasonModel> _seasons;
        public ObservableCollection<SeasonModel> Seasons
        {
            get { return _seasons; }
            set { SetProperty(ref _seasons, value); }
        }
        private ObservableCollection<SeasonModel> _seasons_watched;
        public ObservableCollection<SeasonModel> SeasonsWatched
        {
            get { return _seasons_watched; }
            set { SetProperty(ref _seasons_watched, value); }
        }
    }
}
