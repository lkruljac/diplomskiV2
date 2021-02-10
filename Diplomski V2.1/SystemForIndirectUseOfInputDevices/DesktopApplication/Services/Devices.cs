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
            ObservableCollection<DeviceModel> devices = new ObservableCollection<DeviceModel>();
            ManagementObjectCollection collection;
            
            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_Keyboard"))
                collection = searcher.Get();

            foreach (ManagementObject device in collection)
            {
                var das = device.Properties;

                devices.Add(new DeviceModel(
                    (string)device.GetPropertyValue("Caption"),
                    (string)device.GetPropertyValue("PNPDeviceID"),
                    "Keyboard"
                    ));
             
            }
            collection.Dispose();

            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_PointingDevice"))
                collection = searcher.Get();

            foreach (ManagementObject device in collection)
            {
                var das = device.Properties;

                devices.Add(new DeviceModel(
                    (string)device.GetPropertyValue("Caption"),
                    (string)device.GetPropertyValue("PNPDeviceID"),
                    "PointingDevice"
                    ));

            }
            collection.Dispose();




            return devices;
        }
    }
}
