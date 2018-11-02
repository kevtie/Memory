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
    /// <summary>
    /// Game is a class thats holds the Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        private const bool CARDS_START_STATE_FLIPPED = false;
        private const bool CARD_COMPARE_DEACTIVATE = true;

        private const int FIRST_GAME_GRID_ROWS = 4;
        private const int FIRST_GAME_GRID_COLUMNS = 4;

        private const int SECOND_GAME_GRID_COLUMNS = 6;
        private const int SECOND_GAME_GRID_ROWS = 6;

        private const int CARD_SCORE_VALUE = 50;
        private const int CARD_SCORE_NEGATIVE_VALUE = -CARD_SCORE_VALUE / 5;

        private const int START_PLAYER = 1;

        private const int WON_GAME_TRANSITION_DELAY = 2000;
        private const int CARD_COMPARE_DELAY = 500;

        private bool cardsClickable = true;

        //Misschien wie gewonnen heeft popup

        private Player currentPlayer;

        private List<Position> positions = new List<Position>();
        private List<Background> backgrounds = new List<Background>();

        private Main main = ((Main)Application.Current.MainWindow);

        private bool gameGridOptionsNoise = false;

        /// <summary>
        /// Game is a method that gets excecuted when a new Game object is created.
        /// </summary>
        public Game(bool loaded = false)
        {
            InitializeComponent();

            gameGridOptionsNoise = true;

            if(!loaded)
            {
                if (main.players.Count > 1)
                    SetGridSize(SECOND_GAME_GRID_COLUMNS, SECOND_GAME_GRID_ROWS);
                else
                    SetGridSize(FIRST_GAME_GRID_COLUMNS, FIRST_GAME_GRID_ROWS);

                InitializeGameGrid(main.currentGameColumns, main.currentGameRows);
            }
            else
            {
                SetCurrentPlayer(GetActivePlayer());
                LoadGameGrid();
            }

            InitializeGameBoard();

            gameGridOptionsNoise = false;
        }

        /// <summary>
        /// SetGridOptionValue is a method that sets GameGridOptions.Text to current grid data.
        /// </summary>
        private void SetGridOptionValue()
        {   
            GameGridOptions.Text = $"{main.currentGameColumns}x{main.currentGameRows}";
        }

        /// <summary>
        /// InitializeGameBoard is a method that makes sets the GameBoard container data.
        /// </summary>
        private void InitializeGameBoard()
        {
            ClearGameBoard();
            SetPlayers();
        }

        /// <summary>
        /// SetCurrentPlayer is a method that sets currentPlayer variable to a Player object.
        /// </summary>
        /// <param name="player">Player object.</param>
        private void SetCurrentPlayer(Player player)
        {
            currentPlayer = player;
        }

        /// <summary>
        /// SetTurn is a method that sets a Player objects turn to true or false.
        /// </summary>
        /// <param name="player">Player Object.</param>
        /// <param name="turn">Boolean.</param>
        private void SetTurn(Player player, bool turn = true)
        {
            player.Turn = turn;
            main.players.Remove(player);
            main.players.Add(player);
            SetCurrentPlayer(player);
        }

        /// <summary>
        /// SetScore is a method that sets a Player objects score to a chosen value.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="score"></param>
        private void SetScore(Player player, int score = CARD_SCORE_VALUE)
        {
            if (score == 0)
                player.Score = 0;
            else
                player.Score = player.Score + score;

            main.players.Remove(player);
            main.players.Add(player);
        }

        /// <summary>
        /// ResetScoresAndTurns is a method that resets the scores and turns of all current active players.
        /// </summary>
        private void ResetScoresAndTurns()
        {
            foreach(var player in main.players.ToList())
            {
                SetTurn(player, false);
                SetScore(player, 0);
            }
        }

        /// <summary>
        /// GetActivePlayer is a method that returns a Player object where turn is true.
        /// </summary>
        /// <returns>Player Object</returns>
        private Player GetActivePlayer()
        {
            return main.players.Where(p => p.Turn == true).ToList().First();
        }

        /// <summary>
        /// GetPlayerById is a method that returns a player Object from id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Player Object</returns>
        private Player GetPlayerById(int id)
        {
            return main.players.Where(p => p.Id == id).ToList().First();
        }

        /// <summary>
        /// HandleGameGridOptions is a method that sets GridOptions items functionality.
        /// </summary>
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

        /// <summary>
        /// GameGridOptions_SelectionChanged is a method that checks for changes in GridOptions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameGridOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gameGridOptionsNoise) return;
            HandleGameGridOptions();
        }

        /// <summary>
        /// SetGridSize is a method that sets currentGame cols and rows global variables.
        /// </summary>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
        private void SetGridSize(int cols, int rows)
        {
            main.currentGameColumns = cols;
            main.currentGameRows = rows;
        }

        /// <summary>
        /// GetGridSize is a method that gets current grid size from global cols and rows variables.
        /// </summary>
        private int GetGridSize()
        {
            return main.currentGameColumns * main.currentGameRows;
        }

        /// <summary>
        /// RandomizePositions is a method that randomizes global Position Objects list.
        /// </summary>
        private void RandomizePositions()
        {
            Random rng = new Random();
            positions = positions.OrderBy(x => rng.Next()).ToList();
        }

        /// <summary>
        /// SetCard is a method that adds and positions a card object on the GameGrid.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="duplicateId"></param>
        /// <param name="active"></param>
        /// <param name="title"></param>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <param name="flipped"></param>
        /// <param name="frontBackground"></param>
        /// <param name="backBackground"></param>
        private void SetCard(int id, int duplicateId, bool active, string title, int column, int row, bool flipped, string frontBackground, string backBackground)
        {
            Image image = new Image();
            Border border = new Border();

            image.Source = SetCardState(flipped, frontBackground, backBackground);
            image.Name = "c" + id.ToString();
            image.Margin = new Thickness(2);
            image.MouseDown += new MouseButtonEventHandler(Card_Click);

            Grid.SetColumn(image, column);
            Grid.SetRow(image, row);
            GameGrid.Children.Add(image);
        }


        /// <summary>
        /// SetCardState is a method that sets card background based on flipped boolean.
        /// </summary>
        /// <param name="flipped"></param>
        /// <param name="frontBackground"></param>
        /// <param name="backBackground"></param>
        /// <returns></returns>
        private ImageSource SetCardState(bool flipped, string frontBackground, string backBackground)
        {
            if(flipped)
                return new BitmapImage(new Uri(frontBackground, UriKind.Relative));

            return new BitmapImage(new Uri(backBackground, UriKind.Relative));
        }

        /// <summary>
        /// SetCards is a method that adds all global main.cards and positions them on the GameGrid.
        /// </summary>
        private void SetCards()
        {
            foreach(var card in main.cards)
            {
                SetCard(card.Id, card.DuplicateId, card.Active, card.Title, card.Column, card.Row, card.Flipped, card.FrontBackground, card.BackBackground); 
            }
        }

        /// <summary>
        /// AddPositions is a method that adds all positions to the global position objects list.
        /// </summary>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
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

        /// <summary>
        /// AddBackgrounds is a method that adds all backgrounds to the global background objects list.
        /// </summary>
        private void AddBackgrounds()
        {
            for(int i = 1; i <= GetGridSize() / 2; i++)
                backgrounds.Add(new Background(i, "Resources/card_back.jpg", $"Resources/{i}.jpg"));
        }

        /// <summary>
        /// AddBackgrounds is a method that adds all main.cards to the global card objects list.
        /// </summary>
        private void AddCards()
        {
            int id = 1;
            int duplicateId = 1;
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

                main.cards.Add(new Card(id, duplicateId, CARDS_START_STATE_FLIPPED, pos.X, pos.Y, $"Card {duplicateId} [{pos.X} - {pos.Y}] ({positions.Count})", CARDS_START_STATE_FLIPPED, frontBg, backBg));
                duplicateId++;
                id++;
            }
        }

        /// <summary>
        /// GetRandomNumber is a method that returns a random number.
        /// </summary>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <returns>Random Number</returns>
        private int GetRandomNumber(int max, int min = 0)
        {
            return new Random().Next(min, max);
        }

        /// <summary>
        /// ClearGrid is a method that removes all items on the GameGrid.
        /// </summary>
        private void ClearGrid()
        {
            GameGrid.RowDefinitions.Clear();
            GameGrid.ColumnDefinitions.Clear();
        }

        private void ClearGameData()
        {
            positions.Clear();
            main.cards.Clear();
        }

        /// <summary>
        /// ClearGameBoard is a method that removes all data inside the GameBoard.
        /// </summary>
        private void ClearGameBoard()
        {
            GameBoard.Children.Clear();
        }

        /// <summary>
        /// FlipCard is a method that sets card Object flipped state.
        /// </summary>
        /// <param name="card"></param>
        private void FlipCard(Card card)
        {
            cardsClickable = false;

            if (!card.Flipped) 
                card.Flipped = true;
            else 
                card.Flipped = false;

            SetActiveCard(card, card.Flipped);
        }

        private void LoadGameGrid()
        {
            ClearGrid();
            SetGridSize(main.currentGameColumns, main.currentGameRows);
            CreateGrid(main.currentGameColumns, main.currentGameRows);
            SetCards();
            SetGridOptionValue();
        }

        /// <summary>
        /// InitializeGameGrid is a method that activates all methods for initializing the GameGrid.
        /// </summary>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
        public void InitializeGameGrid(int cols, int rows)
        {
            ClearGrid();
            ClearGameData();
            ResetScoresAndTurns();
            SetGridSize(cols, rows);
            CreateGrid(cols, rows);
            AddPositions(cols, rows);
            AddBackgrounds();
            RandomizePositions();
            AddCards();
            SetCards();
            SetGridOptionValue();
            SetTurn(GetPlayerById(START_PLAYER));
        }

        /// <summary>
        /// CreateGrid is a method that creates rows and columns for the GameGrid.
        /// </summary>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
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

        /// <summary>
        /// SetPlayers is a method that sets all active player data on the GameBoard.
        /// </summary>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
        private void SetPlayers()
        {
            SolidColorBrush foreground = new SolidColorBrush(Colors.Red);

            foreach(var player in main.players)
            {
                if (player.Turn)
                    foreground = new SolidColorBrush(Colors.Green);

                GameBoard.Children.Add(
                    new TextBlock
                    {
                        Text = $"Speler {player.Id}: {player.Name}: Score: {player.Score}",
                        Margin = new Thickness(2),
                        Padding = new Thickness(2),
                        Background = new SolidColorBrush(Colors.White),
                        Foreground = foreground,
                        FontSize = 20,
                        HorizontalAlignment = HorizontalAlignment.Center
                    }
                );
            }
        }

        /// <summary>
        /// GetCardById is a method that return a card object based on specific Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Card Object</returns>
        private Card GetCardById(int id)
        {
            return main.cards.Where(c => c.Id == id).ToList().First();
        }

        /// <summary>
        /// Reset_Click is a button action that reset the GameGrid and GameBoard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            InitializeGameGrid(main.currentGameColumns, main.currentGameRows);
            InitializeGameBoard();
        }

        /// <summary>
        /// Card_Click is a method that flips the card the player clicked on.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Card_Click(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image)sender;
            Card card = GetCardById(Convert.ToInt32(image.Name.Remove(0, 1)));

            if (!card.Flipped && cardsClickable)
            {
                FlipCard(card);

                List<Card> activeCards = GetActiveCards();

                SetCards();

                if (activeCards.Count == 2)
                {
                    CompareCards(activeCards.ElementAt(0), activeCards.ElementAt(1));
                }

                Task.Delay(CARD_COMPARE_DELAY).ContinueWith(_ =>
                {
                    main.Dispatcher.Invoke(() =>
                    {
                        cardsClickable = true;
                        SetCards();
                    });
                });
            }

            if (GetFlippedCards().Count == main.cards.Count)
            {
                Task.Delay(WON_GAME_TRANSITION_DELAY).ContinueWith(_ =>
                {
                    main.Dispatcher.Invoke(() =>
                    {
                        NavigationService.Navigate(new HighScore());
                    });
                });
            }
        }

        /// <summary>
        /// GetActiveCards is a method that return all active main.cards.
        /// </summary>
        /// <returns>Card Object List</returns>
        private List<Card> GetActiveCards()
        {
            return main.cards.Where(c => c.Active == true).ToList();
        }

        /// <summary>
        /// GetFlippedCards is a method that returns aall flipped main.cards.
        /// </summary>
        /// <returns>Card Object List</returns>
        private List<Card> GetFlippedCards()
        {
            return main.cards.Where(c => c.Flipped == true).ToList();
        }

        /// <summary>
        /// CompareCards is a method that compares duplicateId of two main.cards.
        /// </summary>
        private void CompareCards(Card cardOne, Card cardTwo)
        {
            if(cardOne.DuplicateId == cardTwo.DuplicateId)
            {
                if (CARD_COMPARE_DEACTIVATE)
                {
                    SetActiveCard(cardOne, false);
                    SetActiveCard(cardTwo, false);
                }
                else
                {
                    main.cards.Remove(cardOne);
                    main.cards.Remove(cardTwo);
                }
                
                SetScore(GetActivePlayer());
                SetTurn(currentPlayer);
            }
            else
            {
                FlipCard(cardOne);
                FlipCard(cardTwo);
                SetScore(GetActivePlayer(), CARD_SCORE_NEGATIVE_VALUE);
                SetTurn(currentPlayer, false);

                if(currentPlayer.Id >= main.players.Count)
                    SetTurn(GetPlayerById(START_PLAYER));
                else
                    SetTurn(GetPlayerById(currentPlayer.Id + 1));
            }

            InitializeGameBoard();
        }

        /// <summary>
        /// SetActiveCard is a method thats sets a card object active state.
        /// </summary>
        /// <param name="card"></param>
        /// <param name="active"></param>
        private void SetActiveCard(Card card, bool active)
        {
            card.Active = active;
            main.cards.Remove(card);
            main.cards.Add(card);
        }
    }
}
