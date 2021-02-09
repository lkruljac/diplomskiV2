using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;
using ViewModel;

namespace ViewModel
{
    public abstract class BasePageViewModel : BaseViewModel
    {
        protected MainWindowViewModel _ownerWindow;
        public BasePageViewModel(MainWindowViewModel ownerWindow)
        {
            _ownerWindow = ownerWindow;
        }
        public abstract void EnterPage();
    }
}
