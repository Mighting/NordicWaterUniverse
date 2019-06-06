using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace NordicWaterUniverse
{
    class CheckInViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<CheckIn> checkins = new ObservableCollection<CheckIn>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<CheckIn> Checkins
        {
            get { return checkins; }
            set
            {
                checkins = value;
                OnPropertyChanged(checkins[0].InputFromPort);
            }
        }

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public CheckInViewModel()
        {
            checkins.Add(new CheckIn("Area 1"));

        }
    }
}