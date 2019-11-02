using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Documents;
using System.Windows.Controls;
using CsvHelper;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Memory_Game.Classes
{
    public class ListConverter
    {
        private List<Image> l;
        private List<string> imageSources = new List<string>();
        public ListConverter()
        {
            
        }

        public void Export(List<Image> l)
        {
            this.l = l;
            //hier wordt game.sav aangemaakt, waar de game later in wordt opgeslagen.
            System.IO.File.WriteAllBytes("game.sav", new byte[0]);
            using (System.IO.StreamWriter file =
                                new System.IO.StreamWriter("game.sav", true))
            {
                //Voor elke plek in de lijst van l wordt de source van de afbeelding opgeschreven in de .sav file. 
                for (int i = 0; i < l.Count(); i++)
                {
                    if((l[i] != null))
                    {
                        file.WriteLine(l[i].Source.ToString().Substring(23));
                    }
                    
                }
                
            }
        }

        public List<string> Import()
        {

            string[] lines = System.IO.File.ReadAllLines("game.sav");

            foreach(string line in lines)
            {
                imageSources.Add(line);
                
            }

            return imageSources;

            
        }

       
    }
}
