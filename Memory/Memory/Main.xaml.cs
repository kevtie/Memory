﻿using System;
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

        private void Button_Clicknew(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Game();
        }

        private void Button_Clickload(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Load();
        }

        private void Button_Clickhelp(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Help();
        }

        private void Button_Clickscore(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Clickquit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}