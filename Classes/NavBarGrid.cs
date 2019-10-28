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

namespace Memory_Game
{
    public class NavBarGrid : Grid
    {
        private Grid grid;
        private Grid GameGrid;
        private Grid TimerGrid;

        TextBox enterPlayerName = new TextBox();
        private const int NR_OF_COLUMNS = 4;
        private const int NR_OF_ROWS = 4;
        bool isSet_ = false;
        TimerGrid timer = new TimerGrid();

        public NavBarGrid(Grid grid, Grid GameGrid, Grid TimerGrid)
        {
            this.grid = grid;
            this.GameGrid = GameGrid;
            this.TimerGrid = TimerGrid;

            LoadGrid();
            LoadButtons();
            LoadTextBoxes();

            
        }

        private void LoadGrid()
        {
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

        }

        private void LoadButtons()
        {
            Button single = new Button();
            single.Content = "SinglePlayer";
            single.FontSize = 42;
            single.Click += SinglePlayerClick;
            Grid.SetColumn(single, 0);
            Grid.SetRow(single, 0);
            grid.Children.Add(single);

            Button multi = new Button();
            multi.Content = "MultiPlayer";
            multi.FontSize = 42;
            Grid.SetColumn(multi, 1);
            Grid.SetRow(multi, 0);
            grid.Children.Add(multi);

            Button reset = new Button();
            reset.Content = "Reset";
            reset.FontSize = 42;
            reset.Click += Reset;
            Grid.SetColumn(reset, 2);
            Grid.SetRow(reset, 0);
            grid.Children.Add(reset);




        }

        private void LoadTextBoxes()
        {
            enterPlayerName.FontSize = 42;
            Grid.SetColumn(enterPlayerName, 0);
            Grid.SetRow(enterPlayerName, 1);
            grid.Children.Add(enterPlayerName);
        }

        private void SinglePlayerClick(object sender, System.EventArgs e)
        {
            if (isSet_ == false)
            {
                GameGrid = new SinglePlayerGrid(GameGrid, NR_OF_COLUMNS, NR_OF_ROWS, enterPlayerName.Text,TimerGrid);
                enterPlayerName.Visibility = Visibility.Hidden;
                isSet_ = true;
            }
            else
            {
                isSet_ = true;
            }
            
        }

  

        private void MultiPlayerClick(object sender, System.EventArgs e)
        {
            if (isSet_ == false)
            {
                GameGrid = new MultiPlayerGrid(GameGrid, NR_OF_COLUMNS, NR_OF_ROWS, enterPlayerName.Text);
                enterPlayerName.Visibility = Visibility.Hidden;
                isSet_ = true;
            }
            else
            {
                isSet_ = true;
            }
        }

        private void Reset(object sender, System.EventArgs e)
        {

    


        }

    }
}
