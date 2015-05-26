using ResourcesCreator.Core;
using ResourcesCreator.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ResourcesCreator.Pages.Settings
{
    /// <summary>
    /// Interaction logic for GoogleSetting.xaml
    /// </summary>
    public partial class GoogleSetting : UserControl
    {
        private readonly GoogleSettingViewModel googleSettingViewModel;
        private readonly SharedPreferences preferences;


        public GoogleSetting()
        {
            InitializeComponent();
            googleSettingViewModel = new GoogleSettingViewModel();
            preferences = SharedPreferences.Instance;


            InitializeSettings();

            DataContext = googleSettingViewModel;
        }

        private void InitializeSettings()
        {
            googleSettingViewModel.ClientID = preferences.Model.ClientID;
            googleSettingViewModel.ClientSecret = preferences.Model.ClientSecret;
            googleSettingViewModel.ApiKey = preferences.Model.ApiKey;
        }
    }
}
