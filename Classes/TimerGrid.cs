﻿using Memory_Game.Classes;
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
        DispatcherTimer timer = new DispatcherTimer();
        Label labelTimer = new Label();
        Label labelTimer2 = new Label();

        public TimerGrid()
        {
            
        }

        

        public void init(Grid grid)
        {
            this.grid = grid;
            labelTimer.Visibility = Visibility.Visible;
            labelTimer2.Visibility = Visibility.Visible;

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timerTicker;
            timer.Start();
            LoadLabels();
        }



        public void LoadLabels()
        {
           
            labelTimer.Content = "Timer: ";
            labelTimer2.Content = increment.ToString();


            labelTimer.FontSize = 42;
            labelTimer2.FontSize = 42;


            labelTimer.VerticalContentAlignment = VerticalAlignment.Bottom;
            labelTimer2.VerticalContentAlignment = VerticalAlignment.Bottom;

            labelTimer2.Margin = new Thickness(50);

            grid.Children.Add(labelTimer);
            grid.Children.Add(labelTimer2);

        }

        


        public void StopTimer()
        {
            timer.Stop();
        }

        public int getTimer()
        {
            return increment;
        }

        public void start()
        {
            timer.Start();
        }

        public void ResetTimer()
        {
            increment = 120;
        }

        private int increment = 120;

        public void timerTicker(object sender, EventArgs e)
        {
            increment--;

            labelTimer2.Content = increment.ToString();
        }


    }
}
