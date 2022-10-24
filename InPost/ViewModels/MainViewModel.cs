using InPost.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InPost.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand NadajPaczkeCommand { get; set; }
        public ICommand OdbierzPaczkeCommand { get; set; }
        public Paczkomat PaczkomatMain = new Paczkomat(1);
        Task ZadanieZKolejki;
        public ObservableCollection<OperacjaViewModel> MainHistory { get; set; }
        public string Text { get; set; }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public MainViewModel()
        {
            Text = "Paczkomat!!!";
            NadajPaczkeCommand = new RelayCommand(o => NadajPaczkeClick("Nadałeś paczkę!"));
            OdbierzPaczkeCommand = new RelayCommand(o => OdbierzPaczkeClick("Odebrałeś paczkę!"));
            ZadanieZKolejki = new Task(() => PaczkomatMain.ObsluzInteresanta());
            ZadanieZKolejki.Start();
        }

        private void NadajPaczkeClick(object sender)
        {
            Paczka x = new Paczka("Łukasz", "Pasek", "Mateusz", "Pasek", 222);
            Nadanie y = new Nadanie(x, 1);
            PaczkomatMain.UstawDoKolejki(y);
            //ZadanieZKolejki.Start();
            //if (PaczkomatMain.Q.IsEmpty) ZadanieZKolejki.Wait();
            MessageBox.Show(sender.ToString());
        }
        private void OdbierzPaczkeClick(object sender)
        {
            Odebranie y = new Odebranie(222,1);
            PaczkomatMain.UstawDoKolejki(y);
            //ZadanieZKolejki.Start();
            //if (PaczkomatMain.Q.IsEmpty) ZadanieZKolejki.Wait();
            MessageBox.Show(sender.ToString());
        }
    }
}
