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
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash : Page
    {
        private Main main = ((Main)Application.Current.MainWindow);

        public Splash()
        {
            InitializeComponent();
        }

        private void Button_Clicknew(object sender, RoutedEventArgs e)
        {
            main.Button_Clicknew(sender, e);
        }

        private void Button_Clickload(object sender, RoutedEventArgs e)
        {
            main.Button_Clickload(sender, e);
        }

        private void Button_ClickHighScore(object sender, RoutedEventArgs e)
        {
            main.Button_ClickHighScore(sender, e);
        }

        private void Button_Clickhelp(object sender, RoutedEventArgs e)
        {
            main.Button_Clickhelp(sender, e);
        }

        private void Button_Clickquit(object sender, RoutedEventArgs e)
        {
            main.Button_Clickquit(sender, e);
        }
    }
}
