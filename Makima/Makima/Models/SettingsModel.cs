using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makima.Models
{
    public class SettingsModel
    {
        public static FolderModel Root = new FolderModel(
            $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Makima",
            true
        );
        public static FolderModel CacheFolder = new FolderModel(
            $"{Root.Path}\\Cache",
            true
        );
        public static FolderModel GamesFolder = new FolderModel(
            $"{Root.Path}\\Anime",
            true
        );
        public static FolderModel LibraryFolder = new FolderModel(
            $"{Root.Path}\\Library",
            true
        );
        public static FolderModel LogFolder = new FolderModel(
            "Logs",
            true
        );
    }
}
