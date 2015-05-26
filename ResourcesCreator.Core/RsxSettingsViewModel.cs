using FirstFloor.ModernUI.Presentation;
using ResourcesCreator.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourcesCreator.Core
{
    public class RsxSettingsViewModel : NotifyPropertyChanged
    {
        private string _name;
        private string _path;
        private string _key;
        private string _workSheet;
        private string _regexp;

        private readonly SharedPreferences preferences = SharedPreferences.Instance;


        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if (this._name != value)
                {
                    this._name = value;
                    preferences.SetName(value);
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Path
        {
            get
            {
                return this._path;
            }
            set
            {
                if (this._path != value)
                {
                    this._path = value;
                    preferences.SetPath(value);
                    OnPropertyChanged("Path");
                }
            }
        }


        public string Key
        {
            get
            {
                return this._key;
            }
            set
            {
                if (this._key != value)
                {
                    this._key = value;
                    preferences.SetKey(value);

                    OnPropertyChanged("Key");
                }
            }
        }

        public string WorkSheet
        {
            get
            {
                return this._workSheet;
            }
            set
            {
                if (this._workSheet != value)
                {
                    this._workSheet = value;
                    preferences.SetLastWorkSheet(value);

                    OnPropertyChanged("WorkSheet");
                }
            }
        }

        public string RegExp
        {
            get
            {
                return this._regexp;
            }
            set
            {
                if (this._regexp != value)
                {
                    this._regexp = value;
                    preferences.SetRegExp(value);

                    OnPropertyChanged("RegExp");
                }
            }
        }

    }
}
