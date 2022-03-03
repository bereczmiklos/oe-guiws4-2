using oe_guiws4_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oe_guiws4_2.ViewModels
{
    public class HeroCreateWindowViewModel
    {
        public Hero ActualHero { get; set; }

        public void Setup(Hero hero)
        {
            this.ActualHero = hero;
        }

        public HeroCreateWindowViewModel()
        {

        }
    }
}
