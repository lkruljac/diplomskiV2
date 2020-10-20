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

using RPIControllerEmulator_Server.Model;
using RPIControllerEmulator_Server.ViewModel.Controllers;

namespace RPIControllerEmulator_Server
{
    /// <summary>
    /// Interaction logic for VirtualKeyboard.xaml
    /// </summary>
    public partial class VirtualKeyboard : Window
    {

        Controller controller;

        public VirtualKeyboard(ViewModel.Controllers.Keyboard keyboard)
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(VirtualKeyboard_KeyDown);
            this.controller = keyboard; 
        }


        public void KeyboardButton_Click(object sender, RoutedEventArgs e)
        {
            Button contenet = (Button)sender;
            string message = contenet.Content.ToString();
            controller.Button_Click("From keyboard: " + message);
        }

        public void VirtualKeyboard_KeyDown(object sender, KeyEventArgs e)
        {
            if (!this.IsActive)
            {
                return;
            }
            string message = e.Key.ToString();
            controller.Button_Click("From keyboard: " + message);
        }
    }
}
