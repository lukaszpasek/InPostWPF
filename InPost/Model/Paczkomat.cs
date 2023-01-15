﻿using System;
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
        private int _nadanePaczki;
        public int NrPaczkomatu => _nrPaczkomatu;
        public ConcurrentQueue<IOperacja> Q;
        private List<Komorka> K { get; }
        public ObservableCollection<OperacjaViewModel> History { get; set; }
        public ObservableCollection<Paczka> PaczkiDoOdebrania { get; set; }
        public ObservableCollection<PaczkomatViewModel> SiecPaczkomatow { get; set; }
        public ObservableCollection<IOperacja> KlienciWKolejce { get; set; }
        public List<Paczka> OtwarteKomorki { get; }
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
            K = new List<Komorka>();
            OtwarteKomorki = new List<Paczka>();
            PaczkiDoOdebrania = new ObservableCollection<Paczka>();
            History = new ObservableCollection<OperacjaViewModel>();
            KlienciWKolejce = new ObservableCollection<IOperacja>();
        }
        private bool ZaladujPaczke(Paczka paczkaDoNadania)
        {
            if (_pos < MAXSIZE)
            {
                paczkaDoNadania.NumerPaczki = _nadanePaczki+1;
                K.Insert(_pos, new Komorka());
                if (K[_pos].Wrzuc(paczkaDoNadania))
                {
                    _pos++;
                    _nadanePaczki++;
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
            if (_pos>=1 && _pos<MAXSIZE)
            {
                Komorka tmp = K.Find(x => x.NumerPaczki == nrPaczki);
                if (tmp is null) return null;
                K.Remove(tmp);
                _pos--;
                return tmp.Paczka;
            }
            return null;
        }
        public bool ObsluzInteresanta()
        {
            IOperacja x;
            
            Q.TryDequeue(out x);
            //View.this.SiecPaczkomatow[0].Paczkomat.KlienciWKolejce.Remove(x);
            
            
            if (x is Dostarczenie)
            {
                Dostarczenie y = (Dostarczenie)x;
                Thread.Sleep(5000);
                return this.ZaladujPaczke(y.Paczka);

            }
            else if(x is Odebranie)
            {
                Odebranie y = (Odebranie)x;
                Thread.Sleep(1000);
                Paczka tmp = OdbierzPaczke(y.NumerPaczki);
                if (tmp is not null)  OtwarteKomorki.Add(tmp);
                else return false;
            }
            else
            {
                Nadanie y = (Nadanie)x;
                Thread.Sleep(1000);
                return true;
            }
            //Random rnd = new Random();
            //History.Insert(0, new OperacjaViewModel(_nrPaczkomatu, x));
            return true;
        }
        public void UstawDoKolejki(IOperacja interesant)
        {
            Q.Enqueue(interesant);
            KlienciWKolejce.Add(interesant);
        }
    }
}

