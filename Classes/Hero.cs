using System;
using System.Text;
using System.IO;

namespace RPG
{
    class Hero
    {
        private string name;
        public string Name
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

        private int level;
        public int Level
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

        private int health;
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                if (value < 0) health = 0;
                else if (value > Player.MaxHealth) health = Player.MaxHealth;
                else health = value;
            }
        }

        private int cure;
        public int Cure
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

        private int attack;
        public int Attack
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
        private int experience;
        public int Experience
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

        private int positionX;
        public int PositionX
        {
            get
            {
                return positionX;
            }
            set
            {
                if (value < 0) positionX = 0;
                else if (value > this.Level) positionX = this.Level;
                else positionX = value;
            }
        }

        private int positionY;
        public int PositionY
        {
            get
            {
                return positionY;
            }
            set
            {
                if (value < 0) positionY = 0;
                else if (value > this.Level) positionY = this.Level;
                else positionY = value;
            }
        }

        public Hero()
        {
            this.Name = Player.Name;
            this.Health = Player.MaxHealth;
            this.Attack = Player.Attack;
            this.Level = Player.Level;
            this.Cure = Player.Cure;
            this.Experience = Player.Experience;
        }

        private bool go;
        public bool Go
        {
            get
            {
                return go;
            }
            set
            {
                go = value;
            }
        }

        public void GeneratePosition()
        {
            Random r = new Random();
            int x = r.Next(0, this.Level + 1);
            int y = r.Next(0, this.Level + 1);
            this.PositionX = x;
            this.PositionY = y;
        }

        public string Show()
        {
            return this.Name + " "
                + "[" + this.Level + "]\n"
                + "Exp: " + this.Experience + "\n"
                + "Health: " + this.Health + "\n"
                + "Attack: " + this.Attack + "\n"
                + "Cure: " + this.Cure;
        }

        public string Kill(Enemy enemy)
        {
            int addExperience = ((enemy.Attack + (Player.MaxHealth - this.Health)) / (this.Cure + this.Level));
            this.Experience += addExperience;
            return addExperience.ToString();
        }

        public void End()
        {
            File.Delete("../../SAVE.txt");
        }
    }
}
