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
            if (!File.Exists("Memory.sav"))
                CreateSaveFile();

            ChangeSaveFile();
        }

        /// <summary>
        /// CreateSaveFile is a method that creates a memory.sav file.
        /// </summary>
        private void CreateSaveFile()
        {
            new XDocument(
                new XElement("Players", "Cards", "Positions", "Backgrounds")
            ).Save("Memory.sav");
        }

        /// <summary>
        /// ChangeSaveFile is a method that changes the memory.sav file.
        /// </summary>
        private void ChangeSaveFile()
        {

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
