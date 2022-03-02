using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using oe_guiws4_2.Logic;
using oe_guiws4_2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace oe_guiws4_2.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        IHeroLogic heroLogic;
        ObservableCollection<Hero> Heroes { get; set; }
        ObservableCollection<Hero> SelectedHeroes { get; set; }

        //Commands:
        public ICommand AddToBattleCommand { get; set; }
        public ICommand RemoveFromBattleCommand { get; set; }
        public ICommand CreateHeroCommand { get; set; }
        public ICommand ClearHeroesCommand { get; set; }

        //Propfulls:
        private Hero selectedFromHeroes;

        public Hero SelectedFromHeroes
        {
            get { return selectedFromHeroes; }
            set 
            {
                SetProperty(ref selectedFromHeroes, value);
                (AddToBattleCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private Hero removeHeroFromBattle;

        public Hero RemoveHeroFromBattle
        {
            get { return removeHeroFromBattle; }
            set 
            {
                SetProperty(ref removeHeroFromBattle, value);
                (RemoveFromBattleCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private Hero createHero;

        public Hero CreateHero
        {
            get { return createHero; }
            set
            {
                SetProperty(ref createHero, value);
                (CreateHeroCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        //Logic suppplies:
        public int SumPrice 
        {
            get { return heroLogic.SumPrice; }
        }

        public double AvgPower
        {
            get { return heroLogic.AvgPower; }
        }
        public double AvgSpeed
        {
            get { return heroLogic.AvgSpeed; }
        }

        //Checking design mode:
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        //No param ctor:
        public MainWindowViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<IHeroLogic>())
        {

        }

        //Ctor:
        public MainWindowViewModel(IHeroLogic? heroLogic)
        {
            //Initializations:
            this.heroLogic = heroLogic;
            Heroes = new ObservableCollection<Hero>();
            SelectedHeroes = new ObservableCollection<Hero>();

            //Test data seed:
            Heroes.Add(new Hero()
            {
                Name = "Thanos",
                Type = HeroTypes.EVIL,
                Power = 9,
                Speed = 1
            });
            Heroes.Add(new Hero()
            {
                Name = "IronMan",
                Type = HeroTypes.GOOD,
                Power = 6,
                Speed = 7
            });
            Heroes.Add(new Hero()
            {
                Name = "Matyi",
                Type = HeroTypes.NEUTRAL,
                Power = 2,
                Speed = 1
            });

            //Testing the battle side:
            SelectedHeroes.Add(Heroes[0].GetCopy());

            //Setting up collections in logic:
            heroLogic.SetUpCollections(Heroes,SelectedHeroes);

            //Initializing commands:
            AddToBattleCommand = new RelayCommand(
                () => heroLogic.AddHeroToBattle(SelectedFromHeroes),
                () => SelectedHeroes != null);

            RemoveFromBattleCommand = new RelayCommand(
                () => heroLogic.RemoveHeroFromBattle(removeHeroFromBattle),
                () => removeHeroFromBattle != null);

            CreateHeroCommand = new RelayCommand(
                () => heroLogic.ClearAllHeroFromBattle());
            //TODO:
            //CREATE HERO

            //Registering messenger for "HeroInfo"
            Messenger.Register<MainWindowViewModel, string, string>(this, "HeroInfo", (recipient, msg) =>
            {
                OnPropertyChanged("SumPrice");
                OnPropertyChanged("AvgPower");
                OnPropertyChanged("AvgSpeed");
            });
        }
    }
}
