using Makima.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Makima.ViewModels
{
    public class DatabaseViewModel: BaseViewModel
    {
        private string Root { get; set; }
        private string Extension { get; set; }
        private CacheViewModel Cache { get; set; }
        private Models.DatabaseModel _database;
        public Models.DatabaseModel Database
        {
            get { return _database; }
            set { SetProperty(ref _database, value); }
        }

        public DatabaseViewModel()
        {
            Root = "Library";
            Extension = "mlf";
            Cache = new CacheViewModel();

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

        private async void Load()
        {
            Models.DatabaseModel data = null;
            string[] files = Directory.GetFiles(Root, $"*.{Extension}", SearchOption.AllDirectories);

            Database = new Models.DatabaseModel()
            {
                Series = new List<Models.SeriesModel>()
                {
                    new Models.SeriesModel()
                    {
                        Name = "lycoris recoil",
                        Splash = new BitmapImage(await Cache.Search("lycoris recoil"))
                    },
                    new Models.SeriesModel()
                    {
                        Name = "chainsawman",
                        Splash = new BitmapImage(await Cache.Search("chainsawman"))
                    },
                    new Models.SeriesModel()
                    {
                        Name = "sword art online",
                        Splash = new BitmapImage(await Cache.Search("sword art online"))
                    }
                }
            };

            foreach (string file in files)
            {
                data = JsonConvert.DeserializeObject<Models.DatabaseModel>(File.ReadAllText(file));
                Database.Series.AddRange(data.Series);
            }
        }

        private void Refresh()
        {

        }

        public async void Add(string path)
        {
            string[] series = null;
            Models.DatabaseModel db = null;

            if (Directory.Exists(path) == true)
            {
                db = new Models.DatabaseModel()
                {
                    Path = path,
                    ID = $"{path.GetHashCode()}",
                    Series = new List<Models.SeriesModel>()
                };
                series = Directory.GetDirectories(path);
                foreach (string folder in series)
                {
                    db.Series.Add(await Series(folder, GetName(folder)));
                }
            }

            Save(db);
        }

        private async Task<SeriesModel> Series(string path, string name)
        {
            string[] folders = Directory.GetDirectories(path);
            SeriesModel model = new SeriesModel()
            {
                Name = name,
                ID = $"{name.GetHashCode()}",
                Seasons = new List<SeasonModel>(),
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
                Episodes = new List<EpisodeModel>(),
                EpisodesWatched = new List<EpisodeModel>()
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
