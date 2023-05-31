using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;
using Makima.Models;
using Makima.Properties;

namespace Makima.ViewModels
{
    public class NyaaViewModel: BaseViewModel
    {
        private string Domain { get; set; }
        private XmlDocument Catalog { get; set; }
        private HttpClient Client { get; set; }
        private XmlNamespaceManager NamespaceManager { get; set; }
        private ObservableCollection<NyaaModel> _nyaa;
        public ObservableCollection<NyaaModel> Nyaa
        {
            get { return _nyaa; }
            set { SetProperty(ref _nyaa, value); }
        }

        public NyaaViewModel()
        {
            Domain = "https://nyaa.si/?page=rss&f=0&c=1_0";
            Client = new HttpClient();
            Catalog = new XmlDocument();
            Nyaa = new ObservableCollection<NyaaModel>();
        }

        private void Prepare()
        {
            NyaaModel Model = null;
            BrushConverter converter = new BrushConverter();

            if (Nyaa.Count > 0)
            {
                Nyaa.Clear();
            }

            foreach (XmlNode xmlNode in Catalog.DocumentElement.SelectNodes("/rss/channel/item"))
            {
                NamespaceManager = new XmlNamespaceManager(xmlNode.OwnerDocument.NameTable);
                NamespaceManager.AddNamespace("nyaa", "https://nyaa.si/xmlns/nyaa");
                Model = new NyaaModel()
                {
                    Title = xmlNode.SelectSingleNode("title", NamespaceManager).InnerText,
                    Link = xmlNode.SelectSingleNode("link", NamespaceManager).InnerText,
                    Guid = xmlNode.SelectSingleNode("guid", NamespaceManager).InnerText,
                    PubDate = DateTime.Parse(xmlNode.SelectSingleNode("pubDate", NamespaceManager).InnerText),
                    Seeders = (xmlNode.SelectSingleNode("nyaa:seeders", NamespaceManager) != null ? Int16.Parse(xmlNode.SelectSingleNode("nyaa:seeders", NamespaceManager).InnerText) : -1),
                    Leechers = (xmlNode.SelectSingleNode("nyaa:leechers", NamespaceManager) != null ? Int16.Parse(xmlNode.SelectSingleNode("nyaa:leechers", NamespaceManager).InnerText) : -1),
                    Downloads = (xmlNode.SelectSingleNode("nyaa:downloads", NamespaceManager) != null ? Int16.Parse(xmlNode.SelectSingleNode("nyaa:downloads", NamespaceManager).InnerText) : -1),
                    Size = (xmlNode.SelectSingleNode("nyaa:size", NamespaceManager) != null ? xmlNode.SelectSingleNode("nyaa:size", NamespaceManager).InnerText : null),
                };

                Model.StatusSeeders = (
                    Model.Seeders > 0 ?
                    (Brush)converter.ConvertFrom(Application.Current.Resources["Green"].ToString()) :
                    (Brush)converter.ConvertFrom(Application.Current.Resources["Red"].ToString())
                );
                Model.StatusLeechers = (
                    Model.Leechers > 0 ?
                    (Brush)converter.ConvertFrom(Application.Current.Resources["Green"].ToString()) :
                    (Brush)converter.ConvertFrom(Application.Current.Resources["Red"].ToString())
                );
                
                Nyaa.Add(Model);
            }
        }

        private async Task Download(string Url)
        {
            HttpResponseMessage response = await Client.GetAsync(Url);

            Catalog.LoadXml(await response.Content.ReadAsStringAsync());
        }

        public async void New()
        {
            await Download(Domain);

            Prepare();
        }

        public async void DownloadTorrent(string Url)
        {
            HttpResponseMessage response = await Client.GetAsync(Url);

            File.WriteAllBytes($"{SettingsModel.TorrentFolder.Path}\\{Path.GetFileName(Url)}", await response.Content.ReadAsByteArrayAsync());
        }

        public async void Search(string Series)
        {
            await Download($"{Domain}&q={Format(Series)}");

            Prepare();
        }

        private string Format(string Series)
        {
            (string, string)[] filters = new (string, string)[]
            {
                ( " ", "+" )
            };

            foreach ((string, string) filter in filters)
            {
                if (Series.Contains(filter.Item1) == true)
                {
                    Series = Series.Replace(filter.Item1, filter.Item2);
                }
            }

            return (Series);
        }
    }
}
