using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourcesCreator.Core.Model
{
    public class PreferencesModel
    {
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Key { get; set; }
        public string LastUrl { get; set; }
        public string LastWorkSheet { get; set; }
        public string ApiKey { get; set; }
        public string RegExp { get; set; }
    }
}
