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
                keyboard.Input += (sender, e) =>
                {
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



    }
}
