using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NordicWaterUniverse
{
    class DBViewModel : INotifyPropertyChanged
    {
        //To bind a button in the GUI
        private ICommand dbCall;

        //The Datatable we want to show in the GUI
        private DataTable dataTableToShow;

        public event PropertyChangedEventHandler PropertyChanged;



        public ICommand DbCall
        {
            get { return dbCall; }
            set { dbCall = value; }
        }

        public DataTable DataTableToShow
        {
            get { return dataTableToShow; }
            set
            {
                dataTableToShow = value;
                //Tell the GUI to Update when our Datatable changes
                Application.Current.Dispatcher.Invoke((Action)(() => { OnPropertyChanged("DataTableToShow"); }));
            }
        }


        public DBViewModel()
        {
            DbCall = new DelegateCommand(Dbcallaction);
            //Subcribe to the event so we can see when the Datatable have been filled in DataTableLog
            DataTableLog.getInstance().DataTableFilled += AddToList;
        }
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddToList(object sender, EventArgs e)
        {
            //Set our Datatable to be the datatable from DataTabelLog
            DataTableToShow = DataTableLog.getInstance().MyDataTable;
        }


        private void Dbcallaction(object obj)
        {
            //Tells the DBController to run this method
            DBController.getInstance().DbLog();
        }

    }
}
