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
using System.ComponentModel;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Media;
using System.Numerics;
using System.Windows.Navigation;
using Makima.Views;
using Makima.ViewModels;
using static Makima.ViewModels.BaseViewModel;

namespace Makima.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public SettingsViewModel Settings { get; set; }
        public DelegateCommand NavigateCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand MinimizeCommand { get; set; }
        public DelegateCommand MaximizeCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }

        private UserControl _currentView = null;
        public UserControl CurrentView
        {
            get { return (_currentView); }
            set { SetProperty(ref _currentView, value); }
        }
        private Dictionary<string, (UserControl view, bool dynamic, object param)> Bind { get; set; }
        private Views.AnimeView _animeView = null;
        public Views.AnimeView AnimeView
        {
            get { return (_animeView); }
            set { SetProperty(ref _animeView, value); }
        }
        private Views.SearchView _searchView = null;
        public Views.SearchView SearchView
        {
            get { return (_searchView); }
            set { SetProperty(ref _searchView, value); }
        }
        private Views.SettingsView _settingsView = null;
        public Views.SettingsView SettingsView
        {
            get { return (_settingsView); }
            set { SetProperty(ref _settingsView, value); }
        }
        private LoggerViewModel _logger;
        public LoggerViewModel Logger
        {
            get { return (_logger); }
            set { SetProperty(ref _logger, value); }
        }

        public MainViewModel()
        {
            Logger = new LoggerViewModel();

            LoadModels();
            LoadCommands();
            Load();
        }

        private void Load()
        {
            Settings.LoadVersion();

            AnimeView = new AnimeView();
            SearchView = new SearchView();
            SettingsView = new SettingsView();

            Bind = new Dictionary<string, (UserControl view, bool dynamic, object param)>()
            {
                { "Anime", (AnimeView, false, null) },
                { "Search", (SearchView, false, null) },
                { "Settings", (SettingsView, false, null) }
            };
            CurrentView = Bind["Anime"].view;
        }

        private void LoadModels()
        {
            Logger.Record("loading models");

            Settings = new SettingsViewModel();

            Logger.Record("models loaded");
        }

        private void LoadCommands()
        {
            Logger.Record("loading commands");

            NavigateCommand = new DelegateCommand(Navigate);
            SaveCommand = new DelegateCommand(Save);
            MinimizeCommand = new DelegateCommand(Reduce);
            MaximizeCommand = new DelegateCommand(Maximize);
            CloseCommand = new DelegateCommand(Close);

            Logger.Record("commands loaded");
        }

        private void Reduce(object data)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void Maximize(object data)
        {
            Dictionary<WindowState, WindowState> States = new Dictionary<WindowState, WindowState>()
            {
                { WindowState.Normal, WindowState.Maximized },
                { WindowState.Minimized, WindowState.Normal },
                { WindowState.Maximized, WindowState.Normal }
            };

            Application.Current.MainWindow.WindowState = States[Application.Current.MainWindow.WindowState];
        }

        private void Close(object data)
        {
            Application.Current.Shutdown();
        }

        private void Save(object data)
        {
            Logger.Record("saving logs");
            Logger.Save();
            Logger.Record("logs saved");
        }

        private void Navigate(object data)
        {
            string key = "Search";

            if (Bind.ContainsKey(data.ToString()) == true)
                key = data.ToString();
            if (Bind[key].dynamic == true)
            {
                Logger.Record($"building new instance for {key}");
                Bind[key] = ((UserControl)Activator.CreateInstance(Bind[key].view.GetType(), null, null), Bind[key].dynamic, Bind[key].param);
            }
            Logger.Record($"moving to {key}");
            CurrentView = Bind[key].view;
        }
    }
}
