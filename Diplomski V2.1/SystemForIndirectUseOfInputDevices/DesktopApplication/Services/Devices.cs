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
                new DeviceModel("All keyboards", null, null),
                new DeviceModel("All mouses", null, null),
                new DeviceModel("All joystick", null, null)
            };

            return devices;
        }       
    }
}
