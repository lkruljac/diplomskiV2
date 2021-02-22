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

        public Services.Connectors.Connector Connector;

        public delegate void ProcessRawInputData(RawInputData message);
        public ProcessRawInputData GUIOnDeviceEvent;

        public virtual void OnDeviceEvent(RawInputData data)
        {
            GUIOnDeviceEvent?.Invoke(data);
            Connector?.SendMessage(data.ToString());
        }
    }
}
