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

            if (Convert.ToBoolean(players.Count))
            {
                foreach(Player player in players)
                    AddPlayerToSaveFile(player);
            }
        }

        /// <summary>
        /// CreateSaveFile is a method that creates a memory.sav file.
        /// </summary>
        private void CreateSaveFile()
        {
            new XDocument(
                new XElement("Players")
            ).Save("Memory.sav");
        }

        private void RemoveSaveFile()
        {
            File.Delete("Memory.sav");
        }

        /// <summary>
        /// GetGameData is a method that gets the data from memory.sav file and puts them into global variables.
        /// </summary>
        private void GetGameData()
        {

        }

        private void AddCardsToSaveFile(Card card)
        {
            XDocument doc = XDocument.Load("Memory.sav");
            XElement cards = doc.Element("Cards");

            cards.Add(
               new XElement("Player",
                   new XElement("Id", card.Id),
                   new XElement("DuplicateId", card.DuplicateId),
                   new XElement("Active", card.Active),
                   new XElement("Row", card.Row),
                   new XElement("Column", card.Column),
                   new XElement("Flipped", card.Flipped),
                   new XElement("FrontBackground", card.FrontBackground),
                   new XElement("BackBackground", card.BackBackground)
               )
           );
        }

        private void AddPlayerToSaveFile(Player player)
        {
            XDocument doc = XDocument.Load("Memory.sav");
            XElement players = doc.Element("Players");

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
            //XDocument doc = XDocument.Load("Memory.sav");
            //XElement players = doc.Element("Players");
            //XElement cards = doc.Element("Cards");
            //XElement positions = doc.Element("Positions");
            //XElement backgrounds = doc.Element("Backgrounds");

            //players.Add(
            //    new XElement("Player",
            //        new XElement("Id", player.Id),
            //        new XElement("Score", player.Score),
            //        new XElement("Turn", player.Turn),
            //        new XElement("Name", player.Name)
            //    //new XElement("Status", player.Status)
            //    )
            //);

            //cards.Add(
            //    new XElement("Player",
            //        new XElement("Id", card.Id),
            //        new XElement("DuplicateId", card.DuplicateId),
            //        new XElement("Active", card.Active)
            //        new XElement("Row", card.Row)
            //        new XElement("Column", card.Column)
            //        new XElement("Flipped", card.Flipped)
            //        new XElement("FrontBackground", card.frontBackground)
            //        new XElement("BackBackground", card.backBackground)
            //    )
            //);

            //players.Add(
            //    new XElement("Player",
            //        new XElement("Id", player.Id),
            //        new XElement("Score", player.Score),
            //        new XElement("Turn", player.Turn),
            //        new XElement("Name", player.Name)
            //    //new XElement("Status", player.Status)
            //    )
            //);

            //players.Add(
            //    new XElement("Player",
            //        new XElement("Id", player.Id),
            //        new XElement("Score", player.Score),
            //        new XElement("Turn", player.Turn),
            //        new XElement("Name", player.Name)
            //    //new XElement("Status", player.Status)
            //    )
            //);
        }

        /// <summary>
        /// LoadSaveFile is a method that loads data from memory.sav file.
        /// </summary>
        private void LoadSaveFile()
        {

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
