using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makima.Models
{
    public class SeasonModel: BaseModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public IEnumerable<EpisodeModel> Episodes { get; set; }
        public IEnumerable<EpisodeModel> EpisodesWatched { get; set; }
    }
}
