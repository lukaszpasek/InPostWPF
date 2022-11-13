using InPost.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace InPost.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand NadajPaczkeCommand { get; set; }
        public ICommand OdbierzPaczkeCommand { get; set; }
        public Paczkomat Paczkomat1 { get; set; }
        public Paczkomat Paczkomat2 { get; set; }
        public Paczkomat Paczkomat3 { get; set; }
        Task ZadanieZKolejki;
        SynchronizationContext uiContext;
        public ObservableCollection<OperacjaViewModel> MainHistory { get; set; }
        public ObservableCollection<PaczkomatViewModel> SiecPaczkomatow { get; set; }
        public string Text { get; set; }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public MainViewModel()
        {
            Text = "Paczkomat!!!";
            MainHistory = new ObservableCollection<OperacjaViewModel>();
            SiecPaczkomatow = new ObservableCollection<PaczkomatViewModel>();
            Paczkomat1 = new Paczkomat(1);
            SiecPaczkomatow.Add(new PaczkomatViewModel(Paczkomat1));
            Paczkomat2 = new Paczkomat(2);
            SiecPaczkomatow.Add(new PaczkomatViewModel(Paczkomat2));
            Paczkomat3 = new Paczkomat(3);
            SiecPaczkomatow.Add(new PaczkomatViewModel(Paczkomat3));
            //ZadanieZKolejki = new Task(() => PaczkomatMain.ObsluzInteresanta());
            //ZadanieZKolejki.Start();
        }

        
    }
}
