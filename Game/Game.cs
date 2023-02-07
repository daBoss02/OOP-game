using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public static class Game
    {
        // weapons
        #region
        private static List<Weapon> _elfWeapons = new List<Weapon>()
        {
            new Weapon("Needle", 30, 30, 40),
            new Weapon("Holy Longsword", 40, 20, 40),
            new Weapon("Lifedrinker", 50, 30, 20)
        };
        private static List<Weapon> _dwarfWeapons = new List<Weapon>()
        {
            new Weapon("Light's Bane", 70, 30, 0),
            new Weapon("Lockjaw", 60, 30, 10),
            new Weapon("DeathBringer", 80, 30, -10)
        };
        private static List<Weapon> _humanWeapons = new List<Weapon>()
        {
            new Weapon("Slice of Life", 40, 40, 20),
            new Weapon("Justifier", 50, 50, 0),
            new Weapon("Shadow Strike", 20, 30, 50)
        };
        #endregion
        // armour
        #region
        private static List<Armour> _elfArmour = new List<Armour>()
        {
            new Armour("Faerie Knight's Friend", 20, 30, 10),
            new Armour("Unseelie Plates", 20, 20, 20),
            new Armour("Psionic Pauldrons", 0, 10, 50)
        };
        private static List<Armour> _dwarfArmour = new List<Armour>()
        {
            new Armour("Hide of Malar", 30, 40, -10),
            new Armour("Organ Keeper", 30, -10, 40),
            new Armour("Warmonger's Refuge", 40, 40, -20)
        };
        private static List<Armour> _humanArmour = new List<Armour>()
        {
            new Armour("Studded Leather", 20, 30, 10),
            new Armour("Chain Mail", 30, 20, 10),
            new Armour("Tuscan Steel", 40, 20, 0)
        };
        #endregion

        private static HashSet<Hero> _heroes = new HashSet<Hero>()
        {
            new Hero("Elf", 60, 80, 60, 100, _elfWeapons, _elfArmour),
            new Hero("Dwarf", 90, 0, 110, 100, _dwarfWeapons, _dwarfArmour),
            new Hero("Human", 80, 50, 70, 100, _humanWeapons, _humanArmour)
        };
        private static HashSet<Monster> _monsters = new HashSet<Monster>()
        {
            new Monster("Cryptling", 120, 90, 250),
            new Monster("The Ugly Bulge", 120, 20, 275),
            new Monster("Ash Hag", 120, 110, 100),
            new Monster("The Skeletal Thing", 140, 100, 190),
            new Monster("Dawnling", 150, 110, 100),
            new Monster("The Boss", 200, 150, 400)
        };
        private static bool _started = false;
        public static void Start()
        {
            bool continueGame = true;
            while (continueGame)
            {
                MainMenu();
            }
        }
        public static void MainMenu()
        {
            Console.WriteLine("\n\nMain Menu");
            Console.WriteLine("To select an option input the number next to it");
            Console.WriteLine("> 1: Play");
            Console.WriteLine("> 2: View Heroes");
            Console.WriteLine("> 3: Quit");
            string menuInput = Console.ReadLine();
            bool validInput = ValidateInput(menuInput, 3);
            while (!validInput)
            {
                Console.WriteLine("Please select a valid option");
                menuInput = Console.ReadLine();
                validInput = ValidateInput(menuInput, 3);
            }
            int choice = int.Parse(menuInput);
            switch(choice)
            {
                case 1:
                    Play();
                    break;
                case 2:
                    Hero hero = SelectHero();
                    hero.GetStats();
                    break;
                case 3:
                    Quit();
                    break;
            }
        }
        public static void Play()
        {
            Console.WriteLine("\n\nWelcome to the Dungeon!");
            Hero hero = SelectHero();
            hero.EquipWeapon();
            hero.EquipArmour();
            foreach (Monster monster in _monsters)
            {
                Console.WriteLine($"\nYou have encountered {monster.Name}" +
                    $"\n> Health: {monster.OriginalHealth}" +
                    $"\n> Attack: {monster.Strength}" +
                    $"\n> Defense: {monster.Defense}");
                Fight battle = new Fight(hero, monster);
                while (monster.CurrentHealth > 0 && hero.CurrentHealth > 0)
                {
                    Console.WriteLine();
                    battle.HeroTurn();
                    if (monster.CurrentHealth > 0)
                    {
                        battle.MonsterTurn();
                    } else
                    {
                        Console.WriteLine($"You have defeated {monster.Name}");
                        hero.ResetHealth();
                        hero.Coins += 20;
                        bool leave = false;
                        Console.WriteLine("\nYou have three options now");
                        while (!leave)
                        {
                            Console.WriteLine("> 1: Continue");
                            Console.WriteLine("> 2: Main Menu");
                            Console.WriteLine("> 3: Shop");
                            string choice = Console.ReadLine();
                            bool validChoice = ValidateInput(choice, 3);
                            while (!validChoice)
                            {
                                Console.WriteLine("Please select a valid option");
                                choice = Console.ReadLine();
                                validChoice = ValidateInput(choice, 3);
                            }
                            switch (int.Parse(choice))
                            {
                                case 1:
                                    leave = true;
                                    break;
                                case 2:
                                    Console.WriteLine("\nAll progress will be lost, are you sure?");
                                    Console.WriteLine("> 1: Yes");
                                    Console.WriteLine("> 2: No");
                                    string yesNo = Console.ReadLine();
                                    validChoice = ValidateInput(yesNo, 2);
                                    while (!validChoice)
                                    {
                                        Console.WriteLine("Please select a valid option");
                                        yesNo = Console.ReadLine();
                                        validChoice = ValidateInput(yesNo, 2);
                                    }
                                    if (int.Parse(yesNo) == 1)
                                    {
                                        leave = true;
                                        hero.EndRound();
                                    }
                                    break;
                                case 3:
                                    Shop(hero);
                                    leave = true;
                                    break;
                            }
                        }
                    }
                }
                if (hero.CurrentHealth == 0)
                {
                    ResetGame(hero);
                    break;
                }
            }
        }
        public static void Shop(Hero hero)
        {
            Console.WriteLine("Welcome to Shop");
            bool leave = false;
            while (!leave)
            {
                Console.WriteLine($"You have {hero.Coins} coins");
                Console.WriteLine("What would you like to buy?");
                Console.WriteLine("> 1: Weapons");
                Console.WriteLine("> 2: Armour");
                Console.WriteLine("> 3: Leave");
                string choice = Console.ReadLine();
                bool validChoice = ValidateInput(choice, 3);
                while (!validChoice)
                {
                    Console.WriteLine("Please select a valid option");
                    choice = Console.ReadLine();
                    validChoice = ValidateInput(choice, 3);
                }
                if (int.Parse(choice) == 1)
                {
                    hero.BuyWeapon();
                } else if (int.Parse(choice) == 2)
                {
                    hero.BuyArmour();
                } else
                {
                    Console.WriteLine("Good Luck");
                    leave = true;
                }
            }
        }
        public static void ResetGame(Hero hero)
        {
            foreach (Monster monster in _monsters)
            {
                monster.Reset();
            }
            hero.Reset();

        }
        public static Hero SelectHero()
        {
            Console.WriteLine("Select Hero:");
            Console.WriteLine("> 1: Elf");
            Console.WriteLine("> 2: Dwarf");
            Console.WriteLine("> 3: Human");
            string heroInput = Console.ReadLine();
            bool validHero = ValidateInput(heroInput, 3);
            while (!validHero)
            {
                Console.WriteLine("Invalid input");
                heroInput = Console.ReadLine();
                validHero = ValidateInput(heroInput, 3);
            }
            Hero hero = null;
            string name = null;
            switch (int.Parse(heroInput))
            {
                case 1:
                    name = "elf";
                    break;
                case 2:
                    name = "dwarf";
                    break;
                case 3:
                    name = "human";
                    break;
            }
            foreach (Hero h in _heroes)
            {
                if (h.Name.ToLower() == name)
                {
                    hero = h;
                }
            }
            return hero;
        }
        public static void Quit()
        {
            Environment.Exit(0);
        }
        public static bool ValidateInput(string input, int options)
        {
            if (!string.IsNullOrEmpty(input) && input.All(c => char.IsDigit(c)) && int.Parse(input) <= options && int.Parse(input) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
