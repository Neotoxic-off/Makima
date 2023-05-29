using Makima.Models;
using Microsoft.Win32;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Ookii.Dialogs.Wpf;
using System.ComponentModel;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace Makima.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private SeriesModel _selected_series = null;
        public SeriesModel SelectedSeries
        {
            get { return _selected_series; }
            set { SetProperty(ref _selected_series, value); }
        }
        private bool _season_lock = false;
        public bool SeasonLock
        {
            get { return _season_lock; }
            set { SetProperty(ref _season_lock, value); }
        }
        private bool _episode_lock = false;
        public bool EpisodeLock
        {
            get { return _episode_lock; }
            set { SetProperty(ref _episode_lock, value); }
        }


        private SeasonModel _seasons;
        public SeasonModel Seasons
        {
            get { return _seasons; }
            set { SetProperty(ref _seasons, value); }
        }
        private EpisodeModel _episodes;
        public EpisodeModel Episodes
        {
            get { return _episodes; }
            set { SetProperty(ref _episodes, value); }
        }

        public SettingsViewModel Settings { get; set; }
        public LoggerViewModel Logger { get; set; }
        private VistaFolderBrowserDialog FolderSelector { get; set; }

        public DatabaseViewModel Database { get; set; }

        public DelegateCommand AddLibraryCommand { get; set; }
        public DelegateCommand RightArrowCommand { get; set; }
        public DelegateCommand LeftArrowCommand { get; set; }
        public DelegateCommand SendCommand { get; set; }
        public DelegateCommand GithubCommand { get; set; }
        public DelegateCommand DiscordCommand { get; set; }
        public DelegateCommand CodeCommand { get; set; }
        public DelegateCommand GetSelectedCommand { get; set; }
        public DelegateCommand WatchCommand { get; set; }
        public DelegateCommand ConfigurationCommand { get; set; }

        public MainViewModel()
        {
            Logger = new LoggerViewModel();

            LoadModels();
            LoadCommands();
            Load();
        }

        private void Load()
        {
            Logger.Record("loading client");

            Settings.LoadVersion();
            FolderSelector = new VistaFolderBrowserDialog();

            Logger.Record("client loaded");
        }

        private void LoadModels()
        {
            Logger.Record("loading models");

            Settings = new SettingsViewModel();
            Database = new DatabaseViewModel();

            Logger.Record("models loaded");
        }

        private void LoadCommands()
        {
            Logger.Record("loading commands");

            AddLibraryCommand = new DelegateCommand(AddLibrary);
            RightArrowCommand = new DelegateCommand(RightArrow);
            LeftArrowCommand = new DelegateCommand(LeftArrow);

            GithubCommand = new DelegateCommand(Github);
            DiscordCommand = new DelegateCommand(Discord);
            CodeCommand = new DelegateCommand(Code);

            GetSelectedCommand = new DelegateCommand(GetSelected);
            WatchCommand = new DelegateCommand(Watch);

            Logger.Record("commands loaded");
        }

        private void Watch(object data)
        {
            string path = null;
            DatabaseModel db = Database.Search(SelectedSeries);

            if (db != null)
            {
                SelectedSeries.LatestSeason = Seasons.Name;
                SelectedSeries.LatestEpisode = Episodes.Name;

                if (SelectedSeries.LatestSeason != null && SelectedSeries.LatestEpisode != null)
                {
                    path = $"{db.Path}/{SelectedSeries.Name}/{SelectedSeries.LatestSeason}/{SelectedSeries.LatestEpisode}";
                    if (File.Exists(path) == true)
                    {
                        Logger.Record("updating series in database");
                        Database.Update(SelectedSeries);
                        Logger.Record("starting episode");
                        System.Diagnostics.Process.Start(path);
                        Logger.Record("episode started");
                    }
                }
            }
        }

        private void SetLatestInformation()
        {
            SetLatestSeason();
            SetLatestEpisode();
        }

        private void SetLatestSeason()
        {
            if (SelectedSeries.Seasons != null)
            {
                foreach (SeasonModel season in SelectedSeries.Seasons)
                {
                    if (season.Name == SelectedSeries.LatestSeason)
                    {
                        Seasons = season;
                        return;
                    }
                }
            }
        }

        private void SetLatestEpisode()
        {
            if (Seasons != null)
            {
                foreach (EpisodeModel episode in Seasons.Episodes)
                {
                    if (episode.Name == SelectedSeries.LatestEpisode)
                    {
                        Episodes = episode;
                        return;
                    }
                }
            }
        }

        private void GetSelected(object data)
        {
            SeasonLock = false;
            EpisodeLock = false;
            Logger.Record($"searching series {data}");
            SelectedSeries = Database.Search($"{data}");

            if (SelectedSeries != null)
            {
                SetLatestInformation();
                Logger.Record($"series {data} loaded");
                SeasonLock = true;
                EpisodeLock = true;
            }
            else
            {
                Logger.Record($"series {data} not loaded");
            }
        }

        private void AddLibrary(object data)
        {
            if (FolderSelector.ShowDialog() == true)
            {
                Logger.Record("adding library");
                Database.Add(FolderSelector.SelectedPath);
                Logger.Record("library added");
            }
        }

        private void RightArrow(object data)
        {
            Database.MoveRight();
        }

        private void LeftArrow(object data)
        {
            Database.MoveLeft();
        }

        private void Github(object data)
        {
            System.Diagnostics.Process.Start(Settings.Github);
        }

        private void Discord(object data)
        {
            System.Diagnostics.Process.Start(Settings.Discord);
        }

        private void Code(object data)
        {
            System.Diagnostics.Process.Start(Settings.Code);
        }
    }
}
