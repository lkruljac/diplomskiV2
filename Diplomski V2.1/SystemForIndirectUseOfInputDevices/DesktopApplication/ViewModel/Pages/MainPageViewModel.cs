using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using Common;
using System.Collections.ObjectModel;
using ViewModel.Controls;

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

        #endregion


        #region Constructor(s)
        public MainPageViewModel(MainWindowViewModel ownerWindow) : base(ownerWindow)
        {
            DeviceListVM = new DeviceListViewModel();
        }
        #endregion

        #region Methods
        public override void EnterPage()
        {
            DeviceListVM.ListAllConnectedDevices();
        }
        #endregion
    }
}
