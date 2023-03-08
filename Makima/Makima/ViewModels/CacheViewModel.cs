using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace Makima.ViewModels
{
    public class CacheViewModel: BaseViewModel
    {
        private string Root { get; set; }
        private HttpClient Client { get; set; }
        public CacheViewModel()
        {
            Root = $"{RootFolder}/Cache";
            Client = new HttpClient();

            Initialize();
        }

        private void Initialize()
        {
            if (Directory.Exists(Root) == false)
            {
                Directory.CreateDirectory(Root);
            }
        }

        private string Format(string name)
        {
            string copy = name;
            List<(char, char)> map = new List<(char, char)>()
            {
                (' ', '+'),
                ('-', '+'),
                ('_', '+')
            };

            if (name != null && name != string.Empty)
            {
                foreach ((char, char) item in map)
                {
                    if (copy.Contains(item.Item1) == true)
                    {
                        copy = copy.Replace(item.Item1, item.Item2);
                    }
                }
            }

            return (copy.ToLower());
        }

        public async Task<Uri> Search(string name)
        {
            Models.SeachModel seachModel = null;
            HttpResponseMessage response = await Client.GetAsync($"https://api.themoviedb.org/3/search/tv?api_key={Properties.Resources.api_key}&query={Format(name)}"); ;
            
            if (response.IsSuccessStatusCode == true)
            {
                seachModel = JsonConvert.DeserializeObject<Models.SeachModel>(await response.Content.ReadAsStringAsync());
                if (seachModel.results.Count() > 0)
                {
                    return (new Uri($"https://image.tmdb.org/t/p/original{seachModel.results[0].poster_path}"));
                }
            }

            return (new Uri("https://raw.githubusercontent.com/Neotoxic-off/Makima/main/Assets/Placeholder.png"));
        }

        public async Task<BitmapImage> Load(string name)
        {
            string path = $"{Root}/{name}.jpg";

            if (Directory.Exists(Root) == true)
            {
                if (File.Exists(path) == true)
                {
                    return (LoadImage(path));
                }
            }
            return (await Download(path, name));
        }

        private BitmapImage LoadImage(string path)
        {
            return (new BitmapImage(new Uri(path)));
        }

        private async Task<BitmapImage> Download(string path, string name)
        {
            Stream stream = null;
            Uri uri = null;

            uri = await Search(name);
            stream = await Client.GetStreamAsync(uri);
            using (FileStream outputFileStream = new FileStream(path, FileMode.Create))
            {
                stream.CopyTo(outputFileStream);
            }

            return (LoadImage(path));
        }
    }
}
