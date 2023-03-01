using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Makima.ViewModels
{
    public class DatabaseViewModel: BaseViewModel
    {
        private string Root { get; set; }
        private Models.DatabaseModel _database;
        public Models.DatabaseModel Database
        {
            get { return _database; }
            set { SetProperty(ref _database, value); }
        }

        public DatabaseViewModel()
        {
            Root = "Library";
            Database = new Models.DatabaseModel()
            {
                Series = new List<Models.SeriesModel>()
                {
                    new Models.SeriesModel()
                    {
                        Name = "Test 1",
                        Splash = new BitmapImage(new Uri("https://www.themoviedb.org/t/p/w600_and_h900_bestv2/zI3E2a3WYma5w8emI35mgq5Iurx.jpg"))
                    },
                    new Models.SeriesModel()
                    {
                        Name = "Test 2",
                        Splash = new BitmapImage(new Uri("https://www.themoviedb.org/t/p/w1280/el8RGlODqenqIs6Mcbyaog6DnXe.jpg"))
                    },
                    new Models.SeriesModel()
                    {
                        Name = "Test 3",
                        Splash = new BitmapImage(new Uri("https://www.themoviedb.org/t/p/w600_and_h900_bestv2/zI3E2a3WYma5w8emI35mgq5Iurx.jpg"))
                    },
                    new Models.SeriesModel()
                    {
                        Name = "Test 3",
                        Splash = new BitmapImage(new Uri("https://www.themoviedb.org/t/p/w600_and_h900_bestv2/zI3E2a3WYma5w8emI35mgq5Iurx.jpg"))
                    },
                    new Models.SeriesModel()
                    {
                        Name = "Test 3",
                        Splash = new BitmapImage(new Uri("https://www.themoviedb.org/t/p/w600_and_h900_bestv2/zI3E2a3WYma5w8emI35mgq5Iurx.jpg"))
                    },
                    new Models.SeriesModel()
                    {
                        Name = "Test 3",
                        Splash = new BitmapImage(new Uri("https://www.themoviedb.org/t/p/w600_and_h900_bestv2/zI3E2a3WYma5w8emI35mgq5Iurx.jpg"))
                    },
                    new Models.SeriesModel()
                    {
                        Name = "Test 3",
                        Splash = new BitmapImage(new Uri("https://www.themoviedb.org/t/p/w600_and_h900_bestv2/zI3E2a3WYma5w8emI35mgq5Iurx.jpg"))
                    },
                    new Models.SeriesModel()
                    {
                        Name = "Test 3",
                        Splash = new BitmapImage(new Uri("https://www.themoviedb.org/t/p/w600_and_h900_bestv2/zI3E2a3WYma5w8emI35mgq5Iurx.jpg"))
                    },
                    new Models.SeriesModel()
                    {
                        Name = "Test 3",
                        Splash = new BitmapImage(new Uri("https://www.themoviedb.org/t/p/w600_and_h900_bestv2/zI3E2a3WYma5w8emI35mgq5Iurx.jpg"))
                    },
                    new Models.SeriesModel()
                    {
                        Name = "Test 3",
                        Splash = new BitmapImage(new Uri("https://www.themoviedb.org/t/p/w600_and_h900_bestv2/zI3E2a3WYma5w8emI35mgq5Iurx.jpg"))
                    },
                    new Models.SeriesModel()
                    {
                        Name = "Test 3",
                        Splash = new BitmapImage(new Uri("https://www.themoviedb.org/t/p/w600_and_h900_bestv2/zI3E2a3WYma5w8emI35mgq5Iurx.jpg"))
                    },
                    new Models.SeriesModel()
                    {
                        Name = "Test 3",
                        Splash = new BitmapImage(new Uri("https://www.themoviedb.org/t/p/w600_and_h900_bestv2/zI3E2a3WYma5w8emI35mgq5Iurx.jpg"))
                    },
                    new Models.SeriesModel()
                    {
                        Name = "Test 3",
                        Splash = new BitmapImage(new Uri("https://www.themoviedb.org/t/p/w600_and_h900_bestv2/zI3E2a3WYma5w8emI35mgq5Iurx.jpg"))
                    },
                    new Models.SeriesModel()
                    {
                        Name = "Test 3",
                        Splash = new BitmapImage(new Uri("https://www.themoviedb.org/t/p/w600_and_h900_bestv2/zI3E2a3WYma5w8emI35mgq5Iurx.jpg"))
                    }
                }
            };

            Initialize();
            Load();
        }

        private void Initialize()
        {
            if (Directory.Exists(Root) == false)
            {
                Directory.CreateDirectory(Root);
            }
        }

        private void Load()
        {
            Models.DatabaseModel data = null;
            string[] files = Directory.GetFiles(Root, "*.mlc", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                data = JsonConvert.DeserializeObject<Models.DatabaseModel>(File.ReadAllText(file));
                Database.Series = Enumerable.Concat(Database.Series, data.Series);
            }
        }

        private void Refresh()
        {

        }

        private void Add()
        {

        }

        private void Remove()
        {

        }
    }
}
