using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class Monster
    {
        // private properties
        #region
        private string _name;
        private int _strength;
        private int _defense;
        private int _originalHealth;
        private int _currentHealth;
        #endregion

        // public getters
        #region
        public string Name { get { return _name; } }
        public int Strength { get { return _strength; } }
        public int Defense { get { return _defense; } }
        public int OriginalHealth { get { return _originalHealth; } }
        public int CurrentHealth { get { return _currentHealth;} }
        #endregion

        // public methods
        #region
        public void Defend(int damage)
        {
            if (damage - _defense > 0 && _defense > 0)
            {
                
                _currentHealth -= (damage - _defense);
                Console.WriteLine($"The monster blocks {_defense} of that power" +
                    $" and you only do {damage - _defense} damage");
                _defense -= 10;
            } else if (_defense <= 0)
            {
                Console.WriteLine("The monster has no defenses left");
                _currentHealth -= damage;
            }
            else
            {
                Console.WriteLine("All damage was blocked");
                _defense -= 20;
            }
        }
        public void Reset()
        {
            _currentHealth = _originalHealth;
        }
        #endregion

        public Monster(string name, int strength, int defense, int originalHealth)
        {
            _name = name;
            _strength = strength;
            _defense = defense;
            _originalHealth = originalHealth;
            _currentHealth = _originalHealth;
        }
    }
}
