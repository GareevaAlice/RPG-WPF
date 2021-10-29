using System.Text;
using System.IO;

namespace RPG
{
    class Player
    {
        public static string path = "../../SAVE.txt";
        private static string name;
        public static string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private static int level;
        public static int Level
        {
            get
            {
                return level;
            }
            set
            {
                if (value < 1) level = 1;
                else level = value;
            }
        }

        private static int maxHealth;
        public static int MaxHealth
        {
            get
            {
                return maxHealth;
            }
            set
            {
                if (value < 0) maxHealth = 0;
                else maxHealth = value;
            }
        }

        private static int attack;
        public static int Attack
        {
            get
            {
                return attack;
            }
            set
            {
                if (value <= 1) attack = 1;
                else attack = value;
            }
        }

        private static int cure;
        public static int Cure
        {
            get
            {
                return cure;
            }
            set
            {
                if (value <= 1) cure = 1;
                else cure = value;
            }
        }

        private static int experience;
        public static int Experience
        {
            get
            {
                return experience;
            }
            set
            {
                if (value < 0) experience = 0;
                else experience = value;
            }
        }

        private static int lastMaxHealth;
        public static int LastMaxHealth
        {
            get
            {
                return lastMaxHealth;
            }
            set
            {
                if (value < 0) lastMaxHealth = 0;
                else lastMaxHealth = value;
            }
        }

        private static int lastAttack;
        public static int LastAttack
        {
            get
            {
                return lastAttack;
            }
            set
            {
                if (value <= 1) lastAttack = 1;
                else lastAttack = value;
            }
        }

        private static int lastCure;
        public static int LastCure
        {
            get
            {
                return lastCure;
            }
            set
            {
                if (value <= 1) lastCure = 1;
                else lastCure = value;
            }
        }
        
        public static void NewLevel(Hero hero)
        {
            Player.Level++;
            Player.MaxHealth += 2;
            Player.Attack += 2;
            Player.Cure += 2;
            Player.LastMaxHealth = Player.MaxHealth;
            Player.LastAttack = Player.Attack;
            Player.LastCure = Player.Cure;
            Player.Experience = hero.Experience;
        }

        public static void Create(string login)
        {
            Player.Name = login;
            Player.Level = 1;
            Player.MaxHealth = 20;
            Player.Attack = 5;
            Player.Cure = 5;
            Player.Experience = 0;
        }

        public static void Exit(Hero hero)
        {
            Player.Level++;
            Player.MaxHealth += 0;
            Player.Attack += 0;
            Player.Cure += 0;
            Player.LastMaxHealth = Player.MaxHealth;
            Player.LastAttack = Player.Attack;
            Player.LastCure = Player.Cure;
            Player.Experience = hero.Experience;
        }

        public static void Save()
        {
            if (File.Exists(Player.path))
            {
                File.Delete(Player.path);
            }
            using (StreamWriter sw = File.AppendText(Player.path))
            {
                sw.WriteLine(Player.Name);
                sw.WriteLine(Player.Level);
                sw.WriteLine(Player.Experience);
                sw.WriteLine(Player.MaxHealth);
                sw.WriteLine(Player.Attack);
                sw.WriteLine(Player.Cure);
            }
        }

        public static string Show() {
            return Player.Name + " "
                    + "[" + Player.Level + "]\n"
                    + "Exp: " + Player.Experience + "\n"
                    + "Health: " + Player.MaxHealth + "\n"
                    + "Attack: " + Player.Attack + "\n"
                    + "Cure: " + Player.Cure;
        }
    }
}
