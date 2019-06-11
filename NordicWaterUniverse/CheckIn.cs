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
    class CheckIn : INotifyPropertyChanged
    {
        public event EventHandler newInput;

        public event PropertyChangedEventHandler PropertyChanged;

        private static CheckIn instance = new CheckIn();


        string myChipId;

        private CheckIn()
        {
            ComPortListener.getInstance().newScan += OnNewInput;
        }

        public static CheckIn getInstance()
        {
            return instance;
        }


        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnNewInput(object sender, EventArgs e)
        {
            if (e is CheckInEventArgs)
            {
                CheckInEventArgs ch = (CheckInEventArgs)e;
                myChipId = ch.ChipIdNumber;
            }
        }







    }
}
