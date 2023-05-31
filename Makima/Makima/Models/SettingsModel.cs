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
        public static FolderModel AnimeFolder = new FolderModel(
            $"{Root.Path}\\Anime",
            true
        );
        public static FolderModel TorrentFolder = new FolderModel(
            $"Torrents",
            true
        );
        public static FolderModel LogFolder = new FolderModel(
            $"{Root.Path}\\Logs",
            true
        );
    }
}
