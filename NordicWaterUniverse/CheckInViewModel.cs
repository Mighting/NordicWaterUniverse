using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using VJCollections;
using System.Windows;

namespace NordicWaterUniverse
{
    class CheckInViewModel : INotifyPropertyChanged
    {
        //Create a truely observable list.
        private ItemsChangeObservableCollection<CheckIn> checkins = new ItemsChangeObservableCollection<CheckIn>();

        //Event to check on the list.
        public event PropertyChangedEventHandler PropertyChanged;

        //Create a checkin.
        CheckIn checkin = CheckIn.getInstance();


        public CheckInViewModel()
        {
            CheckIn.getInstance().newInput += AddToList;
        }

        public ItemsChangeObservableCollection<CheckIn> Checkins
        {
            get { return checkins; }
            set
            {
                Application.Current.Dispatcher.Invoke((Action)(() => { OnPropertyChanged("Checkins"); checkins = value; }));
            }
        }

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddToList(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)(() => { checkins.Add(CheckIn.getInstance()); }));
        }
    }
}