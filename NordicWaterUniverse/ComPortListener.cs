using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NordicWaterUniverse
{
    class ComPortListener

    {
        //Connection port
        private static SerialPort port = new SerialPort("COM9", 9600);
        //Thread to make the connection
        Thread OpenConnectionThread = new Thread(OpenConnection);

        public event EventHandler newScan;
        public string chipId = "Is empty";

        private static ComPortListener instance = new ComPortListener();

        private ComPortListener()
        {
            //Start the Connection Thread
            OpenConnectionThread.Start();

            //Subscribe to the Serial Data recived event
            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
        }

        public static ComPortListener getInstance()
        {
            return instance;
        }


        public static void OpenConnection()
        {
            //Open the connection if it is closed
            if (!port.IsOpen)
            {
                port.Open();
            }
        }

        public void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            chipId = port.ReadLine();
        }
    }
}
