using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;

namespace NordicWaterUniverse
{
    class DBController
    {
        private static DBController Dbc_Instance = new DBController();

        string connection = ("Data Source=ZBC-E-ROMA-1843;Initial Catalog=NordicWaterUniverseDB;Integrated Security=True");

        string newScanChipId;

        private DBController()
        {

            ComPortListener.getInstance().newScan += DBController_AddtoDB;
        }

        public static DBController getInstance()
        {
            return Dbc_Instance;
        }


        private void DBController_AddtoDB(object sender, EventArgs e)
        {

            //Add to database here
            //We check to see what E is, and then we parse it into our own EventArgs to get the ChipIdNumber and put that into our own ChipId
            if (e is CheckInEventArgs)
            {
                CheckInEventArgs ch = (CheckInEventArgs)e;
                newScanChipId = ch.ChipIdNumber;
            }

            //Connection to DB
            using (var conn = new SqlConnection(connection))
            {
                //Call the stored procedure called CheckIn in the DB
                SqlCommand command = new SqlCommand("dbo.CheckIn", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ChipID", newScanChipId);

                conn.Open();
                command.ExecuteNonQuery();
            }

        }

    }

}
