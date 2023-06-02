using Makima.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Makima.ViewModels
{
    public class DownloadsViewModel: BaseViewModel
    {
        private ObservableCollection<FileModel> _files;
        public ObservableCollection<FileModel> Files
        {
            get { return _files; }
            set { SetProperty(ref _files, value); }
        }
        public DelegateCommand RemoveCommand { get; set; }
        public DelegateCommand OpenCommand { get; set; }

        public DownloadsViewModel()
        {
            Load();
        }

        private void Load()
        {
            RemoveCommand = new DelegateCommand(Remove);
            OpenCommand = new DelegateCommand(Open);

            Files = new ObservableCollection<FileModel>();

            foreach (string file in Directory.GetFiles(SettingsModel.TorrentFolder.Path, $"*.torrent"))
            {
                Files.Add(new FileModel(file));
            }
        }

        private void Open(object data)
        {
            System.Diagnostics.Process.Start(data.ToString());
        }

        private void Remove(object data)
        {
            string id = (string)data;
            int index = -1;

            foreach (FileModel file in Files)
            {
                if (file.Path == id)
                {
                    File.Delete(file.Path);
                    index = Files.IndexOf(file);
                }
            }

            if (index > -1)
            {
                Files.RemoveAt(index);
            }
        }
    }
}
