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

        public Card(int column, int row, string title)
        {
            Title = title;
            Row = row;
            Column = column;
        }
    }

    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Window
    {
        private int rows = 4;
        private int columns = 4;

        private List<Card> cards;

        public GameView()
        {
            InitializeComponent();
            CreateGrid(columns, rows);
            SetCards(columns, rows);
            AddCards();
        }

        private void AddCard(string title, int column, int row)
        {
            Button button = new Button();
            button.Content = title;
            Grid.SetColumn(button, column);
            Grid.SetRow(button, row);
            Grid.Children.Add(button);
        }

        private void AddCards()
        {
            foreach(var card in cards)
            {
                AddCard(card.Title, card.Column, card.Row);
            }
        }

        private void SetCards(int columns, int rows)
        {
            cards = new List<Card>();

            for(int i = 0; i < (columns * rows / 2); i++)
            {
                cards.Add(new Card(i, i, "Card" + i));
                cards.Add(new Card(i, i, "Card" + i));
            }
        }

        private void ClearGrid()
        {
            Grid.RowDefinitions.Clear();
            Grid.ColumnDefinitions.Clear();
        }

        private void CreateGrid(int columns, int rows)
        {
            ClearGrid();

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
                    CreateGrid(4, 4);
                    break;
                case "6x6":
                    Background = new SolidColorBrush(Colors.Red);
                    CreateGrid(6, 6);
                    break;
            }
        }
    }
}
