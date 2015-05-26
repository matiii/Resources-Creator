using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ResourcesCreator.Core.Model;

namespace ResourcesCreator.Core.Settings
{
    public class SharedPreferences
    {
        private static SharedPreferences instance;
        private const string path = "setting.hyper";
        private PreferencesModel model;

        public SharedPreferences() { }

        public static SharedPreferences Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SharedPreferences();
                }

                return instance;
            }
        }


        public PreferencesModel GetModel(string route = "")
        {
            if (model == null)
            {
                if (String.IsNullOrEmpty(route))
                    route = path;

                GetNewModel(route);
            }

            return model;
        }



        public PreferencesModel GetNewModel(string route)
        {
            if (File.Exists(route))
            {
                try
                {
                    using (var s = new StreamReader(route))
                    {
                        string json = s.ReadToEnd();
                        model = JsonConvert.DeserializeObject<PreferencesModel>(json);
                    }
                }
                catch
                {
                    InitializeModel();
                }
            }
            else
            {
                InitializeModel();
            }

            return model;
        }

        /// <summary>
        /// 
        /// Save shared preferences
        /// 
        /// </summary>
        public void SaveModel(string route = "")
        {
            if (this.model == null)
            {
                return;
            }

            try
            {
                string json = JsonConvert.SerializeObject(model);

                if (!String.IsNullOrEmpty(route) && route.Last() != '\\')
                {
                    route += '\\';
                }

                using (var file = new StreamWriter(route + path))
                {
                    file.Write(json);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Get path to settings
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            string[] elements = Model.Path.Split('\\');
            return elements.Take(elements.Length - 1).Aggregate((a, b) => a + "\\" + b);
        }

        public async Task<PreferencesModel> GetModelAsync(string route = "")
        {
            return await Task.Run(() => { return GetModel(route); });
        }

        public PreferencesModel Model
        {
            get
            {
                return GetModel();
            }
        }


        public void SetName(string name)
        {
            InitializeModel();

            model.Name = name;
        }


        public void SetRegExp(string regexp)
        {
            InitializeModel();

            model.RegExp = regexp;
        }

        public void SetPath(string path)
        {
            InitializeModel();

            model.Path = path;
        }

        public void SetClientId(string clientId)
        {
            InitializeModel();

            model.ClientID = clientId;
        }

        public void SetClientSecret(string clientSecret)
        {
            InitializeModel();

            model.ClientSecret = clientSecret;
        }

        public void SetKey(string key)
        {
            InitializeModel();

            model.Key = key;
        }

        public void SetLastUrl(string lastUrl)
        {
            InitializeModel();

            model.LastUrl = lastUrl;
        }

        public void SetLastWorkSheet(string lastWorkSheet)
        {
            InitializeModel();

            model.LastWorkSheet = lastWorkSheet;
        }


        public void SetApiKey(string apiKey)
        {
            InitializeModel();

            model.ApiKey = apiKey;
        }

        private void InitializeModel()
        {
            if (model == null)
            {
                model = new PreferencesModel();
            }
        }
    }
}
