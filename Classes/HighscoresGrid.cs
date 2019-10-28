﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Memory_Game
{
    public class HighScoresGrid : Grid
    {
        private Grid grid;

        public HighScoresGrid(Grid grid)
        {
            this.grid = grid;

            InitializeGrid();
            LoadScores();

        }

        private void InitializeGrid()
        {
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            grid.ColumnDefinitions.Add(new ColumnDefinition());

        }

        private void LoadScores()
        {
            string[] lines = System.IO.File.ReadAllLines("scores.txt");

                Label label = new Label();
                int index = lines.Length -1;
                label.Content = lines[index];
                Grid.SetColumn(label, 0);
                Grid.SetRow(label, 1);
                grid.Children.Add(label);



                
                    
            
        }
    }
}
