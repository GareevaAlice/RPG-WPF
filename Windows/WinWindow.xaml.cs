using System.Windows;

namespace RPG
{
    public partial class WinWindow : Window
    {
        public WinWindow()
        {
            InitializeComponent();
            textBlockWin.Text = "YOU WON!!!\n" + Player.Show();
        }
    }
}
