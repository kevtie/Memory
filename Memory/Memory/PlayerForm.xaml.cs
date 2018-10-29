using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for PlayerForm.xaml
    /// </summary>
    public partial class PlayerForm : Page
    {
        private Main main = ((Main)Application.Current.MainWindow);
        private int playerCounter = 1;

        private const int PLAYER_LIMIT = 4;

        public PlayerForm()
        {
            InitializeComponent();
            SetButtons();
        }

        private void SetStartGameButton()
        {
            if (main.players.Count <= 0)
                StartGameButton.Visibility = Visibility.Hidden;
            else
                StartGameButton.Visibility = Visibility.Visible;
        }

        private void SetAddPlayerButton()
        {
            if (main.players.Count >= PLAYER_LIMIT)
                AddPlayerButton.Visibility = Visibility.Hidden;
            else
                AddPlayerButton.Visibility = Visibility.Visible;
        }

        private void SetButtons()
        {
            SetStartGameButton();
            SetAddPlayerButton();
        }

        private void Button_ClickAdd(object sender, RoutedEventArgs e)
        {
            SetPlayerData(new Player(playerCounter, false, 0, UserInput.Text));
            playerCounter++;
            SetButtons();
            UserInput.Clear();
        }

        private void SetPlayerData(Player player)
        {
            TextBlock nameBlock = new TextBlock();
            nameBlock.Text = $"Speler {player.Id}: {player.Name}";

            main.players.Add(player);
            ActivePlayerList.Items.Add(nameBlock);
        }

        private void Button_ClickStart(object sender, RoutedEventArgs e)
        {
            main.MainFrame.Content = new Game();
        }
    }
}
