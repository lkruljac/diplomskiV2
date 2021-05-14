using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Model.Connectors
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

        private int _Port;
        public int Port
        {
            get { return _Port; }
            set { _Port = value; RaisePropertyChangedEvent("Port"); }
        }

        private string _IP;
        public string IP
        {
            get { return _IP; }
            set { _IP = value; RaisePropertyChangedEvent("IP"); }
        }

    
        #endregion


        #region Constructor(s)
        public NetworkConnector()
        {
            Port = 54000;
            IP = "192.168.0.100";
            Status = "Disconnected";
        }
        #endregion


        #region Methods

        public override void SendMessage(byte[] data)
        {
            try
            {
                Stream.Write(data, 0, data.Length);
     
            }
            catch
            {
                Console.WriteLine("Error");
            }
         
        }

        public override void Connect()
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                TcpClient client = new TcpClient(IP, Port);

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
                    IsConnected = true;
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
