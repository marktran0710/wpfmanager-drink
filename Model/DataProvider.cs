using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using manager_drink.ViewModel;

namespace manager_drink.Model
{
    public class DataProvider
    {
        private static DataProvider _ins;
        public static DataProvider Ins
        {
            get
            {
                if (_ins == null) _ins = new DataProvider();
                return _ins;
            }
            set
            {
                _ins = value;
            }
        }
        public MANAGER_DRINKEntities1 DB { get; set; }
        private DataProvider()
        {
            DB = new MANAGER_DRINKEntities1();
        }

    }
}
