using Common;
using Model.Connectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Pages.Connections
{
    public abstract class BaseConnectionControl : BaseViewModel
    {
        private Connector _Connector;
        public Connector Connector
        {
            get { return _Connector; }
            set { _Connector = value; RaisePropertyChangedEvent("Connector"); }
        }
    }
}
