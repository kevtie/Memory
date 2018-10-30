using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Xml;
using System.Xml.Linq;

namespace Memory
{
    /// <summary>
    /// Interaction logic for HighScore.xaml
    /// </summary>
    public partial class HighScore : Page
    {
        private Main main = ((Main)Application.Current.MainWindow);
        private List<Player> highScoreList = new List<Player>();

        private const int HIGH_SCORE_LIMIT = 10;

        public HighScore()
        {
            InitializeComponent();
            CreateGrid();

            if (!File.Exists("HighScores.xml"))
                CreateHighScoreFile();
            else
                GetHighScoreFileData();

            SetHighScores();
        }

        private void AddHighScores()
        {
            foreach (var player in main.players)
                AddPlayerHighScore(player);
        }

        private void CreateGrid(int rows = HIGH_SCORE_LIMIT + 1)
        {
            HighScoreGrid.RowDefinitions.Clear();

            for (int i = 0; i < rows; i++)
                HighScoreGrid.RowDefinitions.Add(new RowDefinition());
        }

        private void CreateHighScoreFile()
        {
            new XDocument(
                new XElement("Players")
            ).Save("HighScores.xml");
        }

        private void AddPlayerHighScore(Player player)
        {
            XDocument doc = XDocument.Load("HighScores.xml");
            XElement players = doc.Element("Players");

            players.Add(
                new XElement("Player", new XAttribute("Id", player.Id),
                    new XElement("Score", player.Score),
                    new XElement("Turn", player.Turn),
                    new XElement("Name", player.Name)
                    //Player.status gewonnen of verloren
                )
            );

            doc.Save("HighScores.xml");
        }

        private void GetHighScoreFileData()
        {
            XDocument doc = XDocument.Load("HighScores.xml");
            foreach (XElement player in doc.Element("Players").Elements())
            {
                bool playerTurn = Convert.ToBoolean(player.Element("Turn").Value);
                string playerName = player.Element("Name").Value;
                int playerScore = Convert.ToInt32(player.Element("Score").Value);

                highScoreList.Add(new Player(0, playerTurn, playerScore, playerName));
            }
        }

        private void SetHighScores()
        {
            int row = 1;

            if(main.players.Count > 0)
                AddHighScores();

            foreach (var player in highScoreList.OrderByDescending(i => i.Score).Take(HIGH_SCORE_LIMIT))
            {
                TextBlock rankBlock = new TextBlock();
                TextBlock scoreBlock = new TextBlock();
                TextBlock nameBlock = new TextBlock();

                rankBlock.Text = row.ToString();
                scoreBlock.Text = player.Score.ToString();
                nameBlock.Text = player.Name;

                Grid.SetColumn(rankBlock, 0);
                Grid.SetColumn(scoreBlock, 1);
                Grid.SetColumn(nameBlock, 2);

                Grid.SetRow(rankBlock, row);
                Grid.SetRow(scoreBlock, row);
                Grid.SetRow(nameBlock, row);

                HighScoreGrid.Children.Add(rankBlock);
                HighScoreGrid.Children.Add(scoreBlock);
                HighScoreGrid.Children.Add(nameBlock);

                row++;
            }
        }
    }
}
