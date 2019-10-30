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
        List<Image> l;

        public ListConverter()
        {
            
        }

        public void Convert(List<Image> l)
        {
            this.l = l;
            using (StreamWriter sw = File.CreateText("game.sav"))
            {
                for (int i = 0; i < l.Count(); i++)
                {
                    sw.WriteLine(l[i].Source.ToString()); 
                }
            }
        }
            
        public List<Image> Import()
        {
            string path = "list.csv";
            List<Image> ImageList = new List<Image>();

            string[] lines = System.IO.File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] columns = line.Split(',');
                foreach (string column in columns)
                {

                    Image i = new Image();
                    i.Source = new ImageSourceConverter().ConvertFromString(column) as ImageSource;
                    ImageList.Add(i);
                }
            }

            return ImageList;

        }
    }
}
