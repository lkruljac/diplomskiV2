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
using InTheHand.Net.Bluetooth;
using InTheHand.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Net;
using Windows.Networking.Connectivity;
using RPIControllerEmulator_Server.src;

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
            networkRadioButton.IsChecked = true;
            keyboard_RadioButton.IsChecked = true;
        }

        private NetworkLinkAdapter networkLinkAdapter = null;

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (uartRadioButton.IsChecked == true)
            {
            }
            else if (bluetootRadioButton.IsChecked == true)
            {
  
                ConnectBluetooth();
            }
            else if (networkRadioButton.IsChecked == true)
            {
                ConnectNetwork();
            }
            else
            {
                MessageBox.Show("Connection type is not selected", "Unselected option", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        
        private void ConnectNetwork()
        {
            NetworkConfigurationWindow networkConfigurationWindow = new NetworkConfigurationWindow();
            networkConfigurationWindow.ShowDialog();
            this.networkLinkAdapter = new NetworkLinkAdapter();
            this.networkLinkAdapter.Connect(networkConfigurationWindow.getIP(), networkConfigurationWindow.getPort());
            if (networkLinkAdapter.getStatus() == "Connected")
            {
                connectionStatusLabel.Background = new SolidColorBrush(Color.FromRgb(100, 150, 30));
                connectionStatusLabel.Content = "Connection status: Connected on ip " + networkConfigurationWindow.getIP();
                connectionStatusLabel.Content += ", on port " + networkConfigurationWindow.getPort();
            }
        }

        private void ConnectBluetooth()
        {

        }

        private void ShowControllerButton_Click(object sender, RoutedEventArgs e)
        {
            if (keyboard_RadioButton.IsChecked == true)
            {

                VirtualKeyboard virtualKeyboard = new VirtualKeyboard();
                virtualKeyboard.setLinkAdapter(networkLinkAdapter);
                virtualKeyboard.Show();
            }
            else if (joystic_RadioButton.IsChecked == true)
            {

            }
            else if (remote_RadioButton.IsChecked == true)
            {

            }
            else if (custom_RadioButton.IsChecked == true)
            {
                
            }
            else
            {
                MessageBox.Show("Controller type is not selected", "Unselected option", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}