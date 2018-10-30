using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SaveLoadTest
{
    public class Player
    {
        public int Id;
        public bool Turn;
        public int Score;
        public string Name;
  

        public Player(int id, bool turn, int score, string name)
        {      


            Id = id;
            Turn = turn;
            Score = score;
            Name = name;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

           

            TextWriter tw = new StreamWriter("Memory.sav");

            List<Player> playerlist = new List<Player>();

            playerlist.Add(new Player(34, true, 30, "Hey"));
            playerlist.Add(new Player(32, false, 4134, "Hallo"));

            //Maakt string van Player Objecten
            for (int i = 0 ; i < playerlist.Count(); i++)
            {
                string id = Convert.ToString(playerlist[i].Id);
                string turn = Convert.ToString(playerlist[i].Turn);
                string score = Convert.ToString(playerlist[i].Score);
                string name = playerlist[i].Name;

                string players = "/" +id + "/" + turn + "/" + score + "/" + name + "|";
                tw.Write(players);
            }
            tw.WriteLine();
            

            // close the stream     
            tw.Close();

            




            //Roept .sav bestand op
            
            var data = File.ReadLines("Memory.sav");

            //Selecteerd de line van het . sav bestand
            string Object1 = data.ToArray()[0];

            //alle object waarden splitsen.
            List<string> result = Object1.Split(new char[] { '|' }).ToList();

            //Pack alle waarden van PLayer Object [0].
            List<string> result2 = result[0].Split(new char[] { '/' }).ToList();

            //print de waardes van de lijst van het object [0]
            result2.ForEach(Console.WriteLine);
            
            //Maak nieuw object met de nieuwe waardes.
            new Player(Convert.ToInt32(result2[1]), Convert.ToBoolean(result2[2]), Convert.ToInt32(result2[3]), result2[4]) ;

            Console.ReadKey();
      
        }
    }
}
