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
using System.Diagnostics;
using System.Windows.Automation;

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
            NadajPaczkeCommand = new RelayCommand(o => AutoMode());
            OdbierzPaczkeCommand = new RelayCommand(o => AutoMode());
            uiContext = SynchronizationContext.Current;

            var list = new ObservableCollection<DataObject>();
            list.Add(new DataObject() { A = 6, B = 7, C = 5 });
            list.Add(new DataObject() { A = 5, B = 8, C = 4 });
            list.Add(new DataObject() { A = 4, B = 3, C = 0 });
      

        }

        public async void AutoMode()
        {
            Random rnd = new Random();
            for(int i=0; i<1000;i+=2)
            {
                int nrPaczkomatu = rnd.Next(1, 4);
                uiContext.Post(x => Paczkomat.SiecPaczkomatow[nrPaczkomatu - 1].NadajPaczkeClick("Nadałeś paczkę!",i), null);
                nrPaczkomatu = rnd.Next(1, 4);
                uiContext.Post(x => Paczkomat.SiecPaczkomatow[nrPaczkomatu - 1].OdbierzPaczkeClick("Odebrałeś paczkę!",i+1), null);
                await Task.Delay(1000);
            }
        }
        private async void NadajPaczkeClick(object sender,int idZlecenia)
        {
            Paczka x = new Paczka(true);
            //Task.Run(() => Paczkomat.SiecPaczkomatow[1].AutoMode());
            
            int doKtoregoPaczkomatu = 0;
            //Debug.WriteLine(x.NazwiskoOdbiorcy[0]);
            if (x.NazwiskoOdbiorcy is null) return;
            if (x.NazwiskoOdbiorcy[0] < 'K') doKtoregoPaczkomatu = 1;
            else if (x.NazwiskoOdbiorcy[0] >= 'K' && x.NazwiskoOdbiorcy[0] < 'S') doKtoregoPaczkomatu = 2;
            else doKtoregoPaczkomatu = 3;
            Nadanie _x = new Nadanie(x, doKtoregoPaczkomatu);
           
            Dostarczenie y = new Dostarczenie(x);
            y.IdZlecenia = idZlecenia;
            //SiecPaczkomatow.UstawDoKolejki(y);
            if (x.ImieNadawcy is not null && x.NazwiskoNadawcy is not null && x.ImieOdbiorcy is not null && x.NazwiskoOdbiorcy is not null)
            {
                Paczkomat.SiecPaczkomatow[Paczkomat.NrPaczkomatu - 1].Paczkomat.KlienciWKolejce.Add(_x);
                await Task.Delay(1000);
                Paczkomat.SiecPaczkomatow[doKtoregoPaczkomatu - 1].Paczkomat.UstawDoKolejki(y);

                //ZadanieZKolejki.Start();

                //Task.Run(() => PaczkomatMain.ObsluzInteresanta());
                this.Paczkomat.KlienciWKolejce.Remove(_x);
                Paczkomat.History.Insert(0, new OperacjaViewModel(Paczkomat.NrPaczkomatu, _x));
                var myTask = Task.Run(() => Paczkomat.SiecPaczkomatow[doKtoregoPaczkomatu - 1].Paczkomat.ObsluzInteresanta(y));
                bool pom = await myTask;
                
                Paczkomat.SiecPaczkomatow[doKtoregoPaczkomatu - 1].Paczkomat.KlienciWKolejce.Remove(y);
                if (!pom) 
                {
                    // MessageBox.Show("Nie można nadać paczki.Paczkomat Pełny!");
                    return; 
                }
                //MessageBox.Show(sender.ToString());
                Paczkomat.SiecPaczkomatow[doKtoregoPaczkomatu - 1].Paczkomat.History.Insert(0, new OperacjaViewModel(Paczkomat.NrPaczkomatu, y));
                
                Paczkomat.SiecPaczkomatow[doKtoregoPaczkomatu - 1].Paczkomat.PaczkiDoOdebrania.Add(y.Paczka);
            }
            else if(x !=null)
            {
                MessageBox.Show("Nieprawidlowe dane!");
            }
            //PaczkomatMain.History.Add(new OperacjaViewModel(1, y));
            //MainHistory.Add(new OperacjaViewModel(1, y));
            //MainHistory = PaczkomatMain.History;
        }
        private async void OdbierzPaczkeClick(object sender,int idZlecenia)
        {
            Odebranie y = new Odebranie(Paczkomat.NrPaczkomatu);
            y.IdZlecenia = idZlecenia;
            Paczkomat.UstawDoKolejki(y);
            // while (!PaczkomatMain.Q.IsEmpty)
            //{
            //  uiContext.Send(x => PaczkomatMain.ObsluzInteresanta(), null);
            //}
            //ZadanieZKolejki.Start();
            //if (PaczkomatMain.Q.IsEmpty) ZadanieZKolejki.Wait();
            var myTask = Task.Run(() => Paczkomat.ObsluzInteresanta(y));
            bool pom = await myTask;
            //Wait(10);
            Paczkomat.KlienciWKolejce.Remove(y);
            if (!Paczkomat.OtwarteKomorki.Exists(x => x.NumerPaczki == y.NumerPaczki)) 
            { 
               // MessageBox.Show("Nie ma takiej paczki!"); 
                return; 
            }
            Paczka x = Paczkomat.OtwarteKomorki.Find(x => x.NumerPaczki == y.NumerPaczki);
            Paczkomat.OtwarteKomorki.RemoveAt(Paczkomat.OtwarteKomorki.FindIndex(x => x.NumerPaczki == y.NumerPaczki));
            //MessageBox.Show(sender.ToString());
            Paczkomat.History.Insert(0, new OperacjaViewModel(Paczkomat.NrPaczkomatu, y));
            Paczkomat.PaczkiDoOdebrania.Remove(x);
            //MainHistory = PaczkomatMain.History;

        }

    }
}
