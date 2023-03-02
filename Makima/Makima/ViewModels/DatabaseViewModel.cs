using Makima.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace Makima.ViewModels
{
    public class DatabaseViewModel: BaseViewModel
    {
        private string Root { get; set; }
        private string Extension { get; set; }
        private CacheViewModel _cache;
        public CacheViewModel Cache
        {
            get { return _cache; }
            set { SetProperty(ref _cache, value); }
        }
        private DatabaseModel _database;
        public DatabaseModel Database
        {
            get { return _database; }
            set { SetProperty(ref _database, value); }
        }
        private ObservableCollection<SeriesModel> _library;
        public ObservableCollection<SeriesModel> Library
        {
            get { return _library; }
            set { SetProperty(ref _library, value); }
        }

        public DatabaseViewModel()
        {
            Root = "Library";
            Extension = "mlf";
            Cache = new CacheViewModel();
            Library = new ObservableCollection<SeriesModel>();
            Database = new DatabaseModel()
            {
                Series = new ObservableCollection<SeriesModel>()
            };

            Initialize();
            Load();
        }

        private void Initialize()
        {
            if (Directory.Exists(Root) == false)
            {
                Directory.CreateDirectory(Root);
            }
        }

        private void Load()
        {
            DatabaseModel data = null;
            string[] files = Directory.GetFiles(Root, $"*.{Extension}", SearchOption.AllDirectories);


            foreach (string file in files)
            {
                data = JsonConvert.DeserializeObject<DatabaseModel>(File.ReadAllText(file));
                AddSeriesToDatabase(data.Series);
            }
        }

        public async void Add(string path)
        {
            string[] series = null;
            DatabaseModel db = null;
            SeriesModel series_model = null;

            if (Directory.Exists(path) == true)
            {
                db = new Models.DatabaseModel()
                {
                    Path = path,
                    ID = $"{path.GetHashCode()}",
                    Series = new ObservableCollection<Models.SeriesModel>()
                };
                series = Directory.GetDirectories(path);
                foreach (string folder in series)
                {
                    series_model = await Series(folder, GetName(folder));
                    if (db.Series.Contains(series_model) == false)
                    {
                        db.Series.Add(series_model);
                    }
                }
                Save(db);
                AddSeriesToDatabase(db.Series);
            }
        }

        private void AddSeriesToDatabase(ObservableCollection<SeriesModel> series)
        {
            foreach (SeriesModel seriesModel in series)
            {
                Database.Series.Add(seriesModel);
            }
        }

        private async Task<SeriesModel> Series(string path, string name)
        {
            string[] folders = Directory.GetDirectories(path);
            SeriesModel model = new SeriesModel()
            {
                Name = name,
                ID = $"{name.GetHashCode()}",
                Seasons = new ObservableCollection<SeasonModel>(),
                SeasonsWatched = new ObservableCollection<SeasonModel>(),
                Splash = new BitmapImage(await Cache.Search(name))
            };

            foreach (string folder in folders)
            {
                model.Seasons.Add(Season(folder, GetName(folder)));
            }

            return (model);
        }

        private SeasonModel Season(string path, string name)
        {
            string[] files = Directory.GetFiles(path);
            SeasonModel model = new SeasonModel()
            {
                Name = name,
                ID = $"{name.GetHashCode()}",
                Episodes = new ObservableCollection<EpisodeModel>(),
                EpisodesWatched = new ObservableCollection<EpisodeModel>()
            };

            foreach (string file in files)
            {
                model.Episodes.Add(Episode(GetName(file)));
            }

            return (model);
        }

        private EpisodeModel Episode(string name)
        {
            return (new EpisodeModel()
            {
                Name = name,
                ID = $"{name.GetHashCode()}"
            });
        }

        private string GetName(string path)
        {
            string[] splitted = null;

            if (path != null && path != string.Empty)
            {
                if (path.Contains('\\') == true)
                {
                    splitted = path.Split('\\');
                    return (splitted[splitted.Length - 1]);
                }
            }
            return (null);
        }

        public void MoveRight()
        {
            SeriesModel buffer = null;
            int count = Database.Series.Count();

            if (count > 0)
            {
                buffer = Database.Series[0];
                Database.Series.RemoveAt(0);
                Database.Series.Add(buffer);
            }
        }

        public void MoveLeft()
        {
            SeriesModel buffer = null;
            int count = Database.Series.Count();

            if (count > 0)
            {
                Console.WriteLine($"Left move: {Database.Series[0].ID}");
                buffer = Database.Series[count - 1];
                Database.Series.RemoveAt(count - 1);
                Database.Series.Insert(0, buffer);
                Console.WriteLine($"Left move: {Database.Series[0].ID}");
            }
        }

        private void Save(Models.DatabaseModel db)
        {
            string complete = $"{Root}/{db.ID}.{Extension}";

            if (File.Exists(complete) == true)
            {
                File.Delete(complete);
            }
            File.WriteAllText(complete, Newtonsoft.Json.JsonConvert.SerializeObject(db));
        }

        private void Remove()
        {

        }
    }
}
