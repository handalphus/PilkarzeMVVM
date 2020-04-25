using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
namespace PilkarzeMVVM.ViewModel
{
    using Model;
    using BaseClass;
    using System.Windows;

    internal class PilkarzeVM: ViewModelBase
    {
        private Pilkarze pilkarze = new Pilkarze();
        
        private string imie = null;
        private string nazwisko = null;
       
        private uint wiek = 25;
        private uint waga = 75;
        
        public ObservableCollection<Pilkarz> ZbiorPilkarzy
        {
            get { ObservableCollection<Pilkarz> zbiorPilkarzy = new ObservableCollection<Pilkarz>();
                foreach (var item in pilkarze.ListaPilkarzy)
                {
                    zbiorPilkarzy.Add(item);
                }
                return zbiorPilkarzy;
            }
            
        }
        private Pilkarz zaznaczonyPilkarz = null;
        public Pilkarz ZaznaczonyPilkarz
        {
            get { return zaznaczonyPilkarz; }
            set { zaznaczonyPilkarz = value;
                if (zaznaczonyPilkarz != null)
                {
                    Imie = zaznaczonyPilkarz.Imie;
                    Nazwisko = zaznaczonyPilkarz.Nazwisko;
                    Wiek = zaznaczonyPilkarz.Wiek;
                    Waga = zaznaczonyPilkarz.Waga;
                }
            }
        }
        
        
       
        public PilkarzeVM()
        {
            pilkarze.WczytajPilkarzy();

        }
        public string Imie
        {
            get { if (imie != null) return imie.Trim();
                else return null;
            }
            set { imie = value;
                onPropertyChanged(nameof(Imie));
            }
        }
        public string Nazwisko
        {
            get {
                if (nazwisko != null) return nazwisko.Trim();
                else return null;
            }
            set { nazwisko = value;
                onPropertyChanged(nameof(Nazwisko));
            }
        }
        public uint Wiek
        {
            get { return wiek; }
            set { wiek = value;
                onPropertyChanged(nameof(Wiek));
            }
        }
        public uint Waga
        {
            get { return waga; }
            set { waga = value;
                onPropertyChanged(nameof(Waga));
            }
        }
        private ICommand _add = null;
        public ICommand Add
        {
            get
            {
                if (_add == null)
                {

                    _add = new RelayCommand(
                        arg =>
                        {
                            if (!string.IsNullOrEmpty(Imie) && !string.IsNullOrEmpty(Nazwisko))
                            {
                                Pilkarz pilkarzDoDodania = new Pilkarz(Imie, Nazwisko, Wiek, Waga);
                                if (!pilkarze.CzyIstniejePilkarz(pilkarzDoDodania))
                                {
                                    pilkarze.AddPilkarz(pilkarzDoDodania);
                                    onPropertyChanged(nameof(ZbiorPilkarzy));
                                    Clear();
                                }
                                else
                                {
                                    var dialog = MessageBox.Show($"{pilkarzDoDodania.ToString()} już jest na liście {Environment.NewLine} Czy wyczyścić formularz?", "Uwaga", MessageBoxButton.OKCancel);
                                    if (dialog == MessageBoxResult.OK)
                                    {
                                        Clear();
                                    }
                                }
                            }
                            
                            
                            
                        },
                        arg => true
                        );;
                }
                return _add;
            }
        }
        private ICommand _save = null;
        public ICommand SaveCommand
        {
            get
            {
                if (_save == null)
                {       _save = new RelayCommand(
                        arg =>
                        {
                            pilkarze.ZapiszPilkarzy();
                        },
                        arg => true
                        );
                }
                return _save;
            }
        }
        
        private ICommand _remove = null;
        public ICommand Remove
        {
            get
            {
                if (_remove == null)
                {
                    _remove = new RelayCommand(
                        arg => { 
                            var dialog = MessageBox.Show($"Czy na pewno chcesz usunąć {zaznaczonyPilkarz.ToString()}?", "Uwaga", MessageBoxButton.OKCancel);
                            if (dialog == MessageBoxResult.OK)
                            {
                                pilkarze.RemovePilkarz(zaznaczonyPilkarz);
                                ZaznaczonyPilkarz = null;
                                onPropertyChanged(nameof(ZbiorPilkarzy));
                                Clear();
                                
                            }
                            
                        },
                        arg => pilkarze.CzyIstniejePilkarz(new Pilkarz(Imie, Nazwisko, Wiek, Waga)));

                }
                return _remove;
            }

        }

        private ICommand _editCommand = null;
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand = new RelayCommand(
                        arg =>
                        {
                                if (pilkarze.EditPilkarz(zaznaczonyPilkarz, new Pilkarz(Imie, Nazwisko, Wiek, Waga)))
                            {
                                onPropertyChanged(nameof(ZbiorPilkarzy));
                                var dialog = MessageBox.Show("Edytowano pomyślnie", "Edycja", MessageBoxButton.OK);
                                ZaznaczonyPilkarz = null;
                                onPropertyChanged(nameof(ZaznaczonyPilkarz));
                                Clear();
                            }
                            else
                            {
                                var dialog = MessageBox.Show("Dane były niezmienione", "Edycja", MessageBoxButton.OK);
                                ZaznaczonyPilkarz = null;
                                onPropertyChanged(nameof(ZaznaczonyPilkarz));
                                Clear();
                            } 
                            
                        },
                        arg => (!string.IsNullOrEmpty(Imie) && !string.IsNullOrEmpty(Nazwisko) && ZaznaczonyPilkarz != null)
                        );
                }
                return _editCommand;
            }
        }
        
        public void Clear()
        {
            Imie = "";
            Nazwisko = "";
            Wiek = 25;
            Waga = 75;
        }
    }
}
