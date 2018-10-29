using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for HighScore.xaml
    /// </summary>
    public partial class HighScore : Page
    {
        private Main main = ((Main)Application.Current.MainWindow);
        private ObservableCollection<Player> highscoreList = new ObservableCollection<Player>();

        private const int HIGH_SCORE_LIMIT = 10;

        public HighScore()
        {
            InitializeComponent();
            CreateGrid();
            SetHighScore();
        }

        private void AddHighScore()
        {
            foreach(var player in main.players.OrderByDescending(i => i.Score).Take(HIGH_SCORE_LIMIT))
                highscoreList.Add(player);
        }

        private void CreateGrid(int rows = HIGH_SCORE_LIMIT + 1)
        {
            for (int i = 0; i < rows; i++)
                HighScoreGrid.RowDefinitions.Add(new RowDefinition());
        }

        private void SetHighScore()
        {
            int row = 1;
            AddHighScore();

            foreach(var player in highscoreList)
            {
                TextBlock rankBlock = new TextBlock();
                TextBlock scoreBlock = new TextBlock();
                TextBlock nameBlock = new TextBlock();

                rankBlock.Text = player.Id.ToString();
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
