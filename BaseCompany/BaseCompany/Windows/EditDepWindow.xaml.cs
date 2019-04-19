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
    /// Логика взаимодействия для EditDepWindow.xaml
    /// </summary>
    public partial class EditDepWindow : Window
    {
        uint depid;

        public EditDepWindow(uint id, string oldName)
        {
            InitializeComponent();
            tblOldName.Text = oldName;
            depid = id;
        }

        /// <summary>
        /// Обработка кнопки "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.database.editDep(tboxNewName.Text, depid))
            {
                MessageBox.Show("Новое название добавлено в базу!");
                this.Close();
            }
            else
                MessageBox.Show("Такое название уже существует!");
        }

    }
}