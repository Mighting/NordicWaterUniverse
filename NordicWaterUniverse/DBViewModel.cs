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

        private ICommand dbCall;

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
                Application.Current.Dispatcher.Invoke((Action)(() => { OnPropertyChanged("DataTableToShow"); }));
            }
        }


        public DBViewModel()
        {
            DbCall = new DelegateCommand(Dbcallaction);
            DataTableLog.getInstance().DataTableFilled += AddToList;
        }
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddToList(object sender, EventArgs e)
        {
            DataTableToShow = DataTableLog.getInstance().MyDataTable;
        }


        private void Dbcallaction(object obj)
        {
            DBController.getInstance().DbLog();
        }

    }
}
