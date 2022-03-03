using oe_guiws4_2.Models;
using System.Collections.Generic;

namespace oe_guiws4_2.Logic
{
    public interface IHeroLogic
    {
        double AvgPower { get; }
        double AvgSpeed { get; }
        int SumPrice { get; }

        void AddHeroToBattle(Hero hero);
        void ClearAllHeroFromBattle();
        void CreateHero();
        void RemoveHeroFromBattle(Hero hero);
        void SetUpCollections(IList<Hero> heroes, IList<Hero> battleingHeroes);
    }
}