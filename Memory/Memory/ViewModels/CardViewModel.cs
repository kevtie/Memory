using System;
using System.ComponentModel;

namespace Memory.ViewModels
{
    public class CardViewModel
    {
        public CardViewModel()
        {
            CreateGrid();
        }

        public void CreateGrid()
        {
            CardGrid.RowDefinitions.Add(new RowDefinition());
        }
    }
}
