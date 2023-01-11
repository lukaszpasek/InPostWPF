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
using InPost.Helpers.View;

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
            Paczka x = new Paczka(true);
            Nadanie y = new Nadanie(x, Paczkomat.NrPaczkomatu);

            //SiecPaczkomatow.UstawDoKolejki(y);
            if (x.ImieNadawcy is not null && x.NazwiskoNadawcy is not null && x.ImieOdbiorcy is not null && x.NazwiskoOdbiorcy is not null)
            {
                Paczkomat.UstawDoKolejki(y);
                //ZadanieZKolejki.Start();

                //Task.Run(() => PaczkomatMain.ObsluzInteresanta());

                var myTask = Task.Run(() => Paczkomat.ObsluzInteresanta());
                bool pom = await myTask;
                if (!pom) { MessageBox.Show("Nie można nadać paczki.Paczkomat Pełny!"); return; }
                MessageBox.Show(sender.ToString());
                Paczkomat.History.Insert(0, new OperacjaViewModel(Paczkomat.NrPaczkomatu, y));
                Paczkomat.PaczkiDoOdebrania.Add(y.Paczka);
            }
            else if(x !=null)
            {
                MessageBox.Show("Nieprawidlowe dane!");
            }
            //PaczkomatMain.History.Add(new OperacjaViewModel(1, y));
            //MainHistory.Add(new OperacjaViewModel(1, y));
            //MainHistory = PaczkomatMain.History;
        }
        private async void OdbierzPaczkeClick(object sender)
        {
            Odebranie y = new Odebranie(Paczkomat.NrPaczkomatu);
            Paczkomat.UstawDoKolejki(y);

            // while (!PaczkomatMain.Q.IsEmpty)
            //{
            //  uiContext.Send(x => PaczkomatMain.ObsluzInteresanta(), null);
            //}
            //ZadanieZKolejki.Start();
            //if (PaczkomatMain.Q.IsEmpty) ZadanieZKolejki.Wait();
            var myTask = Task.Run(() => Paczkomat.ObsluzInteresanta());
            bool pom = await myTask;
            if (!Paczkomat.OtwarteKomorki.Exists(x => x.NumerPaczki == y.NumerPaczki)) { MessageBox.Show("Nie ma takiej paczki!"); return; }
            Paczka x = Paczkomat.OtwarteKomorki.Find(x => x.NumerPaczki == y.NumerPaczki);
            Paczkomat.OtwarteKomorki.RemoveAt(Paczkomat.OtwarteKomorki.FindIndex(x => x.NumerPaczki == y.NumerPaczki));
            MessageBox.Show(sender.ToString());
            Paczkomat.History.Insert(0, new OperacjaViewModel(Paczkomat.NrPaczkomatu, y));
            Paczkomat.PaczkiDoOdebrania.Remove(x);
            //MainHistory = PaczkomatMain.History;

        }

    }
}
