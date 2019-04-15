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
using System.Windows.Navigation;
using System.Windows.Shapes;

using BaseCompany.Classes;

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
        internal static db database;

        public MainWindow()
        {
            InitializeComponent();
            database = new db();
            empList.ItemsSource = database.GetEmployees();
            cbDepList.ItemsSource = database.GetDepartments();
            this.DataContext = database;
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
    }
}
