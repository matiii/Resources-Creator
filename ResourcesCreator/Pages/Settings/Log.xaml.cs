using System.Windows.Controls;
using ResourcesCreator.Core;

namespace ResourcesCreator.Pages.Settings
{
    /// <summary>
    /// Interaction logic for Log.xaml
    /// </summary>
    public partial class Log : UserControl
    {

        private readonly LogViewModel logs = new LogViewModel();

        public Log()
        {
            InitializeComponent();
            LogsDataGrid.ItemsSource = logs.Collection;
        }
    }
}
