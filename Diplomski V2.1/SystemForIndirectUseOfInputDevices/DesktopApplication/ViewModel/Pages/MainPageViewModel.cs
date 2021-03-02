using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using Common;
using System.Collections.ObjectModel;
using ViewModel.Controls;
using System.ComponentModel;
using System.Threading;
using Linearstar.Windows.RawInput;
using ViewModel.Pages.Connections;

namespace ViewModel.Pages
{
    public class MainPageViewModel : BasePageViewModel
    {
        #region Properites
        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; RaisePropertyChangedEvent("Title"); }
        }

        private DeviceListViewModel _DeviceListVM;
        public DeviceListViewModel DeviceListVM
        {
            get { return _DeviceListVM; }
            set { _DeviceListVM = value; RaisePropertyChangedEvent("DeviceListVM"); }
        }


        private string _StreamOutputText;
        public string StreamOutputText
        {
            get { return _StreamOutputText; }
            set { _StreamOutputText = value; RaisePropertyChangedEvent("StreamOutputText"); }
        }


        private bool _IsStreamRuning;
        public bool IsStreamRuning
        {
            get { return _IsStreamRuning; }
            set 
            { 
                _IsStreamRuning = value; 
                RaisePropertyChangedEvent("IsStreamRuning");
                StartStreamCommand.RaiseCanExecuteChanged();
                StopStreamCommand.RaiseCanExecuteChanged();
            }
        }

        #region Radiobuttons
        private bool _IsUARTChecked;
        public bool IsUARTChecked
        {
            get { return _IsUARTChecked; }
            set 
            { 
                _IsUARTChecked = value; 
                RaisePropertyChangedEvent("IsUARTChecked");
                if (_IsUARTChecked)
                {
                    SelectedConnectionControl = null;
                }
            }
        }


        private bool _IsBluetoothChecked;
        public bool IsBluetoothChecked
        {
            get { return _IsBluetoothChecked; }
            set 
            { 
                _IsBluetoothChecked = value; 
                RaisePropertyChangedEvent("IsBluetoothChecked");
                if (_IsBluetoothChecked)
                {
                    SelectedConnectionControl = null;
                }
            }
        }

        private bool _IsTCPIPChecked;
        public bool IsTCPIPChecked
        {
            get { return _IsTCPIPChecked; }
            set 
            { 
                _IsTCPIPChecked = value; 
                RaisePropertyChangedEvent("IsTCPIPChecked");
                if (_IsTCPIPChecked)
                {
                    SelectedConnectionControl = new NetworkViewModel();
                }
            }
        }

        #endregion


        private BaseConnectionControl  _SelectedConnectionControl;

        public BaseConnectionControl SelectedConnectionControl
        {
            get { return _SelectedConnectionControl; }
            set { _SelectedConnectionControl = value; RaisePropertyChangedEvent("SelectedConnectionControl"); }
        }


        private List<Thread> ListenerThreads;

        #endregion



        #region Constructor(s)
        public MainPageViewModel(MainWindowViewModel ownerWindow) : base(ownerWindow)
        {
            DeviceListVM = new DeviceListViewModel();
            _IsStreamRuning = false;
            IsTCPIPChecked = true;
        }
        #endregion



        #region Methods
        public override void EnterPage()
        {
            DeviceListVM.ListAllConnectedDevices();
        }

        public void OnDeviceEvent(RawInputData message)
        {
            StreamOutputText += message+"\n";
        }


        #endregion



        #region Commands

        private DelegateCommand _StartStreamCommand;
        public DelegateCommand StartStreamCommand
        {
            get 
            {
                _StartStreamCommand ??= new DelegateCommand(new Action(StartStream), () => { return !IsStreamRuning; });
                return _StartStreamCommand; 
            }
            set { _StartStreamCommand = value; }
        }
        public void StartStream()
        {
            IsStreamRuning = true;
            ListenerThreads = new List<Thread>();
            
            foreach(var device in DeviceListVM.Devices)
            {
                if (device.IsSelected)
                {
                    Thread deviceThread = null;
                    if(device.Type == "Keyboard")
                    {
                        Services.KeyboardHandler keyboardHandler = new Services.KeyboardHandler();
                        keyboardHandler.Connector = SelectedConnectionControl.Connector;
                        keyboardHandler.GUIOnDeviceEvent += OnDeviceEvent;
                        deviceThread = Services.RawInput.RawInputWrapper.KeyboardListenerThread(keyboardHandler.OnDeviceEvent, device.Id);
                    }
                    else if (device.Type == "Mouse")
                    {
                        Services.JoystickHandler mouseHandler = new Services.JoystickHandler();
                        mouseHandler.GUIOnDeviceEvent += OnDeviceEvent;
                        deviceThread = Services.RawInput.RawInputWrapper.MouseListenerThread(mouseHandler.OnDeviceEvent, device.Id);
                    }
                    else if (device.Type == "Joystick")
                    {
                        Services.JoystickHandler joystickHandler = new Services.JoystickHandler();
                        joystickHandler.GUIOnDeviceEvent += OnDeviceEvent;
                        deviceThread = Services.RawInput.RawInputWrapper.JoystickListenerThread(joystickHandler.OnDeviceEvent, device.Id);
                    }
                    else
                    {
                        throw new Exception("Something went wrong");
                    }
                    ListenerThreads.Add(deviceThread);
                }
            }
        }       
        
        
        private DelegateCommand _StopStreamCommand;
        public DelegateCommand StopStreamCommand
        {
            get 
            {
                _StopStreamCommand ??= new DelegateCommand(new Action(StopStream), () => { return IsStreamRuning; });
                return _StopStreamCommand; 
            }
            set { _StopStreamCommand = value; }
        }
        public void StopStream()
        {
            IsStreamRuning = false;
            foreach(var thread in ListenerThreads)
            {
                thread.Abort();
            }
        }

        private DelegateCommand _OnScreenKeyboardCommand;

        public DelegateCommand OnScreenKeyboardCommand
        {
            get { return _OnScreenKeyboardCommand; }
            set { _OnScreenKeyboardCommand = value; RaisePropertyChangedEvent("OnScreenKeyboardCommand"); }
        }


        #endregion


    }
}
