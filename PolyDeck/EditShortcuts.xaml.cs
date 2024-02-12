using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using PolyDeckModel;
using PolyDeckModel.Model;

namespace PolyDeck
{
    /// <summary>
    /// Logique d'interaction pour EditShortcuts.xaml
    /// </summary>
    public partial class EditShortcuts : Window
    {

        private List<KeyCodeCheckBox> _keyCodeCheckBoxes;

        private DeviceGPIO _deviceGPIO;

        public EditShortcuts(DeviceGPIO deviceGPIO)
        {

            _deviceGPIO = deviceGPIO;

            InitializeComponent();

            _keyCodeCheckBoxes = new List<KeyCodeCheckBox>();
            foreach (var keyCode in Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().ToList())
            {
                if (deviceGPIO.Action.Shortcuts.IndexOf(keyCode) == -1)
                {
                    _keyCodeCheckBoxes.Add(new KeyCodeCheckBox(keyCode));
                }
                else
                {
                    _keyCodeCheckBoxes.Add(new KeyCodeCheckBox(keyCode, true));
                }
            }

            ComboBox.ItemsSource = _keyCodeCheckBoxes;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox.SelectedIndex = -1;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            List<KeyCode> keyCodes = new List<KeyCode>();
            foreach (var keyCodeCheckBox in _keyCodeCheckBoxes)
            {
                if (keyCodeCheckBox.IsChecked)
                {
                    keyCodes.Add(keyCodeCheckBox.KeyCode);
                }
            }
            _deviceGPIO.Action.Shortcuts = keyCodes;
            this.Close();
        }
    }
}
