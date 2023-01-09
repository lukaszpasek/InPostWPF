using InPost.Model;
using InPost.ViewModels;
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
using InPost.ViewModels;
namespace InPost.View
{
    /// <summary>
    /// Interaction logic for Paczkomat.xaml
    /// </summary>
    public partial class Paczkomat : UserControl
    {
        private readonly PaczkomatViewModel viewModel;
        public InPost.Model.Paczkomat Paczkomat1 { get; set; }
        public Paczkomat()
        {
            InitializeComponent();
            Paczkomat1 = new InPost.Model.Paczkomat(8);
            DataContext = new PaczkomatViewModel(Paczkomat1);
        }

    }
}
