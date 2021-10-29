using System;

namespace RPG
{
    class Dragon : Enemy
    {
        public Dragon(int heroAttack, int heroCure, int heroLevel)
            : base()
        {
            Random r = new Random();
            this.Type = "Dragon";
            this.Health = r.Next(heroAttack + 5, heroAttack + 10);
            this.Attack = r.Next(heroCure - 5, heroCure + 5);
        }
    }
}
