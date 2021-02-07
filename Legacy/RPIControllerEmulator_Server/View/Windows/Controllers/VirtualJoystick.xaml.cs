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

namespace RPIControllerEmulator_Server.View.Windows.Controllers
{
    /// <summary>
    /// Interaction logic for VirtualJoystick.xaml
    /// </summary>
    public partial class VirtualJoystick : Window
    {
        Controller controller;

        public VirtualJoystick(ViewModel.Controllers.Joystick joystick)
        {
            InitializeComponent();
            this.controller = joystick;
        }

    
        private void JoystickButton_Click(object sender, RoutedEventArgs e)
        {
            Button contenet = (Button)sender;
            string message = contenet.Name.ToString();
            controller.Button_Click("From joystick: " + message);
        }
    }
}
