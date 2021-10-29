using System.Collections.Generic;

namespace RPG
{
    class Room
    {
        private string type;
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                if (!((value == "None") || (value == "Door") || (value == "Enemy"))) type = "None";
                type = value;
            }
        }

        private int howManyEnemies;
        public int HowManyEnemies
        {
            get
            {
                return howManyEnemies;
            }
            set
            {
                if (value < 1) howManyEnemies = 1;
                else if (value > 2) howManyEnemies = 2;
                else howManyEnemies = value;
            }
        }

        private List<Enemy> enemies = new List<Enemy>();
        public List<Enemy> Enemies
        {
            get
            {
                return enemies;
            }
            set
            {
                enemies = value;
            }
        }

        public Room()
        {
            this.Type = "None";
        }

        public Room(string type)
        {
            this.Type = "Door";
        }

        public Room(string type, int howMany, List<Enemy> enemies)
        {
            this.Type = "Enemy";
            this.HowManyEnemies = howMany;
            this.Enemies = enemies;
        }
    }
}
