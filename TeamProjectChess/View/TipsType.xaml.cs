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
    /// Логика взаимодействия для TipsType.xaml
    /// </summary>
    public partial class TipsType : UserControl, ISwitchable
    {
        public TipsType()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());

        }

        private void DebutTips_Click(object sender, RoutedEventArgs e)
        {

            string type = "Debut";
            Switcher.Switch(new TipsList(type));
        }


        private void MiddlegameTips_Click(object sender, RoutedEventArgs e)
        {

            string type = "Middlegame";
            Switcher.Switch(new TipsList(type));
        }

        private void EndgameTips_Click(object sender, RoutedEventArgs e)
        {
            string type = "Endgame";
            Switcher.Switch(new TipsList(type));
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        
    }
}
