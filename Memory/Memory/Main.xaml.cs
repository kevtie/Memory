using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Linq;

namespace Memory
{
    /// <summary>
    /// Main is the class that holds the interaction logic for Main.xaml.
    /// </summary>
    public partial class Main : Window  
    {

        private const int MAIN_SAVED_TEXT_DELAY = 1000;

        public int currentGameColumns;
        public int currentGameRows;


        public List<Card> cards = new List<Card>();
        public List<Player> players = new List<Player>();

        /// <summary>
        /// Main is a method that gets excecuted when a new Main object is created.
        /// </summary>
        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Button_Clicknew is a navgition button that empties the players list and shows a page with a player form in it.
        /// In the playerform you can add players to the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Clicknew(object sender, RoutedEventArgs e)
        {
            players.Clear();
            MainFrame.Content = new PlayerForm();
        }

        /// <summary>
        /// Button_Click is a navigation button that shows options in a dropdown menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (sender as Button);
            button.ContextMenu.IsEnabled = true;
            button.ContextMenu.PlacementTarget = button;
            button.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            button.ContextMenu.IsOpen = true;
        }

        /// <summary>
        /// Button_Clickload is a button action that loads data from the memory.sav file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Clickload(object sender, RoutedEventArgs e)
        {
            LoadSaveFile();
        }

        /// <summary>
        /// Button_Clickhelp is a button action that shows a page called Help.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Clickhelp(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Help();
        }

        /// <summary>
        /// Button_ClickHighScore is a button that shows a page called HighScore.
        /// Under construction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickHighScore(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new HighScore();
        }

        /// <summary>
        /// Button_ClickSave is a button action that saves the current state of the game.
        /// Under construction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickSave(object sender, RoutedEventArgs e)
        {
            if (File.Exists("Memory.sav"))
                RemoveSaveFile();

            if (!File.Exists("Memory.sav"))
                CreateSaveFile();

            SetMainSavedText();
            AddGameData();
        }

        /// <summary>
        /// SetStartGameButton is a method that toggles between hidden and visble on the StartGameButton.
        /// </summary>
        private void SetMainSavedText()
        {
            MainSavedText.Visibility = Visibility.Visible;

            Task.Delay(MAIN_SAVED_TEXT_DELAY).ContinueWith(_ =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    MainSavedText.Visibility = Visibility.Hidden;
                });
            });
        }

        /// <summary>
        /// CreateSaveFile is a method that creates a memory.sav file.
        /// </summary>
        private void CreateSaveFile()
        {
            XDocument doc = new XDocument(new XElement("Main"));
            doc.Root.Add(new XElement("Players"));
            doc.Root.Add(new XElement("Cards"));
            doc.Root.Add(new XElement("Grid"));
            doc.Save("Memory.sav");
        }

        private void RemoveSaveFile()
        {
            File.Delete("Memory.sav");
        }

        private void GetAndSetGridSize()
        {
            XDocument doc = XDocument.Load("Memory.sav");

            XElement grid = doc.Element("Main").Element("Grid");

            if (grid.Element("Columns") != null && grid.Element("Rows") != null)
            {
                currentGameColumns = Convert.ToInt32(grid.Element("Columns").Value);
                currentGameRows = Convert.ToInt32(grid.Element("Rows").Value);
            }
        }

        /// <summary>
        /// GetPlayersFromSaveFile is a method that gets the data from memory.sav file and puts them into global variables.
        /// </summary>
        private void GetPlayersFromSaveFile()
        {
            XDocument doc = XDocument.Load("Memory.sav");

            if (doc.Element("Main").Element("Players").Elements() != null)
            {
                players.Clear();

                foreach (XElement player in doc.Element("Main").Element("Players").Elements())
                {
                    int id = Convert.ToInt32(player.Element("Id").Value);
                    bool turn = Convert.ToBoolean(player.Element("Turn").Value);
                    int score = Convert.ToInt32(player.Element("Score").Value);
                    string name = player.Element("Name").Value;

                    players.Add(new Player(id, turn, score, name));
                }
            }
        }

        private void GetCardsFromSaveFile()
        {
            XDocument doc = XDocument.Load("Memory.sav");

            if (doc.Element("Main").Element("Cards").Elements() != null)
            {
                cards.Clear();

                foreach (XElement card in doc.Element("Main").Element("Cards").Elements())
                {
                    int id = Convert.ToInt32(card.Element("Id").Value);
                    int dId = Convert.ToInt32(card.Element("DuplicateId").Value);
                    bool active = Convert.ToBoolean(card.Element("Active").Value);
                    int column = Convert.ToInt32(card.Element("Column").Value);
                    int row = Convert.ToInt32(card.Element("Row").Value);
                    string title = card.Element("Title").Value;
                    bool flipped = Convert.ToBoolean(card.Element("Flipped").Value);
                    string fBg = card.Element("FrontBackground").Value;
                    string bBg = card.Element("BackBackground").Value;

                    cards.Add(new Card(id, dId, active, column, row, title, flipped, fBg, bBg));
                }
            }
        }

        private void AddCardToSaveFile(Card card)
        {
            XDocument doc = XDocument.Load("Memory.sav");
            XElement cards = doc.Element("Main").Element("Cards");

            cards.Add(
               new XElement("Card",
                   new XElement("Id", card.Id),
                   new XElement("DuplicateId", card.DuplicateId),
                   new XElement("Active", card.Active),
                   new XElement("Column", card.Column),
                   new XElement("Row", card.Row),
                   new XElement("Title", card.Title),
                   new XElement("Flipped", card.Flipped),
                   new XElement("FrontBackground", card.FrontBackground),
                   new XElement("BackBackground", card.BackBackground)
               )
           );

            doc.Save("Memory.sav");
        }

        private void AddGridSizeToSaveFile(int cols, int rows)
        {
            XDocument doc = XDocument.Load("Memory.sav");
            XElement grid = doc.Element("Main").Element("Grid");

            grid.Add(
                new XElement("Columns", cols),
                new XElement("Rows", rows)
            );

            doc.Save("Memory.sav");
        }

        private void AddPlayerToSaveFile(Player player)
        {
            XDocument doc = XDocument.Load("Memory.sav");
            XElement players = doc.Element("Main").Element("Players");

            players.Add(
                new XElement("Player",
                    new XElement("Id", player.Id),
                    new XElement("Score", player.Score),
                    new XElement("Turn", player.Turn),
                    new XElement("Name", player.Name)
                    //new XElement("Status", player.Status)
                )
            );

            doc.Save("Memory.sav");
        }

        /// <summary>
        /// AddGameData is a method that changes the memory.sav file.
        /// </summary>
        private void AddGameData()
        {
            if (Convert.ToBoolean(players.Count))
            {
                foreach (Player player in players)
                    AddPlayerToSaveFile(player);
            }

            if (Convert.ToBoolean(cards.Count))
            {
                foreach (Card card in cards)
                    AddCardToSaveFile(card);
            }

            if (currentGameColumns > 0 && currentGameRows > 0)
            {
                AddGridSizeToSaveFile(currentGameColumns, currentGameRows);
            }
        }

        /// <summary>
        /// LoadSaveFile is a method that loads data from memory.sav file.
        /// </summary>
        private void LoadSaveFile()
        {
            if (File.Exists("Memory.sav"))
            {
                GetPlayersFromSaveFile();
                GetCardsFromSaveFile();
                GetAndSetGridSize();

                if (players.Any() && cards.Any() && currentGameColumns > 0 && currentGameRows > 0)
                {
                    MainFrame.Content = new Game(true);
                }
            }
        }

        /// <summary>
        /// Button_Clickquit is a button action that closes the Main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Clickquit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
