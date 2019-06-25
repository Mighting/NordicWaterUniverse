using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace NordicWaterUniverse
{
    class DbTableLogEventArgs : EventArgs
    {
        //Our own EventArgs so we can store the Datatable that returned

        private DataTable dt;

        public DbTableLogEventArgs(DataTable dt)
        {
            Dt = dt;
        }

        public DataTable Dt
        {
            get;
            set;
        }



    }
}
