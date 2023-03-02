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

namespace Makima.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public SettingsViewModel Settings { get; set; }
        public LoggerViewModel Logger { get; set; }
        private VistaFolderBrowserDialog FolderSelector { get; set; }

        public DatabaseViewModel Database { get; set; }

        public DelegateCommand AddLibraryCommand { get; set; }
        public DelegateCommand SendCommand { get; set; }
        public DelegateCommand GithubCommand { get; set; }
        public DelegateCommand DiscordCommand { get; set; }
        public DelegateCommand CodeCommand { get; set; }

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

            GithubCommand = new DelegateCommand(Github);
            DiscordCommand = new DelegateCommand(Discord);
            CodeCommand = new DelegateCommand(Code);

            Logger.Record("commands loaded");
        }

        private void AddLibrary(object data)
        {
            if (FolderSelector.ShowDialog() == true)
            {
                Database.Add(FolderSelector.SelectedPath);
            }
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
