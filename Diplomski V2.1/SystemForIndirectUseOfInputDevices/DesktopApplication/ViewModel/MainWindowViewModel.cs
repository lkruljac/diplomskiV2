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
        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel
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

        public MainWindowViewModel(Window mainWindowInstance)
        {
            SelectedViewModel = new MainPageViewModel();
            MainWindow = mainWindowInstance;
        }

    }
}
