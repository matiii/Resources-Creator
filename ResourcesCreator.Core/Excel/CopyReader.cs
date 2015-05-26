using Excel;
using ResourcesCreator.Core.Model;
using ResourcesCreator.Core.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ResourcesCreator.Core.Excel
{
    public class CopyReader: IDisposable
    {
        private string path;
        private string key;

        private IDictionary<int, string> cultures;
        private int? columnNumberKey;

        private IExcelDataReader reader;
        private readonly SharedPreferences preferences = SharedPreferences.Instance;


        public CopyReader(string path, string key)
        {
            this.path = path;
            this.key = key;
        }


        public bool Open()
        {
            if (File.Exists(path))
            {
                FileStream excel = File.Open(path, FileMode.Open, FileAccess.Read);

                reader = ExcelReaderFactory.CreateOpenXmlReader(excel);

                return true;
            }

            return false;
        }


        public bool Open(Stream stream)
        {
            try
            {
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Resources GetValues(string workSheet, Action<string> callback)
        {
            var r = new Resources();
            cultures = new Dictionary<int, string>();

            if (callback != null)
            {
                callback("START find the key in document.");
            }

            FindColumnNumberKeyAndCultures(workSheet, callback);

            if (!columnNumberKey.HasValue)
            {
                return null;
            }

            ReadCultures(r);

            return r;

        }


        public async Task<Resources> GetValuesAsync(string workSheet, Action<string> callback)
        {
            return await Task.Run(() => { return GetValues(workSheet, callback); });
        }

        private void ReadCultures(Resources r)
        {
            var keys = new List<string>();
            IEnumerable<CultureResource> res = GenerateCultures();

            while (reader.Read())
            {
                string key = reader.GetString(columnNumberKey.Value);

                if (!String.IsNullOrEmpty(key))
                {
                    keys.Add(key);
                    foreach (var item in cultures)
                    {
                        CultureResource c = res.First(f => f.CultureCode == item.Value);
                        c.Values.Add(reader.GetString(item.Key));
                    }
                }
                
            }

            r.Keys = keys;
            r.Cultures = res;
        }

        private void FindColumnNumberKeyAndCultures(string workSheet, Action<string> callback)
        {
            bool findKey = false;

            if (!String.IsNullOrEmpty(workSheet))
            {

                if (callback != null)
                {
                    callback("Start search worksheet.");

                    if (String.IsNullOrEmpty(reader.Name))
                    {
                        callback("Error, something wrong with excel file.");
                    }
                }

                do
                {
                    if (reader.Name.Equals(workSheet, StringComparison.InvariantCultureIgnoreCase))
                    {
                        break;
                    }
                } while (reader.NextResult());
                
            }

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (!findKey)
                    {
                        if (key.Equals(reader.GetString(i), StringComparison.InvariantCultureIgnoreCase))
                        {
                            columnNumberKey = i;
                            findKey = true;

                            if (callback !=null)
                            {
                                callback(String.Format("Find the key in {0} row.", i));
                            }

                            continue;
                        }
                    }
                    else
                    {
                        string value = reader.GetString(i);

                        if (!String.IsNullOrEmpty(value))
                        {
                            var regex = new Regex(preferences.Model.RegExp, RegexOptions.IgnoreCase);
                            Match match = regex.Match(value);

                            if (match.Success)
                                cultures.Add(i, match.Value);
                        }
                        
                    }
                }

                if (findKey)
                {
                    break;
                }
            }

            if (callback != null && !findKey)
            {
                callback("Key can't be found.");
            }
        }

        private IEnumerable<CultureResource> GenerateCultures()
        {
            var cultureResource = new List<CultureResource>();

            foreach (var item in cultures)
            {
                cultureResource.Add(new CultureResource() { CultureCode = item.Value, Values = new List<string>() });   
            }

            return cultureResource;
        }

        public void Dispose()
        {
            if (reader != null)
            {
                reader.Close();
            }
        }
    }
}
