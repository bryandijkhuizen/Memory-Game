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

namespace Memory_Game.Classes
{
    public class SinglePlayerGrid : Grid
    {
        private Grid grid;
        private int cols;
        private int rows;
        private int clicks = 0;
        private List<Image> cards = new List<Image>();
        private List<Image> finishedCards = new List<Image>();
        private string playerName1;
        public DateTime InitDate { get; set; }

        public SinglePlayerGrid(Grid grid, int cols, int rows, string playerName1)
        {
            this.grid = grid;
            this.cols = cols;
            this.rows = rows;
            this.playerName1 = playerName1;
            InitializeGameGrid(cols, rows);
            AddImages();

        }


        private void InitializeGameGrid(int cols, int rows)
        {
            for (int i = 0; i < rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < cols; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            DateTime now = DateTime.Now;

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\Users\Public\TestFolder\scores.txt", true))
            {
                file.WriteLine("Game Initialized by: " + playerName1 + " on " + now);
            }


        }

        private void AddImages()
        {
            List<ImageSource> images = GetImagesList();
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < cols; column++)
                {
                    Image backgroundImage = new Image();
                    backgroundImage.Source = new BitmapImage(new Uri("images/mystery_image.jpg", UriKind.Relative));
                    backgroundImage.Tag = images.First();
                    images.RemoveAt(0);
                    backgroundImage.MouseDown += new MouseButtonEventHandler(cardClick);
                    Grid.SetColumn(backgroundImage, column);
                    Grid.SetRow(backgroundImage, row);
                    grid.Children.Add(backgroundImage);
                }
            }
        }

        private List<ImageSource> GetImagesList()
        {
            List<ImageSource> images = new List<ImageSource>();

            for (int i = 0; i < 16; i++)
            {
                int imageNumber = i % 8 + 1;
                ImageSource source = new BitmapImage(new Uri("Images/" + imageNumber + ".jpg", UriKind.Relative));
                images.Add(source);
            }

            Random random = new Random();
            for (int i = 0; i < (rows * cols); i++)
            {
                int r = random.Next(0, (rows * cols));
                ImageSource card = images[r];
                images[r] = images[i];
                images[i] = card;
            }

            return images;
        }

        private void cardClick(object sender, MouseButtonEventArgs e)
        {
            Image card = (Image)sender;
            ImageSource back = card.Source;
            ImageSource front = (ImageSource)card.Tag;

            clicks++;
            card.Source = front;
            cards.Add(card);

            if (clicks == 2)
            {
                clicks = 0;

                if (cards[0].Source.ToString() == cards[1].Source.ToString())
                {
                    MessageBox.Show("GOED");
                    finishedCards.Add(cards[0]);
                    finishedCards.Add(cards[1]);
                    if (finishedCards.Count() == 16)
                    {
                        using (System.IO.StreamWriter file =
                            new System.IO.StreamWriter(@"C:\Users\Public\TestFolder\scores.txt", true))
                        {
                            file.WriteLine("Game Finished by: " + playerName1);
                        }

                        if (MessageBox.Show("Play another game?", "Confirm") == MessageBoxResult.Yes)
                        {
                            for (int i = 0; i < finishedCards.Count(); i++)
                            {
                                finishedCards[i].Source = back;
                            }
                        }
                        else
                        {
                            System.Environment.Exit(0);
                        }



                    }
                    cards.Clear();
                }
                else
                {
                    cards[0].Source = back;
                    cards[1].Source = back;

                    MessageBox.Show("FOUT");
                    cards.Clear();
                }
            }


        }

    }
}
