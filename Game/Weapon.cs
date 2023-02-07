using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class Weapon : Item
    {
        public int Attack()
        {
            double strength = _strength * ((double)_durability / (double)_originalDurability);

            if (_durability - 5 >= 10)
            {
                _durability -= 5;
            } else
            {
                _durability = 10;
            }
            return (int)(strength);
        }
        public Weapon(string name, int strength, int durability, int agility)
        {
            _name = name;
            _strength = strength;
            _durability = durability;
            _originalDurability = durability;
            _agility = agility;
        }
        public Weapon(string name, int strength, int durability, int agility, int price)
        {
            _name = name;
            _strength = strength;
            _durability = durability;
            _originalDurability = durability;
            _agility = agility;
            _price = price;
        }
    }
}
