using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RPIControllerEmulator_Server.Model;

using RPIControllerEmulator_Server.Model.Enumerations;

namespace RPIControllerEmulator_Server.ViewModel.Connectors
{
    class Helper
    {
        public static byte[] ParseMessage(string stringMessage)
        {

            string[] strings = stringMessage.Split('#');
            //TODO SHORT INT
            Int16 codeInt = 0;
            Int16 deviceTypeInt = 0;
            Int16 eventTypeInt = 0;

            KeyboardCodes code;
            Enum.TryParse(strings[0], out code);
            codeInt = (Int16)code;

            DeviceTypes deviceType;
            Enum.TryParse(strings[1], out deviceType);
            deviceTypeInt = (Int16)deviceType;

            EventTypes eventType;
            Enum.TryParse(strings[2], out eventType);
            eventTypeInt = (Int16)eventType;

            byte[] data = new byte[sizeof(Int16) * 3];

            using (MemoryStream stream = new MemoryStream(data))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(codeInt);
                writer.Write(deviceTypeInt);
                writer.Write(eventTypeInt);
            }
            
            return data;
        }
    }
}
