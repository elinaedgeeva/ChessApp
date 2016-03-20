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
    /// Логика взаимодействия для TipsList.xaml
    /// </summary>
    public partial class TipsList : UserControl, ISwitchable
    {
        public TipsList(string str)
        {
            string smth= str;
            InitializeComponent();
            DBConnection dbc = new DBConnection();
            ObservableCollection<Tip> coll= dbc.GetTips(str);
            TipsListView.ItemsSource = coll;


        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new TipsType());

        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
    }
}
