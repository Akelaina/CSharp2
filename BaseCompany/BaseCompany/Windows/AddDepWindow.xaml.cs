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
    /// Логика взаимодействия для AddDepWindow.xaml
    /// </summary>
    public partial class AddDepWindow : Window
    {

        public AddDepWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработка кнопки "Добавить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveDep_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.database.addDep(tboxNewDep.Text))
            {
                MessageBox.Show("Новый отдел добавлен!");
                this.Close();
            }
            else
                MessageBox.Show("Такой отдел уже существует!");
        }
    }
}