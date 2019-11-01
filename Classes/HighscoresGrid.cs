using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Memory_Game
{
    public class HighScoresGrid : Grid
    {
        private Dictionary<string, int> ScoresDict = new Dictionary<string, int>();
        private Grid grid;

        public HighScoresGrid(Grid grid)
        {
            this.grid = grid;

            InitializeGrid();
            LoadScores();

        }

        private void InitializeGrid()
        {
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            grid.ColumnDefinitions.Add(new ColumnDefinition());

        }

        private void LoadScores()
        {
            string[] lines = System.IO.File.ReadAllLines("scores.csv");

            ListBox listbox = new ListBox();
            
                
            foreach(string score in lines)
            {
                string[] scores = score.Split(';');

                if (ScoresDict.ContainsKey(scores[0]))
                {
                    if (ScoresDict[scores[0]] > Convert.ToInt32(scores[1]))
                    {

                    } else
                    {
                        ScoresDict.Remove(scores[0]);
                        ScoresDict.Add(scores[0], Convert.ToInt32(scores[1]));
                    }
                } else
                {
                    ScoresDict.Add(scores[0], Convert.ToInt32(scores[1]));
                }

            }

           var highscores = from pair in ScoresDict
                            orderby pair.Value descending
                            select pair;



            foreach (KeyValuePair<string, int> pair in highscores)
            {
                listbox.Items.Add(pair.Key + ": " + pair.Value);

            }

            Grid.SetColumn(listbox, 0);
            Grid.SetRow(listbox, 1);
            grid.Children.Add(listbox);





        }
    }
}
