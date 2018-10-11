﻿using Memory.ViewModels;
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
        public string FrontBackground;
        public Brush BackBackground;

        public Card(int id, int column, int row, string title, bool flipped, string frontBackground, Brush backBackground)
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

    public class Background
    {
        public int Id;
        public SolidColorBrush Color;
        public string Picture;

        public Background(int id, SolidColorBrush color, string picture)
        {
            Id = id;
            Color = color;
            Picture = picture;
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
        private List<Background> backgrounds = new List<Background>();

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

        private void SetCard(int id, string title, int column, int row, bool flipped, string frontBackground, Brush backBackground)
        {
            Image img = new Image();
            img.Source = new BitmapImage(new Uri("C:/Users/boele/source/repos/Memory2/Memory/Memory/Pictures/catharina/00fool.jpg", UriKind.Absolute));

            TextBlock tb = new TextBlock();
            var bold = new Bold(new Run(title));
            tb.Inlines.Add(bold);

            StackPanel stackPnl = new StackPanel();
            stackPnl.Orientation = Orientation.Horizontal;
            stackPnl.Margin = new Thickness(10);
            stackPnl.Children.Add(tb);
            stackPnl.Children.Add(img);

            Button button = new Button();
            button.Content = stackPnl;
            button.Click += Card_Click;

            // Set in FlipCard method
            //if(!flipped)
               // button.Background = frontBackground;
            //else
               // button.Background = backBackground;

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

        private void AddCard(int id, int column, int row, string title, bool flipped, string frontBackground, Brush backBackground)
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

        private void AddBackgrounds()
        {
            for(int i = 0; i < GetGridSize() / 2; i++)
            {
                backgrounds.Add(new Background(i, GetRandomColor(i), "Pictures/catharina/00fool.jpg"));
            }
        }

        private void AddCards()
        {
            int id = 1;
            //Brush cbg = new SolidColorBrush(Colors.Red);
            string frontBg = "";

            foreach(var pos in positions)
            {
                if(id > GetGridSize() / 2)
                    id = 1;

                foreach(var bg in backgrounds)
                {
                    if(id == bg.Id)
                        frontBg = bg.Picture;
                }

                 AddCard(
                    id, 
                    pos.X,
                    pos.Y, 
                    $"Card {id} [{pos.X} - {pos.Y}] ({positions.Count})", 
                    false, 
                    frontBg,     
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
            positions.Clear();
            cards.Clear();
            Grid.RowDefinitions.Clear();
            Grid.ColumnDefinitions.Clear();
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
            Button button = (Button)sender;
        }
    }
}
