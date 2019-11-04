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
using System.Windows.Threading;

namespace Memory_Game.Classes
{
    public class SinglePlayerGrid : Grid
    {
        private Grid grid;
        private Grid TimerGrid;

        private Dictionary<string, int> scores = new Dictionary<string, int>();


        private int cols;
        private int rows;
        private int clicks = 0;

        private List<Image> cards = new List<Image>();
        private List<Image> finishedCards = new List<Image>();

        private string playerName1;
        public DateTime InitDate { get; set; }

        TimerGrid timer = new TimerGrid();

        public SinglePlayerGrid(Grid grid, int cols, int rows, string playerName1, Grid TimerGrid)
        {
            this.grid = grid;
            this.cols = cols;
            this.rows = rows;
            this.TimerGrid = TimerGrid;
            this.playerName1 = playerName1;

            InitializeGameGrid(cols, rows);
            AddImages();
            AddButtons();

        }

        private void AddButtons()
        {
            Button button = new Button();
            Button Loadbutton = new Button();
            Button resetButton = new Button();

            button.Content = "Save";
            button.Click += SaveToCSV;

            resetButton.Content = "Reset";
            resetButton.Click += Reset;


            Loadbutton.Content = "LoadGame";
            Loadbutton.Click += LoadFromCSV;


            button.Height = 40;
            button.Width = 200;

            Loadbutton.Height = 40;
            Loadbutton.Width = 200;

            resetButton.Height = 40;
            resetButton.Width = 200;

            Grid.SetRow(button, 4);
            Grid.SetRow(Loadbutton, 5);
            Grid.SetRow(resetButton, 6);

            grid.Children.Add(resetButton);
            grid.Children.Add(button);
            grid.Children.Add(Loadbutton);
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            AddImages();
            timer.ResetTimer();

        }

        public void SaveToCSV(object sender, System.EventArgs e)
        {
            ListConverter lc = new ListConverter();
            lc.Export(finishedCards);
        }

        public void LoadFromCSV(object sender, System.EventArgs e)
        {
            ListConverter lc = new ListConverter();

            finishedCards.Clear();

            foreach(string line in lc.Import())
            {
                Image image = new Image();
                image.Tag = new BitmapImage(new Uri(line, UriKind.Relative));
                finishedCards.Add(image);       
            }

            List<Image> griditems = this.grid.Children.OfType<Image>().ToList();

            foreach(Image fitem in finishedCards)
            {
                Console.WriteLine("finished: " + fitem.Tag.ToString());

                foreach (Image item in griditems)
                {
                    if(fitem.Tag.ToString() == item.Tag.ToString())
                    {
                        item.Source = new BitmapImage(new Uri(item.Tag.ToString(), UriKind.Relative));
                    }
                }

            }

            
           
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

            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            timer.init(TimerGrid);



        }

        

        private void AddImages()
        {
            List < ImageSource > images  = GetImagesList();
            
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < cols; column++)
                {
                    Image backgroundImage = new Image();
                    backgroundImage.Source = new BitmapImage(new Uri("images/mystery_image.jpg", UriKind.Relative));
                    backgroundImage.Tag = images.First();
                    images.RemoveAt(0);
                    backgroundImage.Margin = new Thickness(5);
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
                if(cards.Count() <= 1)
                {

          
                } else
                {
                    if (cards[0].Source.ToString() == cards[1].Source.ToString())
                    {

                        if (isSame(cards[0], cards[1]))
                        {
                            clicks = 0;
                            MessageBox.Show("You cannot click the same card");

                            cards[0].Source = new BitmapImage(new Uri("images/mystery_image.jpg", UriKind.Relative));
                            cards[1].Source = new BitmapImage(new Uri("images/mystery_image.jpg", UriKind.Relative));

                        }
                        else
                        {
                            finishedCards.Add(cards[0]);
                            finishedCards.Add(cards[1]);
                        }


                        if (finishedCards.Count() == 16)
                        {
                            {
                                timer.StopTimer();
                                int score = (timer.getTimer());

                                scores.Add(playerName1, score);
                                String csv = String.Join(
                                Environment.NewLine,
                                scores.Select(d => $"{d.Key};{d.Value};"));

                                using (System.IO.StreamWriter file =
                                    new System.IO.StreamWriter("scores.csv", true))
                                {
                                    file.WriteLine(csv);
                                }
                                Reset();

                            }
                            for (int i = 0; i < finishedCards.Count(); i++)
                            {
                                finishedCards[i].Source = back;
                                
                            }

                        }
                        cards.Clear();
                    }
                    else
                    {
                        cards[1].Source = front;

                        DispatcherTimer dt = new DispatcherTimer();

                        dt.Interval = TimeSpan.FromSeconds(0.5);
                        dt.Start();

                        dt.Tick += (sender2, args) => {
                            dt.Stop();
                            cards[0].Source = back;
                            cards[1].Source = back;
                            Console.WriteLine(back);
                            cards.Clear();
                        };
                    }
                }
                clicks = 0;
               
            }

        }

        private Boolean isSame(Image card1, Image card2)
        {
            bool isTrue_;
            if (Grid.GetRow(card1) == Grid.GetRow(card2) && Grid.GetColumn(card1) == Grid.GetColumn(card2))
            {
                isTrue_ = true;
            }
            else
            {
                isTrue_ = false;
            }

            return isTrue_;
        }

        private void Reset()
        {
            AddImages();
            timer.ResetTimer();
            timer.start();
        }
    }
}
