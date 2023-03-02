using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makima.Models
{
    public class DatabaseModel: BaseModel
    {
        private string _id;
        public string ID
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        private string _path;
        public string Path
        {
            get { return _path; }
            set { SetProperty(ref _path, value); }
        }

        private ObservableCollection<SeriesModel> _series;
        public ObservableCollection<SeriesModel> Series
        {
            get { return _series; }
            set { SetProperty(ref _series, value); }
        }
    }
}
