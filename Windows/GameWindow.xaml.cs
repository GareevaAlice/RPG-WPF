using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RPG
{
    public partial class GameWindow : Window
    {
        
        Hero hero = new Hero();
        Room[,] rooms = new Room[(Player.Level * Player.Level) + 1, (Player.Level * Player.Level) + 1];
        Random r = new Random();


        public GameWindow()
        {
            Player.Save();
            InitializeComponent();
            GenerateLevel();
            ShowRoom();
            hero.Go = true;
            textBlockHelpingText.Text = "Find the exit";
            Fight();
        }

        private void GenerateLevel()
        {
            rooms = new Room[hero.Level + 1, hero.Level + 1];
            for (int i = 0; i <= hero.Level; i++)
            {
                for (int j = 0; j <= hero.Level; j++)
                {
                    rooms[i, j] = new Room();
                }
            }
            hero.GeneratePosition();
            GeneratePositionExit();
            GeneratePositionEnemies();
        }

        private void GeneratePositionExit()
        {
            int x = 0, y = 0;
            bool heroPos = true;
            while (heroPos)
            {
                x = r.Next(0, hero.Level + 1);
                y = r.Next(0, hero.Level + 1);
                if (hero.PositionX != x && hero.PositionY != y) heroPos = false;
            }
            rooms[x, y] = new Room("Door");
        }

        private void GeneratePositionEnemies()
        {
            int howMany = ((hero.Level + 1) * (hero.Level + 1)) / 2;
            for (int i = 0; i < howMany; i++)
            {
                int x = 0, y = 0;
                bool doorPos = false;
                while (!doorPos)
                {
                    x = r.Next(0, hero.Level + 1);
                    y = r.Next(0, hero.Level + 1);
                    if ((rooms[x, y].Type == "None")) doorPos = true;

                }
                int howManyEnemies = r.Next(1, 3);
                List<Enemy> enemies = new List<Enemy>();
                for (int j = 0; j < howManyEnemies; j++)
                {
                    int type = r.Next(0, 2);
                    if (type == 0) enemies.Add(new Dragon(hero.Attack, hero.Cure, hero.Level));
                    if (type == 1) enemies.Add(new Griffin(hero.Attack, hero.Cure, hero.Level));
                }
                rooms[x, y] = new Room("Enemy", howManyEnemies, enemies);
            }
        }

        private void ShowRoom()
        {
            Room room = rooms[hero.PositionX, hero.PositionY];
            buttonLeft.Visibility = Visibility.Visible;
            if (hero.PositionX == 0) buttonLeft.Visibility = Visibility.Hidden;
            buttonUp.Visibility = Visibility.Visible;
            if (hero.PositionY == 0) buttonUp.Visibility = Visibility.Hidden;
            buttonRight.Visibility = Visibility.Visible;
            if (hero.PositionX == (hero.Level)) buttonRight.Visibility = Visibility.Hidden;
            buttonDown.Visibility = Visibility.Visible;
            if (hero.PositionY == (hero.Level)) buttonDown.Visibility = Visibility.Hidden;

            buttonEnemy1.Visibility = Visibility.Hidden;
            buttonEnemy2.Visibility = Visibility.Hidden;

            textBlockMinusHeroHealth.Visibility = Visibility.Hidden;
            textBlockPlusHeroHealth.Visibility = Visibility.Hidden;
            textBlockPlusHeroExperinece.Visibility = Visibility.Hidden;
            textBlockMinusFirstEnemyHealth.Visibility = Visibility.Hidden;
            textBlockMinusSecondEnemyHealth.Visibility = Visibility.Hidden;

            textBlockStatBarFirstEnemy.Visibility = Visibility.Hidden;
            textBlockStatBarSecondEnemy.Visibility = Visibility.Hidden;
            
            buttonLeft.IsEnabled = true;
            buttonRight.IsEnabled = true;
            buttonUp.IsEnabled = true;
            buttonDown.IsEnabled = true;

            if (room.Type == "Enemy")
            {
                ShowEnemy(0, room, buttonEnemy1, textBlockStatBarFirstEnemy, textBlockMinusFirstEnemyHealth);
                ShowEnemy(1, room, buttonEnemy2, textBlockStatBarSecondEnemy, textBlockMinusSecondEnemyHealth);
            }
            buttonExit.Visibility = Visibility.Hidden;
            if (room.Type == "Door")
            {
                buttonExit.Visibility = Visibility.Visible;
            }
            textBlockStatBarHero.Text = hero.Show();
        }

        private void ShowEnemy(int pos,
            Room room, 
            Button buttonEnemy, 
            TextBlock StatBarEnemy,
            TextBlock textBlockMinusEnemyHealth) {
            if (room.HowManyEnemies >= pos + 1 && room.Enemies[pos].Health > 0)
            {
                buttonLeft.IsEnabled = false;
                buttonRight.IsEnabled = false;
                buttonUp.IsEnabled = false;
                buttonDown.IsEnabled = false;
                buttonEnemy.Visibility = Visibility.Visible;
                StatBarEnemy.Visibility = Visibility.Visible;
                StatBarEnemy.Text = room.Enemies[pos].Show();
                Style style;
                if (room.Enemies[pos].Type == "Dragon")
                {
                    style = this.FindResource("DRAGON") as Style;
                }
                else
                {
                    style = this.FindResource("GRIFFIN") as Style;
                }
                buttonEnemy.Style = style;
            }
            if (room.HowManyEnemies >= pos + 1 && room.Enemies[pos].Health <= 0)
            {
                textBlockMinusEnemyHealth.Visibility = Visibility.Hidden;
            }
        }


        private void Fight()
        {
            Room room = rooms[hero.PositionX, hero.PositionY];
            if (room.Type == "Enemy")
            {
                bool play = true;
                if (room.HowManyEnemies == 1) if (room.Enemies[0].Health <= 0) play = false;
                if (room.HowManyEnemies == 2) if (room.Enemies[0].Health <= 0 && room.Enemies[1].Health <= 0) play = false;
                if (play)
                {
                    textBlockHelpingText.Text = "FIGHT!";
                    if (hero.Go)
                    {
                        buttonEnemy1.IsEnabled = true;
                        buttonEnemy2.IsEnabled = true;
                        textBlockHelpingText.Text += " Attack (Click on the enemy) or Cure (Click on the hero)";
                        hero.Go = false;
                    }
                    else
                    {
                        buttonEnemy1.IsEnabled = false;
                        buttonEnemy2.IsEnabled = false;
                        int whoAttack = r.Next(0, room.HowManyEnemies);
                        if (room.HowManyEnemies >= 2) if (room.Enemies[0].Health <= 0) whoAttack = 1;
                        if (room.HowManyEnemies >= 2) if (room.Enemies[1].Health <= 0) whoAttack = 0;
                        hero.Health += -1 * room.Enemies[whoAttack].Attack;
                        textBlockInfoText.Text += "\nYou was attacked by " + room.Enemies[whoAttack].Type +
                            " (" + (whoAttack + 1).ToString() + ")";
                        textBlockMinusHeroHealth.Visibility = Visibility.Visible;
                        textBlockMinusHeroHealth.Text = room.Enemies[whoAttack].Attack.ToString();
                        hero.Go = true;
                        textBlockStatBarHero.Text = hero.Show();
                        if (hero.Health == 0)
                        {
                            hero.End();
                            RIPWindow nw = new RIPWindow();
                            nw.Show();
                            this.Close();
                        }
                        Fight();
                    }
                }
            }
        }

        private void HeroClick(object sender, RoutedEventArgs e)
        {
            ShowRoom();
            if (Player.MaxHealth <= hero.Health + hero.Cure)
            {
                textBlockInfoText.Text = "You cure yourself";
                textBlockPlusHeroHealth.Visibility = Visibility.Visible;
                textBlockPlusHeroHealth.Text = (Player.MaxHealth - hero.Health).ToString();
            }
            else
            {
                textBlockInfoText.Text = "You cure yourself";
                textBlockPlusHeroHealth.Visibility = Visibility.Visible;
                textBlockPlusHeroHealth.Text = hero.Cure.ToString();
            }
            hero.Health += hero.Cure;
            textBlockStatBarHero.Text = hero.Show();
            Fight();
        }

        private void ClickEnemy(int pos)
        {
            ShowRoom();
            Room room = rooms[hero.PositionX, hero.PositionY];
            room.Enemies[pos].Health += -1 * hero.Attack;
            textBlockInfoText.Text = "You attacked " + room.Enemies[pos].Type + " (" + (pos + 1).ToString() + ")";
            if (pos == 0)
            {
                textBlockMinusFirstEnemyHealth.Visibility = Visibility.Visible;
                textBlockMinusFirstEnemyHealth.Text = (hero.Attack).ToString();
            }
            else
            {
                textBlockMinusSecondEnemyHealth.Visibility = Visibility.Visible;
                textBlockMinusSecondEnemyHealth.Text = (hero.Attack).ToString();
            }
            if (room.Enemies[pos].Health == 0)
            {
                ShowRoom();
                textBlockMinusFirstEnemyHealth.Visibility = Visibility.Hidden;
                textBlockMinusSecondEnemyHealth.Visibility = Visibility.Hidden;
                string experience = hero.Kill(room.Enemies[pos]);
                textBlockInfoText.Text += "\nYou killed " + room.Enemies[pos].Type + " (" + (pos + 1).ToString() + ")";
                textBlockInfoText.Text += "\nYou got " + experience + " experience";
                textBlockPlusHeroExperinece.Visibility = Visibility.Visible;
                textBlockPlusHeroExperinece.Text = experience;
            }
            if (pos == 0)
            {
                ShowEnemy(0, room, buttonEnemy1, textBlockStatBarFirstEnemy, textBlockMinusFirstEnemyHealth);
            }
            else
            {
                ShowEnemy(1, room, buttonEnemy2, textBlockStatBarSecondEnemy, textBlockMinusSecondEnemyHealth);
            }
            Fight();
        }

        private void Enemy1Click(object sender, RoutedEventArgs e)
        {
            ClickEnemy(0);
        }

        private void Enemy2Click(object sender, RoutedEventArgs e)
        {
            ClickEnemy(1);
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            if (Player.Level >= 10)
            {
                hero.End();
                WinWindow nw = new WinWindow();
                nw.Show();
                this.Close();
            }
            else
            {
                Player.Exit(hero);
                ChangeWindow nw = new ChangeWindow();
                nw.Show();
                this.Close();
            }
        }

        private void Base()
        {
            ShowRoom();
            hero.Go = true;
            textBlockHelpingText.Text = "Find the exit";
            textBlockInfoText.Text = "";
            Fight();
        }

        private void LeftClick(object sender, RoutedEventArgs e)
        {
            hero.PositionX--;
            Base();
        }

        private void UpClick(object sender, RoutedEventArgs e)
        {
            hero.PositionY--;
            Base();
        }

        private void RightClick(object sender, RoutedEventArgs e)
        {
            hero.PositionX++;
            Base();
        }

        private void DownClick(object sender, RoutedEventArgs e)
        {
            hero.PositionY++;
            Base();
        }

        private void MenuItemClickExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItemClickAbout(object sender, RoutedEventArgs e)
        {
            AboutWindow nw = new AboutWindow();
            nw.Show();
        }

        private void MouseEnterUp(object sender, MouseEventArgs e)
        {
            textBlockHelpingText.Text = "Click to go up";
        }

        private void MouseEnterLeft(object sender, MouseEventArgs e)
        {
            textBlockHelpingText.Text = "Click to go left";
        }

        private void MouseEnterRight(object sender, MouseEventArgs e)
        {
            textBlockHelpingText.Text = "Click to go right";
        }

        private void MouseEnterDown(object sender, MouseEventArgs e)
        {
            textBlockHelpingText.Text = "Click to go down";
        }

        private void MouseEnterHero(object sender, MouseEventArgs e)
        {
            textBlockHelpingText.Text = "Click to cure yourself";
        }

        private void MouseEnterEnemy(object sender, MouseEventArgs e)
        {
            textBlockHelpingText.Text = "Click to attack";
        }

        private void MouseEnterAway(object sender, MouseEventArgs e)
        {
            textBlockHelpingText.Text = "Click to go to the next level";
        }

        private new void MouseLeave(object sender, MouseEventArgs e)
        {
            Room room = rooms[hero.PositionX, hero.PositionY];
            bool play = true;
            if (room.HowManyEnemies == 1) if (room.Enemies[0].Health <= 0) play = false;
            if (room.HowManyEnemies == 2) if (room.Enemies[0].Health <= 0 && room.Enemies[1].Health <= 0) play = false;
            if (play && room.Type == "Enemy") textBlockHelpingText.Text = "FIGHT! Attack (Click on the enemy) or Cure (Click on the hero)";
            else textBlockHelpingText.Text = "Find the exit";
        }
    }
}
