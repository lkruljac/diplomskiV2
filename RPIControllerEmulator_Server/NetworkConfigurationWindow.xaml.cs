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
using System.Windows.Shapes;

namespace RPIControllerEmulator_Server
{
    /// <summary>
    /// Interaction logic for NetworkConfigurationWindow.xaml
    /// </summary>
    public partial class NetworkConfigurationWindow : Window
    {
        private int port = 54000;
        private string ip = "127.0.0.0";
        public NetworkConfigurationWindow()
        {
            InitializeComponent();
        }

        private void ConnectButton(object sender, RoutedEventArgs e)
        {
            int.TryParse(portTextBox.Text, out port);
            ip = ipTextBox.Text;
            this.Close();
        }

        public string getIP()
        {
            return ip;
        }
        public int getPort()
        {
            return port;
        }
    }
}
