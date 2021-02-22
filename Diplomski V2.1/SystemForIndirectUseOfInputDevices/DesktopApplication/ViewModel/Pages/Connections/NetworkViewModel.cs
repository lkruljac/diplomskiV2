using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Pages.Connections
{
    public class NetworkViewModel : BaseViewModel
    {
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

        private bool _IsConnected;

        public bool IsConnected
        {
            get { return _IsConnected; }
            set { _IsConnected = value; RaisePropertyChangedEvent("IsConnected"); RaisePropertyChangedEvent("ConnectionStatus"); ConnectCommand.RaiseCanExecuteChanged(); DisconnectCommand.RaiseCanExecuteChanged(); }
        }

        private string _ConnectionStatus;

        public string ConnectionStatus
        {
            get { if (IsConnected) { _ConnectionStatus = "Connected"; } else { _ConnectionStatus = "Disconnected"; } return _ConnectionStatus; }
            set { _ConnectionStatus = value; RaisePropertyChangedEvent("ConnectionStatus"); }
        }


        private DelegateCommand _ConnectCommand;

        public DelegateCommand ConnectCommand
        {
            get { _ConnectCommand ??= new DelegateCommand(new Action(Connect), () => { return !IsConnected; }); return _ConnectCommand; }
            set { _ConnectCommand = value; RaisePropertyChangedEvent("ConnectCommand"); }
        }


        private DelegateCommand _DisconnectCommand;

        public DelegateCommand DisconnectCommand
        {
            get { _DisconnectCommand ??= new DelegateCommand(new Action(Disconnect), () => { return IsConnected; }); return _DisconnectCommand; }
            set { _DisconnectCommand = value; RaisePropertyChangedEvent("DisconnectCommand"); }
        }


        #region Methods

        public void Connect()
        {
            IsConnected = true;

        }

        public void Disconnect()
        {
            IsConnected = false;
        }
        #endregion


    }
}
