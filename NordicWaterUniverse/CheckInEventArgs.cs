using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordicWaterUniverse
{
    class CheckInEventArgs : EventArgs
    {

        private string chipIdNumber;

        public string ChipIdNumber
        {
            get;
            set;
        }

        public CheckInEventArgs(string chipIdNumber)
        {
            ChipIdNumber = chipIdNumber;
        }


    }
}
