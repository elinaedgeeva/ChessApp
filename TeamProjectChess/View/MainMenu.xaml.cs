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
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl, ISwitchable
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void Puzzles_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PuzzleType());

        }

        private void Rating_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new RatingList());
        }

        private void Tips_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new TipsType());
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
    }
}
