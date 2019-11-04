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

namespace Memory_Game.Classes
{
    public class TimerGrid : Grid
    {
        private Grid grid;

        //Hier worden drie labels aangemaakt. Eentje voor de de content van Timer: en de tweedwe voor de aflopende timer zelf. De derde voor de higscorelabel
        Label labelTimer = new Label();
        Label labelTimer2 = new Label();

        Label highscoresLabel = new Label();

        public TimerGrid()
        {

        }

        public void init(Grid grid)
        {
            //hier wordt de timers in de grid geplaatst en visible gemaakt. 
            this.grid = grid;
            labelTimer.Visibility = Visibility.Visible;
            labelTimer2.Visibility = Visibility.Visible;

            //Elke seconde wordt dit stukje code opnieuw uitgevoerd. 
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timerTicker;
            timer.Start();
            LoadLabels();
        }



        public void LoadLabels()
        {

            //Hier worden de labels van hierboven gevuld. 
            labelTimer.Content = "Timer: ";
            labelTimer2.Content = increment.ToString();

            //Dit is de opmaak van de labels. 
            labelTimer.FontSize = 42;
            labelTimer2.FontSize = 42;

            //Hier wordt bepaald waar de labels in de grid komen te staan. 
            labelTimer.VerticalContentAlignment = VerticalAlignment.Bottom;
            labelTimer2.VerticalContentAlignment = VerticalAlignment.Bottom;

            labelTimer2.Margin = new Thickness(50);

            //Hier wordt de timer toegevoegd aan de grid. 
            grid.Children.Add(labelTimer);
            grid.Children.Add(labelTimer2);
        }

        DispatcherTimer timer = new DispatcherTimer();

        //Hiermee kan de timer in singleplayer worden aangeroepen zodat hij gestopt wordt.
        public void StopTimer()
        {
            timer.Stop();
        }

        //Hiermee wordt de tijd van de timer opgehaald zodat de timer aftelt in de singleplayer.
        public int getTimer()
        {
            return increment;
        }

        //Hier begint de timer met aftellen vanaf 120 seconden.
        private int increment = 120;

        //Hier wordt er elke seconde naar deze methode verwezen waardoor er een seconde van de timer af gaat. 
        public void timerTicker(object sender, EventArgs e)
        {
            increment--;

            labelTimer2.Content = increment.ToString();
        }

        public void ResetTimer()
        {
            increment = 120;
        }

        public void start()
        {
            timer.Start();
        }


    }
}
