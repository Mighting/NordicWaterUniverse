using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordicWaterUniverse
{
    class CheckOut : Area
    {
        public CheckOut(string areaName) : base(areaName)
        {
        }


        public int CheckOutofArea(int chipid)
        {
            //Listen to the comport then check if it is checked in
            return chipid;
        }
    }
}
