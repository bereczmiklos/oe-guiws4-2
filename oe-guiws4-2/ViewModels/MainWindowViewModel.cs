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
        //IHeroLogic heroLogic;
        public ObservableCollection<Hero> Heroes { get; set; }
        public ObservableCollection<Hero> HeroesInBattle { get; set; }

        //Commands:
        public ICommand AddToBattleCommand { get; set; }
        public ICommand RemoveFromBattleCommand { get; set; }
        public ICommand CreateHeroCommand { get; set; }
        public ICommand ClearHeroesCommand { get; set; }

        ////Propfulls:
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

        private Hero selectedFromHeroesInBattle;

        public Hero SelectedFromHeroesInBattle
        {
            get { return selectedFromHeroesInBattle; }
            set { selectedFromHeroesInBattle = value; }
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
            //get { return heroLogic.SumPrice; }
            get { return 4444;  }
        }

        public double AvgPower
        {
            //get { return heroLogic.AvgPower; }
            get { return 3.44; }
        }
        public double AvgSpeed
        {
            // get { return heroLogic.AvgSpeed; }
            get { return 4.44; }
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
        //TODO:
        //hidden dependency, resolution: IsInDesignMode ? null : Ioc.Default.GetService<IHeroLogic>()
        public MainWindowViewModel() : this(new HeroLogic())
        {

        }

        //Ctor:
        public MainWindowViewModel(IHeroLogic heroLogic)
        {
            //Initializations:
            //this.heroLogic = heroLogic;
            Heroes = new ObservableCollection<Hero>();
            HeroesInBattle = new ObservableCollection<Hero>();

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
            HeroesInBattle.Add(Heroes[0].GetCopy());

            //Setting up collections in logic:
            //heroLogic.SetUpCollections(Heroes,SelectedHeroes);

            ////Initializing commands:
            AddToBattleCommand = new RelayCommand(
                () => heroLogic.AddHeroToBattle(SelectedFromHeroes),
                () => SelectedFromHeroes != null);

            RemoveFromBattleCommand = new RelayCommand(
                () => heroLogic.RemoveHeroFromBattle(SelectedFromHeroesInBattle),
                () => SelectedFromHeroesInBattle != null);

            CreateHeroCommand = new RelayCommand(
                () => heroLogic.ClearAllHeroFromBattle());
            ////TODO:
            ////CREATE HERO

            ////Registering messenger for "HeroInfo"
            //Messenger.Register<MainWindowViewModel, string, string>(this, "HeroInfo", (recipient, msg) =>
            //{
            //    OnPropertyChanged("SumPrice");
            //    OnPropertyChanged("AvgPower");
            //    OnPropertyChanged("AvgSpeed");
            //});
        }
    }
}
