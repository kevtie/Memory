using Memory.ViewModels;
using System;
using System.Windows;

namespace Memory.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class CardView : Window
    {
        public CardView()
        {
            InitializeComponent();
            DataContext = new CardViewModel();
        }
    }
}
