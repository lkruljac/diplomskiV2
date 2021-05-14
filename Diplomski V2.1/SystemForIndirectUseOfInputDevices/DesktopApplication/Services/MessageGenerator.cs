using Linearstar.Windows.RawInput;
using Model;
using Newtonsoft.Json.Linq;
using Services.RawInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class MessageGenerator
    {

        public static byte[] GenerateByteArray(RawInputData rawInputData)
        {

            dynamic dataObject = rawInputData;
            EDeviceType deviceType = RawInputWrapper.GetCustomDeviceType(rawInputData.Device);
            EEventType eventType;
            byte[] ByteData = new byte[] { };
            switch (deviceType)
            {
                
                case EDeviceType.Keyboard:
                    eventType = (dataObject.Keyboard.Flags.ToString() == "Down") ? EEventType.KeyPressed : EEventType.KeyReleased;
                    int keyCode = (dataObject.Keyboard.VirutalKey);
                    int scanCode = (dataObject.Keyboard.ScanCode);
                    ByteData = ByteData.Combine(BitConverter.GetBytes((int)deviceType));
                    ByteData = ByteData.Combine(BitConverter.GetBytes((int)eventType));
                    ByteData = ByteData.Combine(BitConverter.GetBytes((int)keyCode));
                    ByteData = ByteData.Combine(BitConverter.GetBytes((int)scanCode));
                    break;
                case EDeviceType.Joystick:
                    int size = dataObject.Hid.ElementSize;
                    byte[] content = dataObject.Hid.RawData;
                    ByteData = ByteData.Combine(BitConverter.GetBytes((int)deviceType));
                    ByteData = ByteData.Combine(BitConverter.GetBytes((int)size));
                    ByteData = ByteData.Combine(content);
                    break;
            }
            return ByteData.ToArray();
        }

        public static string GenerateString(RawInputData rawInputData)
        {
            throw new NotImplementedException();
        }

        public static void Insert(this Byte[] ExistingByteArray, byte[] array)
        {
            foreach (byte b in array)
            {
                ExistingByteArray.Append(b);
            }
            //return ExistingByteArray;
        }

        public static byte[] Combine(this byte[] first, byte[] second)
        {
            byte[] bytes = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, bytes, 0, first.Length);
            Buffer.BlockCopy(second, 0, bytes, first.Length, second.Length);
            return bytes;
        }

    }
}
