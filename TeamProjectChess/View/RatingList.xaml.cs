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
    /// Логика взаимодействия для RatingList.xaml
    /// </summary>
    public partial class RatingList : UserControl, ISwitchable
    {
        public RatingList()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());

        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PlayersCard());
        }
        
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
    }
}
