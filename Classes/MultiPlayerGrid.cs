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

        //Aanroep methode voor de aanmaak van het gamegrid in de multiplayer omgeving
        public MultiPlayerGrid(Grid grid, int cols, int rows, string playerName1, string playerName2)
        {
            //De verschillende conpomentent van het speel veld
            this.grid = grid;
            this.cols = cols;
            this.rows = rows;
            //Aanmaak van beide spelers. Met de benodigdheden voor beide spelers
            this.player1 = new Player(0, playerName1);
            this.player2 = new Player(0, playerName2);

            //maakt het speel veld aan.
            InitializeGameGrid(cols, rows);
            //Voegt de "achterkant van de speel kaarten"
            AddImages();
            //Laat het "scorenbord zien onder het spe"
            ShowScores();
        }

        //Aanmaak van het gamegrid voor de multiplayer omgeving.
        private void InitializeGameGrid(int cols, int rows)
        {
            //aanmaak van de regels. Hoeveel wordt aangegeven in NavBarGrid.cs
            grid.RowDefinitions.Add(new RowDefinition());
            for (int i = 0; i < rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            //aanmaak van de kolommen. Hoeveel wordt aangegeven in NavBarGrid.cs
            for (int i = 0; i < cols; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            //aanmaak van de regel waar Player1(naam en score) komt te staan.
            grid.RowDefinitions.Add(new RowDefinition());
            //aanmaak van de regel waar Player2(naam en score) komt te staan.
            grid.RowDefinitions.Add(new RowDefinition());
        }
        //Aanmaak label voor speler1 
        Label score1 = new Label();
        //Aanmaak label voor speler2
        Label score2 = new Label();

        //Methode voor het updaten van de score labels
        private void UpdateScores()
        {
            //tekst die wordt geupdate voor speler 1
            score1.Content = player1.GetNaam() + ": " + (player1.GetScore().ToString());
            //tekst die wordt geupdate voor speler 2
            score2.Content = player2.GetNaam() + ": " + (player2.GetScore().ToString());
        }

        private void ShowScores()
        {
            //Scores van spelers worden hier werkelijk op het beeld geupdate
            UpdateScores();

            //De score voor regel van speler 1
            Grid.SetRow(score1, 4);
            //De score voor regel van speler 2
            Grid.SetRow(score2, 5);

            //Scores moeten in het spel grid dat weer in het maingrid zit(kind van de maingrid)
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

        //Reset methode van scores voor speler 1 en speler 2
        private void Reset()
        {
            //kaarten worden opnieuw neergezet
            AddImages();
            //scores van beide speler worden weer op 0 gezet
            player1.ResetScores();
            player2.ResetScores();
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

        //Klik methode voor het draaien van de kaarten + punten geven bij correcte combinaties
        private void cardClick(object sender, MouseButtonEventArgs e)
        {
            //
            Image card = (Image)sender;
            //Achterkant foto kaarten
            ImageSource back = card.Source;
            //Voorkant foto kaarten
            ImageSource front = (ImageSource)card.Tag;

            //Na elke gekozen kaart 1 optellen bij de hoeveelheid aangeklikte
            clicks++;
            //Aangeklikt plaatje "draait" van achterkant foto naar voorkant foto
            card.Source = front;
            //Deze wordt toegevoegd in de tijdelijke lijst "cards" die gekozen is/zijn
            cards.Add(card);

            //Als de turn teller op 0 staat is speler 1 aan de beurt
            if (turn == 0)
            {
                score1.Background = Brushes.MistyRose;
                //Als er 2 kaarten zijn aangeklikt
                if (clicks == 2)
                {
                    //Dan wordt de teller van kliks weer op 0 gezet
                    clicks = 0;
                    //Als de kaarten foto's gelijk aan elkaar?
                    if (cards[0].Source.ToString() == cards[1].Source.ToString())
                    {
                        //Als de locatie van de kaarten gelijk aan elkaar zijn?
                        if (isSame(cards[0], cards[1]))
                        {
                            //Dan gaat de klik teller terug naar 0
                            clicks = 0;
                            //Dan krijgt speler een pop-up bericht dat dit geen mogelijke speel optie is
                            MessageBox.Show("You cannot click the same card");

                            //Dan wordt er na sluiting van het pop-up bericht kaart 1 teruggedraaid naar de achterkant foto
                            cards[0].Source = new BitmapImage(new Uri("images/mystery_image.jpg", UriKind.Relative));
                            //Dan wordt er na sluiting van het pop-up bericht kaart 2 teruggedraaid naar de achterkant foto
                            cards[1].Source = new BitmapImage(new Uri("images/mystery_image.jpg", UriKind.Relative));

                        }
                        //Als de foto's gelijk zijn, maar niet op dezelfde locatie
                        else
                        {
                            //Dan worden de foto's toegevoegd aan de lijst finishCards
                            finishedCards.Add(cards[0]);
                            finishedCards.Add(cards[1]);
                            //Dan krijgt player1 1 punt voor de goed gekozen kaarten.
                            player1.AddPoint();
                            //Dan wordt het score label vernieuwd
                            UpdateScores();
                            //dan krijgt player1 nogmaals de kans om 2 kaarten te kiezen.
                            turn = 0;
                            //Speler 1 blijft roze en speler 2 blijft wit
                            score1.Background = Brushes.MistyRose;
                            score2.Background = Brushes.White;
                        }
                        //Lijst "cards" wordt geleegd
                        cards.Clear();
                    }
                    //Als de kaarten niet gelijk zijn aan elkaar
                    else
                    {
                        //Dan wordt de "voorkan" van gekozen kaart 2 weergegeven
                        cards[1].Source = front;
                        //Dan gaat de beurt naar Speler 2
                        turn++;
                        //Dan start een nieuwe timer voor de weergave van beide gekozen kaarten
                        DispatcherTimer dt = new DispatcherTimer();
                        //Beide kaarten worden voor 1 seconden weergegeven
                        dt.Interval = TimeSpan.FromSeconds(1);
                        //Timer start
                        dt.Start();
                        //Timer vervolg acties
                        dt.Tick += (sender2, args) => {
                            //Timer wordt na 1 seconden stopgezet
                            dt.Stop();
                            //gekozen kaart 1 wordt omgedraaid naar de "achterkant"
                            cards[0].Source = back;
                            //gekozen kaart 2 wordt omgedraaid naar de "achterkant"
                            cards[1].Source = back;
                            //Dan wordt de lijst "cards" geleegd
                            cards.Clear();
                            //De beurt van speler 1 wordt wit en speler 2 wordt paars.
                            score2.Background = Brushes.Lavender;
                            score1.Background = Brushes.White;

                        };
                    }
                }
            }
            //Als de turn teller op 1 staat is speler 2 aan de beurt
            if (turn == 1)
            {
                score2.Background = Brushes.Lavender;
                //Als er 2 kaarten zijn aangeklikt
                if (clicks == 2)
                {
                    score2.Background = Brushes.Lavender;
                    score1.Background = Brushes.White;
                    //Dan wordt de teller van kliks weer op 1 gezet
                    clicks = 0;
                    //Zijn de kaarten gelijk aan elkaar?
                    if (cards[0].Source.ToString() == cards[1].Source.ToString())
                    {
                        if (isSame(cards[0], cards[1]))
                        {
                            score2.Background = Brushes.Lavender;
                            score1.Background = Brushes.White;
                            //Dan gaat de klik teller terug naar 0
                            clicks = 0;
                            //Dan krijgt speler een pop-up bericht dat dit geen mogelijke speel optie is
                            MessageBox.Show("You cannot click the same card");

                            //Dan wordt er na sluiting van het pop-up bericht kaart 1 teruggedraaid naar de achterkant foto
                            cards[0].Source = new BitmapImage(new Uri("images/mystery_image.jpg", UriKind.Relative));
                            //Dan wordt er na sluiting van het pop-up bericht kaart 2 teruggedraaid naar de achterkant foto
                            cards[1].Source = new BitmapImage(new Uri("images/mystery_image.jpg", UriKind.Relative));
                        }
                        //Als de foto's gelijk zijn, maar niet op dezelfde locatie
                        else
                        {
                            //Dan worden de foto's toegevoegd aan de lijst finishCards
                            finishedCards.Add(cards[0]);
                            finishedCards.Add(cards[1]);
                            //Dan krijgt player1 1 punt voor de goed gekozen kaarten.
                            player2.AddPoint();
                            //Dan wordt het score label vernieuwd
                            UpdateScores();
                            //dan krijgt player1 nogmaals de kans om 2 kaarten te kiezen.
                            turn = 1;
                            //Speler 1 blijft wit en speler 2 blijft paars
                            score2.Background = Brushes.Lavender;
                            score1.Background = Brushes.White;
                        }
                        //Dan wordt de lijst "cards" geleegd
                        cards.Clear();
                    }
                    else
                    {
                        //speler 2 wordt wit en speler 1 wordt roze. 
                        score2.Background = Brushes.White;
                        score1.Background = Brushes.MistyRose;
                        //Dan wordt de "voorkan" van gekozen kaart 2 weergegeven
                        cards[1].Source = front;
                        //Dan gaat de beurt naar Speler 1
                        turn--;
                        //Dan start een nieuwe timer voor de weergave van beide gekozen kaarten
                        DispatcherTimer dt = new DispatcherTimer();
                        //Beide kaarten worden voor 1 seconden weergegeven
                        dt.Interval = TimeSpan.FromSeconds(1);
                        //Timer start
                        dt.Start();
                        //Timer vervolg acties
                        dt.Tick += (sender2, args) => {
                            //Timer wordt na 1 seconden stopgezet
                            dt.Stop();
                            //gekozen kaart 1 wordt omgedraaid naar de "achterkant"
                            cards[0].Source = back;
                            //gekozen kaart 2 wordt omgedraaid naar de "achterkant"
                            cards[1].Source = back;
                            //Dan wordt de lijst "cards" geleegd
                            cards.Clear();
                        };
                    }
                }
            }

            //Als de lijst "finishCards gelijk staat aan de hoeveelheid kaarten op het speel veld"
            if (finishedCards.Count() == 16)
            {
                //Dan wordt de score van speler 1 gelezen
                int score1 = player1.GetScore();
                //Dan wordt de score van speler 2 gelezen
                int score2 = player2.GetScore();
                //Als score speler 1 hoger is als die van speler 2
                if (score1 > score2)
                {
                    //Dan komt de pop-up voor speler 1
                    MessageBox.Show(player1.GetNaam() + " heeft gewonnen :D");
                }
                //Als score speler 2 hoger is als die van speler 1
                if (score2 > score1)
                {
                    //Dan komt de pop-up voor speler 2
                    MessageBox.Show(player2.GetNaam() + " heeft gewonnen :D");
                }
                //Als scores van speler 1 en speler 2 gelijk zijn
                if (score1 == score2)
                {
                    //Dan komt de pop-up
                    MessageBox.Show("Gelijkspel!");
                }
                //Dan wordt de game instantie opnieuw gestart
                Reset();
            }




        }





    }




}
