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
using TeamProjectChess.ViewModel;

namespace TeamProjectChess.View
{
    /// <summary>
    /// Логика взаимодействия для PuzzleListMate.xaml
    /// </summary>
    public partial class PuzzleListMate : UserControl, ISwitchable
    {
        string matetype;
        string atributeStr;

        public PuzzleListMate(string mateType)
        {
            InitializeComponent();
            matetype = mateType;
            DBConnection dbc = new DBConnection();
            //mateType = String.Format(mateType + "Puzzle");
            List<int> Ids = dbc.GetId(mateType).ToList();
            PuzzleListBox.ItemsSource = Ids;
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MateInN());
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            if (PuzzleListBox.SelectedItem != null)
            {
                DBConnection dbc = new DBConnection();
                int index = matetype.IndexOf('P');
                string atributeStr = matetype.Remove(index);
                atributeStr = String.Format(atributeStr + "StartPosition");
                string str = dbc.DisplayCertainPuzzle((int)PuzzleListBox.SelectedItem, matetype, atributeStr);
                switch (matetype)
                {
                    case "MateInOnePuzzle": Switcher.Switch(new BoardMate(str)); break;
                    case "MateInTwoPuzzle": Switcher.Switch(new BoardMateInTwo(str)); break;
                    case "MateInThreePuzzle": Switcher.Switch(new BoardMateInThree(str)); break;
                }
                //Switcher.Switch(new BoardMate(str));
            }
        }
    }
}
