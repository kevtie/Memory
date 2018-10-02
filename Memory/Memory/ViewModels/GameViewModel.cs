using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Memory.ViewModels
{
    public class GameViewModel
    {
        public int columns = 4;
        public int rows = 4;

        public List<Object> Columns {
            get
            {
                var list = new List<Object>();
                for (int i = 0; i < columns; i++)
                {
                    list.Add(SetColumnDetails(new ColumnDefinition()));
                }
                return list;
            }
        }

        public List<Object> Rows
        {
            get
            {
                var list = new List<Object>();
                for (int i = 0; i < rows; i++)
                {
                    list.Add(SetRowDetails(new RowDefinition()));
                }
                return list;
            }
        }

        public GameViewModel()
        {

        }

        private object SetColumnDetails(ColumnDefinition column)
        {
            column.Width = new GridLength('*', GridUnitType.Star);

            return column;
        }

        private object SetRowDetails(RowDefinition row)
        {
            row.Height = new GridLength('*', GridUnitType.Star);

            return row;
        }
    }
}
