﻿using System;
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
                NetworkConfigurationWindow networkConfigurationWindow = new NetworkConfigurationWindow();
                networkConfigurationWindow.ShowDialog();

                this.networkLinkAdapter = new NetworkLinkAdapter();
                this.networkLinkAdapter.Connect(networkConfigurationWindow.getIP(), networkConfigurationWindow.getPort());
                
                if(networkLinkAdapter.getStatus() == "Connected")
                {
                    connectionStatusLabel.Content = "Connection status: Connected on ip " + networkConfigurationWindow.getIP();
                    connectionStatusLabel.Content += ", on port " + networkConfigurationWindow.getPort();
                }

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

        private void ShowControllerButton_Click(object sender, RoutedEventArgs e)
        {
            this.networkLinkAdapter.SendMessage("Bla");
        }
    }
}