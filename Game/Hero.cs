using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class Hero
    {
        // Private properties
        #region
        private string _name;
        private int _baseStrength;
        private int _agility;
        private int _baseDefense;
        private int _originalHealth;
        private int _currentHealth;
        private int _coins = 0;
        private Weapon _equippedWeapon;
        private Armour _equippedArmour;
        private List<Weapon> _availableWeapons;
        private List<Armour> _availableArmour;
        #endregion

        // Public Getters 
        #region
        public string Name { get { return _name; } }
        public int BaseStrength { get { return _baseStrength; } }
        public int Agility { get { return _agility; } }
        public int BaseDefense { get { return _baseDefense; } }
        public int OriginalHealth { get { return _originalHealth; } }
        public int CurrentHealth { get { return _currentHealth; } }
        public int Coins 
        { 
            get { return _coins; }
            set { _coins = value; }
        }
        public Weapon EquippedWeapon { get {  return _equippedWeapon; } }
        public Armour EquippedArmour { get { return _equippedArmour; } }
        public List<Armour> AvailableArmour()
        {
            return _availableArmour.ToList();
        }
        public List<Weapon> AvailableWeapons()
        {
            return _availableWeapons.ToList();
        }

        private List<Weapon> _lootWeapons = new List<Weapon>()
        {
            new Weapon("Mithril", 90, 70, 40, 20),
            new Weapon("Anger's Tear", 130, 70, 0, 25),
            new Weapon("Gladius", 100, 90, 10, 20),
            new Weapon("Silver Sabre", 90, 90, 20, 30),
            new Weapon("World Slayer", 150, 50, 0, 30)
        };


        private List<Armour> _lootArmour = new List<Armour>()
        {
            new Armour("Second Skin", 80, 80, 40, 30),
            new Armour("Coward's Coif", 60, 60, 80, 25),
            new Armour("Saci's Cap", 70, 40, 90, 20),
            new Armour("Ad Infinitum", 100, 70, 30, 20),
            new Armour("Horned Helm of the Nine Hells", 60, 120, 20, 30)
        };
        #endregion


        // Public Methods
        #region
        public void GetStats()
        {
            Console.WriteLine($"\n{_name}" +
                $" -- Health: {_originalHealth}" +
                $" -- Strength: {_baseStrength}" +
                $" -- Defense: {_baseDefense}" +
                $" -- Agility: {_agility}");
            Console.WriteLine("\nWeapons:");
            foreach (Weapon weapon in _availableWeapons)
            {
                Console.WriteLine($"\n{weapon.Name}" +
                    $" -- Added Strength: {weapon.Strength}" +
                    $" -- Added Durability: {weapon.Durability}" +
                    $" -- Added Agility: {weapon.Agility}");
            }
            Console.WriteLine("\nArmour:");
            foreach (Armour armour in _availableArmour)
            {
                Console.WriteLine($"\n{armour.Name}" +
                    $" -- Added Defense: {armour.Strength}" +
                    $" -- Added Durability: {armour.Durability}" +
                    $" -- Added Agility: {armour.Agility}");
            }
        }

        public void EquipWeapon()
        {
            Console.WriteLine("Which weapon would you like to equip:");
            int count = 1;
            foreach (Weapon weapon in _availableWeapons)
            {
                Console.WriteLine($"> {count}: {weapon.Name}");
                count++;
                Console.WriteLine($"-- Strength: {weapon.Strength}");
                Console.WriteLine($"-- Durability: {weapon.Durability}");
                Console.WriteLine($"-- Agility: {weapon.Agility}");
            }
            string selection = Console.ReadLine();
            bool validWeapon = Game.ValidateInput(selection, _availableWeapons.Count());
            while (!validWeapon)
            {
                Console.WriteLine("Please select a valid weapon");
                selection = Console.ReadLine();
                validWeapon = Game.ValidateInput(selection, _availableWeapons.Count());
            }
            _equippedWeapon = _availableWeapons[int.Parse(selection) - 1];
            _equippedWeapon.AddHero(this);
            Console.WriteLine($"\nYou have equipped {_equippedWeapon.Name}!");
        }

        public void EquipArmour()
        {
            Console.WriteLine("\nWhich Armour would you like to equip:");
            int count = 1;
            foreach (Armour armour in _availableArmour)
            {
                Console.WriteLine($"> {count}: {armour.Name}");
                count++;
                Console.WriteLine($"-- Strength: {armour.Strength}");
                Console.WriteLine($"-- Durability: {armour.Durability}");
                Console.WriteLine($"-- Agility: {armour.Agility}");
            }
            string selection = Console.ReadLine();
            bool validArmour = Game.ValidateInput(selection, _availableArmour.Count());
            while (!validArmour)
            {
                Console.WriteLine("Please select a valid armour piece");
                selection = Console.ReadLine();
                validArmour = Game.ValidateInput(selection, _availableArmour.Count());
            }
            _equippedArmour = _availableArmour[int.Parse(selection) - 1];
            _equippedArmour.AddHero(this);
            Console.WriteLine($"\nYou have equipped {_equippedArmour.Name}!");

        }

        public void Defend(int damage)
        {
            double agilityBuff = _agility / 24;
            int defense = _baseDefense + _equippedArmour.Defend() + (int)agilityBuff;

            if (damage - defense > 0)
            {
                if (_currentHealth - (damage - defense) > 0)
                {
                    _currentHealth -= (damage - defense);

                } else
                {
                    _currentHealth = 0;
                }
                Console.WriteLine($"You have taken {damage - defense} damage");
                if (_currentHealth == 0)
                {
                    Console.WriteLine("You have been defeated");
                }
            } else
            {
                Console.WriteLine($"You have a total of {defense} defense");
                Console.WriteLine("You have blocked all damage");
            }
        }
        public int Attack()
        {
            return _baseStrength + _equippedWeapon.Attack() + (_baseStrength * (_agility + _equippedWeapon.Agility) / 32);

        }
        public void Reset()
        {
            _currentHealth = _originalHealth;
            _equippedArmour.Reset();
            _equippedWeapon.Reset();
            _coins = 0;
        }
        public void EndRound()
        {
            _currentHealth = 0;
        }
        public void ResetHealth()
        {
            _currentHealth = _originalHealth;
        }

        public void BuyWeapon()
        {
            Console.WriteLine("Available Weapons:");
            int count = 2;
            Console.WriteLine("> 1: Back to Shop");
            foreach (Weapon weapon in _lootWeapons)
            {
                Console.WriteLine($"> {count}: {weapon.Name}");
                count++;
                Console.WriteLine($"Price: {weapon.Price}");
                Console.WriteLine($"-- Strength: {weapon.Strength}");
                Console.WriteLine($"-- Durability: {weapon.Durability}");
                Console.WriteLine($"-- Agility: {weapon.Agility}");
            }
            string selection = Console.ReadLine();
            bool validWeapon = Game.ValidateInput(selection, _lootWeapons.Count() + 1);
            while (!validWeapon)
            {
                Console.WriteLine("Please select a valid weapon");
                selection = Console.ReadLine();
                validWeapon = Game.ValidateInput(selection, _lootWeapons.Count() + 1);
            }
            if (int.Parse(selection) != 1)
            {
                Weapon newWeapon = _lootWeapons[int.Parse(selection) - 2];
                if (newWeapon.Price <= _coins)
                {
                    _equippedWeapon.Reset();
                    _equippedWeapon = newWeapon;
                    _coins -= _equippedWeapon.Price;
                    _equippedWeapon.AddHero(this);
                    Console.WriteLine($"\nYou have equipped {_equippedWeapon.Name}!");
                } else
                {
                    Console.WriteLine("You need more coins"); 
                }
            }

        }
        public void BuyArmour()
        {
            Console.WriteLine("\nAvailable Armour:");
            int count = 2;
            Console.WriteLine("\n> 1: Back to Shop");
            foreach (Armour armour in _lootArmour)
            {
                Console.WriteLine($"\n> {count}: {armour.Name}");
                count++;
                Console.WriteLine($"Price: {armour.Price}");
                Console.WriteLine($"-- Strength: {armour.Strength}");
                Console.WriteLine($"-- Durability: {armour.Durability}");
                Console.WriteLine($"-- Agility: {armour.Agility}");
            }
            string selection = Console.ReadLine();
            bool validWeapon = Game.ValidateInput(selection, _lootArmour.Count() + 1);
            while (!validWeapon)
            {
                Console.WriteLine("Please select a valid piece of armour");
                selection = Console.ReadLine();
                validWeapon = Game.ValidateInput(selection, _lootArmour.Count() + 1);
            }
            if (int.Parse(selection) != 1)
            {
                Armour newArmour = _lootArmour[int.Parse(selection) - 2];
                if (newArmour.Price <= _coins)
                {
                    _equippedArmour.Reset();
                    _equippedArmour = newArmour;
                    _coins -= _equippedArmour.Price;
                    _equippedArmour.AddHero(this);
                    Console.WriteLine($"\nYou have equipped {_equippedArmour.Name}!");
                }
                else
                {
                    Console.WriteLine("You need more coins");
                }
            }

        }
        #endregion

        public Hero(string name, int baseStrength, int agility, int baseDefense, int originalHealth, List<Weapon> availableWeapons, List<Armour> availableArmour)
        {
            _name = name;
            _baseStrength = baseStrength;
            _agility = agility;
            _baseDefense = baseDefense;
            _originalHealth = originalHealth;
            _currentHealth = originalHealth;
            _availableWeapons = availableWeapons;
            _availableArmour = availableArmour;
        }
    }
}
