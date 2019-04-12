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

namespace BaseCompany
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<string> Department = new List<string>()
            {
                "Отдел 1",
                "Отдел 2",
                "Отдел 3",
                "Отдел 4",
                "Отдел 5",
            };

            List<string> Employee = new List<string>()
            {
                "Имя Фамилия 1",
                "Имя Фамилия 2",
                "Имя Фамилия 3",
                "Имя Фамилия 4",
                "Имя Фамилия 5",
                "Имя Фамилия 6",
                "Имя Фамилия 7",
                "Имя Фамилия 8",
            };

            DepList.ItemsSource = Department;
            EmpList.ItemsSource = Employee;

        }

        private void AddDep_Click(object sender, RoutedEventArgs e)
        {
           new AddDepWindow().Show();
        }

        private void EditDep_Click(object sender, RoutedEventArgs e)
        {
            new EditDepWindow().Show();
        }

        private void AddEmp_Click(object sender, RoutedEventArgs e)
        {
            new AddEmpWindow().Show();
        }

        private void EditEmp_Click(object sender, RoutedEventArgs e)
        {
            new EditEmpWindow().Show();
        }
    }
}
