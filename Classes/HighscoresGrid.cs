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
        //Dictionary waar eerst alle scores worden opgeslagen
        private Dictionary<string, int> ScoresDict = new Dictionary<string, int>();
        private Grid grid;

        public HighScoresGrid(Grid grid)
        {
            ///grid opbouw nogmaals herbruikt voor de Highscores
            this.grid = grid;
            //Grid wordt aangemaakt
            InitializeGrid();
            //Scores worden ingeladen in het Grid
            LoadScores();

        }

        private void InitializeGrid()
        {
            //Aanmaak meerdere regels binnen het Main grid
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            //Aanmaak kolom
            grid.ColumnDefinitions.Add(new ColumnDefinition());

        }

        private void LoadScores()
        {
            //String array die alle regels leest uit het textdocument van "scores"
            string[] lines = System.IO.File.ReadAllLines("scores.csv");
            //Aanmaak listbox waarin alle Highscores gegevens worden weergegeven
            ListBox listbox = new ListBox();
            //Gaat door de gehele lijst zoekend naar scores
            foreach (string score in lines)
            {
                //string array regel opsplitsen bij ";"
                string[] scores = score.Split(';');
                //Als speler met score bekend is in het textdocument
                if (ScoresDict.ContainsKey(scores[0]))
                {
                    //Als de nieuwe bekende score lager is
                    if (ScoresDict[scores[0]] > Convert.ToInt32(scores[1]))
                    {
                        //Niks
                    }
                    //Als niet nieuwe bekende score hoger is
                    else
                    {
                        //Dan wordt oude bekende score verwijderd uit de lijst
                        ScoresDict.Remove(scores[0]);
                        //Dan wordt nieuwe bekende score geconverteerd en in de lijst gezet
                        ScoresDict.Add(scores[0], Convert.ToInt32(scores[1]));
                    }
                }
                //Als speler niet bekend is in het textdocument
                else
                {
                    //Dan wordt nieuwe onbekende score geconverteerd en in de lijst gezet
                    ScoresDict.Add(scores[0], Convert.ToInt32(scores[1]));
                }

            }

            //lijst highscore bekijkt per regel in ScoresDict
            var highscores = from pair in ScoresDict
                                 //Kijkt naar de score hooghte en ordert hierbij "van hoog naar laag"
                             orderby pair.Value descending
                             select pair;

            //Gaat elke text regel in Highscores langs
            foreach (KeyValuePair<string, int> pair in highscores)
            {
                //Naam + : + score worden in de listbox toegevoed en weergegeven
                listbox.Items.Add(pair.Key + ": " + pair.Value);
            }
            //opbouw listbox in het grid
            Grid.SetColumn(listbox, 0);
            Grid.SetRow(listbox, 1);
            grid.Children.Add(listbox);





        }
    }
}
