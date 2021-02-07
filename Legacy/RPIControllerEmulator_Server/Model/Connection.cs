using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

namespace RPIControllerEmulator_Server.Model
{
    public class Connection
    {
        public virtual void SendMessage(string message)
        {
            MessageBox.Show("It works, sended " + message);
        }
        
        

    }
}
