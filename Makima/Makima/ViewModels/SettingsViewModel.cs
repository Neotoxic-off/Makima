using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Makima.ViewModels
{
    public class SettingsViewModel: BaseViewModel
    {
        private string _version;
        public string Version
        {
            get { return _version; }
            set { SetProperty(ref _version, value); }
        }

        private string _root = "Downloads";
        public string Root
        {
            get { return _root; }
            set { SetProperty(ref _root, value); }
        }

        private string _profile = "https://avatars.githubusercontent.com/u/44700383?v=4";
        public string Profile
        {
            get { return _profile; }
            set { SetProperty(ref _profile, value); }
        }

        private string _code = "https://github.com/Neotoxic-off/Makima";
        public string Code
        {
            get { return _code; }
            set { SetProperty(ref _code, value); }
        }

        private string _discord = "https://discord.gg/vW5PA5VASb";
        public string Discord
        {
            get { return _discord; }
            set { SetProperty(ref _discord, value); }
        }

        private string _github = "https://github.com/Neotoxic-off";
        public string Github
        {
            get { return _github; }
            set { SetProperty(ref _github, value); }
        }

        public void LoadVersion()
        {
            Version = $"{Assembly.GetExecutingAssembly().GetName().Version}";
        }
    }
}
