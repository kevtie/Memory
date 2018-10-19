using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memory
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window  
    {
        public Main()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }
        private void MenuItem_Clicksave(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItem_Clickload(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItem_Clickhelp(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Help();
        }

        private void MenuItem_Clickquit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       

      


    }
}
