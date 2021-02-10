using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.Pages;

namespace ViewModel
{

    public class MainWindowViewModel : BaseViewModel
    {
        #region Properties
        private BasePageViewModel _selectedViewModel;
        public BasePageViewModel SelectedViewModel
        {
            get
            {
                return _selectedViewModel;
            }
            set
            {
                _selectedViewModel = value;
                RaisePropertyChangedEvent("SelectedViewModel");
            }
        }
       
        public Window MainWindow;
        #endregion
               
        #region Constructor(s)
        public MainWindowViewModel(Window mainWindowInstance)
        {
            SelectedViewModel = new MainPageViewModel(this);
            SelectedViewModel.EnterPage();
            MainWindow = mainWindowInstance;
        }
        #endregion
    }
}
