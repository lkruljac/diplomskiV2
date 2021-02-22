using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Services.Connectors
{
    public class NetworkConnector : Connector
    {
        #region Properites

        private NetworkStream _Stream;
        public NetworkStream Stream
        {
            get { return _Stream; }
            set { Stream = value; }
        }

        private string _ServerIP;
        public string ServerIP
        {
            get { return _ServerIP; }
            set { _ServerIP = value; }
        }

        private int _ServerPort;
        public int ServerPort
        {
            get { return _ServerPort; }
            set { _ServerPort = value; }
        }


        #endregion


        #region Constructor(s)
        public NetworkConnector()
        {
            Status = "Disconnected";
        }
        #endregion


        #region Methods

        public override void SendMessage(string stringMessage)
        {
            var data = Encoding.ASCII.GetBytes(stringMessage);
            // Send the message to the connected TcpServer.
            Stream.Write(data, 0, data.Length);
            Console.WriteLine("Sent: {0}", stringMessage);
        }

        public override void Connect()
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                TcpClient client = new TcpClient(ServerIP, ServerPort);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("Test message");

                // Get a client stream for reading and writing.
                this._Stream = client.GetStream();

                // Send the message to the connected TcpServer.
                _Stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", "Test message");

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = _Stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                if (responseData == "Test message\0")
                {
                    Status = "Connected";
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Exception("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                throw new Exception("SocketException: {0}", e);
            }
        }



        #endregion
    }
}
