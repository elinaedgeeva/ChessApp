using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProjectChess.ViewModel
{
    public class DBConnection
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=X:\TeamProjectChess\TeamProjectChess\PuzzlesDB.mdf;Integrated Security=True");
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
