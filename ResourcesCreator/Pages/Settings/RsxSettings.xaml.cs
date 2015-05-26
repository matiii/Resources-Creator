using System;
using System.Windows;
using System.Windows.Forms;
using ResourcesCreator.Core;
using ResourcesCreator.Core.Settings;
using UserControl = System.Windows.Controls.UserControl;

namespace ResourcesCreator.Pages.Settings
{
    /// <summary>
    /// Interaction logic for RsxSettings.xaml
    /// </summary>
    public partial class RsxSettings : UserControl
    {
        private readonly RsxSettingsViewModel settingsVM;
        private readonly SharedPreferences preferences;

        public RsxSettings()
        {
            InitializeComponent();
            settingsVM  = new RsxSettingsViewModel();
            preferences = SharedPreferences.Instance;

            InitializeSettings();

            DataContext = settingsVM;
        }

        private void InitializeSettings()
        {
            settingsVM.Name      = preferences.Model.Name;
            settingsVM.Path      = preferences.Model.Path;
            settingsVM.Key       = preferences.Model.Key;
            settingsVM.WorkSheet = preferences.Model.LastWorkSheet;

            if (String.IsNullOrEmpty(preferences.Model.RegExp))
                preferences.SetRegExp("^[a-z]{2}-[a-z]{2}$|^[a-z]{2}$");

            settingsVM.RegExp = preferences.Model.RegExp;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();

            if (result == DialogResult.OK)
            {
                string path = folderBrowser.SelectedPath;

                if (!String.IsNullOrEmpty(path))
                {
                    settingsVM.Path = path;
                }
            }


        }

        /// <summary>
        /// Load model from configuration file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Hyper files|*.hyper";

            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string route = fileDialog.FileName;
                preferences.GetNewModel(route);

                InitializeSettings();
                FromFileViewModel.Instance.Url = preferences.Model.LastUrl;
            }
        }
    }
}
