﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TeamProjectChess.Model;
using System.Windows;
using TeamProjectChess.Model;

namespace TeamProjectChess.ViewModel
{
    public class DBConnection: ViewModelBase
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
        ObservableCollection<Tip> coll_tips;
        public ObservableCollection<Tip> GetTips(string str)
        {
            cmd.Connection = connection;
            connection.Open();
            commandStr = String.Format("select Tip from {0}", str);
            cmd.CommandText = commandStr;
            reader = cmd.ExecuteReader();
            coll_tips = new ObservableCollection<Tip>();
            while (reader.Read())
            {
                
                coll_tips.Add( new Tip(reader.GetString(reader.GetOrdinal("Tip"))));
               // yield return reader.GetInt32(reader.GetOrdinal("Id"));
            }
            reader.Close();
            connection.Close();
            return coll_tips;


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
            bool comp = false;
            //Point compMove;
            switch (moveNum)
            {
                case 1: puzzleAtribute = "MoveOne"; break;
                case 2: puzzleAtribute = "CompOne"; comp = true; break;
                case 3: puzzleAtribute = "MoveTwo"; break;
                case 4: puzzleAtribute = "CompTwo"; comp = true; break;
                case 5: puzzleAtribute = "MoveThree"; break;
                default: puzzleAtribute = ""; break;
            }
            commandStr = String.Format("select {0} from {1} where Id={2}", puzzleAtribute, puzzleTypeStr, id);
            cmd.CommandText = commandStr;
            reader = cmd.ExecuteReader();
            string AnswerString;
            if (reader.Read())
                AnswerString = reader.GetString(reader.GetOrdinal(puzzleAtribute));
            else AnswerString = "";
            bool done = false;
            reader.Close();
            #region Switch
                switch (AnswerString.ElementAt(0))
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
            #endregion
            return done;
            reader.Close();
            connection.Close();
        }
                //if (done)
                //    moveNum++
        public void MakeCompMove(int id, string puzzleTypeStr, ref int moveNum, ref Point compStart, ref Point compEnd)
        {
            string puzzleAtribute;
            bool comp = false;
            switch (moveNum)
            {
                case 1: puzzleAtribute = "MoveOne"; break;
                case 2: puzzleAtribute = "CompOne"; comp = true; break;
                case 3: puzzleAtribute = "MoveTwo"; break;
                case 4: puzzleAtribute = "CompTwo"; comp = true; break;
                case 5: puzzleAtribute = "MoveThree"; break;
                default: puzzleAtribute = ""; break;
            }
            commandStr = String.Format("select {0} from {1} where Id={2}", puzzleAtribute, puzzleTypeStr, id);
            cmd.CommandText = commandStr;
            reader = cmd.ExecuteReader();
            string compAnswer;
            if (reader.Read())
                compAnswer = reader.GetString(reader.GetOrdinal(puzzleAtribute));
            else compAnswer = "";
            string[] FromTo = compAnswer.Split(new char[] { '-' });
            for (int i = 0; i < 2; i++)
            {
                switch(FromTo[i].ElementAt(0))
                {
                    case '(':
                        {   
                            int xcooord = (int)Char.GetNumericValue(FromTo[i].ElementAt(1));
                            int ycoord = (int)Char.GetNumericValue(FromTo[i].ElementAt(3));
                            if (i == 0)
                            compStart = new Point(xcooord, ycoord);
                            else compEnd = new Point(xcooord, ycoord);
                            break;
                        }
                    default :
                        {
                            int xcooord = (int)Char.GetNumericValue(FromTo[i].ElementAt(2));
                            int ycoord = (int)Char.GetNumericValue(FromTo[i].ElementAt(4));
                            if (i == 0)
                                compStart = new Point(xcooord, ycoord);
                            else compEnd = new Point(xcooord, ycoord);
                            break;
                        }
                }
            }
            moveNum++;
            
        }
        
        
        public void ViewPuzzleList()
        {

        }
}

}
