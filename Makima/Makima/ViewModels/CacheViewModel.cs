using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace Makima.ViewModels
{
    public class CacheViewModel: BaseViewModel
    {
        private HttpClient Client { get; set; }
        public CacheViewModel()
        {
            Client = new HttpClient();
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
                    return (new Uri($"https://image.tmdb.org/t/p/original/{seachModel.results[0].poster_path}"));
                }
            }

            return (new Uri("https://townofmaringouin.net/wp-content/uploads/image-placeholder-500x500-1.jpg"));
        }
    }
}
