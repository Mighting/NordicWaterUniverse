﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace NordicWaterUniverse
{
    class CheckInController : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static CheckInController instance = new CheckInController();

        private string myChipId;

        public string MyChipId
        {
            get { return myChipId; }
            set { myChipId = value;
                //This gets called when MyChipId changes.
                    OnPropertyChanged(MyChipId);
            }
        }

        //Event to see if a property changed.
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private CheckInController()
        {
            //Subscribe to ComPortListener's newScan so we know when there have been a scan
            ComPortListener.getInstance().newScan += OnNewInput;
        }

        public static CheckInController getInstance()
        {
            return instance;
        }

        //Gets called when a scan from ComportListener happend
        private void OnNewInput(object sender, EventArgs e)
        {
            //We check to see what E is, and then we parse it into our own EventArgs to get the ChipIdNumber and put that into our own ChipId
            if (e is CheckInEventArgs)
            {
                CheckInEventArgs ch = (CheckInEventArgs)e;
                MyChipId = ch.ChipIdNumber;
            }
        }







    }
}
