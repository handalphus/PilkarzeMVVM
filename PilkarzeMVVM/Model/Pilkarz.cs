using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace PilkarzeMVVM.Model
{
    [DataContract] public class Pilkarz
    {
        [DataMember] public string Imie { get; set; }
        [DataMember] public string Nazwisko { get; set; }
        [DataMember] public uint Wiek { get; set; }
        [DataMember] public uint Waga { get; set; }
        public Pilkarz(string imie, string nazwisko, uint wiek, uint waga)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Wiek = wiek;
            Waga = waga;
        }
        public bool isTheSame(Pilkarz pilkarz)
        {
            if (pilkarz.Nazwisko != Nazwisko) return false;
            if (pilkarz.Imie != Imie) return false;
            if (pilkarz.Wiek != Wiek) return false;
            if (pilkarz.Waga != Waga) return false;
            return true;
        }

        public override string ToString()
        {
            return $"{Nazwisko} {Imie} lat: {Wiek} waga: {Waga} kg";
        }

    }
}
