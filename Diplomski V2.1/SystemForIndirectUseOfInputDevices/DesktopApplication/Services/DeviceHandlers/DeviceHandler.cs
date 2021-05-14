using Linearstar.Windows.RawInput;
using Model.Connectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public abstract class DeviceHandler
    {

        public Connector Connector;

        public delegate void ProcessRawInputData(RawInputData message);
        public ProcessRawInputData GUIOnDeviceEvent;

        public virtual void OnDeviceEvent(RawInputData data)
        {
            GUIOnDeviceEvent?.Invoke(data);
            if (Connector == null)
            {
                return;
            }
            if (Connector.IsConnected)
            {
                Connector?.SendMessage(MessageGenerator.GenerateByteArray(data));
            }

        }
    }
}