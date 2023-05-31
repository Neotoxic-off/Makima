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
    public class LibraryViewModel: BaseViewModel
    {
        private ObservableCollection<DatabaseModel> _databases; 
        public ObservableCollection<DatabaseModel> Databases
        {
            get { return _databases; }
            set { SetProperty(ref _databases, value); }
        }
        public DelegateCommand RemoveCommand { get; set; }

        public LibraryViewModel()
        {
            Load();
        }

        private void Load()
        {
            RemoveCommand = new DelegateCommand(Remove);
            Databases = new ObservableCollection<DatabaseModel>();

            foreach (string file in Directory.GetFiles(SettingsModel.AnimeFolder.Path, $"*.{SettingsModel.Extension}"))
            {
                Databases.Add(JsonConvert.DeserializeObject<DatabaseModel>(File.ReadAllText(file)));
            }
        }

        private void Remove(object data)
        {
            string id = (string)data;
            int index = -1;

            foreach (DatabaseModel db in Databases)
            {
                if (db.ID == id)
                {
                    File.Delete($"{SettingsModel.AnimeFolder.Path}\\{id}.{SettingsModel.Extension}");
                    index = Databases.IndexOf(db);
                }
            }

            if (index > -1)
            {
                Databases.RemoveAt(index);
            }
        }
    }
}
