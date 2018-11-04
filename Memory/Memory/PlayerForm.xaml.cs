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
    /// PlayerForm is a class thats holds the Interaction logic for PlayerForm.xaml
    /// </summary>
    public partial class PlayerForm : Page
    {
        private Main main = ((Main)Application.Current.MainWindow);
        private int playerCounter = 1;

        private const int PLAYER_LIMIT = 4;

        /// <summary>
        /// PlayerForm is a method that gets excecuted when a new PlayerForm object is created.
        /// </summary>
        public PlayerForm()
        {
            InitializeComponent();
            SetButtons();
            UserInput.Focus();
        }

        /// <summary>
        /// SetStartGameButton is a method that toggles between hidden and visble on the StartGameButton.
        /// </summary>
        private void SetStartGameButton()
        {
            if (main.players.Count <= 0)
                StartGameButtonBorder.Visibility = Visibility.Hidden;
            else
                StartGameButtonBorder.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// SetAddPlayerButton is a method that toggles between hidden and visble on the AddPlayerButton.
        /// </summary>
        private void SetAddPlayerButton()
        {
            if (main.players.Count >= PLAYER_LIMIT)
            {
                UserInputBorder.Visibility = Visibility.Hidden;
                AddPlayerButtonBorder.Visibility = Visibility.Hidden;
            }
            else
                AddPlayerButtonBorder.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// SetButtons is a method that excecutes all button toggle methods.
        /// </summary>
        private void SetButtons()
        {
            SetStartGameButton();
            SetAddPlayerButton();
        }

        /// <summary>
        /// Button_ClickAdd is a button action sets player and button data for adding a player to a ListView called ActivePlayerList.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAdd(object sender, RoutedEventArgs e)
        {
            if(UserInput.Text.Length > 0)
            {
                SetPlayerData(new Player(playerCounter, false, 0, UserInput.Text));
                playerCounter++;
                SetButtons();
                ActivePlayerListBorder.Visibility = Visibility.Visible;
                UserInput.Clear();
            }
        }

        /// <summary>
        /// SetPlayerData is a method that adds a Player TextBlock to a ListView called ActivePlayerList.
        /// This method also adds a player to the main.players list from Main : Window.
        /// </summary>
        /// <param name="player"></param>
        private void SetPlayerData(Player player)
        {
            TextBlock nameBlock = new TextBlock();
            nameBlock.Text = $"Speler {player.Id}: {player.Name}";
            main.players.Add(player);
            ActivePlayerList.Items.Add(nameBlock);
        }

        /// <summary>
        /// Button_ClickStart is a button action that switches to the Game : Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickStart(object sender, RoutedEventArgs e)
        {
            main.MainFrame.Content = new Game();
        }
    }
}
