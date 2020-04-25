using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilkarzeMVVM.Model
{
    
    class Pilkarze
    {
        private string plikArchiwizacji = "archiwum.txt";

        public List<Pilkarz> ListaPilkarzy { get { return listaPilkarzy; } }

        private List<Pilkarz> listaPilkarzy = new List<Pilkarz>();
        public void AddPilkarz(Pilkarz pilkarz)
        {
            listaPilkarzy.Add(pilkarz);
        }
        public void RemovePilkarz(Pilkarz pilkarz)
        {
            listaPilkarzy.Remove(pilkarz);
        }
        public bool CzyIstniejePilkarz(Pilkarz pilkarz)
        {
            
            foreach (var p in ListaPilkarzy)
            {
                var p1 = p as Pilkarz;
                if (p1.isTheSame(pilkarz))
                {
                    return true;
                   
                }
            }
            return false;
        }
        public bool EditPilkarz(Pilkarz pilkarzOryginalny, Pilkarz pilkarzEdytowany)
        {
            if (!pilkarzOryginalny.isTheSame(pilkarzEdytowany))
            {
                ListaPilkarzy[ListaPilkarzy.IndexOf(pilkarzOryginalny)].Imie = pilkarzEdytowany.Imie;
                ListaPilkarzy[ListaPilkarzy.IndexOf(pilkarzOryginalny)].Nazwisko = pilkarzEdytowany.Nazwisko;
                ListaPilkarzy[ListaPilkarzy.IndexOf(pilkarzOryginalny)].Wiek = pilkarzEdytowany.Wiek;
                ListaPilkarzy[ListaPilkarzy.IndexOf(pilkarzOryginalny)].Waga = pilkarzEdytowany.Waga;
                return true;
            }
            else
                return false;
        }
        public void ZapiszPilkarzy()
        {
            int n = listaPilkarzy.Count;
            
            if (n > 0)
            {
                Pilkarz[] pilkarze = new Pilkarz[n];
                int index = 0;
                foreach (var o in ListaPilkarzy)
                {
                    pilkarze[index++] = o as Pilkarz;
                }
                Archiwizacja.ZapisPilkarzyDoPliku(plikArchiwizacji, pilkarze);
            }
            else
            {
                Pilkarz[] pilkarze = null;
                Archiwizacja.ZapisPilkarzyDoPliku(plikArchiwizacji, pilkarze);
            }
                

        }
        public void WczytajPilkarzy()
        {
            var pilkarze = Archiwizacja.CzytajPilkarzyZPliku(plikArchiwizacji);
            if (pilkarze != null)
                foreach (var p in pilkarze)
                {
                    listaPilkarzy.Add(p);
                }
        }
    }
}
