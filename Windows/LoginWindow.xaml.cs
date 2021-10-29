using System.Windows;
using System.IO;

namespace RPG
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void ButtonClickPlay(object sender, RoutedEventArgs e)
        {
            if (textBlockLogin.Text == "")
            {
                MessageBox.Show("Enter login", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else if (textBlockLogin.Text.Length >= 10) 
            {
                MessageBox.Show("Login is too long. \nIt should be shorter than 10 symbols.", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else
            {
                bool allow = true;
                foreach (char s in textBlockLogin.Text)
                {
                    if (!((s >= 'A' && s <= 'Z') || (s >= 'a' && s <= 'z'))) allow = false;
                }
                if (allow)
                {
                    Player.Create(textBlockLogin.Text);
                    GameWindow nw = new GameWindow();
                    nw.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid characters. \nUse only Latin symbols.", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
        }
    }
}
