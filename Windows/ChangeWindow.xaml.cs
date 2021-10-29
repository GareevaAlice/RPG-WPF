using System.Windows;

namespace RPG
{
    public partial class ChangeWindow : Window
    {
        public ChangeWindow()
        {
            InitializeComponent();
            PlusExperience.Visibility = Visibility.Hidden;
            MinusExperience.Visibility = Visibility.Hidden;
            Show();
        }

        private void ButtonClickAway(object sender, RoutedEventArgs e)
        {
            GameWindow nw = new GameWindow();
            nw.Show();
            Player.Save();
            this.Close();
        }

        private new void Show()
        {
            textBlockInfo.Text = Player.Name + " "
                + "[" + Player.Level + "]\n";
            textBlockInfo.Text += "Experience: " + Player.Experience;
            textBlockHealth.Text = "Health: " + Player.MaxHealth;
            textBlockAttack.Text = "Attack: " + Player.Attack;
            textBlockCure.Text = "Cure: " + Player.Cure;
            if (Player.Experience <= 0)
            {
                buttonPlusHealth.IsEnabled = false;
                buttonPlusAttack.IsEnabled = false;
                buttonPlusCure.IsEnabled = false;
            }
            else
            {
                buttonPlusHealth.IsEnabled = true;
                buttonPlusAttack.IsEnabled = true;
                buttonPlusCure.IsEnabled = true;
            }
            if (Player.MaxHealth == Player.LastMaxHealth) buttonMinusHealth.IsEnabled = false;
            else buttonMinusHealth.IsEnabled = true;

            if (Player.Attack == Player.LastAttack) buttonMinusAttack.IsEnabled = false;
            else buttonMinusAttack.IsEnabled = true;

            if (Player.Cure == Player.LastCure) buttonMinusCure.IsEnabled = false;
            else buttonMinusCure.IsEnabled = true;
        }
        private void ButtonClickPlusHealth(object sender, RoutedEventArgs e)
        {
            PlusExperience.Visibility = Visibility.Hidden;
            MinusExperience.Visibility = Visibility.Visible;
            Player.Experience--;
            Player.MaxHealth++;
            Show();
        }

        private void ButtonClickMinusHealth(object sender, RoutedEventArgs e)
        {
            PlusExperience.Visibility = Visibility.Visible;
            MinusExperience.Visibility = Visibility.Hidden;
            Player.Experience++;
            Player.MaxHealth--;
            Show();
        }

        private void ButtonClickPlusAttack(object sender, RoutedEventArgs e)
        {
            PlusExperience.Visibility = Visibility.Hidden;
            MinusExperience.Visibility = Visibility.Visible;
            Player.Experience--;
            Player.Attack++;
            Show();
        }

        private void ButtonClickMinusAttack(object sender, RoutedEventArgs e)
        {
            PlusExperience.Visibility = Visibility.Visible;
            MinusExperience.Visibility = Visibility.Hidden;
            Player.Experience++;
            Player.Attack--;
            Show();
        }

        private void ButtonClickPlusCure(object sender, RoutedEventArgs e)
        {
            PlusExperience.Visibility = Visibility.Hidden;
            MinusExperience.Visibility = Visibility.Visible;
            Player.Experience--;
            Player.Cure++;
            Show();
        }

        private void ButtonClickMinusCure(object sender, RoutedEventArgs e)
        {
            PlusExperience.Visibility = Visibility.Visible;
            MinusExperience.Visibility = Visibility.Hidden;
            Player.Experience++;
            Player.Cure--;
            Show();
        }
    }
}
