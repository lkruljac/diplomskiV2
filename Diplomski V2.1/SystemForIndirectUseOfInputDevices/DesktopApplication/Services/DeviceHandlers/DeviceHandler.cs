using Linearstar.Windows.RawInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public abstract class DeviceHandler
    {
        public virtual void OnDeviceEvent(RawInputData data)
        {
            //Console.WriteLine(data);
        }
    }
}
