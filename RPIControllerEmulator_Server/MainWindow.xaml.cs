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
using DarrenLee.Bluetooth;

namespace RPIControllerEmulator_Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            string conectionType = null;

            if (uartRadioButton.IsChecked == true)
            {
                conectionType = "uart";
            }
            else if (bluetootRadioButton.IsChecked == true)
            {
                BluetoothConnect();
            }
            else if (networkRadioButton.IsChecked==true)
            {

                conectionType = "network";
            }
            else
            {
                MessageBox.Show("Connection type is not selected", "Unselected option", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Selected connection is " + conectionType);
        }

        private void BluetoothConnect()
        {
            Bluetooth_Server btServer = new Bluetooth_Server();

            btServer.Start();
            btServer.IsConnected += BtServer_IsConnected;
            btServer.DataReceived += BtServer_DataReceived;
        }

        private void BtServer_DataReceived(object sender, BluetoothServerEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtServer_IsConnected(object sender, EventArgs e)
        {
            
        }
    }
}
