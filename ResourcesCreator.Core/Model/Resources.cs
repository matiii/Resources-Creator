using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourcesCreator.Core.Model
{
    public class Resources
    {
        public IEnumerable<string> Keys { get; set; }
        public IEnumerable<CultureResource> Cultures { get; set; }
    }
}
