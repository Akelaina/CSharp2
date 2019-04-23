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

namespace Company
{
    /// <summary>
    /// Interaction logic for DepEditWindow.xaml
    /// </summary>
    public partial class DepEditWindow : Window
    {
        public DataRow resultRow { get; set; }
        public DepEditWindow(DataRow dataRow)
        {
            InitializeComponent();
            resultRow = dataRow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tblOldName.Text = resultRow["dName"].ToString();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            resultRow["dName"] = tboxNewName.Text;
            this.DialogResult = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
