using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makima.Models
{
    public class DatabaseModel: BaseModel
    {
        public IEnumerable<SeriesModel> Series { get; set; }
    }
}
