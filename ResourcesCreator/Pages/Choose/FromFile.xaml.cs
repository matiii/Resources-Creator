using System;
using System.Windows;
using System.Windows.Forms;
using ResourcesCreator.Core;
using ResourcesCreator.Core.Excel;
using ResourcesCreator.Core.Model;
using ResourcesCreator.Core.Settings;
using UserControl = System.Windows.Controls.UserControl;

namespace ResourcesCreator.Pages.Choose
{
    /// <summary>
    /// Interaction logic for FromFile.xaml
    /// </summary>
    public partial class FromFile : UserControl
    {
        private readonly FromFileViewModel data = FromFileViewModel.Instance;
        private readonly LogViewModel logs = new LogViewModel();
        private readonly Messages msgs;
        private readonly SharedPreferences preferences = SharedPreferences.Instance;
        private CopyReader reader;


        public FromFile()
        {
            InitializeComponent();
            msgs = new Messages(data, logs);
            DataContext = data;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Excel files|*.xlsx";

            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                data.Path = fileDialog.FileName;
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(preferences.Model.Key))
                {
                    msgs.AddLog("ERROR. You have to define key in settings.");
                    return;
                }

                data.RingIsActive = true;

                reader = new CopyReader(data.Path, preferences.Model.Key);
                reader.Open();

                Resources res = await reader.GetValuesAsync(preferences.Model.LastWorkSheet, (msg) =>
                {
                    msgs.AddLog(msg);
                });


                if (String.IsNullOrEmpty(preferences.Model.Path) || String.IsNullOrEmpty(preferences.Model.Name))
                {
                    msgs.AddLog("ERROR. You have to define path to save and name resx resources.");
                    data.RingIsActive = false;
                    return;
                }

                var resx = new ResxCreator();

                await resx.CreateResxAsync(res, preferences.Model.Path, preferences.Model.Name, (msg) =>
                {
                    msgs.AddLog(msg);
                });

                string path = preferences.GetPath();

                preferences.SaveModel(path);
            }
            catch
            {
                msgs.AddLog("Error");
            }
            

            data.RingIsActive = false;
        }


        
    }
}
