using Company.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Company
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SqlConnection connection;
        SqlDataAdapter adGeneral;
        SqlDataAdapter adEmpl;
        SqlDataAdapter adDep;
        DataTable dtGeneral;
        DataTable dtEmpl;
        DataTable dtDep;

        public MainWindow()
        {
            InitializeComponent();

            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "lesson7_1713"
            };

            connection = new SqlConnection(connectionStringBuilder.ConnectionString);
            adGeneral = new SqlDataAdapter();

            //select all information
            SqlCommand command =
                new SqlCommand(@"SELECT e.ID, eName as Name, Surname," +
                             " Age, Salary, dName as Department" +
                             " FROM Employees e" +
                             " JOIN Departments d on e.DepartmentID = d.ID",
                connection);
            adGeneral.SelectCommand = command;
            dtGeneral = new DataTable();
            adGeneral.Fill(dtGeneral);
            dgWorkers.DataContext = dtGeneral.DefaultView;
            dtEmpl = new DataTable();


            //select all empolyees
            adEmpl = new SqlDataAdapter();
            command =
                new SqlCommand(@"SELECT ID, eName, Surname," +
                             " Age, Salary, DepartmentID" +
                             " FROM Employees",
                connection);
            adEmpl.SelectCommand = command;
            adEmpl.Fill(dtEmpl);
            empList.ItemsSource = dtEmpl.DefaultView;
            dtDep = new DataTable();


            //select all departments
            adDep = new SqlDataAdapter();
            command =
               new SqlCommand(@"SELECT ID, dName" +
                            " FROM Departments",
               connection);
            adDep.SelectCommand = command;
            adDep.Fill(dtDep);
            cbDepList.ItemsSource = dtDep.DefaultView;


            //insert into empolyees
            command = new SqlCommand(@"INSERT INTO Employees (eName, Surname, Age, Salary, DepartmentID) 
                          VALUES (@eName, @Surname, @Age, @Salary, @DepartmentID); SET @ID = @@IDENTITY;",
                          connection);

            command.Parameters.Add("@eName", SqlDbType.NVarChar, -1, "eName");
            command.Parameters.Add("@Surname", SqlDbType.NVarChar, -1, "Surname");
            command.Parameters.Add("@Age", SqlDbType.Int, 58, "Age");
            command.Parameters.Add("@Salary", SqlDbType.Money, -1, "Salary");
            command.Parameters.Add("@DepartmentID", SqlDbType.Int, -1, "DepartmentID");
            SqlParameter param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.Direction = ParameterDirection.Output;
            adEmpl.InsertCommand = command;


            //insert into departmens
            command = new SqlCommand(@"INSERT INTO Department (dName) 
                          VALUES (@dName); SET @ID = @@IDENTITY;",
                          connection);

            command.Parameters.Add("@dName", SqlDbType.NVarChar, -1, "dName");
            //SqlParameter param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.Direction = ParameterDirection.Output;
            adDep.InsertCommand = command;


            //update employees
            command = new SqlCommand(@"UPDATE Employees SET eName = @eName,
                        Surname = @Surname, Age = @Age, Salary = @Salary, 
                        DepartmentID = @DepartmentID WHERE ID = @ID", connection);

            command.Parameters.Add("@eName", SqlDbType.NVarChar, -1, "eName");
            command.Parameters.Add("@Surname", SqlDbType.NVarChar, -1, "Surname");
            command.Parameters.Add("@Age", SqlDbType.Int, -1, "Age");
            command.Parameters.Add("@Salary", SqlDbType.Money, -1, "Salary");
            command.Parameters.Add("@DepartmentID", SqlDbType.Int, -1, "DepartmentID");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adEmpl.UpdateCommand = command;

            //update departments
            command = new SqlCommand(@"UPDATE Departments SET dName = @dName WHERE ID = @ID", connection);

            command.Parameters.Add("@dName", SqlDbType.NVarChar, -1, "dName");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adDep.UpdateCommand = command;
        }

        /// <summary>Обработка события выбора сотрудника из списка</summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void SelectedEmp(object sender, SelectionChangedEventArgs args)
        {
            string result = String.Empty;
            DataRowView selectedEmp = (DataRowView)empList.SelectedItem;

            var request = dtGeneral
            .AsEnumerable()
            .Where(empID => empID.Field<int>("ID") == (int)selectedEmp["ID"]);

            foreach (var item in request)
            {
                result += $"{item[1]} {item[2]}, возраст: {item[3]}, " +
                    $"зарплата: {item[4]:##}, отдел: {item[5]}\n";
            }
            
            tbInfo.Text = result;
        }

        /// <summary>Обработка события выбора сотрудника из списка</summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void SelectedDep(object sender, SelectionChangedEventArgs args)
        {
            string result = String.Empty;
            DataRowView selectedDep = (DataRowView)cbDepList.SelectedItem;

            var request = dtEmpl
            .AsEnumerable()
            .Where(depID => depID.Field<int>("DepartmentID") == (int)selectedDep["ID"]);

            foreach (var item in request)
            {
                result += $"{item[1]} {item[2]}, возраст: {item[3]}, " +
                    $"зарплата: {item[4]:##}\n";
            }

            tbInfo.Text = result;
        }

        /// <summary>Обработка нажатия кнопки "редактировать департамент"</summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void BtnEditDep_Click(object sender, RoutedEventArgs e)
        {
            if (cbDepList.SelectedItem != null)
            {
                DataRowView editDepRow = (DataRowView)cbDepList.SelectedItem;
                editDepRow.BeginEdit();

                DepEditWindow depEditWindow = new DepEditWindow(editDepRow.Row);
                depEditWindow.ShowDialog();

                if (depEditWindow.DialogResult.HasValue && depEditWindow.DialogResult.Value)
                {
                    editDepRow.EndEdit();
                    adDep.Update(dtDep);
                    dtGeneral.Clear();
                    adGeneral.Fill(dtGeneral);

                }
                else
                {
                    editDepRow.CancelEdit();
                }
            }
            else
                MessageBox.Show("Выберите отдел для редактирования!");
        }

        /// <summary>Обработка нажатия кнопки "редактировать сотрудника"</summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void BtnEditEmp_Click(object sender, RoutedEventArgs e)
        {
            if (empList.SelectedItem != null)
            {
                //EmpEditWindow empEditWindow = new EmpEditWindow(empList.SelectedItem as Employee);
                //empEditWindow.Owner = this;
                //empEditWindow.DataContext = dbd;
                //empEditWindow.cboxDepartment.ItemsSource = dbd.GetDeptaments();
                //empEditWindow.Show();
            }
            else
                MessageBox.Show("Выберите сотрудника для редактирования!");
        }

        /// <summary>Обработка нажатия кнопки "добавить сотрудника"</summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void BtnCreateEmp_Click(object sender, RoutedEventArgs e)
        {
            //AddEmpWindow addEmpWindow = new AddEmpWindow();
            //addEmpWindow.Owner = this;
            //addEmpWindow.DataContext = dbd;
            //addEmpWindow.cboxDepartment.ItemsSource = dbd.GetDeptaments();
            //addEmpWindow.Show();
        }

        /// <summary>Обработка нажатия кнопки "Добавить отдел"</summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void BtnCreateDep_Click(object sender, RoutedEventArgs e)
        {
            AddDepWindow addDepWindow = new AddDepWindow
            {
                Owner = this
            };
            addDepWindow.Show();
        }
    }
}
