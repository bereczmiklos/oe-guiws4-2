using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oe_guiws4_2.Models
{

    public class Hero : ObservableObject
    {
        HeroTypes type;
        string name;
        int power;
        int speed;

        public HeroTypes Type
        {
            get { return type; }
            set { SetProperty(ref type, value); }
        }

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public int Power
        {
            get { return power; }
            set { SetProperty(ref power, value); }
        }

        public int Speed
        {
            get { return speed; }
            set { SetProperty(ref speed, value); }
        }

        public Hero GetCopy()
        {
            return new Hero()
            {
                Type = this.Type,
                Name = this.Name,
                Power = this.Power,
                Speed = this.Speed
            };
        }
    }
}
