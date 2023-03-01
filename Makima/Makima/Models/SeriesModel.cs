using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Makima.Models
{
    public class SeriesModel: BaseModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Splash { get; set; }
        public IEnumerable<SeasonModel> Seasons { get; set; }
    }
}
