using MahApps.Metro.Controls;
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
using PolyDeck.UserControls;
using System.Net.Http;
using System.IO;
using PolyDeckModel.Model;
using System.Net.Http.Json;

namespace PolyDeck
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
            UserControlWindow = new Home(this);
            ContentWindow.Content = UserControlWindow;
        }


        private UserControl _userControlWindow;

        public UserControl UserControlWindow
        {
            get { return _userControlWindow; } 
            set { 
                _userControlWindow = value; 
                ContentWindow.Content = _userControlWindow;
            }
        }

    }
}
