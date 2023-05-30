using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Makima.Models;

namespace Makima.ViewModels
{
    public class NyaaViewModel: BaseViewModel
    {
        private string Domain { get; set; }
        private XmlDocument Catalog { get; set; }
        private HttpClient Client { get; set; }
        private List<NyaaModel> _nyaa;
        public List<NyaaModel> Nyaa
        {
            get { return _nyaa; }
            set { SetProperty(ref _nyaa, value); }
        }

        public NyaaViewModel()
        {
            Domain = "https://nyaa.si/?page=rss";
            Client = new HttpClient();
            Catalog = new XmlDocument();
            Nyaa = new List<NyaaModel>();
        }

        private void Prepare()
        {
            if (Nyaa.Count > 0)
            {
                Nyaa.Clear();
            }

            foreach (XmlNode xmlNode in Catalog.DocumentElement.SelectNodes("/rss/channel/item"))
            {
                Nyaa.Add(new NyaaModel()
                {
                    Title = xmlNode.SelectSingleNode("title").InnerText,
                    Link = xmlNode.SelectSingleNode("link").InnerText,
                    Guid = xmlNode.SelectSingleNode("guid").InnerText,
                    PubDate = DateTime.Parse(xmlNode.SelectSingleNode("pubDate").InnerText),
                    Seeders = (xmlNode.SelectSingleNode("seeders") != null ? Int16.Parse(xmlNode.SelectSingleNode("seeders").InnerText) : -1),
                    Leechers = (xmlNode.SelectSingleNode("leechers") != null ? Int16.Parse(xmlNode.SelectSingleNode("leechers").InnerText) : -1),
                    Downloads = (xmlNode.SelectSingleNode("downloads") != null ? Int16.Parse(xmlNode.SelectSingleNode("downloads").InnerText) : -1),
                });
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

        public async void Search(string Series)
        {
            await Download($"{Domain}&q={Series}");

            Prepare();
        }
    }
}
