using PolyDeckModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace PolyDeck
{
    /// <summary>
    /// Logique d'interaction pour RenameDevice.xaml
    /// </summary>
    public partial class RenameDevice : Window
    {

        private List<Device> _devices;

        private Collection<DeviceGPIO> _devicesGPIO;

        public List<Device> Devices { get => _devices; }

        public RenameDevice(Collection<DeviceGPIO> devicesGPIO)
        {
            _devicesGPIO = devicesGPIO;
            _devices = new List<Device>();
            foreach (var deviceGPIO in devicesGPIO)
            {
                if (_devices.IndexOf(deviceGPIO.Device) == -1)
                {
                    _devices.Add(deviceGPIO.Device);
                }
            }

            InitializeComponent();

            CB_Device.ItemsSource = Devices;
        }

        private void CB_Device_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TXT_DeviceName.Text = ((Device)CB_Device.SelectedItem).Name;
        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            ((Device)CB_Device.SelectedItem).Name = TXT_DeviceName.Text;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
