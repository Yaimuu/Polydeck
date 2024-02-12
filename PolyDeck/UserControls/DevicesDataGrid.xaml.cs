using PolyDeckModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Printing;
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

namespace PolyDeck.UserControls
{
    /// <summary>
    /// Logique d'interaction pour DevicesDataGrid.xaml
    /// </summary>
    public partial class DevicesDataGrid : UserControl 
    { 

        private ObservableCollection<DeviceGPIO> _deviceGPIOs;

        public ObservableCollection<DeviceGPIO> DeviceGPIOs { get => _deviceGPIOs; }


        public DevicesDataGrid()
        {
            InitializeComponent();

            Device device1 = new Device();
            //device1.MACAddress = "00:00:00:00:00:00";
            //device1.Name = "Device 1";
            //Device device2 = new Device();
            //device2.MACAddress = "00:00:00:00:00:00";
            //device2.Name = "Device 2";
            //_deviceGPIOs = new ObservableCollection<DeviceGPIO>();

            //ActionPC actionPC = new ActionPC();
            //actionPC.AddShortcut(PolyDeckModel.KeyCode.Backspace);
            //DeviceGPIO deviceGPIO = new DeviceGPIO(1, actionPC, "logo1");
            //device1.addDeviceGPIO(deviceGPIO);
            //DeviceGPIOs.Add(deviceGPIO);

            //actionPC = new ActionPC();
            //actionPC.AddShortcut(PolyDeckModel.KeyCode.Esc);
            //actionPC.AddShortcut(PolyDeckModel.KeyCode.F7);
            //deviceGPIO = new DeviceGPIO(2, actionPC, "logo2");
            //device2.addDeviceGPIO(deviceGPIO);
            //DeviceGPIOs.Add(deviceGPIO);

            //actionPC = new ActionPC();
            //actionPC.AddShortcut(PolyDeckModel.KeyCode.Esc);
            //deviceGPIO = new DeviceGPIO(3, actionPC, "logo3");
            //device2.addDeviceGPIO(deviceGPIO);
            //DeviceGPIOs.Add(deviceGPIO);

            GetDevicesGPIO();


        }

        private async void GetDevicesGPIO()
        {
            _deviceGPIOs = await DeviceService.Instance.GetDevicesGPIOsAsync();
            DataGrid.ItemsSource = _deviceGPIOs;
            ICollectionView cvDevicesGPIO = CollectionViewSource.GetDefaultView(DataGrid.ItemsSource);
            if (cvDevicesGPIO != null && cvDevicesGPIO.CanGroup == true)
            {
                cvDevicesGPIO.GroupDescriptions.Clear();
                cvDevicesGPIO.GroupDescriptions.Add(new PropertyGroupDescription("Device"));
            }
        }

        private async void SaveDevices()
        {
            List<Device> devices = new List<Device>();
            List<DeviceGPIO> deviceGPIOs = this.DeviceGPIOs.ToList();
            foreach (DeviceGPIO deviceGPIO in deviceGPIOs)
            {
                if (devices.IndexOf(deviceGPIO.Device) == -1)
                {
                    devices.Add(deviceGPIO.Device);
                }
            }
            await DeviceService.Instance.UpdateDevicesAsync(devices);
        }


        private void btn_AddGPIO_Click(object sender, RoutedEventArgs e)
        {
            AddGPIO addGPIOWindow = new AddGPIO(DeviceGPIOs);
            addGPIOWindow.ShowDialog();
        }

        private void DataGrid_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            if (e.Column.SortMemberPath == "ShortcutsString")
            {
                new EditShortcuts((DeviceGPIO)DataGrid.SelectedItem).ShowDialog();
            }
        }

        private void BTN_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveDevices();
            MessageBox.Show("Modification saved with success !", "Save finished", MessageBoxButton.OK, MessageBoxImage.Question);
        }

        private void btn_RenameDevice_Click(object sender, RoutedEventArgs e)
        {
            new RenameDevice(DeviceGPIOs).ShowDialog();
            DataGrid.Items.Refresh();
        }
    }

    public class GroupNameConverter : IValueConverter
    {
        public object Convert(object value,
        Type targetType, object parameter, CultureInfo culture)
        {
            Device device = (Device)value;
            if (device.Name is null || device.MACAddress is null)
            {
                return "";
            }
            return string.Format("Device name : {0} - MAC Address : {1} - Number of GPIOs : ", device.Name, device.MACAddress);
        }

        public object ConvertBack(object value,
        Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
