using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace NordicWaterUniverse
{
    class CheckIn : Area, INotifyPropertyChanged
    {
        //Connection port
        public static SerialPort port = new SerialPort("COM9", 9600);

        public event EventHandler newInput;

        //Make sure the Input does not says null
        private string inputFromPort = "Nothing is scanned or the value is null";

        public string InputFromPort
        {
            get { return inputFromPort; }
            set {
                inputFromPort = value;
                OnPropertyChanged(InputFromPort);
                newInput(this,new EventArgs());
            }
        }

        //Thread to make the connection
        Thread OpenConnectionThread = new Thread(OpenConnection);

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public CheckIn(string areaName) : base(areaName)
        {

            //Start the Connection Thread
            OpenConnectionThread.Start();

            //Subscribe to the Serial Data recived event
            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
        }

        static void FileEvent(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Input changed");
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
            //Put the data we get into an buffer
            int intBuffer;
            intBuffer = port.BytesToRead;
            byte[] byteBuffer = new byte[intBuffer];
            //Take what we got and put it in a string
            InputFromPort = port.ReadLine();
            Console.WriteLine("Input from the object class: " + inputFromPort + " From the Model");
        }



    }
}
