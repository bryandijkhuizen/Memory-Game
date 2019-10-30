using Memory_Game.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Memory_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Grid grid, highscores, navbar;

        public MainWindow()
        {
            InitializeComponent();

            grid = new MainMenuGrid(GameGrid);
            highscores = new HighScoresGrid(HighscoreGrid);
            navbar = new NavBarGrid(NavBarGrid, GameGrid, TimerGrid);


        }

        

    }
}
