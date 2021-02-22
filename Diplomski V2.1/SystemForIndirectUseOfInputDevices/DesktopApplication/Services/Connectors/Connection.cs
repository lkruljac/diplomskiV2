using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Services.Connectors
{
    public abstract class Connector
    {

        #region Properites
        private string _Status;
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }


        #endregion

        public abstract void SendMessage(string message);
        public abstract void Connect();
    }
}
