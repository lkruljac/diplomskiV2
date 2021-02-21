using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

using Model;
using System.Collections.ObjectModel;

namespace Services
{
    public static class Devices
    {
        
        public static ObservableCollection<DeviceModel> GetAllDevices()
        {
            ObservableCollection<DeviceModel> devices = new ObservableCollection<DeviceModel>
            {
                new DeviceModel(null, null, "Keyboard"),
                new DeviceModel(null, null, "Mouse"),
                new DeviceModel(null, null, "Joystick")
            };

            return devices;
        }       
    }
}
