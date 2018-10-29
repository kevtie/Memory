using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memory
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window  
    {
        public List<Player> players = new List<Player>();

        public Main()
        {
            InitializeComponent();
        }

        private void Button_Clicknew(object sender, RoutedEventArgs e)
        {
            players.Clear();
            MainFrame.Content = new PlayerForm();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (sender as Button);
            button.ContextMenu.IsEnabled = true;
            button.ContextMenu.PlacementTarget = button;
            button.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            button.ContextMenu.IsOpen = true;
        }

        private void Button_Clickload(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Load();
        }

        private void Button_Clickhelp(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Help();
        }

        private void Button_Clickscore(object sender, RoutedEventArgs e)
        {
            //MainFrame.Content = new HighScore();
        }
        private void Button_Clicksave(object sender, RoutedEventArgs e)
        {
            
        }
        private void Button_Clickquit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
