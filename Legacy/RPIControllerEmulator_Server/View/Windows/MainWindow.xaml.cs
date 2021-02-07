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

using RPIControllerEmulator_Server.ViewModel;
using RPIControllerEmulator_Server.Model.Enumerations;

namespace RPIControllerEmulator_Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Main main;
        
        ConnectionTypes connectionType;
        ControllerTypes controllerType;

        public MainWindow()
        {
            InitializeComponent();
            main = new Main();
        }



        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (main.Connect(connectionType))
            {
                ShowControllerButton.IsEnabled = true;
                connectionStatusLabel.Background = new SolidColorBrush(Color.FromRgb(100, 150, 30));
                connectionStatusLabel.Content = "Connection status: Connected on ip";
                connectionStatusLabel.Content += ", on port ";
            }
            
        }
        
        private void ShowControllerButton_Click(object sender, RoutedEventArgs e)
        {
            main.ShowController(controllerType);
        }

        private void ConnectionTypeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            
            if(rb.Content.ToString() == "Network")
            {
                connectionType = ConnectionTypes.Network;
            }
            else if (rb.Content.ToString() == "UART")
            {
                connectionType = ConnectionTypes.UART;
            }
            else if (rb.Content.ToString() == "Bluetooth")
            {
                connectionType = ConnectionTypes.Bluetooth;
            }
        }

        private void ControllerTypeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            if (rb.Content.ToString() == "Keyboard")
            {
                controllerType = ControllerTypes.Keyboard;
            }
            else if (rb.Content.ToString() == "Joystic")
            {
                controllerType = ControllerTypes.Joystick;
            }
            else if (rb.Content.ToString() == "Remote")
            {
                controllerType = ControllerTypes.Other;
            }
            else if (rb.Content.ToString() == "Custom")
            {
                controllerType = ControllerTypes.Other;
            }
        }


    }
}