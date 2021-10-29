using System.Windows;

namespace RPG
{
    public partial class RIPWindow : Window
    {
        public RIPWindow()
        {
            InitializeComponent();
            textBlockRIP.Text = Player.Show();
        }
    }
}
