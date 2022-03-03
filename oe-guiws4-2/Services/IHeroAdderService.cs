using oe_guiws4_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oe_guiws4_2.Services
{
    public interface IHeroAdderService
    {
        void Create(Hero hero);
    }
}
