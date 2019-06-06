using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NordicWaterUniverse
{
    class CheckIn : Area
    {
        //Connection port
        public static SerialPort port = new SerialPort("COM9", 9600);

        //Make sure the Input does not says null

        private string inputFromPort = "";

        public string InputFromPort
        {
            get { return inputFromPort; }
            set { inputFromPort = value; }
        }


        //Thread to make the connection
        Thread OpenConnectionThread = new Thread(OpenConnection);

        public CheckIn(string areaName) : base(areaName)
        {
            inputFromPort = InputFromPort;
            //Start the Connection Thread
            OpenConnectionThread.Start();
        }

        public static void OpenConnection()
        {
            //Open the connection if it is closed
            if (!port.IsOpen)
            {
                port.Open();
            }
        }

        public void CheckIntoArea()
        {
            //Subscribe to the Serial Data recived event
            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
        }

        public void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //Put the data we get into an buffer
            int intBuffer;
            intBuffer = port.BytesToRead;
            byte[] byteBuffer = new byte[intBuffer];
            //Take what we got and put it in a string
            inputFromPort = port.ReadLine();
        }



    }
}
