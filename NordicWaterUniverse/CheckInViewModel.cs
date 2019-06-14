using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace NordicWaterUniverse
{
    class CheckInViewModel : INotifyPropertyChanged
    {
        //Create a truely observable list.
        private ObservableCollection<CheckIn> checkins = new ObservableCollection<CheckIn>();

        //Event to check on the list.
        public event PropertyChangedEventHandler PropertyChanged;


        public CheckInViewModel()
        {
            //Subscribe to Checkin.PropertyChanged to hear when it changes
            CheckIn.getInstance().PropertyChanged += AddToList;
        }

        public ObservableCollection<CheckIn> Checkins
        {
            get { return checkins; }
            set
            {
                //Tells the dispatcher that we want to update the Gui whenever Checkins is changed
                Application.Current.Dispatcher.Invoke((Action)(() => { OnPropertyChanged("Checkins"); checkins = value; }));
            }
        }

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddToList(object sender, EventArgs e)
        {
            //Tells the GUI that we have added a new object to our list
            Application.Current.Dispatcher.Invoke((Action)(() => { checkins.Add(CheckIn.getInstance()); }));
        }
    }
}