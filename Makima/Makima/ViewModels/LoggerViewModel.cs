using Makima.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makima.ViewModels
{
    public class LoggerViewModel: BaseViewModel
    {
        private ObservableCollection<string> _records;
        public ObservableCollection<string> Records
        {
            get { return (_records); }
            set { SetProperty(ref _records, value); }
        }
        private string _log = "";
        public string Log
        {
            get { return (_log); }
            set { SetProperty(ref _log, value); }
        }

        public LoggerViewModel()
        {
            Records = new ObservableCollection<string>();
        }

        public void Record(string message)
        {
            Log = $"{message.First().ToString().ToUpper()}{message.Substring(1)}";
            Records.Insert(0, Log);
        }

        public void Save()
        {
            System.IO.File.AppendAllLines($"{Models.SettingsModel.LogFolder.Path}/log_{DateTime.Now.ToString("h_mm_ss")}.txt", Records);
        }
    }
}
