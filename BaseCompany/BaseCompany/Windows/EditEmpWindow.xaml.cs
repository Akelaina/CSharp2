using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using BaseCompany.Classes;

namespace BaseCompany
{
    /// <summary>
    /// Логика взаимодействия для EditEmpWindow.xaml
    /// </summary>
    public partial class EditEmpWindow : Window
    {
        Employee oldemp;
        internal EditEmpWindow(Employee employee)
        {
            InitializeComponent();
            oldemp = employee;
            NameColumn.Text = employee.Name;
            SurnameColumn.Text = employee.Surname;
            AgeColumn.Text = employee.Age.ToString();
            SalaryColumn.Text = employee.Salary.ToString();
            DepartmentColumn.Text = employee.DepartmentID.ToString();

        }

        public DataRow resultRow { get; set; }
        public EditEmpWindow(DataRow dataRow)
        {
            InitializeComponent();
            resultRow = dataRow;

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NameColumn.Text = resultRow["Имя"].ToString();
            SurnameColumn.Text = resultRow["Фамилия"].ToString();
            AgeColumn.Text = resultRow["Возраст"].ToString();
            SalaryColumn.Text = resultRow["Зарплата"].ToString();
            DepartmentColumn.Text = resultRow["Отдел"].ToString();
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            resultRow["Имя"] = NameColumn.Text;
            resultRow["Фамилия"] = SurnameColumn.Text;
            resultRow["Возраст"] = AgeColumn.Text;
            resultRow["Зарплата"] = SalaryColumn.Text;
            resultRow["Отдел"] = DepartmentColumn.Text;
            this.DialogResult = true;
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        

        /// <summary>
        /// Обработка кнопки "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void BtnSave_Click(object sender, RoutedEventArgs e)
        //{
        //    if (MainWindow.database.editEmp(oldemp, NameColumn.Text, NameColumn.Text, AgeColumn.Text, SalaryColumn.Text, uint.Parse(DepartmentColumn.Text)))
        //    {
        //        MessageBox.Show("Все данные внесены в базу!");
        //        this.Close();
        //    }
        //    else
        //        MessageBox.Show("Такой сотрудник уже существует, проверьте правильность введенных данных!");
        //}
    }
}
