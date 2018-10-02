using Memory.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
