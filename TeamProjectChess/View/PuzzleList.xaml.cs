using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TeamProjectChess.ViewModel;
using TeamProjectChess.Model;

namespace TeamProjectChess.View
{
    /// <summary>
    /// Логика взаимодействия для PuzzleList.xaml
    /// </summary>
    public partial class PuzzleList : UserControl, ISwitchable
    {
        public PuzzleList()
        {
            InitializeComponent();
            DBConnection dbc = new DBConnection();
            List<int> Ids = dbc.GetDebutId().ToList();
            PuzzleListBox.ItemsSource=Ids;
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PuzzleType());
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            if (PuzzleListBox.SelectedItem != null)
            {
                DBConnection dbc = new DBConnection();
                string str = dbc.DisplayCertainPuzzle(PuzzleListBox.SelectedIndex+1);
                Switcher.Switch(new Board(str));
            }
        }
    }
}
