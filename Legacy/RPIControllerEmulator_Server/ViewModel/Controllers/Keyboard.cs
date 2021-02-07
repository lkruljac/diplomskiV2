using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RPIControllerEmulator_Server.Model;

namespace RPIControllerEmulator_Server.ViewModel.Controllers
{
    public class Keyboard : Controller
    {
        public Keyboard(){
            this.window = new VirtualKeyboard(this);
        }

       
    }
}
