using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class Item
    {
        protected string _name;
        protected int _strength;
        protected int _durability;
        protected int _agility;
        protected int _originalDurability;
        protected Hero _hero;
        protected int _price;

        public void AddHero(Hero hero)
        {
            _hero = hero;
        }

        public void Reset()
        {
            _durability = _originalDurability;
        }

        public string Name { get { return _name; } }
        public int Strength { get { return _strength; } }
        public int Durability { get { return _durability; } }
        public int Agility { get { return _agility; } }
        public int Price { get { return _price; } }
    }
}
