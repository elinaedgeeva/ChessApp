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

namespace TeamProjectChess.View
{
    /// <summary>
    /// Логика взаимодействия для MateInN.xaml
    /// </summary>
    public partial class MateInN : UserControl
    {
        public MateInN()
        {
            InitializeComponent();

        }

        private void MateInOne_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PuzzleListMate("MateInOnePuzzle"));
        }

        private void MateInTwo_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PuzzleListMate("MateInTwoPuzzle"));
        }

        private void MateInThree_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PuzzleListMate("MateInThreePuzzle"));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PuzzleType());
        }
    }
}
