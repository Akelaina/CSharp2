using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using BaseCompany.Classes;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace BaseCompany
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
      public partial class MainWindow : Window
    {
        /// <summary>
        /// Класс базы данных компании
        /// </summary>
        /// 

        SqlConnection connection;
        SqlDataAdapter adapter;
        DataTable dt;

        internal static db database;

        public MainWindow()
        {
            InitializeComponent();
            database = new db();
            empList.ItemsSource = database.GetEmployees();
            cbDepList.ItemsSource = database.GetDepartments();
            this.DataContext = database;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "BaseComp"
            };

            connection = new SqlConnection(connectionStringBuilder.ConnectionString);
            adapter = new SqlDataAdapter();

            //select
            SqlCommand command =
                new SqlCommand("SELECT ID, Имя, Фамилия, Возраст, Зарплата, Отдел",
                connection);
            adapter.SelectCommand = command;

            //insert
            command = new SqlCommand(@"INSERT INTO People (Имя, Фамилия, Возраст, Зарплата, Отдел) 
                          VALUES (Name, Surname, Age, Salary, GetDepartmentName); SET @ID = @@IDENTITY;",
                          connection);

            command.Parameters.Add("Name", SqlDbType.NVarChar, -1, "Имя");
            command.Parameters.Add("Surname", SqlDbType.NVarChar, -1, "Фамилия");
            command.Parameters.Add("Age", SqlDbType.NVarChar, 58, "Возраст");
            command.Parameters.Add("Salary", SqlDbType.NVarChar, -1, "Зарплата");
            command.Parameters.Add("GetDepartmentName", SqlDbType.NVarChar, -1, "Отдел");

            SqlParameter param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");

            param.Direction = ParameterDirection.Output;

            adapter.InsertCommand = command;

            // update
            command = new SqlCommand(@"UPDATE People SET Имя = Name,
Фамилия = Surname, Возраст = Age, Зарплата = Surname, Отдел = GetDepartmentName WHERE ID = @ID", connection);

            command.Parameters.Add("Name", SqlDbType.NVarChar, -1, "Имя");
            command.Parameters.Add("Surname", SqlDbType.NVarChar, -1, "Фамилия");
            command.Parameters.Add("Age", SqlDbType.NVarChar, 58, "Возраст");
            command.Parameters.Add("Salary", SqlDbType.NVarChar, -1, "Зарплата");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");

            param.SourceVersion = DataRowVersion.Original;

            adapter.UpdateCommand = command;
            //delete
            command = new SqlCommand("DELETE FROM People WHERE ID = @ID", connection);
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapter.DeleteCommand = command;

            dt = new DataTable();

            adapter.Fill(dt);
            peopleDataGrid.DataContext = dt.DefaultView;

            cbDepList.ItemsSource = dt.DefaultView;
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            //добавим новую строку
           DataRow newRow = dt.NewRow();
            EditEmpWindow editEmpWindow = new EditEmpWindow(newRow);
            editEmpWindow.ShowDialog();

            if (editEmpWindow.DialogResult.Value)
            {
                dt.Rows.Add(editEmpWindow.resultRow);
                adapter.Update(dt);
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)peopleDataGrid.SelectedItem;
            newRow.BeginEdit();

            EditEmpWindow editWindow = new EditEmpWindow(newRow.Row);
            editWindow.ShowDialog();

            if (editWindow.DialogResult.HasValue && editWindow.DialogResult.Value)
            {
                newRow.EndEdit();
                adapter.Update(dt);
            }
            else
            {
                newRow.CancelEdit();
            }
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)peopleDataGrid.SelectedItem;

            newRow.Row.Delete();
            adapter.Update(dt);
        }


    

    /// <summary>
    /// Выбор события из списка
    /// </summary>
    /// <param name="sender">Объект</param>
    /// <param name="args">Параметры</param>
    private void Selected(object sender, SelectionChangedEventArgs args)
        {
            tbInfo.Text = database.GetInfo(sender);
        }

        /// <summary>
        /// Обработка кнопки "Изменить отдел"
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void BtnEditDep_Click(object sender, RoutedEventArgs e)
        {
            if (cbDepList.SelectedItem != null)
            {
                Department editdep = cbDepList.SelectedItem as Department;
                EditDepWindow depEditWindow = new EditDepWindow(editdep.DepartmentID, editdep.Name);
                depEditWindow.Owner = this;
                depEditWindow.Show();
            }
            else
                MessageBox.Show("Выберите отдел для редактирования!");
        }

        /// <summary>
        /// Обработка кнопки "Изменить сотрудника"
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void BtnEditEmp_Click(object sender, RoutedEventArgs e)
        {
            if (empList.SelectedItem != null)
            {
                EditEmpWindow empEditWindow = new EditEmpWindow(empList.SelectedItem as Employee);
                empEditWindow.Owner = this;
                empEditWindow.Show();
            }
            else
                MessageBox.Show("Выберите сотрудника для редактирования!");
        }

        /// <summary>
        /// Обработка кнопки "Добавить сотрудника"
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void BtnCreateEmp_Click(object sender, RoutedEventArgs e)
        {
            AddEmpWindow addEmpWindow = new AddEmpWindow();
            addEmpWindow.Owner = this;
            addEmpWindow.Show();
        }

        /// <summary>
        /// Обработка кнопки "Добавить отдел"
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void BtnCreateDep_Click(object sender, RoutedEventArgs e)
        {
            AddDepWindow addDepWindow = new AddDepWindow();
            addDepWindow.Owner = this;
            addDepWindow.Show();
        }

        private void PeopleDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
