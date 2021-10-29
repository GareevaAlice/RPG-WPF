namespace RPG
{
    class Enemy
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
                if (!((value == "Griffin") || (value == "Dragon"))) type = "Griffin";
                type = value;
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
                if (value <= 0) health = 0;
                else health = value;
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

        public Enemy() { }

        public string Show()
        {
            return this.Type + "\n" +
                "Health: " + this.Health + "\n"
                + "Attack: " + this.Attack;
        }
    }
}
