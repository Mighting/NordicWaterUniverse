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
        private ItemsChangeObservableCollection<CheckIn> checkins = new ItemsChangeObservableCollection<CheckIn>();

        public event PropertyChangedEventHandler PropertyChanged;

        CheckIn checkin = new CheckIn("Area 1");

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
            Console.WriteLine("Input from first object in the list: " + checkins[0].InputFromPort + " From ViewModel");
        }


        public CheckInViewModel()
        {
            checkin.newInput += AddToList;
        }

        public void AddToList(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)(() => { checkins.Add(new CheckIn("Area 1")); }));
        }
    }
}