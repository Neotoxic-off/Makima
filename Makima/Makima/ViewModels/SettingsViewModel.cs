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

        private string _code = "https://github.com/Neotoxic-off/Makima";
        public string Code
        {
            get { return _code; }
            set { SetProperty(ref _code, value); }
        }

        private string _github = "https://github.com/Neotoxic-off";
        public string Github
        {
            get { return _github; }
            set { SetProperty(ref _github, value); }
        }

        private string _bits;
        public string Bits
        {
            get { return _bits; }
            set { SetProperty(ref _bits, value); }
        }

        private string _buildBits;
        public string BuildBits
        {
            get { return _buildBits; }
            set { SetProperty(ref _buildBits, value); }
        }

        private OperatingSystem _osVersion;
        public OperatingSystem OSVersion
        {
            get { return _osVersion; }
            set { SetProperty(ref _osVersion, value); }
        }

        public SettingsViewModel()
        {
            LoadVersion();

            Bits = (Environment.Is64BitOperatingSystem == true ? "x64" : "x86");
            BuildBits = (Environment.Is64BitProcess == true ? "x64" : "x86");
            OSVersion = Environment.OSVersion;
        }

        public void LoadVersion()
        {
            Version = $"{Assembly.GetExecutingAssembly().GetName().Version}";
        }
    }
}
