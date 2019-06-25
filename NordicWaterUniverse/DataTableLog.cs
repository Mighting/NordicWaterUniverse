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
        //Singleton
        private static DataTableLog Dtl_Instance = new DataTableLog();

        public event EventHandler DataTableFilled;

        private DataTable dt;

        public DataTable MyDataTable
        {
            get { return dt; }
            set { dt = value;
                if (DataTableFilled != null)
                {
                    //Push a notification on the event
                    DataTableFilled(this, new DbTableLogEventArgs(MyDataTable));
                }
            }
        }

        private DataTableLog()
        {
            //Subcribe to the event so we know when a new scan have been made
            DBController.getInstance().NewRequest += DataTableLog_NewRequest;
        }

        private void DataTableLog_NewRequest(object sender, EventArgs e)
        {
            //Get the ChipID of the scan
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
