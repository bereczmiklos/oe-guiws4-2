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
        public ObservableCollection<Hero> Barrack { get; set; }
        public ObservableCollection<Hero> Army { get; set; }

        //Commands:
        public ICommand AddToBattleCommand { get; set; }
        public ICommand RemoveFromBattleCommand { get; set; }
        public ICommand CreateHeroCommand { get; set; }
        public ICommand ClearHeroesCommand { get; set; }

        ////Propfulls:
        private Hero selectedFromBarrack;

        public Hero SelectedFromBarrack
        {
            get { return selectedFromBarrack; }
            set
            {
                SetProperty(ref selectedFromBarrack, value);
                (AddToBattleCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private Hero selectedFromArmy;

        public Hero SelectedFromArmy
        {
            get { return selectedFromArmy; }
            set
            {
                SetProperty(ref selectedFromArmy, value);
                (RemoveFromBattleCommand as RelayCommand).NotifyCanExecuteChanged();
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
        public MainWindowViewModel() 
            :this(IsInDesignMode ? null : Ioc.Default.GetService<IHeroLogic>())
        {

        }

        //Ctor:
        public MainWindowViewModel(IHeroLogic heroLogic)
        {
            //Initializations:
            this.heroLogic = heroLogic;
            Barrack = new ObservableCollection<Hero>();
            Army = new ObservableCollection<Hero>();

            //Test data seed:
            Barrack.Add(new Hero()
            {
                Name = "Thanos",
                Type = HeroTypes.EVIL,
                Power = 9,
                Speed = 1
            });
            Barrack.Add(new Hero()
            {
                Name = "IronMan",
                Type = HeroTypes.GOOD,
                Power = 6,
                Speed = 7
            });
            Barrack.Add(new Hero()
            {
                Name = "Matyi",
                Type = HeroTypes.NEUTRAL,
                Power = 2,
                Speed = 1
            });

            //Testing the battle side:
            Army.Add(Barrack[0].GetCopy());

            //Setting up collections in logic:
            heroLogic.SetUpCollections(Barrack, Army);

            //Initializing commands:
            AddToBattleCommand = new RelayCommand(
                () => heroLogic.AddHeroToBattle(SelectedFromBarrack),
                () => selectedFromBarrack != null);

            RemoveFromBattleCommand = new RelayCommand(
                () => heroLogic.RemoveHeroFromBattle(SelectedFromArmy),
                () => SelectedFromArmy != null);

            ClearHeroesCommand = new RelayCommand(
                () => heroLogic.ClearAllHeroFromBattle(),
                () => Army.Count != 0);

            CreateHeroCommand = new RelayCommand(
                 () => heroLogic.CreateHero(CreateHero)
                 );

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
