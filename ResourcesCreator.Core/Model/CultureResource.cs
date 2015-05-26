using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourcesCreator.Core.Model
{
    public class CultureResource
    {
        public string CultureCode { get; set; }
        public ICollection<string> Values { get; set; }
    }
}
