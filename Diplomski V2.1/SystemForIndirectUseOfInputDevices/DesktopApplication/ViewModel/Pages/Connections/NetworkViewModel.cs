using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using Model.Connectors;
using System.Windows;
using System.Threading;

namespace ViewModel.Pages.Connections
{
    public class NetworkViewModel : BaseConnectionControl
    {
        #region Constructor(s)

        public NetworkViewModel()
        {
            Connector = new NetworkConnector();
            IsLoadingAnimationVisible = false;
        }

        #endregion


        #region Properites
        private bool _IsLoadingAnimationVisible;
        public bool IsLoadingAnimationVisible
        {
            get {  return _IsLoadingAnimationVisible; }
            set { _IsLoadingAnimationVisible = value; if (_IsLoadingAnimationVisible) { LoadingAnimationVisibility = Visibility.Visible; } else { LoadingAnimationVisibility = Visibility.Hidden; } RaisePropertyChangedEvent("LoadingAnimationVisibility"); }
        }
        private Visibility _LoadingAnimationVisibility;

        public Visibility LoadingAnimationVisibility
        {
            get { return _LoadingAnimationVisibility; }
            set { _LoadingAnimationVisibility = value; RaisePropertyChangedEvent("LoadingAnimationVisibility"); }
        }


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
            ConnectCommand._checkCanExecute = () => { return false; };
            ConnectCommand.RaiseCanExecuteChanged();
            ConnectCommand._checkCanExecute = () => { return !Connector.IsConnected; };
            IsLoadingAnimationVisible = true;


            Thread connectThread = new Thread(() =>
            {
                try
                {
                    Connector.Connect();
                }
                catch
                {
                    Connector.Status = "Fail";
                }
                System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                     IsLoadingAnimationVisible = false;
                });
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ConnectCommand.RaiseCanExecuteChanged();
                    DisconnectCommand.RaiseCanExecuteChanged();
                });
 
            });
            connectThread.Start(); 
        }

        public void Disconnect()
        {
            //Connector.Stream = null;
            Connector.Status = "Disconnected";
            Connector.IsConnected = false;
            ConnectCommand.RaiseCanExecuteChanged();
            DisconnectCommand.RaiseCanExecuteChanged();
        }
        #endregion


    }
}
