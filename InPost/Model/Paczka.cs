﻿using InPost.Helpers.View;
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

namespace InPost.Model
{
    public class Paczka
    {
        private double _wysokosc;
        private double _szerokosc;
        private double _waga;
        private int _numerPaczki;
        private string _nazwiskoNadawcy;
        private string _imieNadawcy;
        private string _nazwiskoOdbiorcy;
        private string _imieOdbiorcy;
        private int _numerPrzewozowy;
        public string ImieNadawcy => _imieNadawcy;
        public string NazwiskoNadawcy => _nazwiskoNadawcy;

        public string ImieOdbiorcy => _imieOdbiorcy;
        public string NazwiskoOdbiorcy => _nazwiskoOdbiorcy;

        public Paczka(string imieNadawcy,string nazwiskoNadawcy,string imieOdbiorcy,string nazwiskoOdbiorcy,int numerPaczki)
        {
            _imieNadawcy = imieNadawcy;
            _nazwiskoNadawcy = nazwiskoNadawcy;
            _imieOdbiorcy = imieOdbiorcy;
            _nazwiskoOdbiorcy = nazwiskoOdbiorcy;
            _numerPaczki = numerPaczki;
            Random rnd = new Random();
            _numerPrzewozowy = rnd.Next(1000, 9999);
        }
        public Paczka()
        {
            ;
        }
        public Paczka(bool startDialog)
        {
            /*DialogOperacja inputDialog = new DialogOperacja("Podaj imie i nazwisko nadawcy:", "Podaj imie i nazwisko adresata:", "Jan Kowalski", "Jan Nowak");
            if (inputDialog.ShowDialog() == true && inputDialog.Answer1.Contains(" ") && inputDialog.Answer2.Contains(" "))
            {
                _imieNadawcy = inputDialog.Answer1.Substring(0, inputDialog.Answer1.IndexOf(' '));
                _nazwiskoNadawcy = inputDialog.Answer1.Substring(inputDialog.Answer1.IndexOf(' ') + 1);
                _imieOdbiorcy = inputDialog.Answer2.Substring(0, inputDialog.Answer2.IndexOf(' '));
                _nazwiskoOdbiorcy = inputDialog.Answer2.Substring(inputDialog.Answer2.IndexOf(' ') + 1);
                if (_imieNadawcy == "" || _nazwiskoNadawcy == "" || _imieOdbiorcy == "" || _nazwiskoOdbiorcy == "")
                {
                    MessageBox.Show("Nieprawidlowe dane!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Niepowodzenie utworzenia paczki!");
                return;
            }*/
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789qwertyuiopasdfghjklzxcvbnm,.;'[`=+";
            
         
            Random rnd = new Random();
            _numerPrzewozowy = rnd.Next(1000, 9999);
            _imieNadawcy = new string(Enumerable.Repeat(chars, 6)
               .Select(s => s[rnd.Next(s.Length)]).ToArray());
            _imieOdbiorcy = new string(Enumerable.Repeat(chars, 6)
               .Select(s => s[rnd.Next(s.Length)]).ToArray());
            _nazwiskoOdbiorcy = new string(Enumerable.Repeat(chars, 6)
             .Select(s => s[rnd.Next(s.Length)]).ToArray());
            _nazwiskoNadawcy = new string(Enumerable.Repeat(chars, 6)
             .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
        public int NumerPaczki { get; set; }
        public int NumerPrzewozowy => _numerPrzewozowy;
    }
}

