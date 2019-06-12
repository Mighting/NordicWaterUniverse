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

        //Create a checkin.
        CheckIn checkin = CheckIn.getInstance();


        public CheckInViewModel()
        {
            CheckIn.getInstance().PropertyChanged += AddToList;
        }

        public ObservableCollection<CheckIn> Checkins
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