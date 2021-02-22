using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Linearstar.Windows.RawInput;

namespace Services.RawInput
{
    public static class RawInputWrapper
    {
        public delegate void OnDeviceEvent(RawInputData message);
        public static Thread KeyboardListenerThread(OnDeviceEvent onDeviceEventCallback)
        {
            Thread keyboardListenerThread = new Thread(() =>
            {
                var keyboard = new RawInputReceiverWindow();
                RawInputData previousMessage=null;
                keyboard.Input += (sender, e) =>
                {
                    if(previousMessage?.ToString() == e.Data.ToString())
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

        public static Thread MouseListenerThread(OnDeviceEvent onDeviceEventCallback)
        {
            Thread mouseListenerThread = new Thread(() =>
            {
                var mouse = new RawInputReceiverWindow();
                mouse.Input += (sender, e) =>
                {
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


        public static Thread JoystickListenerThread(OnDeviceEvent onDeviceEventCallback)
        {
            Thread joystickListenerThread = new Thread(() =>
            {
                var joystick = new RawInputReceiverWindow();
                joystick.Input += (sender, e) =>
                {
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


    }
}
