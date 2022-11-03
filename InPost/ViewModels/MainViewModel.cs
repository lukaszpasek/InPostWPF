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
        public Collection<PaczkomatViewModel> SiecPaczkomatow { get; set; }
        public string Text { get; set; }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public MainViewModel()
        {
            Text = "Paczkomat!!!";
            MainHistory = new ObservableCollection<OperacjaViewModel>();
            PaczkomatMain = new Paczkomat(1);
            uiContext =  SynchronizationContext.Current;
            ZadanieZKolejki = new Task(() => PaczkomatMain.ObsluzInteresanta());
            NadajPaczkeCommand = new RelayCommand(o => NadajPaczkeClick("Nadałeś paczkę!"));
            OdbierzPaczkeCommand = new RelayCommand(o => OdbierzPaczkeClick("Odebrałeś paczkę!"));
            //ZadanieZKolejki = new Task(() => PaczkomatMain.ObsluzInteresanta());
            //ZadanieZKolejki.Start();
        }

        private async void NadajPaczkeClick(object sender)
        {
            Paczka x = new Paczka("Łukasz", "Pasek", "Mateusz", "Pasek", 222);
            Nadanie y = new Nadanie(x, 1);
            PaczkomatMain.UstawDoKolejki(y);
        
            //ZadanieZKolejki.Start();
            
                //Task.Run(() => PaczkomatMain.ObsluzInteresanta());
           
            MessageBox.Show(sender.ToString());
            uiContext.Post(x => PaczkomatMain.ObsluzInteresanta(), null);
            //PaczkomatMain.History.Add(new OperacjaViewModel(1, y));
            //MainHistory.Add(new OperacjaViewModel(1, y));
            //MainHistory = PaczkomatMain.History;
        }
        private async void OdbierzPaczkeClick(object sender)
        {
            Odebranie y = new Odebranie(222,2);
            PaczkomatMain.UstawDoKolejki(y);

           // while (!PaczkomatMain.Q.IsEmpty)
            //{
              //  uiContext.Send(x => PaczkomatMain.ObsluzInteresanta(), null);
            //}
            //ZadanieZKolejki.Start();
            //if (PaczkomatMain.Q.IsEmpty) ZadanieZKolejki.Wait();
            MessageBox.Show(sender.ToString());
            uiContext.Post(x => PaczkomatMain.ObsluzInteresanta(), null);
            //MainHistory = PaczkomatMain.History;

        }
    }
}
