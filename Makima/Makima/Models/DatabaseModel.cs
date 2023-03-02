using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makima.Models
{
    public class DatabaseModel: BaseModel
    {
        public string ID { get; set; }
        public string Path { get; set; }
        public List<SeriesModel> Series { get; set; }
    }
}
