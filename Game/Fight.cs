using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class Fight
    {
        private Hero _hero;
        private Monster _monster;
        private string _player;
        public string Winner;
        public string Loser;
        public void HeroTurn()
        {
            int attack = _hero.Attack();
            Console.WriteLine($"You attack with {attack} power");
            _monster.Defend(attack);

        }
        public void MonsterTurn()
        {
            int attack = _monster.Strength;
            Console.WriteLine($"The {_monster.Name} attacks with {attack} power");
            _hero.Defend(attack);
        }

        public Fight(Hero hero, Monster monster, string player)
        {
            _hero = hero;
            _monster = monster;
            _player = player;
        }
    }
}
