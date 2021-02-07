using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RPIControllerEmulator_Server.Model;
using RPIControllerEmulator_Server.View.Windows.Controllers;

namespace RPIControllerEmulator_Server.ViewModel.Controllers
{
    public class Joystick : Controller
    {
        public Joystick()
        {
            this.window = new VirtualJoystick(this);
        }
    }
}
