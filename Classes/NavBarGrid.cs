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
        TextBox enterPlayerName2 = new TextBox();

        private const int NR_OF_COLUMNS = 4;
        private const int NR_OF_ROWS = 4;
        bool isSet_ = false;
        TimerGrid timer = new TimerGrid();

        Button single = new Button();
        Button multi = new Button();
        Button main = new Button();

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
            //grid.ColumnDefinitions.Add(new ColumnDefinition());

        }
        

        private void LoadButtons()
        {
            
            single.Content = "SinglePlayer";
            single.FontSize = 42;
            single.Click += SinglePlayerClick;
            Grid.SetColumn(single, 0);
            Grid.SetRow(single, 0);
            grid.Children.Add(single);
       
            multi.Content = "MultiPlayer";
            multi.FontSize = 42;
            multi.Click += MultiPlayerClick;
            Grid.SetColumn(multi, 1);
            Grid.SetRow(multi, 0);
            grid.Children.Add(multi);

            main.Content = "Main Menu";
            main.FontSize = 42;
            main.Click += MainClick;
            Grid.SetColumn(main, 2);
            Grid.SetRow(main, 0);
            //grid.Children.Add(main);


        }

        private void MainClick(object sender, RoutedEventArgs e)
        {
            GameGrid = new MainMenuGrid(GameGrid);
        }

        private void LoadTextBoxes()
        {
            enterPlayerName.FontSize = 42;
            Grid.SetColumn(enterPlayerName, 0);
            Grid.SetRow(enterPlayerName, 1);
            grid.Children.Add(enterPlayerName);

            enterPlayerName2.FontSize = 42;
            Grid.SetColumn(enterPlayerName2, 1);
            Grid.SetRow(enterPlayerName2, 1);
            grid.Children.Add(enterPlayerName2);
        }

        private void SinglePlayerClick(object sender, System.EventArgs e)
        {

            if (isSet_ == false)
            {
                GameGrid = new SinglePlayerGrid(GameGrid, NR_OF_COLUMNS, NR_OF_ROWS, enterPlayerName.Text,TimerGrid);
                enterPlayerName.Visibility = Visibility.Hidden;
                enterPlayerName2.Visibility = Visibility.Hidden;
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
                GameGrid = new MultiPlayerGrid(GameGrid, NR_OF_COLUMNS, NR_OF_ROWS, enterPlayerName.Text, enterPlayerName2.Text);
                enterPlayerName.Visibility = Visibility.Hidden;
                enterPlayerName2.Visibility = Visibility.Hidden;
                isSet_ = true;
            }
            else
            {
                isSet_ = true;
            }
        }

    }
}
