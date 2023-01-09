using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows;
using InPost.Model;
using System.Windows.Input;
using System.Threading;
using System.Windows.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace InPost.ViewModels
{
    public class PaczkomatViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand NadajPaczkeCommand { get; set; }
        public ICommand OdbierzPaczkeCommand { get; set; }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        SynchronizationContext uiContext;
        public Paczkomat Paczkomat { get; set; }
        public string TEST = "TEST";
        public class DataObject
        {
            public int A { get; set; }
            public int B { get; set; }
            public int C { get; set; }
        }
        public ObservableCollection<DataObject> list;
        public int IleOperacjiPokazac => Math.Min(Paczkomat.History.Count, 4);
        public PaczkomatViewModel(Paczkomat paczkomat)
        {
            Paczkomat = paczkomat;
            NadajPaczkeCommand = new RelayCommand(o => NadajPaczkeClick("Nadałeś paczkę!"));
            OdbierzPaczkeCommand = new RelayCommand(o => OdbierzPaczkeClick("Odebrałeś paczkę!"));
            uiContext = SynchronizationContext.Current;

            var list = new ObservableCollection<DataObject>();
            list.Add(new DataObject() { A = 6, B = 7, C = 5 });
            list.Add(new DataObject() { A = 5, B = 8, C = 4 });
            list.Add(new DataObject() { A = 4, B = 3, C = 0 });

        }
        private async void NadajPaczkeClick(object sender)
        {
            Paczka x = new Paczka("Łukasz", "Pasek", "Mateusz", "Pasek", 222);
            Nadanie y = new Nadanie(x, 1);
            //SiecPaczkomatow.UstawDoKolejki(y);
            Paczkomat.UstawDoKolejki(y);
            //ZadanieZKolejki.Start();

            //Task.Run(() => PaczkomatMain.ObsluzInteresanta());

            MessageBox.Show(sender.ToString());
            uiContext.Post(x => Paczkomat.ObsluzInteresanta(), null);
            //PaczkomatMain.History.Add(new OperacjaViewModel(1, y));
            //MainHistory.Add(new OperacjaViewModel(1, y));
            //MainHistory = PaczkomatMain.History;
        }
        private async void OdbierzPaczkeClick(object sender)
        {
            Odebranie y = new Odebranie(222, 2);
            Paczkomat.UstawDoKolejki(y);

            // while (!PaczkomatMain.Q.IsEmpty)
            //{
            //  uiContext.Send(x => PaczkomatMain.ObsluzInteresanta(), null);
            //}
            //ZadanieZKolejki.Start();
            //if (PaczkomatMain.Q.IsEmpty) ZadanieZKolejki.Wait();
            MessageBox.Show(sender.ToString());
            uiContext.Post(x => Paczkomat.ObsluzInteresanta(), null);
            //MainHistory = PaczkomatMain.History;

        }

    }
}
