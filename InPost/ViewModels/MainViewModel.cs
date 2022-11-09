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
        public Paczkomat PaczkomatMain { get; set; }
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
            PaczkomatMain = new Paczkomat(1);
            SiecPaczkomatow.Add(new PaczkomatViewModel(PaczkomatMain));
            ZadanieZKolejki = new Task(() => PaczkomatMain.ObsluzInteresanta());
            
            //ZadanieZKolejki = new Task(() => PaczkomatMain.ObsluzInteresanta());
            //ZadanieZKolejki.Start();
        }

        
    }
}
