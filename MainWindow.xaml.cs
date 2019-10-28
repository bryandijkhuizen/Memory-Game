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
        private const int NR_OF_COLUMNS = 4;
        private const int NR_OF_ROWS = 4;

        Grid grid;
        Grid highscores;
        bool isSet_ = false;


        public MainWindow()
        {
            InitializeComponent();
            grid = new MainGrid(GameGrid);
            highscores = new HighScores(HighscoreGrid);
            labelTimer.Visibility = Visibility.Hidden;
            labelTimer2.Visibility = Visibility.Hidden;
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

        private void SinglePlayerClick(object sender, RoutedEventArgs e)
        {
            if(isSet_ == false)
            {
                grid = new MemoryGrid(GameGrid, NR_OF_COLUMNS, NR_OF_ROWS, name.Text);
                name.Visibility = Visibility.Hidden;
                isSet_ = true;
                InitializeTimer();
            } else
            {
                isSet_ = true;
            }
            
        }

        private void MultiPlayerClick(object sender, RoutedEventArgs e)
        {
            if (isSet_ == false)
            {
                grid = new MultiPlayer(GameGrid, NR_OF_COLUMNS, NR_OF_ROWS);
                isSet_ = true;
            }
            else
            {
                isSet_ = true;
            }
        }

        private void HighScores(object sender, RoutedEventArgs e)
        {
            if (isSet_ == false)
            {
                grid = new HighScores(GameGrid);      
            }
            else
            {
                isSet_ = true;
            }
        }
    }
}
