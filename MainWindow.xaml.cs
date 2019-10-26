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
        bool isSet_ = false;


        public MainWindow()
        {
            InitializeComponent();
            grid = new MainGrid(GameGrid);
        }

        private void SinglePlayerClick(object sender, RoutedEventArgs e)
        {
            if(isSet_ == false)
            {
                grid = new MemoryGrid(GameGrid, NR_OF_COLUMNS, NR_OF_ROWS, name.Text);
                isSet_ = true;
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
            grid = new HighScores(GameGrid);
        }
    }
}
