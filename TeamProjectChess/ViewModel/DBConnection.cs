using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProjectChess.Model;

namespace TeamProjectChess.ViewModel
{
    public class DBConnection: ViewModelBase
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Олег\Source\Repos\ChessApp\TeamProjectChess\PuzzlesDB.mdf;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        public IEnumerable<int> GetDebutId()
        {
            cmd.Connection = connection; 
            connection.Open();
            cmd.CommandText = "select Id from DebutPuzzle"; 
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
               yield return reader.GetInt32(reader.GetOrdinal("Id"));
            }
            reader.Close();
            connection.Close();
           
        }

        public IEnumerable<int> GetRatingId()
        {
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = "select Id from PlayerCard";
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return reader.GetInt32(reader.GetOrdinal("Id"));
            }
            reader.Close();
            connection.Close();

        }
        
        ObservableCollection<PlayerCard> obc;
        public ObservableCollection<PlayerCard> FullfillOBC()
        {
           List<string> name= GetRatingName().ToList();
            List<int> rank = GetRatingPlace().ToList();
            obc = new ObservableCollection<PlayerCard>();
            for (int i = 0; i < 50; i++)
            {
               
                string ImageName =string.Format( "C:\\Users\\Олег\\Source\\Repos\\ChessApp\\TeamProjectChess\\Resources\\{0}.png", rank[i]);
                obc.Add(new PlayerCard(rank[i], name[i], ImageName));
            }
            return obc;
        }
        public IEnumerable<int> GetRatingPlace()
        {
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = "select Rank from PlayerCard";
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                
                yield return reader.GetInt32(reader.GetOrdinal("Rank"));
            }
            reader.Close();
            connection.Close();
        }
       
        public IEnumerable<string> GetRatingName()
        {
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = "select Name from PlayerCard";
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return reader.GetString(reader.GetOrdinal("Name"));
            }
            reader.Close();
            connection.Close();
        }

        public string Getname(string str)
        {
            string name;
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText =String.Format( "select Name from PlayerCard where Rank={0}", str);
            reader = cmd.ExecuteReader();
            reader.Read();
            name = reader.GetString(reader.GetOrdinal("Name"));
            reader.Close();
            connection.Close();
            return name;
        }

        public string GetRatingCountry(string str)
        {
            cmd.Connection = connection;
            connection.Open();
            string country = null;
            cmd.CommandText = string.Format("select Country from PlayerCard where Rank={0}", str);
            reader = cmd.ExecuteReader();
            reader.Read();
            country =reader.GetString(reader.GetOrdinal("Country"));
            reader.Close();
            connection.Close();
            return country;
        }

        public string GetRatingBirthYear(string str)
        {
            string by;
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText =String.Format("select BirthYear from PlayerCard where Rank={0}", str);
            reader = cmd.ExecuteReader();
            reader.Read();
            by= reader.GetInt32(reader.GetOrdinal("BirthYear")).ToString();
            reader.Close();
            connection.Close();
            return by;
        }

        public string GetRatingClassicValueRate(string str)
        {
            string value = null;
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = String.Format("select ClassicRateValue from PlayerCard Where Rank={0}", str);
            reader = cmd.ExecuteReader();
            reader.Read();
            value=reader.GetInt32(reader.GetOrdinal("ClassicRateValue")).ToString();
            reader.Close();
            connection.Close();
            return value;
        }
        public string GetRatingBlitzValueRate(string str)
        {
            string value = null;
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = String.Format("select BlitzRateValue from PlayerCard Where Rank={0}", str);
            reader = cmd.ExecuteReader();
            reader.Read();
            value = reader.GetInt32(reader.GetOrdinal("BlitzRateValue")).ToString();
            reader.Close();
            connection.Close();
            return value;
        }
        public string GetRatingRapidValueRate( string str)
        {
            string value = null;
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = String.Format("select RapidRateValue from PlayerCard Where Rank={0}", str);
            reader = cmd.ExecuteReader();
            reader.Read();
            value = reader.GetInt32(reader.GetOrdinal("RapidRateValue")).ToString();
            reader.Close();
            connection.Close();
            return value;
        }

        public string DisplayCertainPuzzle(int id)
        {
            cmd.Connection=connection;
            connection.Open();
            string commandStr = String.Format("select DebutStartPosition from DebutPuzzle where Id ={0}", id);
            cmd.CommandText = commandStr;
            reader = cmd.ExecuteReader();
            if (reader.Read())
                return reader.GetString(reader.GetOrdinal("DebutStartPosition")).ToString();
            else return "";
            reader.Close();
            connection.Close();
        }

        

        public void ViewPuzzleList()
        {

        }
    }
}
