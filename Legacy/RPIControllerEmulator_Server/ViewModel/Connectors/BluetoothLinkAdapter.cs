using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPIControllerEmulator_Server.Model;


using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;


using RPIControllerEmulator_Server.ViewModel.Connectors;
using System.Net.Sockets;

namespace RPIControllerEmulator_Server.ViewModel.Connectors
{
    public class BluetoothLinkAdapter : Connection
    {

        private BluetoothEndPoint localEndpoint;
        private BluetoothClient localClient;
        private BluetoothComponent localComponent;
        public List<BluetoothDeviceInfo> DiscoveredDeviceList = new List<BluetoothDeviceInfo>();

        public bool IsSearching;
        
        public string Status { get; }
        private NetworkStream Stream;


        public BluetoothLinkAdapter()
        {
            this.Status = "Disconnected";
            this.IsSearching = false;
            localEndpoint = new BluetoothEndPoint(BluetoothRadio.PrimaryRadio.LocalAddress, BluetoothService.SerialPort);
            localClient = new BluetoothClient(localEndpoint);
            localComponent = new BluetoothComponent(localClient);
        }


        public void SearchDevices()
        {
            IsSearching = true;
            DiscoveredDeviceList.Clear();
            localComponent.DiscoverDevicesProgress += new EventHandler<DiscoverDevicesEventArgs>(component_DiscoverDevicesProgress);
            localComponent.DiscoverDevicesComplete += new EventHandler<DiscoverDevicesEventArgs>(component_DiscoverDevicesComplete);
            localComponent.DiscoverDevicesAsync(255, true, true, true, true, null);


        }


        private void component_DiscoverDevicesProgress(object sender, DiscoverDevicesEventArgs e)
        {
            for (int i = 0; i < e.Devices.Length; i++)
            {
                this.DiscoveredDeviceList.Add(e.Devices[i]);
            }
        }

        private void component_DiscoverDevicesComplete(object sender, DiscoverDevicesEventArgs e)
        {
            IsSearching = false;
        }
        

  

        public void Connect(BluetoothDeviceInfo device)
        {
            localClient.SetPin(null);
            localClient.BeginConnect(device.DeviceAddress, BluetoothService.SerialPort, new AsyncCallback(Connect), device);
            BluetoothEndPoint remoteEP = new BluetoothEndPoint(device.DeviceAddress, BluetoothService.SerialPort);
            BluetoothClient client = new BluetoothClient();
            client.Connect(remoteEP);
            localClient.Connect(remoteEP);
            this.Stream = localClient.GetStream();
        }

        // callback
        private void Connect(IAsyncResult result)
        {


        }

        public override void SendMessage(string stringMessage)
        {
            byte[] data = new byte[12];
            data = Helper.ParseMessage(stringMessage);
            // Send the message to the connected TcpServer.
            this.Stream.Write(data, 0, data.Length);
            Console.WriteLine("Sent: {0}", stringMessage);
        }


    }
}
