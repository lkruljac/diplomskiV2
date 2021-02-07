using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
using System.Threading;

using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;


using RPIControllerEmulator_Server.ViewModel.Connectors;

namespace RPIControllerEmulator_Server.View.Windows
{
    /// <summary>
    /// Interaction logic for BluetoothConfigurationWindow.xaml
    /// </summary>
    public partial class BluetoothConfigurationWindow : Window
    {
        private BluetoothLinkAdapter adapter;

        public BluetoothConfigurationWindow(BluetoothLinkAdapter BluetoothAdapter)
        {
            InitializeComponent();
            this.adapter = BluetoothAdapter;
            BluetoothDevices_List.MouseDoubleClick += new MouseButtonEventHandler(BluetoothDevices_List_DoubleClick);
        }
           
        private void ListBTDevices_Button_Click(object sender, RoutedEventArgs e)
        {
            Info_Label.Content = "Seaching...";
            adapter.SearchDevices();

            Thread waitThread = new Thread(() => t1(this));
            waitThread.Start();
        }
        public static void t1(BluetoothConfigurationWindow window)
        {
            window.Dispatcher.Invoke(() =>
            {
                window.BluetoothDevices_List.Items.Clear();
            });
            while (window.adapter.IsSearching)
            {
                Thread.Sleep(500);
                window.Dispatcher.Invoke(() =>
                {
                    window.Info_Label.Content += ".";
                });
            }
            window.Dispatcher.Invoke(() =>
            {
                foreach(BluetoothDeviceInfo device in window.adapter.DiscoveredDeviceList)
                {
                    window.BluetoothDevices_List.Items.Add(device.DeviceName + "\t\t\t" + device.DeviceAddress + "\t\t\t" + device.Connected);
                }
                window.Info_Label.Content = "Found devices: " + window.adapter.DiscoveredDeviceList.Count();
            });
          
        }

        private void BluetoothDevices_List_DoubleClick(object sender, RoutedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            BluetoothDeviceInfo targetDevice = adapter.DiscoveredDeviceList.ElementAt(listBox.SelectedIndex);
            adapter.Pair(targetDevice);
        }

        private void Pair_Button_Click(object sender, RoutedEventArgs e)
        {
            BluetoothDeviceInfo targetDevice = adapter.DiscoveredDeviceList.ElementAt(BluetoothDevices_List.SelectedIndex);
            adapter.Pair(targetDevice);
        }

        private void Connect_Button_Click(object sender, RoutedEventArgs e)
        {
            BluetoothDeviceInfo targetDevice = adapter.DiscoveredDeviceList.ElementAt(BluetoothDevices_List.SelectedIndex);
            adapter.Connect(targetDevice);
        }


    }
}
