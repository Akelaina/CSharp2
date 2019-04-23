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

namespace Company
{
    /// <summary>
    /// Interaction logic for EmpEditWindow.xaml
    /// </summary>
    public partial class EmpEditWindow : Window
    {
        
        internal EmpEditWindow()
        {
            InitializeComponent();
            //oldemp = employee;
            //tboxName.Text = employee.Name;
            //tboxSurname.Text = employee.Surname;
            //tboxAge.Text = employee.Age.ToString();
            //tboxSalary.Text = employee.Salary.ToString();
            //cboxDepartment.SelectedIndex = (int)employee.DepartmentID;

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //if (MainWindow.dbd.editEmp(oldemp, tboxName.Text, tboxSurname.Text, tboxAge.Text, tboxSalary.Text, 
            //    (cboxDepartment.SelectedItem as Department).DepartmentID))
            //{
            //    MessageBox.Show("Данные о сотруднике изменены!");
            //    this.Close();
            //}
            //else
            //    MessageBox.Show("Такой сотрудник уже существует или введены некоректные данные!");
        }
    }
}
