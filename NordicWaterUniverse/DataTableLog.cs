using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordicWaterUniverse
{
    class DataTableLog
    {
        private static DataTableLog Dtl_Instance = new DataTableLog();

        public event EventHandler DataTableFilled;

        private DataTable dt;

        public DataTable MyDataTable
        {
            get { return dt; }
            set { dt = value;
                if (DataTableFilled != null)
                {
                    DataTableFilled(this, new DbTableLogEventArgs(MyDataTable));
                }
            }
        }

        private DataTableLog()
        {
            DBController.getInstance().NewRequest += DataTableLog_NewRequest;
        }

        private void DataTableLog_NewRequest(object sender, EventArgs e)
        {
            if (e is DbTableLogEventArgs)
            {
                DbTableLogEventArgs ch = (DbTableLogEventArgs)e;
                MyDataTable = ch.Dt;
            }
        }

        public static DataTableLog getInstance()
        {
            return Dtl_Instance;
        }




    }
}
