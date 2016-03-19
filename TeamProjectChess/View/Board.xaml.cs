using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace TeamProjectChess.View
{
    /// <summary>
    /// Логика взаимодействия для Board.xaml
    /// </summary>
    public partial class Board : UserControl
    {
        private ObservableCollection<ChessPiece> Pieces = new ObservableCollection<ChessPiece>();
        int k;
        bool taken=true;
        Point selectedSquare;
        Point releasedSquare;
        PieceType _pieceType;
        DBConnection dbc = new DBConnection();
        int CurrentId;
        int count=0;
        //string testStringGame = "6nr/rb3ppp/p4b2/2B5/1p2kP2/3Q3P/PPB1N1P1/1R2K2R";

        public Board(string str)
        {
            InitializeComponent();
            #region Initialize Start Position
            //this.Pieces = new ObservableCollection<ChessPiece>() {
            //    new ChessPiece{Pos=new Point(0, 6), Type=PieceType.Pawn, Player=Player.White},
            //    new ChessPiece{Pos=new Point(1, 6), Type=PieceType.Pawn, Player=Player.White},
            //    new ChessPiece{Pos=new Point(2, 6), Type=PieceType.Pawn, Player=Player.White},
            //    new ChessPiece{Pos=new Point(3, 6), Type=PieceType.Pawn, Player=Player.White},
            //    new ChessPiece{Pos=new Point(4, 6), Type=PieceType.Pawn, Player=Player.White},
            //    new ChessPiece{Pos=new Point(5, 6), Type=PieceType.Pawn, Player=Player.White},
            //    new ChessPiece{Pos=new Point(6, 6), Type=PieceType.Pawn, Player=Player.White},
            //    new ChessPiece{Pos=new Point(7, 6), Type=PieceType.Pawn, Player=Player.White},
            //    new ChessPiece{Pos=new Point(0, 7), Type=PieceType.Rook, Player=Player.White},
            //    new ChessPiece{Pos=new Point(1, 7), Type=PieceType.Knight, Player=Player.White},
            //    new ChessPiece{Pos=new Point(2, 7), Type=PieceType.Bishop, Player=Player.White},
            //    new ChessPiece{Pos=new Point(3, 7), Type=PieceType.Queen, Player=Player.White},
            //    new ChessPiece{Pos=new Point(4, 7), Type=PieceType.King, Player=Player.White},
            //    new ChessPiece{Pos=new Point(5, 7), Type=PieceType.Bishop, Player=Player.White},
            //    new ChessPiece{Pos=new Point(6, 7), Type=PieceType.Knight, Player=Player.White},
            //    new ChessPiece{Pos=new Point(7, 7), Type=PieceType.Rook, Player=Player.White},
            //    new ChessPiece{Pos=new Point(0, 1), Type=PieceType.Pawn, Player=Player.Black},
            //    new ChessPiece{Pos=new Point(1, 1), Type=PieceType.Pawn, Player=Player.Black},
            //    new ChessPiece{Pos=new Point(2, 1), Type=PieceType.Pawn, Player=Player.Black},
            //    new ChessPiece{Pos=new Point(3, 1), Type=PieceType.Pawn, Player=Player.Black},
            //    new ChessPiece{Pos=new Point(4, 1), Type=PieceType.Pawn, Player=Player.Black},
            //    new ChessPiece{Pos=new Point(5, 1), Type=PieceType.Pawn, Player=Player.Black},
            //    new ChessPiece{Pos=new Point(6, 1), Type=PieceType.Pawn, Player=Player.Black},
            //    new ChessPiece{Pos=new Point(7, 1), Type=PieceType.Pawn, Player=Player.Black},
            //    new ChessPiece{Pos=new Point(0, 0), Type=PieceType.Rook, Player=Player.Black},
            //    new ChessPiece{Pos=new Point(1, 0), Type=PieceType.Knight, Player=Player.Black},
            //    new ChessPiece{Pos=new Point(2, 0), Type=PieceType.Bishop, Player=Player.Black},
            //    new ChessPiece{Pos=new Point(3, 0), Type=PieceType.Queen, Player=Player.Black},
            //    new ChessPiece{Pos=new Point(4, 0), Type=PieceType.King, Player=Player.Black},
            //    new ChessPiece{Pos=new Point(5, 0), Type=PieceType.Bishop, Player=Player.Black},
            //    new ChessPiece{Pos=new Point(6, 0), Type=PieceType.Knight, Player=Player.Black},
            //    new ChessPiece{Pos=new Point(7, 0), Type=PieceType.Rook, Player=Player.Black}
            //};
            #endregion
            Parser pr = new Parser();
            Pieces = pr.DisplayStartPos(str);
            ChessBoard.ItemsSource = Pieces;
            CurrentId = dbc.FindId(str,"DebutPuzzle","DebutStartPosition");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
             Switcher.Switch(new PuzzleList());
        }

        private void NextPuzzle_Click(object sender, RoutedEventArgs e)
        {
            DBConnection dbc = new DBConnection();
            string str = dbc.DisplayCertainPuzzle(CurrentId+1,"DebutPuzzle","DebutStartPosition");
            Switcher.Switch(new Board(str));
        }

        private void ChessBoard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int xCoord = (int)Mouse.GetPosition(ChessBoard).X;
            int yCoord = (int)Mouse.GetPosition(ChessBoard).Y;
            selectedSquare = new Point(xCoord, yCoord);
            for (int i = 0; i < 32; i++)
                if (Pieces[i].Pos == selectedSquare)
                {
                    k = i;
                    _pieceType = Pieces[i].Type;
                    break;
                }
        }

        private void ChessBoard_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int xCoordDest = (int)Mouse.GetPosition(ChessBoard).X;
            int yCoordDest = (int)Mouse.GetPosition(ChessBoard).Y;
            releasedSquare = new Point(xCoordDest, yCoordDest);
            if (Pieces[k].IsMovePossible(selectedSquare, releasedSquare, Pieces[k].Type, Pieces[k].Player, ref Pieces, k, ref taken))
            {
                if (taken == false)
                    Pieces[k - 1].Pos = releasedSquare;
                else Pieces[k].Pos = releasedSquare;
                taken = true;

                if (taken == false)
                    Pieces[k - 1].Pos = selectedSquare;
                else Pieces[k].Pos = selectedSquare;

                if (count != 4)
                {
                    if (dbc.IsMoveCorrectDebut(CurrentId, Pieces[k].Type, releasedSquare))
                    {
                        
                        Info.AppendText("Верно! ");
                        if (count == 3)
                        {
                            Info.AppendText("\nЗадача решена! Перейдите к следующей.");
                        }
                        count++;
                    }
                    else Info.AppendText("Неверный ход! Попробуйте еще! ");
                }
            }
        }
    }
}
