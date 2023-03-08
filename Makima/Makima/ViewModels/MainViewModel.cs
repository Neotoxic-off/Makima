﻿using Makima.Models;
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

namespace Makima.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private SeriesModel _selected_series;
        public SeriesModel SelectedSeries
        {
            get { return _selected_series; }
            set { SetProperty(ref _selected_series, value); }
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
                Logger.Record("episode found in database");
                path = $"{db.Path}/{SelectedSeries.Name}/{SelectedSeries.LatestSeason.Name}/{SelectedSeries.LatestEpisode.Name}";
                Logger.Record("updating series in database");
                Database.Update(SelectedSeries);
                Logger.Record("starting episode");
                System.Diagnostics.Process.Start(path);
            }
        }

        private void GetSelected(object data)
        {
            Logger.Record($"searching series {data}");
            SelectedSeries = Database.Search($"{data}");

            if (SelectedSeries != null)
            {
                Logger.Record($"series {data} loaded");
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
