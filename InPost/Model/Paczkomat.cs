using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
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
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InPost.Model
{
    public class Paczkomat : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        const int MAXSIZE = 32;
        private int _pos = 0;
        private int _nrPaczkomatu;
        public ConcurrentQueue<IOperacja> Q;
        private Komorka[] K { get; }
        public ObservableCollection<OperacjaViewModel> History { get; set; }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        string text = "PaczkomatNr";
        public int IleOperacjiPokazac => Math.Min(History.Count, 3);
        public string Text
        {
            get { return text+_nrPaczkomatu as string; }
            set
            {
                text = value;
            }
        }
        public Paczkomat(int numerPaczkomatu)
        {
            _nrPaczkomatu = numerPaczkomatu;
            Q = new ConcurrentQueue<IOperacja>();
            K = new Komorka[MAXSIZE];
            History = new ObservableCollection<OperacjaViewModel>();
        }
        private bool NadajPaczke(Paczka paczkaDoNadania)
        {
            if (_pos < MAXSIZE)
            {
                if (K[_pos].Wrzuc(paczkaDoNadania))
                {
                    _pos++;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }
        private Paczka OdbierzPaczke(int nrPaczki)
        {
            if (nrPaczki < MAXSIZE)
            {
                return K[nrPaczki].Odbierz;
            }
            return null;
        }
        public async void ObsluzInteresanta()
        {
            IOperacja x,y;
            await Task.Delay(200);
            Q.TryDequeue(out x);
            if(x is Nadanie)
            {
                y = (Nadanie)x;
            }
            else
            {
                y = (Odebranie)x;
            }
            //Random rnd = new Random();
            History.Insert(0, new OperacjaViewModel(_nrPaczkomatu, x));
        }
        public void UstawDoKolejki(IOperacja interesant)
        {
            Q.Enqueue(interesant);
        }
    }
}

