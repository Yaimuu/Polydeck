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
using WinFormsApp1;

namespace PolyDeck.UserControls
{
    /// <summary>
    /// Logique d'interaction pour Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {

        private MainWindow _mainWindow;

        public Home(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            ShowNbDevices();
            ShowNbGPIO();
        }

        private void TileDevices_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.UserControlWindow = new Layout(1, _mainWindow);
        }


        private async void ShowNbDevices()
        {
            int nbDevices = await DeviceService.Instance.CountDevices();
            TILE_NbDevices.SetCurrentValue(MahApps.Metro.Controls.Tile.CountProperty, nbDevices.ToString());
        }

        private async void ShowNbGPIO()
        {
            int nbGPIO = await DeviceService.Instance.CountDevicesGPIO();
            TILE_NbGPIO.SetCurrentValue(MahApps.Metro.Controls.Tile.CountProperty, nbGPIO.ToString());
        }

        private void TILE_Exit_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Close();
        }

        private void TILE_Shortcuts_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.UserControlWindow = new Layout(2, _mainWindow);
        }

        private void TILE_About_Click(object sender, RoutedEventArgs e)
        {
            new AboutWindow().ShowDialog();
        }
    }
}
