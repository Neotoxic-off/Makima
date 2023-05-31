using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makima.Models
{
    public class FilterModel: BaseModel
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public FilterModel(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
