using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Connectors
{
    public abstract class Connector : BaseViewModel
    {

        #region Properites
        private string _Status;
        public string Status
        {
            get { return _Status; }
            set { _Status = value; RaisePropertyChangedEvent("Status"); }
        }
        private bool _IsConnected;

        public bool IsConnected
        {
            get { return _IsConnected; }
            set { _IsConnected = value; RaisePropertyChangedEvent("IsConnected"); RaisePropertyChangedEvent("ConnectionStatus"); }
        }

        private string _ConnectionStatus;

        public string ConnectionStatus
        {
            get { if (IsConnected) { _ConnectionStatus = "Connected"; } else { _ConnectionStatus = "Disconnected"; } return _ConnectionStatus; }
            set { _ConnectionStatus = value; RaisePropertyChangedEvent("ConnectionStatus"); }
        }


        #endregion

        public abstract void SendMessage(string message);
        public abstract void Connect();
    }
}
