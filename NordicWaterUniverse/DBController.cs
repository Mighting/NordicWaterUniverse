using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.SqlClient;

namespace NordicWaterUniverse
{
    class DBController
    {
        private static DBController Dbc_Instance = new DBController();

        SqlConnection connection = new SqlConnection("Data Source=ZBC-E-ROMA-1843;Initial Catalog=NordicWaterUniverseDB;Integrated Security=True");
        SqlCommand command;

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
            OpenSQLConnection();

            //Add to database here
            //We check to see what E is, and then we parse it into our own EventArgs to get the ChipIdNumber and put that into our own ChipId
            if (e is CheckInEventArgs)
            {
                CheckInEventArgs ch = (CheckInEventArgs)e;
                newScanChipId = ch.ChipIdNumber;
            }

            command = new SqlCommand($"INSERT INTO CheckedIn VALUES ({newScanChipId})", connection);

            command.ExecuteNonQuery();

            connection.Close();

        }

        public void OpenSQLConnection()
        {
            connection.Open();
            Console.WriteLine("Connection to Database is now Open");
        }

    }

}
