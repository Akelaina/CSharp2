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

namespace Company.Windows
{
    /// <summary>
    /// Interaction logic for AddEmpWindow.xaml
    /// </summary>
    public partial class AddEmpWindow : Window
    {
        public AddEmpWindow()
        {
            InitializeComponent();
        }

        /// <summary>Обработчик нажатия кнопки "Cохранить"</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //if (MainWindow.dbd.addEmp(tboxName.Text, tboxSurname.Text, tboxAge.Text, tboxSalary.Text, 
            //    (cboxDepartment.SelectedItem as Department).DepartmentID))
            //{
            //    MessageBox.Show("Сотрудник добавлен!");
            //    this.Close();
            //}
            //else
            //    MessageBox.Show("Такой сотрудник уже существует или введены некоректные данные!");
        }
    }
}
