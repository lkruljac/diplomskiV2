using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using Model.Connectors;

namespace ViewModel.Pages.Connections
{
    public class NetworkViewModel : BaseConnectionControl
    {
        #region Constructor(s)

        public NetworkViewModel()
        {
            Connector = new NetworkConnector();
        }

        #endregion


        #region Properites
    

        #endregion


        private DelegateCommand _ConnectCommand;

        public DelegateCommand ConnectCommand
        {
            get { _ConnectCommand ??= new DelegateCommand(new Action(Connect), () => { return !Connector.IsConnected; }); return _ConnectCommand; }
            set { _ConnectCommand = value; RaisePropertyChangedEvent("ConnectCommand"); }
        }


        private DelegateCommand _DisconnectCommand;

        public DelegateCommand DisconnectCommand
        {
            get { _DisconnectCommand ??= new DelegateCommand(new Action(Disconnect), () => { return Connector.IsConnected; }); return _DisconnectCommand; }
            set { _DisconnectCommand = value; RaisePropertyChangedEvent("DisconnectCommand"); }
        }


        #region Methods

        public void Connect()
        {
            Connector.Connect();
            ConnectCommand.RaiseCanExecuteChanged(); 
            DisconnectCommand.RaiseCanExecuteChanged();
        }

        public void Disconnect()
        {
            //Connector.Stream = null;
            Connector.Status = "Disconnected";
            ConnectCommand.RaiseCanExecuteChanged();
            DisconnectCommand.RaiseCanExecuteChanged();
        }
        #endregion


    }
}
