using FirstFloor.ModernUI.Windows.Controls;
using ResourcesCreator.Core.Excel;
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

namespace ResourcesCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        private readonly SharedPreferences preferences;

        public MainWindow()
        {
            InitializeComponent();
            preferences = SharedPreferences.Instance;
        }

        private void ModernWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            preferences.SaveModel();
        }
    }
}
