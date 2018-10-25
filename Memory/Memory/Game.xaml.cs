using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Memory
{ 
    public partial class Game : Page
    {
        private const bool CARDS_START_STATE_FLIPPED = false;

        private const int FIRST_GAME_GRID_ROWS = 4;
        private const int FIRST_GAME_GRID_COLUMNS = 4;

        private const int SECOND_GAME_GRID_COLUMNS = 6;
        private const int SECOND_GAME_GRID_ROWS = 6;

        private const int CARD_SCORE_VALUE = 50;

        private const int START_PLAYER = 1;

        private Player currentPlayer;

        private Main main = ((Main)Application.Current.MainWindow);

        private List<Position> positions = new List<Position>();
        private List<Card> cards = new List<Card>();
        private List<Background> backgrounds = new List<Background>();
        private List<Player> players = new List<Player>();

        private int currentGameColumns = FIRST_GAME_GRID_ROWS;
        private int currentGameRows = FIRST_GAME_GRID_ROWS;

        private bool DEBUG = false;

        private const int PLAYER_COUNT = 4;

        public Game()
        {
            InitializeComponent();

            if (PLAYER_COUNT > 1)
                SetGridSize(SECOND_GAME_GRID_COLUMNS, SECOND_GAME_GRID_ROWS);

            InitializeGameGrid(currentGameColumns, currentGameRows);
            InitializeGameBoard();
        }

        private void InitializeGameBoard()
        {
            ClearGameBoard();
            SetPlayers();
        }

        private void SetCurrentPlayer(Player player)
        {
            currentPlayer = player;
        }

        private void SetTurn(Player player, bool turn = true)
        {
            player.Turn = turn;
            players.Remove(player);
            players.Add(player);
            SetCurrentPlayer(player);
        }

        private void SetScore(Player player, int score = CARD_SCORE_VALUE)
        {
            player.Score = player.Score + score;
            players.Remove(player);
            players.Add(player);
        }

        private void AddPlayers(int count = PLAYER_COUNT)
        {
            for(int i = 1; i < count + 1; i++)
            {
                players.Add(new Player(i, false, 0, $"Player {i}"));
            }
        }

        private Player GetActivePlayer()
        {
            return players.Where(p => p.Turn == true).ToList().First();
        }

        private Player GetPlayerById(int id)
        {
            return players.Where(p => p.Id == id).ToList().First();
        }

        private void HandleGameGridOptions()
        {
            switch (GameGridOptions.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "4x4":
                    InitializeGameGrid(FIRST_GAME_GRID_COLUMNS, FIRST_GAME_GRID_ROWS);
                    InitializeGameBoard();
                    break;
                case "6x6":
                    InitializeGameGrid(SECOND_GAME_GRID_COLUMNS, SECOND_GAME_GRID_ROWS);
                    InitializeGameBoard();
                    break;
            }
        }

        private void GameGridOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HandleGameGridOptions();
        }
            
        private void SetGridSize(int cols, int rows)
        {
            currentGameColumns = cols;
            currentGameRows = rows;
        }

        private int GetGridSize()
        {
            return currentGameColumns * currentGameRows;
        }

        private void RandomizePositions()
        {
            Random rng = new Random();
            positions = positions.OrderBy(x => rng.Next()).ToList();
        }

        private void SetCard(int id, int duplicateId, bool active, string title, int column, int row, bool flipped, string frontBackground, string backBackground)
        {

            Image image = new Image();

            image.Source = SetCardState(flipped, frontBackground, backBackground);
            image.Margin = new Thickness(2);
            image.Name = "c" + id.ToString();
            image.MouseDown += new MouseButtonEventHandler(Card_Click);


            if (DEBUG)
            {
                TextBlock tb = new TextBlock();
                var bold = new Bold(new Run(title));
                tb.Inlines.Add(bold);
            }

            Grid.SetColumn(image, column);
            Grid.SetRow(image, row);
            GameGrid.Children.Add(image);
        }

        private ImageSource SetCardState(bool flipped, string frontBackground, string backBackground)
        {
            if(flipped)
                return new BitmapImage(new Uri(frontBackground, UriKind.Relative));

            return new BitmapImage(new Uri(backBackground, UriKind.Relative));
        }

        private void SetCards()
        {
            foreach(var card in cards)
            {
                SetCard(card.Id, card.DuplicateId, card.Active, card.Title, card.Column, card.Row, card.Flipped, card.FrontBackground, card.BackBackground); 
            }
        }

        private void AddCard(int id, int duplicateId, bool active, int column, int row, string title, bool flipped, string frontBackground, string backBackground)
        {
            cards.Add(new Card(id, duplicateId, active, column, row, title, flipped, frontBackground, backBackground));
        }

        private void AddPositions(int cols, int rows)
        {
            for(int row = 0; row < rows; row++)
            {
                for(int col = 0; col < cols; col++) 
                {
                    positions.Add(new Position(row, col));
                }
            }
        }

        private void AddBackgrounds()
        {
            for(int i = 1; i <= GetGridSize() / 2; i++)
            {
                backgrounds.Add(new Background(i, "Resources/card_back.jpg", $"Resources/{i}.jpg"));
            }
        }

        private void AddCards()
        {
            int id = 1;
            int duplicateId = 1;
            //Brush cbg = new SolidColorBrush(Colors.Red);
            string frontBg = "";
            string backBg = "";

            foreach(var pos in positions)
            {
                if(duplicateId > GetGridSize() / 2)
                    duplicateId = 1;

                foreach(var bg in backgrounds)
                {
                    if(duplicateId == bg.Id) 
                    {
                        frontBg = bg.Front;
                        backBg = bg.Back;
                    }
                }

                 AddCard(
                    id, 
                    duplicateId,
                    CARDS_START_STATE_FLIPPED,
                    pos.X,
                    pos.Y, 
                    $"Card {duplicateId} [{pos.X} - {pos.Y}] ({positions.Count})", 
                    CARDS_START_STATE_FLIPPED, 
                    frontBg,     
                    backBg
                );

                duplicateId++;
                id++;
            }
        }

        private SolidColorBrush GetRandomColor(int seed)
        {
            Random random = new Random(GetRandomNumber(seed));

            return new SolidColorBrush(
                Color.FromRgb(
                    (byte)random.Next(256),
                    (byte)random.Next(256),
                    (byte)random.Next(256)
                ));
        }

        private int GetRandomNumber(int max, int min = 0)
        {
            return new Random().Next(min, max);
        }

        private void ClearGrid()
        {
            positions.Clear();
            cards.Clear();
            players.Clear();
            GameGrid.RowDefinitions.Clear();
            GameGrid.ColumnDefinitions.Clear();
        }

        private void ClearGameBoard()
        {
            GameBoard.Children.Clear();
        }

        private void FlipCard(Card card)
        {
            if(!card.Flipped) 
            {
                card.Flipped = true;
            }
            else 
            {
                card.Flipped = false;
            }

            SetActiveCard(card, card.Flipped);
        }

        public void InitializeGameGrid(int cols, int rows)
        {
            ClearGrid();
            AddPlayers();
            SetGridSize(cols, rows);
            AddPositions(cols, rows);
            AddBackgrounds();
            RandomizePositions();
            AddCards();
            SetCards();
            CreateGrid(cols, rows);
            SetTurn(GetPlayerById(START_PLAYER));
        }

        private void CreateGrid(int cols, int rows)
        {
             for (int i = 0; i < cols; i++)
             {
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
             }

            for (int i = 0; i < rows; i++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition());
            }
        }

        private void SetPlayers(int count = PLAYER_COUNT)
        {
            SolidColorBrush foreground = new SolidColorBrush(Colors.Red);

            foreach(var player in players)
            {
                if (player.Turn)
                    foreground = new SolidColorBrush(Colors.Green);

                GameBoard.Children.Add(
                    new TextBlock
                    {
                        Text = $"{player.Name}: Score: {player.Score}: Turn: {player.Turn}",
                        Margin = new Thickness(2),
                        Foreground = foreground,
                        FontSize = 20,
                        HorizontalAlignment = HorizontalAlignment.Center
                    }
                );
            }
        }

        private Card GetCardById(int id)
        {
            return cards.Where(c => c.Id == id).ToList().First();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            InitializeGameGrid(currentGameColumns, currentGameRows);
            InitializeGameBoard();
        }

        private void Card_Click(object sender, MouseButtonEventArgs e)
        {
            Page page = new Page();
            Image image = (Image)sender;

            int id = Convert.ToInt32(image.Name.Remove(0, 1));

            Card card = GetCardById(id);

            if(!card.Flipped)
            {
                FlipCard(card);
                CompareCards();
                SetCards();
            }

            if (GetFlippedCards().Count == cards.Count)
            {
                InitializeGameGrid(currentGameColumns, currentGameRows);
            }
        }

        private List<Card> GetActiveCards()
        {
            return cards.Where(c => c.Active == true).ToList();
        }

        private List<Card> GetFlippedCards()
        {
            return cards.Where(c => c.Flipped == true).ToList();
        }

        private void CompareCards()
        {
            List<Card> activeCards = GetActiveCards();

            if(activeCards.Count > 2)
            {
                Card card1 = activeCards.ElementAt(0);
                Card card2 = activeCards.ElementAt(1);

                if(card1.DuplicateId == card2.DuplicateId)
                {
                    SetActiveCard(card1, false);
                    SetActiveCard(card2, false);
                    SetScore(GetActivePlayer());
                    SetTurn(currentPlayer);
                }
                else
                {
                    FlipCard(card1);
                    FlipCard(card2);
                    SetTurn(currentPlayer, false);

                    if(currentPlayer.Id >= players.Count)
                        SetTurn(GetPlayerById(START_PLAYER));
                    else
                        SetTurn(GetPlayerById(currentPlayer.Id + 1));
                }

                InitializeGameBoard();
            }
        }

        private void SetActiveCard(Card card, bool active)
        {
            card.Active = active;
            cards.Remove(card);
            cards.Add(card);
        }
    }
}
