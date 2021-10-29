using System.Text;
using System.Windows;
using System.IO;


namespace RPG
{
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
            string path = "../../templates/About.txt";
            using (StreamReader sr = File.OpenText(path))
            {
                textBlockAbout.Text = sr.ReadToEnd();
            }
        }
    }
}
