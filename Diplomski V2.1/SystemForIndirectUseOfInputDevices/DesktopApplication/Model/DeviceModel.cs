using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DeviceModel : ObservableObject
    {
        #region Properties
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; RaisePropertyChangedEvent("Name"); }
        }


        private string _Id;
        public string Id
        {
            get { return _Id; }
            set { _Id = value; RaisePropertyChangedEvent("Id"); }
        }


        private string _Type;
        public string Type
        {
            get { return _Type; }
            set { _Type = value; RaisePropertyChangedEvent("Type"); }
        }

        private bool _IsSelected;

        public bool IsSelected
        {
            get { return _IsSelected; }
            set { _IsSelected = value; RaisePropertyChangedEvent("IsSelected"); }
        }

        #endregion

        #region Constructor(s)
        public DeviceModel(){}

        public DeviceModel(string name, string id, string type)
        {
            Name = name;
            Id = id;
            Type = type;
        }

        #endregion
    }
}
