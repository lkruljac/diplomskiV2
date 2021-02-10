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
        #region Properties
        protected MainWindowViewModel _ownerWindow;
        #endregion
        #region Constructor(s)
        public BasePageViewModel(MainWindowViewModel ownerWindow)
        {
            _ownerWindow = ownerWindow;
        }
        #endregion
        #region Methods
        public abstract void EnterPage();
        #endregion
    }
}
