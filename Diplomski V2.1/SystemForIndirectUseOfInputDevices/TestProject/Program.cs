using HidLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestProject
{
    class Program
    {

        public static int VendorId = 0x046d;
        public static int productId = 0xc077;
        private static HidDevice _device;
        private const int ReportLength = 8;
        private static bool _attached;
        private static readonly Random Random = new Random();


        static void Main()
        {

            _device = HidDevices.Enumerate(VendorId, productId).FirstOrDefault();

            if (_device == null)
            {
                return;
            }



            _device.OpenDevice();

            _device.Inserted += DeviceAttachedHandler;
            _device.Removed += DeviceRemovedHandler;

            _device.MonitorDeviceEvents = true;

            _device.ReadReport(OnReport);



            if (_device != null)
            {
                Console.WriteLine("Gamepad found, press any key to exit.");
                Console.ReadKey();
                _device.CloseDevice();
            }
            else
            {
                Console.WriteLine("Could not find a gamepad.");
                Console.ReadKey();
            }
        }

        private static void OnReport(HidReport report)
        {
            if (_attached == false) { return; }

            foreach(int data in report.Data)
            {
                Console.Write(data + "\t");
            }
            Console.WriteLine();
            _device.ReadReport(OnReport);
        }

        private static void DeviceAttachedHandler()
        {
            _attached = true;
            Console.WriteLine("Gamepad attached.");
            _device.ReadReport(OnReport);
        }

        private static void DeviceRemovedHandler()
        {
            _attached = false;
            Console.WriteLine("Gamepad removed.");
        }
    }
}

