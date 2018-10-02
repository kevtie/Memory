using Memory.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Memory.Views
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Window
    {
        public GameView()
        {
            InitializeComponent();
            CreateGrid(10, 10);
        }

        public void CreateGrid(int columns, int rows)
        {
            for (int i = 0; i < columns; i++)
            {
                Grid.ColumnDefinitions.Add(SetColumnDetails(new ColumnDefinition()));
            }

            for (int i = 0; i < rows; i++)
            {
                Grid.RowDefinitions.Add(SetRowDetails(new RowDefinition()));
            }
        }

        public ColumnDefinition SetColumnDetails(ColumnDefinition column)
        {
            column.Width = new GridLength(30, GridUnitType.Star);

            return column;
        }

        private RowDefinition SetRowDetails(RowDefinition row)
        {
            row.Height = new GridLength(50, GridUnitType.Star);

            return row;
        }
    }
}
