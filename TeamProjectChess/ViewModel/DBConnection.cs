using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TeamProjectChess.Model;

namespace TeamProjectChess.ViewModel
{
    public class DBConnection
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=X:\TeamProjectChess\TeamProjectChess\PuzzlesDB.mdf;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        string commandStr;
        char firstLetter;

        public IEnumerable<int> GetId(string puzzleTypeStr)
        {
            cmd.Connection = connection; 
            connection.Open();
            commandStr = String.Format("select Id from {0}", puzzleTypeStr);
            cmd.CommandText = commandStr; 
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
               yield return reader.GetInt32(reader.GetOrdinal("Id"));
            }
            reader.Close();
            connection.Close();
           
        }

         public string DisplayCertainPuzzle(int id, string puzzleTypeStr, string puzzleAtribute)
        {
            cmd.Connection=connection;
            connection.Open();
            commandStr = String.Format("select {0} from {1} where Id ={2}", puzzleAtribute,puzzleTypeStr, id);
            cmd.CommandText = commandStr;
            reader = cmd.ExecuteReader();
            if (reader.Read())
                return reader.GetString(reader.GetOrdinal(puzzleAtribute)).ToString();
            else return "";
            reader.Close();
            connection.Close();
        }

        public int FindId(string str, string puzzleTypeStr, string puzzleAtribute)
        {
            cmd.Connection = connection;
            connection.Open();
            string commandStr = String.Format("select Id from {0} where {1}=N'{2}'", puzzleTypeStr, puzzleAtribute, str);
            cmd.CommandText = commandStr;
            reader = cmd.ExecuteReader();
            if (reader.Read())
                return reader.GetInt32(reader.GetOrdinal("Id"));
            else return 1;
            reader.Close();
            connection.Close();
        }

        public bool IsMoveCorrectDebut(int id, PieceType pieceType, Point releasedSquare)
        {
            cmd.Connection = connection;
            //connection.Open();
            reader.Close();
            commandStr = String.Format("select DebutAnswer from DebutPuzzle where Id={0}", id);
            cmd.CommandText = commandStr;
            reader = cmd.ExecuteReader();
            string AnswerString;
            if (reader.Read())
                AnswerString = reader.GetString(reader.GetOrdinal("DebutAnswer"));
            else AnswerString = "";
            string [] AnswerList = AnswerString.Split(new char[] {';'});
            bool done=false;
            #region main logic
            foreach (var singleString in AnswerList)
            {
                switch (singleString.ElementAt(0))
                {
                    case 'N':
                        {

                            int xcooord = (int)Char.GetNumericValue(singleString.ElementAt(2));
                            int ycoord = (int)Char.GetNumericValue(singleString.ElementAt(4));
                            if ((new Point(xcooord, ycoord) == releasedSquare) && (pieceType == PieceType.Knight))
                            {
                                done = true;
                            }
                            break;
                        }
                    case 'B':
                        {
                            int xcooord = (int)Char.GetNumericValue(singleString.ElementAt(2));
                            int ycoord = (int)Char.GetNumericValue(singleString.ElementAt(4));
                            if ((new Point(xcooord, ycoord) == releasedSquare) && (pieceType == PieceType.Bishop))
                            {
                                done = true;
                            }
                            break;
                        }
                    case 'R':
                        {
                            int xcooord = (int)Char.GetNumericValue(singleString.ElementAt(2));
                            int ycoord = (int)Char.GetNumericValue(singleString.ElementAt(4));
                            if ((new Point(xcooord, ycoord) == releasedSquare) && (pieceType == PieceType.Rook))
                            {
                                done = true;
                            }
                            break;
                        }
                    case 'Q':
                        {
                            int xcooord = (int)Char.GetNumericValue(singleString.ElementAt(2));
                            int ycoord = (int)Char.GetNumericValue(singleString.ElementAt(4));
                            if ((new Point(xcooord, ycoord) == releasedSquare) && (pieceType == PieceType.Queen))
                            {
                                done = true;
                            }
                            break;
                        }
                    case 'K':
                        {
                            int xcooord = (int)Char.GetNumericValue(singleString.ElementAt(2));
                            int ycoord = (int)Char.GetNumericValue(singleString.ElementAt(4));
                            if ((new Point(xcooord, ycoord) == releasedSquare) && (pieceType == PieceType.King))
                            {
                                done = true;
                            }
                            break;
                        }
                    case '(':
                        {
                            int xcooord = (int)Char.GetNumericValue(singleString.ElementAt(1));
                            int ycoord = (int)Char.GetNumericValue(singleString.ElementAt(3));
                            if ((new Point(xcooord, ycoord) == releasedSquare) && (pieceType == PieceType.Pawn))
                            {
                                done = true;
                            }
                            break;
                        }
                }
                if (done) break;
            }
            #endregion
            return done;
            reader.Close();
            connection.Close();
        }

        public bool IsMoveCorrectMate(int id, PieceType pieceType, Point releasedSquare, string puzzleTypeStr, int moveNum)
        {
            cmd.Connection = connection;
            //connection.Open();
            reader.Close();
            string puzzleAtribute;
            switch(moveNum)
            {
                case 1: puzzleAtribute = "MoveOne"; break;
                case 2: puzzleAtribute = "CompOne"; break;
                case 3: puzzleAtribute = "MoveTwo"; break;
                case 4: puzzleAtribute = "CompTwo"; break;
                case 5: puzzleAtribute = "MoveThree"; break;
                default: puzzleAtribute = ""; break;
            }
            commandStr = String.Format("select {0} from {1} where Id={2}", puzzleAtribute,puzzleTypeStr, id);
            cmd.CommandText = commandStr;
            reader = cmd.ExecuteReader();
            string AnswerString;
            if (reader.Read())
                AnswerString = reader.GetString(reader.GetOrdinal("MoveOne"));
            else AnswerString = "";
            bool done = false;
            switch(AnswerString.ElementAt(0))
            {
                case 'N':
                    {
                        int xcooord = (int)Char.GetNumericValue(AnswerString.ElementAt(2));
                        int ycoord = (int)Char.GetNumericValue(AnswerString.ElementAt(4));
                        if ((new Point(xcooord, ycoord) == releasedSquare) && (pieceType == PieceType.Knight))
                        {
                            done = true;
                        }
                        break;
                    }
                case 'B':
                    {
                        int xcooord = (int)Char.GetNumericValue(AnswerString.ElementAt(2));
                        int ycoord = (int)Char.GetNumericValue(AnswerString.ElementAt(4));
                        if ((new Point(xcooord, ycoord) == releasedSquare) && (pieceType == PieceType.Bishop))
                        {
                            done = true;
                        }
                        break;
                    }
                case 'R':
                    {
                        int xcooord = (int)Char.GetNumericValue(AnswerString.ElementAt(2));
                        int ycoord = (int)Char.GetNumericValue(AnswerString.ElementAt(4));
                        if ((new Point(xcooord, ycoord) == releasedSquare) && (pieceType == PieceType.Rook))
                        {
                            done = true;
                        }
                        break;
                    }
                case 'Q':
                    {
                        int xcooord = (int)Char.GetNumericValue(AnswerString.ElementAt(2));
                        int ycoord = (int)Char.GetNumericValue(AnswerString.ElementAt(4));
                        if ((new Point(xcooord, ycoord) == releasedSquare) && (pieceType == PieceType.Queen))
                        {
                            done = true;
                        }
                        break;
                    }
                case 'K':
                    {
                        int xcooord = (int)Char.GetNumericValue(AnswerString.ElementAt(2));
                        int ycoord = (int)Char.GetNumericValue(AnswerString.ElementAt(4));
                        if ((new Point(xcooord, ycoord) == releasedSquare) && (pieceType == PieceType.King))
                        {
                            done = true;
                        }
                        break;
                    }
                case '(':
                    {
                        int xcooord = (int)Char.GetNumericValue(AnswerString.ElementAt(1));
                        int ycoord = (int)Char.GetNumericValue(AnswerString.ElementAt(3));
                        if ((new Point(xcooord, ycoord) == releasedSquare) && (pieceType == PieceType.Pawn))
                        {
                            done = true;
                        }
                        break;
                    }
                    if (done) break;
            }
            return done;
            reader.Close();
            connection.Close();
        }

        //public bool IsMoveCorrectMate(int id, PieceType pieceType, Point releasedSquare,int moveNum)
        //{
        //    cmd.Connection = connection;
        //    //connection.Open();
        //    reader.Close();
        //    commandStr = String.Format("select MoveOne from MateInOnePuzzle where Id={0}", id);
        //    cmd.CommandText = commandStr;
        //    reader = cmd.ExecuteReader();
        //}

        public void ViewPuzzleList()
        {

        }
    }
}
