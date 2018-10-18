using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WpfDropdownMenu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Test 1");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.Title = "HIIIIIGGGHGHHHHH NOOOOOOOOOOON";
            Process.Start("chrome.exe", "https://www.youtube.com/watch?v=hqCbx5LQIlw");

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();

        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }


    }
}
