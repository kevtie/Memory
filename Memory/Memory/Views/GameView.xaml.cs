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

    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Window
    {
        private static int rows = 4;
        private static int columns = 4;

        private List<Card> cards = new List<Card>();

        private int[] filledColumns = new int[columns * rows];
        private int[] filledRows = new int[columns * rows];

        public GameView()
        {
            InitializeComponent();
            CreateGrid(columns, rows);
        }

        private void SetCard(int id, string title, int column, int row, bool flipped, Brush frontBackground, Brush backBackground)
        {
            Button button = new Button();
            button.Content = title;
            button.Click += Card_Click;

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
            cards.Add(new Card(id, CheckFilled(filledColumns, GetRandomNumber(column)), CheckFilled(filledRows, GetRandomNumber(row)), title, flipped, frontBackground, backBackground));
            cards.Add(new Card(id, CheckFilled(filledColumns, GetRandomNumber(column)), CheckFilled(filledRows, GetRandomNumber(row)), title, flipped, frontBackground, backBackground));
        }

        private void AddCards(int columns, int rows)
        {
            int id = 1;

            for(int x = 0; x < columns; x++)
            {
                for(int y = 0; y < rows; y++) 
                {
                    if(id > (columns * rows / 2))
                        id = 1;

                    filledColumns[id] = x;
                    filledRows[id] = y;

                    AddCard(
                        id, 
                        x,
                        y, 
                        $"Card {id}", 
                        false, 
                        GetRandomColor(id),     
                        Brushes.Red
                    );

                    id++;
                }
            }
        }

        private int CheckFilled(int[] filled, int target)
        {
            // Make recursive
            // Set random number until !filled.Contains(target)
            if(filled.Contains(target))
                return target;

            return 0;
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
            Grid.RowDefinitions.Clear();
            Grid.ColumnDefinitions.Clear();
        }

        private void CreateGrid(int columns, int rows)
        {
            ClearGrid();
            AddCards(columns, rows);
            SetCards();

            for (int i = 0; i < columns; i++)
            {
                Grid.ColumnDefinitions.Add(SetColumnDetails(new ColumnDefinition()));
            }

            for (int i = 0; i < rows; i++)
            {
                Grid.RowDefinitions.Add(SetRowDetails(new RowDefinition()));
            }
        }

        private ColumnDefinition SetColumnDetails(ColumnDefinition column)
        {
            column.Width = new GridLength(30, GridUnitType.Star);

            return column;
        }

        private RowDefinition SetRowDetails(RowDefinition row)
        {
            row.Height = new GridLength(50, GridUnitType.Star);

            return row;
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
                    Background = new SolidColorBrush(Colors.Blue);
                    cards.Clear();
                    CreateGrid(4, 4);
                    break;
                case "6x6":
                    Background = new SolidColorBrush(Colors.Red);
                    cards.Clear();
                    CreateGrid(6, 6);
                    break;
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            cards.Clear();
            CreateGrid(rows, columns);
        }

        private void Card_Click(object sender, RoutedEventArgs e)
        {
            this.Title = (e.Source as Button).Content.ToString();
        }
    }
}
