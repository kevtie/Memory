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

namespace Memory.Views
{
    public class Card 
    {
        public int Id;
        public string Title;
        public int Row;
        public int Column;
        public bool Flipped;
        public Brush FrontBackground;
        public Brush BackBackground;

        public Card(int id, int column, int row, string title, bool flipped, Brush frontBackground, Brush backBackground)
        {
            Id = id;
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

    public partial class GameView : Window
    {
        private const int FIRST_GAME_GRID_ROWS = 4;
        private const int FIRST_GAME_GRID_COLUMNS = 4;

        private const int SECOND_GAME_GRID_COLUMNS = 6;
        private const int SECOND_GAME_GRID_ROWS = 6;

        private int currentGameGridRows = FIRST_GAME_GRID_ROWS;
        private int currentGameGridColumns = FIRST_GAME_GRID_COLUMNS;

        private List<Position> positions = new List<Position>();
        private List<Card> cards = new List<Card>();

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

        private void SetCard(int id, string title, int column, int row, bool flipped, Brush frontBackground, Brush backBackground)
        {
            Button button = new Button();
            button.Content = title;
            button.Click += Card_Click;

            // Set in FlipCard method
            if(!flipped)
                button.Background = frontBackground;
            else
                button.Background = backBackground;

            Grid.SetColumn(button, column);
            Grid.SetRow(button, row);
            Grid.Children.Add(button);
        }

        private void SetCards()
        {
            foreach(var card in cards)
            {
                SetCard(card.Id, card.Title, card.Column, card.Row, card.Flipped, card.FrontBackground, card.BackBackground); 
            }
        }

        private void AddCard(int id, int column, int row, string title, bool flipped, Brush frontBackground, Brush backBackground)
        {
            cards.Add(new Card(id, column, row, title, flipped, frontBackground, backBackground));
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

        private void AddCards()
        {
            int id = 1;

            foreach(var pos in positions)
            {
                if(id > GetGridSize() / 2)
                    id = 1;

                 AddCard(
                    id, 
                    pos.X,
                    pos.Y, 
                    $"Card {id} [{pos.X} - {pos.Y}", 
                    false, 
                    Brushes.White,     
                    Brushes.Red
                );

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
            cards.Clear();
            Grid.RowDefinitions.Clear();
            Grid.ColumnDefinitions.Clear();
        }

        private void InitializeGameGrid(int cols, int rows)
        {
            ClearGrid();
            SetGridSize(cols, rows);
            AddPositions(cols, rows);
            RandomizePositions();
            AddCards();
            SetCards();

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

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            InitializeGameGrid(currentGameGridColumns, currentGameGridRows);
        }

        private void Card_Click(object sender, RoutedEventArgs e)
        {
            this.Title = (e.Source as Button).Content.ToString();
        }
    }
}
