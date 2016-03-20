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
    /// Логика взаимодействия для PlayersCard.xaml
    /// </summary>
    public partial class PlayersCard : UserControl, ISwitchable
    {
        public PlayersCard(int str)
        {
            InitializeComponent();
            string imagename = string.Format("X:\\TeamProjectChess\\TeamProjectChess\\Resources\\{0}.png", str);
            DBConnection dbc = new DBConnection();
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri(@imagename);
            bi3.EndInit();
            image.Stretch = Stretch.Fill;
            image.Source = bi3;

            string[] name_surname= dbc.Getname(str.ToString()).Split(' ');
             int itr=name_surname.Count();
            string name=null;
           textblock1.Text = name_surname[0];
            for (int i = 1; i < itr; i++)
                name += name_surname[i]+" ";
            textblock2.Text = name;

            textblockCountry.Text = dbc.GetRatingCountry(str.ToString());
            textBlockbirth.Text= dbc.GetRatingBirthYear(str.ToString());
            textBlockclassic.Text = dbc.GetRatingClassicValueRate(str.ToString());
            textBlockblitz.Text = dbc.GetRatingBlitzValueRate(str.ToString());
            textBlockrapid.Text = dbc.GetRatingRapidValueRate(str.ToString());
            
        }
        

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new RatingList());

        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
    }
}
