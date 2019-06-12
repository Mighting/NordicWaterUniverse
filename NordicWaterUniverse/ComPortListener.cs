using System;
using System.IO.Ports;
using System.Threading;

namespace NordicWaterUniverse
{
    class ComPortListener

    {
        //Connection port
        private static SerialPort port = new SerialPort("COM9", 9600);
        //Thread to make the connection
        Thread OpenConnectionThread = new Thread(OpenConnection);

        public event EventHandler newScan;

        private string chipId;

        public string ChipId
        {
            get { return chipId; }
            set { chipId = value;
                if (newScan != null)
                {
                    //Throw the event
                    newScan(this,new CheckInEventArgs(ChipId));
                }
            }
        }

        //Do a singleton since we never want more than 1 object listening on the comport
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
            ChipId = port.ReadLine();
        }
    }
}
