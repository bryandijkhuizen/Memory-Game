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
        private List<Image> returnList = new List<Image>();

        public ListConverter()
        {
            
        }

        public void Export(List<Image> l)
        {
            this.l = l;
            System.IO.File.WriteAllBytes("game.sav", new byte[0]);
            using (System.IO.StreamWriter file =
                                new System.IO.StreamWriter("game.sav", true))
            {
                
                for (int i = 0; i < l.Count(); i++)
                {
                    if((l[i].Source.ToString() != null))
                    {
                        file.WriteLine(l[i].Source.ToString().Substring(21));
                    }
                    
                }
                
            }
        }

        public List<Image> Import()
        {

            string[] lines = System.IO.File.ReadAllLines("game.sav");

            foreach(string line in lines)
            {
                Image card = new Image();
                card.Source = new BitmapImage(new Uri(line, UriKind.Relative));
                returnList.Add(card);
            }

            return returnList;
        }

       
    }
}
