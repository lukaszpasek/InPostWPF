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

namespace InPost.Helpers.View
{
    /// <summary>
    public partial class DialogOperacja : Window
    {
        public DialogOperacja(string question1, string question2, string defaultAnswer1 = "", string defaultAnswer2 = "")
        {
            InitializeComponent();
            lblQuestion1.Content = question1;
            txtAnswer1.Text = defaultAnswer1;
            lblQuestion2.Content = question2;
            txtAnswer2.Text = defaultAnswer2;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtAnswer1.SelectAll();
            txtAnswer1.Focus();
            txtAnswer2.SelectAll();
            txtAnswer2.Focus();
        }

        public string Answer1
        {
            get { return txtAnswer1.Text; }
        }
        public string Answer2
        {
            get { return txtAnswer2.Text; }
        }
    }
}
