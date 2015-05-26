using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ResourcesCreator.Core;
using ResourcesCreator.Core.Excel;
using ResourcesCreator.Core.Google;
using ResourcesCreator.Core.Model;
using ResourcesCreator.Core.Settings;

namespace ResourcesCreator.Pages.Choose
{
    /// <summary>
    /// Interaction logic for FromUrl.xaml
    /// </summary>
    public partial class FromUrl : UserControl
    {
        private readonly FromFileViewModel data = FromFileViewModel.Instance;
        private readonly LogViewModel logs = new LogViewModel();
        private readonly Messages msgs;
        private readonly SharedPreferences preferences = SharedPreferences.Instance;
        private readonly GoogleDriveService driveService;
        private CopyReader reader;

        private const string url = "https://docs.google.com/feeds/download/spreadsheets/Export?key={0}&exportFormat=xlsx";


        public FromUrl()
        {
            InitializeComponent();
            driveService = new GoogleDriveService(preferences.Model.ClientID, preferences.Model.ClientSecret, preferences.Model.ApiKey);
            msgs = new Messages(data, logs);
            data.Url = preferences.Model.LastUrl;
            
            DataContext = data;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            data.RingIsActive = true;

            try
            {
                if (String.IsNullOrEmpty(data.Url))
                {
                    msgs.AddLog("ERROR. You need to define document id.");
                    return;
                }
                else
                {
                    msgs.AddLog("Try authorize.");
                    string downloadUrl = String.Format(url, data.Url);
                    Stream s = await driveService.DownloadFile(downloadUrl, msg => msgs.AddLog(msg));
                    msgs.AddLog("Download file completed.");

                    reader = new CopyReader("", preferences.Model.Key);
                    reader.Open(s);

                    Resources res = await reader.GetValuesAsync(preferences.Model.LastWorkSheet, (msg) => {
                        msgs.AddLog(msg);
                    });


                    if (String.IsNullOrEmpty(preferences.Model.Path) || String.IsNullOrEmpty(preferences.Model.Name))
                    {
                        msgs.AddLog("ERROR. You have to define path to save and name resx resources.");
                    }

                    var resx = new ResxCreator();

                    await resx.CreateResxAsync(res, preferences.Model.Path, preferences.Model.Name, (msg) =>
                    {
                        msgs.AddLog(msg);
                    });

                    preferences.SetLastUrl(data.Url);

                    string path = preferences.GetPath();

                    preferences.SaveModel(path);
                }
            }
            catch (Exception)
            {
                msgs.AddLog("Error. Try again.");
            }

            data.RingIsActive = false;
        }

        
    }
}
