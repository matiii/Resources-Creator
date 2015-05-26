using FirstFloor.ModernUI.Presentation;
using ResourcesCreator.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourcesCreator.Core
{
    public class GoogleSettingViewModel : NotifyPropertyChanged
    {
        private string _clientID;
        private string _clientSecret;
        private string _apiKey;

        private readonly SharedPreferences preferences = SharedPreferences.Instance;

        public string ClientID {
            get {
                return _clientID;
            }
            set
            {
                if (_clientID != value)
                {
                    _clientID = value;
                    preferences.SetClientId(value);

                    OnPropertyChanged("ClientID");
                }
            }
        }

        public string ClientSecret
        {
            get
            {
                return _clientSecret;
            }
            set
            {
                if (_clientSecret != value)
                {
                    _clientSecret = value;
                    preferences.SetClientSecret(value);

                    OnPropertyChanged("ClientSecret");
                }
            }
        }

        public string ApiKey
        {
            get
            {
                return _apiKey;
            }
            set
            {
                if (_apiKey != value)
                {
                    _apiKey = value;
                    preferences.SetApiKey(value);

                    OnPropertyChanged("ApiKey");
                }
            }
        }
    }
}
