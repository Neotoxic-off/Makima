using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Makima.Models
{
    public class SeriesModel: BaseModel
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
