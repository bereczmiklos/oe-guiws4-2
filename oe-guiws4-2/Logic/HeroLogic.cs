using Microsoft.Toolkit.Mvvm.Messaging;
using oe_guiws4_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oe_guiws4_2.Logic
{
    public class HeroLogic : IHeroLogic
    {
        IList<Hero> heroesAvailable;
        IList<Hero> heroesInBattle;
        IMessenger messenger;

        //TODO:
        //service class 

        public HeroLogic(IMessenger mess)
        {
            this.messenger = mess;
            //service
        }

        //AVG price (price := power*speed)
        public int SumPrice
        {
            get { return heroesInBattle.Count == 0 ? 0 : heroesInBattle.Sum(h => h.Power * h.Speed); }
        }

        //AVG power
        public double AvgPower
        {
            get { return Math.Round(heroesInBattle.Count == 0 ? 0 : heroesInBattle.Average(h => h.Power), 2); }
        }

        //AVG speed
        public double AvgSpeed
        {
            get { return Math.Round(heroesInBattle.Count == 0 ? 0 : heroesInBattle.Average(h => h.Speed), 2); }
        }

        public void SetUpCollections(IList<Hero> heroes, IList<Hero> battleingHeroes)
        {
            this.heroesAvailable = heroes;
            this.heroesInBattle = battleingHeroes;
        }

        //Add hero to battle
        public void AddHeroToBattle(Hero hero)
        {
            heroesInBattle.Add(hero);
            messenger.Send("Hero added", "HeroInfo");
        }

        //Remove hero from battle
        public void RemoveHeroFromBattle(Hero hero)
        {
            heroesInBattle.Remove(hero);
            messenger.Send("Hero removed", "HeroInfo");
        }

        //Remove all hero from battle
        public void ClearAllHeroFromBattle()
        {
            heroesInBattle.Clear();
            messenger.Send("Heroes cleared", "HeroInfo");
        }

        //Create new hero
        public void CreateHero()
        {

        }
    }
}
