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
using TeamProjectChess.Model;
using TeamProjectChess.ViewModel;
using GalaSoft.MvvmLight;

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
            DBConnection dbc = new DBConnection();
            List<int> ListOfPlaces = dbc.GetRatingPlace().ToList();
           ObservableCollection<PlayerCard> last =  dbc.FullfillOBC();
          RatingListView.ItemsSource = last;
            
        }

        
             


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());

        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            if (RatingListView.SelectedItem != null)
            {
                int str = RatingListView.SelectedIndex+1;
                Switcher.Switch(new PlayersCard(str));
                
            }
        }
        
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        
    }
}
