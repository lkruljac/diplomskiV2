using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Linearstar.Windows.RawInput;
using Model;

namespace Services.RawInput
{
    public static class RawInputWrapper
    {
        public delegate void OnDeviceEvent(RawInputData message);
        public static Thread KeyboardListenerThread(OnDeviceEvent onDeviceEventCallback, string deviceIdFilter=null)
        {
            Thread keyboardListenerThread = new Thread(() =>
            {
                var keyboard = new RawInputReceiverWindow();
                RawInputData previousMessage = null;
                keyboard.Input += (sender, e) =>
                {
                    if(deviceIdFilter != e.Data.Header.DeviceHandle.ToString())
                    {
                        return;
                    }
                    if (previousMessage?.ToString() == e.Data.ToString())
                    {
                        return;
                    }
                    previousMessage = e.Data;
                    onDeviceEventCallback(e.Data);
                };

                try
                {
                    // Register the HidUsageAndPage to watch any device.
                    RawInputDevice.RegisterDevice(HidUsageAndPage.Keyboard, RawInputDeviceFlags.ExInputSink, keyboard.Handle);
                    Application.Run();
                }
                finally
                {
                    RawInputDevice.UnregisterDevice(HidUsageAndPage.Keyboard);
                }
            });
            keyboardListenerThread.Priority = ThreadPriority.Highest;
            keyboardListenerThread.Start();
            return keyboardListenerThread;
        }

        public static Thread MouseListenerThread(OnDeviceEvent onDeviceEventCallback, string deviceIdFilter = null)
        {
            Thread mouseListenerThread = new Thread(() =>
            {
                var mouse = new RawInputReceiverWindow();
                mouse.Input += (sender, e) =>
                {
                    if (deviceIdFilter != e.Data.Header.DeviceHandle.ToString())
                    {
                        return;
                    }
                    onDeviceEventCallback(e.Data);
                };

                try
                {
                    // Register the HidUsageAndPage to watch any device.
                    RawInputDevice.RegisterDevice(HidUsageAndPage.Mouse, RawInputDeviceFlags.ExInputSink, mouse.Handle);
                    Application.Run();
                }
                finally
                {
                    RawInputDevice.UnregisterDevice(HidUsageAndPage.Mouse);
                }
            });
            mouseListenerThread.Priority = ThreadPriority.Highest;
            mouseListenerThread.Start();
            return mouseListenerThread;
        }

        public static Thread JoystickListenerThread(OnDeviceEvent onDeviceEventCallback, string deviceIdFilter = null)
        {
            Thread joystickListenerThread = new Thread(() =>
            {
                var joystick = new RawInputReceiverWindow();
                joystick.Input += (sender, e) =>
                {
                    if (deviceIdFilter != e.Data.Header.DeviceHandle.ToString())
                    {
                        return;
                    }
                    onDeviceEventCallback(e.Data);
                };

                try
                {
                    // Register the HidUsageAndPage to watch any device.
                    RawInputDevice.RegisterDevice(HidUsageAndPage.Joystick, RawInputDeviceFlags.ExInputSink, joystick.Handle);
                    Application.Run();
                }
                finally
                {
                    RawInputDevice.UnregisterDevice(HidUsageAndPage.Joystick);
                }
            });
            joystickListenerThread.Priority = ThreadPriority.Highest;
            joystickListenerThread.Start();
            return joystickListenerThread;
        }


        public static List<DeviceModel> GetAllDevices()
        {
            List<DeviceModel> devices = new List<DeviceModel>();

            var result = RawInputDevice.GetDevices();

            foreach (var item in result)
            {
                var device = new DeviceModel();
                device.IsSelected = false;
                try
                {
                    device.Name = $"{item.ProductName}({item.ManufacturerName})";
                }
                catch
                {
                    continue;
                }
                device.Type = item.DeviceType.ToString();
                device.Id = item.Handle.ToString();
                devices.Add(device);
            }
            //devices = devices.GroupBy(x => x.Id).Select(x => x.First()).ToList();
            return devices;
        }

    }
}
