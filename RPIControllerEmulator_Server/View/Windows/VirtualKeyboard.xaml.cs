using RPIControllerEmulator_Server.src;
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
using Windows.ApplicationModel.VoiceCommands;

namespace RPIControllerEmulator_Server
{
    /// <summary>
    /// Interaction logic for VirtualKeyboard.xaml
    /// </summary>
    public partial class VirtualKeyboard : Window
    {
        private Sender linkAdapter;
        public void setLinkAdapter(Sender adapter)
        {
            this.linkAdapter = adapter;
        }
        
        public VirtualKeyboard()
        {
            InitializeComponent();
        }
        
        public void KeyboardButton_Click(object sender, RoutedEventArgs e)
        {
            Button contenet = (Button)sender;
            string message = contenet.Content + "\tpressed";
            linkAdapter.SendMessage(message);
        }
    }
}
