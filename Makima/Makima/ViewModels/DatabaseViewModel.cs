﻿using Makima.Models;
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
        private ObservableCollection<DatabaseModel> _collection;
        public ObservableCollection<DatabaseModel> Collection
        {
            get { return _collection; }
            set { SetProperty(ref _collection, value); }
        }
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
            Cache = new CacheViewModel();
            Library = new ObservableCollection<SeriesModel>();
            Database = new DatabaseModel()
            {
                Series = new ObservableCollection<SeriesModel>()
            };
            Collection = new ObservableCollection<DatabaseModel>();

            Load();
        }

        public DatabaseModel Search(SeriesModel series)
        {
            foreach (DatabaseModel db in Collection)
            {
                if (db.Series.Contains(series) == true)
                {
                    return (db);
                }
            }

            return (null);
        }

        private void Load()
        {
            DatabaseModel data = null;
            string[] files = Directory.GetFiles(SettingsModel.Root.Path, $"*.{SettingsModel.Extension}", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                data = JsonConvert.DeserializeObject<DatabaseModel>(File.ReadAllText(file));
                Collection.Add(data);
                AddSeriesToDatabase(data.Series);
            }
        }

        public SeriesModel Search(string name)
        {
            foreach (SeriesModel series in Database.Series)
            {
                if (series.Name == name) return series;
            }
            return (null);
        }

        public void Update(SeriesModel series)
        {
            DatabaseModel db = Search(series);
            int index = 0;

            if (db != null)
            {
                index = SearchIndex(db, series.Name);
                if (index != -1)
                {
                    db.Series.RemoveAt(index);
                    db.Series.Insert(index, series);
            
                    Save(db);
                }
            }
        }

        private int SearchIndex(DatabaseModel db, string name)
        {
            SeriesModel series = Search(name);

            if (series != null)
            {
                return (db.Series.IndexOf(series));
            }

            return (-1);
        }

        public async void Add(string path)
        {
            string[] series = null;
            DatabaseModel db = null;
            SeriesModel series_model = null;

            if (Directory.Exists(path) == true)
            {
                db = new DatabaseModel()
                {
                    Path = path,
                    ID = $"{path.GetHashCode()}",
                    Series = new ObservableCollection<SeriesModel>()
                };
                series = Directory.GetDirectories(path);
                foreach (string folder in series)
                {
                    series_model = await Series(folder, GetName(folder));
                    db.Series.Add(series_model);
                }
                AddSeriesToDatabase(db.Series);
                if (UpdateCollection(db) == false)
                {
                    Collection.Add(db);
                }
                Save(db);
            }
        }

        private bool UpdateCollection(DatabaseModel db)
        {
            for (int i = 0; i < Collection.Count(); i++)
            {
                if (db.ID == Collection[i].ID)
                {
                    Collection[i] = db;
                    return (true);
                }
            }

            return (false);
        }

        private void AddSeriesToDatabase(ObservableCollection<SeriesModel> series)
        {
            foreach (SeriesModel seriesModel in series)
            {
                if (SeriesExists(seriesModel) == false)
                    Database.Series.Add(seriesModel);
            }
        }

        private bool SeriesExists(SeriesModel series)
        {
            foreach (SeriesModel current in Database.Series)
            {
                if (current.ID == series.ID)
                {
                    return (true);
                }
            }
            return (false);
        }

        private async Task<SeriesModel> Series(string path, string name)
        {
            string[] folders = Directory.GetDirectories(path);
            SeriesModel model = new SeriesModel()
            {
                Name = name,
                ID = $"{name.GetHashCode()}",
                Seasons = new ObservableCollection<SeasonModel>(),
                Splash = await Cache.Load(name)
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
            string[] extensions =
            {
                "mp4",
                "mov",
                "avi",
                "mkv"
            };
            SeasonModel model = new SeasonModel()
            {
                Name = name,
                ID = $"{name.GetHashCode()}",
                Episodes = new ObservableCollection<EpisodeModel>()
            };

            foreach (string file in files)
            {
                foreach (string extension in extensions)
                {
                    if (file.EndsWith($".{extension}") == true)
                    {
                        model.Episodes.Add(Episode(GetName(file)));
                    }
                }
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

        private void Save(DatabaseModel db)
        {
            string complete = $"{SettingsModel.AnimeFolder.Path}/{db.ID}.{SettingsModel.Extension}";

            if (File.Exists(complete) == true)
            {
                File.Delete(complete);
            }

            File.WriteAllText(complete, JsonConvert.SerializeObject(db));
        }
    }
}
