using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Makima.Models
{
    public class NyaaModel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Guid { get; set; }
        public DateTime PubDate { get; set; }
        public int Seeders { get; set; }
        public int Leechers { get; set; }
        public int Downloads { get; set; }
        public string InfoHash { get; set; }
        public string CategoryId { get; set; }
        public string Category { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public Brush StatusSeeders { get; set; }
        public Brush StatusLeechers { get; set; }
    }
}
