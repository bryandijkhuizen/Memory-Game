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

        Label labelTimer = new Label();
        Label labelTimer2 = new Label();

        Label highscoresLabel = new Label();

        public TimerGrid(Grid grid)
        {
            this.grid = grid;
            LoadLabels();
            InitializeTimer();
        }

        private void LoadLabels()
        {
           
            labelTimer.Content = "Timer: ";
            labelTimer2.Content = increment.ToString();
            highscoresLabel.Content = "Highscores";

            labelTimer.FontSize = 42;
            labelTimer2.FontSize = 42;
            highscoresLabel.FontSize = 42;

            labelTimer.VerticalContentAlignment = VerticalAlignment.Bottom;
            labelTimer2.VerticalContentAlignment = VerticalAlignment.Bottom;
            highscoresLabel.VerticalContentAlignment = VerticalAlignment.Top; 

            grid.Children.Add(labelTimer);
            grid.Children.Add(labelTimer2);
            grid.Children.Add(highscoresLabel);
        }

        private void InitializeTimer()
        {
            labelTimer.Visibility = Visibility.Visible;
            labelTimer2.Visibility = Visibility.Visible;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timerTicker;
            timer.Start();

        }

        private int increment = 0;

        private void timerTicker(object sender, EventArgs e)
        {
            increment++;

            labelTimer2.Content = increment.ToString();
        }


    }
}
