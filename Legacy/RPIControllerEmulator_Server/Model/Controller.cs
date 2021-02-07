using RPIControllerEmulator_Server.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RPIControllerEmulator_Server.Model
{
    public class Controller
    {

        public Window window;
        public Connection connector;


        public void SetWindow(Window controlerWindow)
        {
            this.window = controlerWindow;
        }
           
        public void SetConnection(Connection connection)
        {
            this.connector = connection;
        }

        public void Button_Released(string message)
        {

        }
        
        public virtual void Button_Click(string content)
        {
            
            connector.SendMessage(content);
        }
    }
}
