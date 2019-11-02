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

        Label score1 = new Label();
        Label score2 = new Label();

        int turn = 0; // Deze zal 0 zijn als player1 speelt en 1 zijn als player2 speelt

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
            score1.Background = Brushes.HotPink;
            clicks++;
            card.Source = front;
            cards.Add(card);
            score1.Background = Brushes.White;
            //Als speler 1 aan de buurt is dan wordt er gekeken of deze punten krijgt.
            if (turn == 0)  //player 1 is dan aan de beurt
            {
                score2.Background = Brushes.White;
                score1.Background = Brushes.MistyRose;

                //Als er 2 kaarten zijn aangeklikt
                if (clicks == 2)
                {
                    if(cards.Count() <= 1) { 
                    //Dan wordt de teller van kliks weer op 1 gezet
                    clicks = 0;
                        //Zijn de kaarten gelijk aan elkaar?
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
                                //Zo ja, dan wordt dit goedgerekend.
                                //De 2 goedgekozen kaarten blijven omgedraaid.
                                finishedCards.Add(cards[0]);
                                finishedCards.Add(cards[1]);
                                //Player 1 krijgt 1 punt voor de goedgekozen kaarten.
                                player1.AddPoint();
                                UpdateScores();
                                //Player 1 krijgt nogmaals de kans om 2 kaarten te kiezen.
                                turn = 0;
                                //Cache is leeg. Speler kan weer beginnen met 2 nieuwe kaarten te kiezen
                                score1.Background = Brushes.MistyRose;
                                score2.Background = Brushes.White;
                            }

                            cards.Clear();
                        }
                        else
                        {
                            turn++;
                            cards[1].Source = front;

                            DispatcherTimer dt = new DispatcherTimer();

                            dt.Interval = TimeSpan.FromSeconds(1);
                            dt.Start();
                            dt.Tick += (sender2, args) =>
                            {
                                dt.Stop();
                                cards[0].Source = back;
                                cards[1].Source = back;

                                cards.Clear();

                                score1.Background = Brushes.White;
                                score2.Background = Brushes.Lavender;
                            };
                        }

                    }
                }
                //Als speler 1 aan de buurt is dan wordt er gekeken of deze punten krijgt.
            }

            if (turn == 1)  //player 2 is dan aan de beurt
            {
                score2.Background = Brushes.Lavender;
                score1.Background = Brushes.White;
                //Als er 2 kaarten zijn aangeklikt

                if (clicks == 2)
                {
                    if (cards.Count() <= 1) { 

                        //Dan wordt de teller van kliks weer op 1 gezet
                        clicks = 0;
                    //Zijn de kaarten gelijk aan elkaar?
                    score1.Background = Brushes.White;
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
                                //De 2 goedgekozen kaarten blijven omgedraaid.
                                finishedCards.Add(cards[0]);
                                finishedCards.Add(cards[1]);
                                //Player 2 krijgt 1 punt voor de goedgekozen kaarten.
                                player2.AddPoint();
                                UpdateScores();
                                //Player 2 krijgt nogmaals de kans om 2 kaarten te kiezen.
                                turn = 1;
                                //Cache is leeg. Speler kan weer beginnen met 2 nieuwe kaarten te kiezen
                                score2.Background = Brushes.Lavender;
                                score1.Background = Brushes.White;
                            }

                            cards.Clear();
                        }
                        else
                        {
                            turn--;
                            cards[1].Source = front;
                            DispatcherTimer dt = new DispatcherTimer();

                            dt.Interval = TimeSpan.FromSeconds(1);
                            dt.Start();

                            dt.Tick += (sender2, args) =>
                            {
                                dt.Stop();
                                cards[0].Source = back;
                                cards[1].Source = back;

                                score2.Background = Brushes.White;
                                score1.Background = Brushes.MistyRose;

                                cards.Clear();
                            };
                        }
                    }
                }
            }


            if (finishedCards.Count() == 16)
            {
                int score1 = player1.GetScore();
                int score2 = player2.GetScore();

                if (score1 > score2)
                {
                    MessageBox.Show(player1.GetNaam() + " heeft gewonnen :D");
                }

                if (score2 > score1)
                {
                    MessageBox.Show(player2.GetNaam() + " heeft gewonnen :D");
                }

                if (score1 == score2)
                {
                    MessageBox.Show("Gelijkspel!");
                }

                Reset();
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
            player1.ResetScores();
            player2.ResetScores();
            UpdateScores();
            finishedCards.Clear();
        }

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






    }




}
