using Memory_Game.Classes;
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

namespace Memory_Game
{
    public class MultiPlayerGrid : Grid
    {
        private Grid grid;
        
        private int cols;
        private int rows;
        private int clicks = 0;
        
        private List<Image> cards = new List<Image>();
        private List<Image> finishedCards = new List<Image>();

        Player player1;
        Player player2;

        public MultiPlayerGrid(Grid grid, int cols, int rows, string playerName1, string playerName2)
        {
            this.grid = grid;
            this.cols = cols;
            this.rows = rows;
            this.player1 = new Player(0, playerName1);
            this.player2 = new Player(0, playerName2);

            InitializeGameGrid(cols, rows);
            AddImages();
            ShowScores();
        }

        private void InitializeGameGrid(int cols, int rows)
        {
            grid.RowDefinitions.Add(new RowDefinition());
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
        }

        Label score1 = new Label();
        Label score2 = new Label();

        private void UpdateScores()
        {
            score1.Content = player1.GetNaam() + ": " + (player1.GetScore().ToString());
            score2.Content = player2.GetNaam() + ": " + (player2.GetScore().ToString());
        }

        private void ShowScores()
        {
            UpdateScores();
            
            Grid.SetRow(score1, 4);
            Grid.SetRow(score2, 5);

            grid.Children.Add(score1);
            grid.Children.Add(score2);
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

        int turn = 0; // Deze zal 0 zijn als player1 speelt en 1 zijn als player2 speelt

        private void Reset()
        {
            AddImages();
            player1.ResetScores();
            player2.ResetScores();
        }

        private void cardClick(object sender, MouseButtonEventArgs e)
        {
            Image card = (Image)sender;
            ImageSource back = card.Source;
            ImageSource front = (ImageSource)card.Tag;

            clicks++;
            card.Source = front;
            cards.Add(card);
            
            //Als speler 1 aan de buurt is dan wordt er gekeken of deze punten krijgt.
            if (turn == 0)  //player 1 is dan aan de beurt
            {
                //Als er 2 kaarten zijn aangeklikt
                if (clicks == 2)
                {
                    //Dan wordt de teller van kliks weer op 1 gezet
                    clicks = 0;
                    //Zijn de kaarten gelijk aan elkaar?
                    if (cards[0].Source.ToString() == cards[1].Source.ToString())
                    {
                        //Zo ja, dan wordt dit goedgerekend.
                        //MessageBox.Show("GOED");
                        //De 2 goedgekozen kaarten blijven omgedraaid.
                        finishedCards.Add(cards[0]);
                        finishedCards.Add(cards[1]);
                        //Player 1 krijgt 1 punt voor de goedgekozen kaarten.
                        player1.AddPoint();
                        UpdateScores();
                        //Player 1 krijgt nogmaals de kans om 2 kaarten te kiezen.
                        turn = 0;
                        //Cache is leeg. Speler kan weer beginnen met 2 nieuwe kaarten te kiezen
                        cards.Clear();
                    }
                    else
                    {
                        turn++;
                        cards[1].Source = front;

                        DispatcherTimer dt = new DispatcherTimer();

                        dt.Interval = TimeSpan.FromSeconds(1);
                        dt.Start();

                        dt.Tick += (sender2, args) => {
                            dt.Stop();
                            cards[0].Source = back;
                            cards[1].Source = back;
                            cards.Clear();
                        };
                        
                    }
                }
                //Als speler 1 aan de buurt is dan wordt er gekeken of deze punten krijgt.
            }
            
            if (turn == 1)  //player 2 is dan aan de beurt
            {
                //Als er 2 kaarten zijn aangeklikt
                if (clicks == 2)
                {
                    //Dan wordt de teller van kliks weer op 1 gezet
                    clicks = 0;
                    //Zijn de kaarten gelijk aan elkaar?
                    if (cards[0].Source.ToString() == cards[1].Source.ToString())
                    {
                        //De 2 goedgekozen kaarten blijven omgedraaid.
                        finishedCards.Add(cards[0]);
                        finishedCards.Add(cards[1]);
                        //Player 2 krijgt 1 punt voor de goedgekozen kaarten.
                        player2.AddPoint();
                        UpdateScores();
                        //Player 2 krijgt nogmaals de kans om 2 kaarten te kiezen.
                        turn = 0;
                        //Cache is leeg. Speler kan weer beginnen met 2 nieuwe kaarten te kiezen
                        cards.Clear();
                    }
                    else
                    {
                        turn--;
                        cards[1].Source = front;

                        DispatcherTimer dt = new DispatcherTimer();

                        dt.Interval = TimeSpan.FromSeconds(1);
                        dt.Start();

                        dt.Tick += (sender2, args) => {
                            dt.Stop();
                            cards[0].Source = back;
                            cards[1].Source = back;
                            cards.Clear();
                        };
                    }
                }
            }
            

            if(finishedCards.Count() == 16)
            {
                int score1 = player1.GetScore();
                int score2 = player2.GetScore();

                if(score1 > score2)
                {
                    MessageBox.Show(player1.GetNaam() + " heeft gewonnen :D");
                }

                if(score2 > score1)
                {
                    MessageBox.Show(player2.GetNaam() + " heeft gewonnen :D");
                }

                if(score1 == score2)
                {
                    MessageBox.Show("Gelijkspel!");
                }

                Reset();
            }




        }





    }




}
