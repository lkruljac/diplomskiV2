﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RPIControllerEmulator_Server.ViewModel.Controllers;
using RPIControllerEmulator_Server.ViewModel.Connectors;
using RPIControllerEmulator_Server.Model;
using RPIControllerEmulator_Server.Model.Enumerations;
using RPIControllerEmulator_Server.View.Windows;

namespace RPIControllerEmulator_Server.ViewModel
{
    public class Main
    {

        Controller controller;
        Connection connection;


        public bool Connect(ConnectionTypes connectionType)
        {

            switch (connectionType)
            {
                case ConnectionTypes.Bluetooth:
                    {
                        BluetoothLinkAdapter adapter = new BluetoothLinkAdapter();
                        BluetoothConfigurationWindow configWindow = new BluetoothConfigurationWindow(adapter);
                        configWindow.ShowDialog();

                        break;
                    }

                case ConnectionTypes.Network:
                    {
                        NetworkConfigurationWindow window = new NetworkConfigurationWindow();
                        window.ShowDialog();
                        string ip = window.getIP();
                        int port = window.getPort();
                        NetworkLinkAdapter adapter = new NetworkLinkAdapter();
                        adapter.Connect(ip, port);
                        connection = adapter;
                        return true;
                    }

                case ConnectionTypes.UART:
                    {

                        break;
                    }

                default:
                    {
                        break;
                    }
            }
            return false;
        }


        public void ShowController(ControllerTypes controllerType)
        {
            switch (controllerType)
            {
                case ControllerTypes.Keyboard:
                    {
                        controller = new ViewModel.Controllers.Keyboard();
                        break;
                    }

                case ControllerTypes.Joystick:
                    {
                        controller = new Joystick();
                        break;
                    }

                case ControllerTypes.Other:
                    {

                        break;
                    }

                default:
                    {
                        break;
                    }
            }
            
            controller.SetConnection(connection);
            controller.window.Show();
        }
    }
}
