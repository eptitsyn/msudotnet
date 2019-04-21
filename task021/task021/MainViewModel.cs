using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace task021
{
    class MainViewModel:INotifyPropertyChanged
    {
        private SqlDataAdapter EmployeesAdapter;
        private DataTable _employeesTable;
        public MainModel model { get; set; }

        public DataSet MyDataSet { get; set; }

        public DataTable EmployeesTable
        {
            get { return _employeesTable; }
            set { _employeesTable = value; RaisePropertyChanged(); }
        }

        public MainViewModel()
        {
            model = new MainModel();
            InitCommands();
            ConnectToSql();
        }

        void ConnectToSql()
        {
            MyDataSet = new DataSet("MyDataSet");
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["sqlProvider"];
            EmployeesAdapter = new SqlDataAdapter("select * from Employees", connectionString.ConnectionString);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(EmployeesAdapter);
            EmployeesTable = new DataTable("Employees");
            EmployeesAdapter.Fill(EmployeesTable);
            MyDataSet.Tables.Add(EmployeesTable);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand CommitCommand { get; private set; }
        public RelayCommand RevertCommand { get; private set; }

        private void InitCommands()
        {
            CommitCommand = new RelayCommand(o => Commit(), o=> CanRevert());
            RevertCommand = new RelayCommand(o => Revert(), o=> CanRevert());
        }

        private void Commit()
        {
            EmployeesAdapter.Update(MyDataSet.Tables["Employees"]);
        }

        private void Revert()
        {
            MyDataSet.RejectChanges();
        }

        private bool CanRevert()
        {
            return MyDataSet.HasChanges();
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
