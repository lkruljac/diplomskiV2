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
        }

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
                NetworkConfigurationWindow networkConfigurationWindow = new NetworkConfigurationWindow();
                networkConfigurationWindow.ShowDialog();
                ConnectNetwork(networkConfigurationWindow.getIP(), networkConfigurationWindow.getPort());
            }
            else
            {
                MessageBox.Show("Connection type is not selected", "Unselected option", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
             
        private void ConnectBluetooth()
        {

        }
        private void ConnectNetwork(string server, Int32 port)
        {
            string message = "Hello";
            server = "192.168.1.113";
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer.
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                MessageBox.Show("Received: " + responseData);

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }
    }
}