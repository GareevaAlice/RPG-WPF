using System;

namespace RPG
{
    class Griffin : Enemy
    {
        public Griffin(int heroAttack, int heroCure, int heroLevel)
            : base()
        {
            Random r = new Random();
            this.Type = "Griffin";
            this.Health = r.Next(heroAttack - 5, heroAttack + 5);
            this.Attack = r.Next(heroLevel, heroLevel + 10);
        }
    }
}
