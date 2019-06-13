using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordicWaterUniverse
{
    class CheckInEventArgs : EventArgs
    {

        //Our own EventArgs so we can store the ID scanned

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
