using System;
using System.Text;
using System.Windows;
using System.IO;

namespace RPG
{
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
            if (!File.Exists(Player.path)) {
                buttonOldGame.IsEnabled = false;
            }
        }

        private void ButtonClickNewGame(object sender, RoutedEventArgs e)
        {
            LoginWindow nw = new LoginWindow();
            nw.Show();
            this.Close();
        }

        private void ButtonClickAboutGame(object sender, RoutedEventArgs e)
        {
            AboutWindow nw = new AboutWindow();
            nw.Show();
        }

        private void ButtonClickOldGame(object sender, RoutedEventArgs e)
        {
            using (StreamReader sr = File.OpenText(Player.path))
            {
                string s = sr.ReadLine();
                Player.Name = s;
                s = sr.ReadLine();
                Player.Level = Convert.ToInt32(s);
                s = sr.ReadLine();
                Player.Experience = Convert.ToInt32(s);
                s = sr.ReadLine();
                Player.MaxHealth = Convert.ToInt32(s);
                s = sr.ReadLine();
                Player.Attack = Convert.ToInt32(s);
                s = sr.ReadLine();
                Player.Cure = Convert.ToInt32(s);
                sr.Close();
                GameWindow nw = new GameWindow();
                nw.Show();
                this.Close();
            }
        }
    }
}
