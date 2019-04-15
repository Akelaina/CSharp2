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

namespace BaseCompany
{
    /// <summary>
    /// Логика взаимодействия для AddEmpWindow.xaml
    /// </summary>
    public partial class AddEmpWindow : Window
    {
        public AddEmpWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработка кнопки "Добавить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.database.addEmp(tboxName.Text, tboxSurname.Text, tboxAge.Text, tboxSalary.Text, uint.Parse(tboxDepartment.Text)))
            {
                MessageBox.Show("Новый сотрудник добавлен!");
                this.Close();
            }
            else
                MessageBox.Show("Такой сотрудник уже существует или введены неправильные данные!");
        }
    }
}
