using ResourcesCreator.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace ResourcesCreator.Core.Excel
{
    public class ResxCreator
    {
        private string[] keys;



        public async Task CreateResxAsync(Resources resources, string path, string name, Action<string> callback)
        {
            await Task.Run(() => { CreateResx(resources, path, name, callback); });
        }

        public void CreateResx(Resources resources, string path, string name, Action<string> callback)
        {
            keys = resources.Keys.ToArray();

            foreach (CultureResource culture in resources.Cultures)
            {
                string p = Path.Combine(path, name + "." + culture.CultureCode + ".resx");
                if (callback != null)
                {
                    callback(String.Format("Generate {0}", p));
                }

                GenerateResx(culture, p);
            }

            if (callback != null)
            {
                callback(String.Format("Completed. Generate {0} keys and {1} cultures.", keys.Length, resources.Cultures.Count()));
            }

        }

        private void GenerateResx(CultureResource culture, string path)
        {
            string[] values = culture.Values.ToArray();

            using (var r = new ResXResourceWriter(path))
            {
                for (int i = 0; i < keys.Length; i++)
                {
                    r.AddResource(keys[i], values[i]);
                }
            }
        }
    }
}
