using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;

namespace NordicWaterUniverse
{
    class DBController
    {
        private static DBController Dbc_Instance = new DBController();

        string connection = ("Data Source=ZBC-E-ROMA-1843;Initial Catalog=NordicWaterUniverseDB;Integrated Security=True");

        string newScanChipId;

        public event EventHandler NewRequest;

        private DBController()
        {
            ComPortListener.getInstance().newScan += DBController_RunStoredProcedure;
        }

        public static DBController getInstance()
        {
            return Dbc_Instance;
        }


        private void DBController_RunStoredProcedure(object sender, EventArgs e)
        {

            //We check to see what E is, and then we parse it into our own EventArgs to get the ChipIdNumber and put that into our own ChipId
            if (e is CheckInEventArgs)
            {
                CheckInEventArgs ch = (CheckInEventArgs)e;
                newScanChipId = ch.ChipIdNumber;
            }

            //Connection to DB
            using (var conn = new SqlConnection(connection))
            {
                Random rndarea = new Random();
                int r = rndarea.Next(1, 5);

                //Call the stored procedure called CheckIn in the DB
                //Simulates that a customer enters a random area
                SqlCommand command = new SqlCommand("dbo.CheckIn", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ChipID", newScanChipId);
                command.Parameters.AddWithValue("@Area", r);
                command.Parameters.AddWithValue("@CheckInTime", DateTime.Now);
                command.Parameters.AddWithValue("@CheckOutTime", DateTime.Now);

                conn.Open();
                command.ExecuteNonQuery();
            }

        }


        private DataTable dt;

        public DataTable Dt
        {
            get { return dt; }
            set {
                dt = value;
                if (NewRequest != null)
                {
                    NewRequest(this, new DbTableLogEventArgs(Dt));
                }
            }
        }



        public void DbLog()
        {
            dt = new DataTable();
            using (var conn = new SqlConnection(connection))
            {
                using (var command = new SqlCommand($"SELECT * FROM dbo.Archive", conn))  
                {
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        DataTable dtholder = new DataTable();
                        da.Fill(dtholder);
                        Dt = dtholder;
                    }
                }
            }
        }

    }

}
