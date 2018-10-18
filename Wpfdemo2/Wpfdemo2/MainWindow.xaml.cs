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

namespace Wpfdemo2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Score> myScore { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            myScore = new List<Score>();

            Score score1 = new Score();
            score1.Name = "jan";
            score1.score = 9001;

            Score score2 = new Score();
            score2.Name = "Karel";
            score2.score = 8999;

            Score score3 = new Score();
            score3.Name = "piet";
            score3.score = 8998;


            myScore.Add(score1);
            myScore.Add(score2);
            myScore.Add(score3);

            DataContext = this;
        }
    }
}
