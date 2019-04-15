using System;
using System.Collections.Generic;
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
            tboxName.Text = employee.Name;
            tboxSurname.Text = employee.Surname;
            tboxAge.Text = employee.Age.ToString();
            tboxSalary.Text = employee.Salary.ToString();
            tboxDep.Text = employee.DepartmentID.ToString();

        }

        /// <summary>
        /// Обработка кнопки "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.database.editEmp(oldemp, tboxName.Text, tboxSurname.Text, tboxAge.Text, tboxSalary.Text, uint.Parse(tboxDep.Text)))
            {
                MessageBox.Show("Все данные внесены в базу!");
                this.Close();
            }
            else
                MessageBox.Show("Такой сотрудник уже существует, проверьте правильность введенных данных!");
        }
    }
}
