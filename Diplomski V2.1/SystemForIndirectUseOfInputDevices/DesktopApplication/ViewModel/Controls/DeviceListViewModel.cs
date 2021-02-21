using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ViewModel.Controls
{
    public class DeviceListViewModel : BaseViewModel
    {
        #region Properites
        private ObservableCollection<DeviceModel> _Devices;
        public ObservableCollection<DeviceModel> Devices
        {
            get { return _Devices; }
            set { _Devices = value; RaisePropertyChangedEvent("Devices"); }
        }

        private int _SelectedIndex;
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                _SelectedIndex = value;
                RaisePropertyChangedEvent("SelectedIndex");
                OnSelectdIndexChanged();
            }
        }

        private string _LabelContent;
        public string LabelContent
        {
            get { return _LabelContent; }
            set { _LabelContent = value; RaisePropertyChangedEvent("LabelContent");}
        }
        
        private string _StreamText;

        public string StreamText
        {
            get { return _StreamText; }
            set { _StreamText = value; RaisePropertyChangedEvent("StreamText"); }
        }
        
        #endregion

        #region Constructor(s)
        public DeviceListViewModel()
        {
            LabelContent = "Devices connected to PC";
            Devices = new ObservableCollection<DeviceModel>();
            SelectedIndex = -1;
        }
        #endregion


        #region Methods

        public void ListAllConnectedDevices()
        {
            Devices = Services.Devices.GetAllDevices();
        }

        private void OnSelectdIndexChanged()
        {
            
        }
        #endregion



        #region Commands


        #endregion

    }
}
