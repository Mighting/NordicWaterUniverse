using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace NordicWaterUniverse
{
    class CheckInViewModel : INotifyPropertyChanged
    {
        //Create a truely observable list.
        private ObservableCollection<CheckInController> checkins = new ObservableCollection<CheckInController>();

        //Event to check on the list.
        public event PropertyChangedEventHandler PropertyChanged;

        //Create a checkin.
        CheckInController checkin = CheckInController.getInstance();


        public CheckInViewModel()
        {
            //Subscribe to Checkin.PropertyChanged to hear when it changes
            CheckInController.getInstance().PropertyChanged += AddToList;
        }

        public ObservableCollection<CheckInController> Checkins
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
            Application.Current.Dispatcher.Invoke((Action)(() => { checkins.Add(CheckInController.getInstance()); }));
        }
    }
}