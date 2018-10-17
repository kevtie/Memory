using Memory.ViewModels;
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

namespace Memory.Views
{
    public class Card 
    {
        public int Id;
        public int DuplicateId;
        public string Title;
        public int Row;
        public int Column;
        public bool Flipped;
        public string FrontBackground;
        public string BackBackground;

        // _flipped set Flipped to { get; set; }

        public Card(int id, int duplicateId, int column, int row, string title, bool flipped, string frontBackground, string backBackground)
        {
            Id = id;
            DuplicateId = duplicateId;
            Title = title;
            Row = row;
            Column = column;
            Flipped = flipped;
            FrontBackground = frontBackground;
            BackBackground = backBackground;
        }
    }

    public class Position
    {
        public int X;
        public int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Background
    {
        public int Id;
        public string Front;
        public string Back;

        public Background(int id, string back, string front)
        {
            Id = id;
            Front = front;
            Back = back;
        }
    }

    public partial class GameView : Window
    {
        private const int FIRST_GAME_GRID_ROWS = 4;
        private const int FIRST_GAME_GRID_COLUMNS = 4;

        private const int SECOND_GAME_GRID_COLUMNS = 6;
        private const int SECOND_GAME_GRID_ROWS = 6;

        private const bool CARDS_START_STATE_FLIPPED = false;

        private int currentGameGridRows = FIRST_GAME_GRID_ROWS;
        private int currentGameGridColumns = FIRST_GAME_GRID_COLUMNS;

        private List<Position> positions = new List<Position>();
        private List<Card> cards = new List<Card>();
        private List<Background> backgrounds = new List<Background>();
        private List<Card> activeCards = new List<Card>();

        public GameView()
        {
            InitializeComponent();
            InitializeGameGrid(FIRST_GAME_GRID_ROWS, FIRST_GAME_GRID_COLUMNS);
        }

        private int GetGridSize()
        {
            return currentGameGridColumns * currentGameGridRows;
        }

        private void SetGridSize(int cols, int rows)
        {
            currentGameGridRows = rows;
            currentGameGridColumns = cols;
        }

        private void RandomizePositions()
        {
            Random rng = new Random();
            positions = positions.OrderBy(x => rng.Next()).ToList();
        }

        private void SetCard(int id, int duplicateId, string title, int column, int row, bool flipped, string frontBackground, string backBackground)
        {

            Image image = new Image();

            image.Source = SetCardState(flipped, frontBackground, backBackground);

            TextBlock tb = new TextBlock();
            var bold = new Bold(new Run(title));
            tb.Inlines.Add(bold);

            StackPanel stackPnl = new StackPanel();
            stackPnl.Orientation = Orientation.Horizontal;
            stackPnl.Margin = new Thickness(10);
            stackPnl.Name = "c" + id.ToString();
            stackPnl.Children.Add(tb);
            stackPnl.Children.Add(image);

            Button button = new Button();
            button.Content = stackPnl;
            button.Click += Card_Click;

            Grid.SetColumn(button, column);
            Grid.SetRow(button, row);
            Grid.Children.Add(button);
        }

        private ImageSource SetCardState(bool flipped, string frontBackground, string backBackground)
        {
            if(flipped)
                return new BitmapImage(new Uri(frontBackground, UriKind.Absolute));

            return new BitmapImage(new Uri(backBackground, UriKind.Absolute));
        }

        private void SetCards()
        {
            foreach(var card in cards)
            {
                SetCard(card.Id, card.DuplicateId, card.Title, card.Column, card.Row, card.Flipped, card.FrontBackground, card.BackBackground); 
            }
        }

        private void AddCard(int id, int duplicateId, int column, int row, string title, bool flipped, string frontBackground, string backBackground)
        {
            cards.Add(new Card(id, duplicateId, column, row, title, flipped, frontBackground, backBackground));
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
                backgrounds.Add(new Background(i, $"C:/Users/boele/source/repos/Memory2/Memory/Memory/Pictures/card_back.jpg", $"C:/Users/boele/source/repos/Memory2/Memory/Memory/Pictures/{i}.jpg"));
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
            Grid.RowDefinitions.Clear();
            Grid.ColumnDefinitions.Clear();
        }

        private void CompareCards()
        {
            if(activeCards.Count >= 2)
            {
                List<int> cardIds = new List<int>();

                foreach(var card in activeCards)
                {
                    cardIds.Add(card.Id);
                }
            }
            else 
            {
                // Get first activeCard card for selection return
                // Cannot compare less than 1 card or more than 2 cards
                // Show what the player selected as one card
            }

            // If give point to player and extra turn
            // Else give next player turn without points
        }

        private bool FlipCard(Card card)
        {
            if(!card.Flipped) 
            {
                SetActiveCard(card);
                return true;
            }

            return false;
        }

        private void InitializeGameGrid(int cols, int rows)
        {
            ClearGrid();
            SetGridSize(cols, rows);
            AddPositions(cols, rows);
            AddBackgrounds();
            RandomizePositions();
            AddCards();
            SetCards();
            CreateGrid(cols, rows);
        }

        private void CreateGrid(int cols, int rows)
        {
             for (int i = 0; i < cols; i++)
             {
                Grid.ColumnDefinitions.Add(new ColumnDefinition());
             }

            for (int i = 0; i < rows; i++)
            {
                Grid.RowDefinitions.Add(new RowDefinition());
            }
        }

        private void GridOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            HandleGridOptions();
        }

        private void HandleGridOptions()
        {
            switch (GridOptions.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "4x4":
                    InitializeGameGrid(FIRST_GAME_GRID_COLUMNS, FIRST_GAME_GRID_ROWS);
                    break;
                case "6x6":
                    InitializeGameGrid(SECOND_GAME_GRID_COLUMNS, SECOND_GAME_GRID_ROWS);
                    break;
                default:
                    InitializeGameGrid(FIRST_GAME_GRID_COLUMNS, FIRST_GAME_GRID_ROWS);
                    break;
            }
        }

        private Card GetCardById(int id)
        {
            return cards.Where(c => c.Id == id).ToList().First();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            InitializeGameGrid(currentGameGridColumns, currentGameGridRows);
        }

        private void Card_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel stackPanel = (StackPanel)button.Content;

            int id = Convert.ToInt32(stackPanel.Name.Remove(0, 1));

            Card card = GetCardById(id);

            this.Title = card.Title.ToString();

            card.Flipped = FlipCard(card);

            SetActiveCard(card);

            SetCards();
        }

        private void SetActiveCard(Card card)
        {
            Card card2 = card;
            cards.Remove(card);
            cards.Add(card);
        }
    }
}
