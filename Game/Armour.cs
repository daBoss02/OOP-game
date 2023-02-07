using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class Armour : Item
    {
        public int Defend()
        {
            double durability = _durability / (double)_originalDurability;
            double agility = _strength * (double)_agility / 32;
            double strength = _strength * durability + agility;
            if (_durability - 10 > 0)
            {
                _durability -= 10;
            } else
            {
                _durability = 1;
            }
            return (int)strength;
        }
        public Armour(string name, int strength, int durability, int agility)
        {
            _name = name;
            _strength = strength;
            _durability = durability;
            _originalDurability= durability;
            _agility = agility;
        }
        public Armour(string name, int strength, int durability, int agility, int price)
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
