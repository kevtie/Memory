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

namespace HighScoreWpf
{
    public partial class MainWindow : Window
    {

        public static List<User> MyList = new List<User>();
        public static List<int> Top10 = new List<int>();
        public static List<string> LijstTop = new List<string>();
        int counter = 0;

        public MainWindow()
        {
            InitializeComponent();
            Top10.Add(0);
            lvDataBinding.ItemsSource = MyList; 
        }

       



        private void Button_Click(object sender, RoutedEventArgs e) {
            MyList.Add(new User() { Name = MyTextBox.Text, Score = Convert.ToInt32(MyTextBox2.Text) });
            MyTextBox.Clear();
            MyTextBox2.Clear();

                string Hallo = MyList[counter].ToString();
                string[] result = Hallo.Split('|');
                counter++;
                int getal = Convert.ToInt32(string.Join("", result[1].ToCharArray().Where(Char.IsDigit)));
                

                for (int j = 0; j < Top10.Count; j++)
                {

                    if (getal > Top10[j])
                    {
                        Top10.Insert(j, getal);
                        LijstTop.Insert(j, Hallo);
                        break;
                    }

                }
            
            

            var Top = LijstTop.Take(10);

            lvDataBinding.ItemsSource = Top;
            lvDataBinding.Items.Refresh();
        }

    }

    public class User
    {
        public string Name { get; set; }

        public int Score { get; set;}

        public override string ToString()
        {
            return "Naam: " + this.Name + "  | Score: " + this.Score;
        }
    }
}
   
