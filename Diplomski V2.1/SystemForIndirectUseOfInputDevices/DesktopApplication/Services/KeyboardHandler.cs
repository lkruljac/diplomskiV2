using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linearstar.Windows.RawInput;
using Services.RawInput;
namespace Services
{
    public class KeyboardHandler
    {
        public static void OnKeyboardEvent(RawInputData data)
        {
            Console.WriteLine(data);
        }
    }
}
