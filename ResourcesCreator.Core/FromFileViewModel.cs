using System;
using FirstFloor.ModernUI.Presentation;

namespace ResourcesCreator.Core
{
    public class FromFileViewModel : NotifyPropertyChanged
    {
        private string _path;
        private string _url;
        private string _logs;
        private bool _ringIsActive;
        private bool _buttonIsEnabled;
        private static FromFileViewModel singleton;

        public static FromFileViewModel Instance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new FromFileViewModel();
                }

                return singleton;
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
                if (_path != value)
                {
                    _path = value;

                    if (!String.IsNullOrEmpty(_path))
                    {
                        ButtonIsEnabled = true;
                    }
                    else
                    {
                        ButtonIsEnabled = false;
                    }

                    OnPropertyChanged("Path");
                }
            }
        }

        public string Url
        {
            get
            {
                return this._url;
            }
            set
            {
                if (_url != value)
                {
                    _url = value;

                    if (!String.IsNullOrEmpty(_url))
                    {
                        ButtonIsEnabled = true;
                    }
                    else
                    {
                        ButtonIsEnabled = false;
                    }

                    OnPropertyChanged("Url");
                }
            }
        }

        public string Log
        {
            get { return this._logs; }
            set
            {

                _logs = value;

                OnPropertyChanged("Log");
            }
        }


        public bool RingIsActive
        {
            get
            {
                return this._ringIsActive;
            }
            set
            {
                if (_ringIsActive != value)
                {
                    _ringIsActive = value;

                    OnPropertyChanged("RingIsActive");
                }
            }
        }

        public bool ButtonIsEnabled
        {
            get
            {
                return this._buttonIsEnabled;
            }
            set
            {
                if (_buttonIsEnabled != value)
                {
                    _buttonIsEnabled = value;

                    OnPropertyChanged("ButtonIsEnabled");
                }
            }
        }
    }
}
