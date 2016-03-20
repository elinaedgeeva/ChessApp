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

namespace TeamProjectChess.View
{
    /// <summary>
    /// Логика взаимодействия для BoardMateInTwo.xaml
    /// </summary>
    public partial class BoardMateInTwo : UserControl
    {
        private ObservableCollection<ChessPiece> PiecesMate2 = new ObservableCollection<ChessPiece>();
        int k;
        bool taken = true;
        Point selectedSquare;
        Point releasedSquare;
        PieceType _pieceType;
        DBConnection dbc = new DBConnection();
        int CurrentId;
        int whichMove = 1;
        Point compStart;
        Point compEnd;
        
        public BoardMateInTwo(string str)
        {
            InitializeComponent();
            Parser pr = new Parser();
            PiecesMate2 = pr.DisplayStartPos(str);
            ChessBoardMateIn2.ItemsSource = PiecesMate2;
            CurrentId = dbc.FindId(str, "MateInTwoPuzzle", "MateInTwoStartPosition");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PuzzleListMate("MateInTwoPuzzle"));
        }

        private void NextPuzzle_Click(object sender, RoutedEventArgs e)
        {
            DBConnection dbc = new DBConnection();
            string str = dbc.DisplayCertainPuzzle(CurrentId + 1, "MateInTwoPuzzle", "MateInTwoStartPosition");
            Switcher.Switch(new BoardMateInTwo(str));
        }

        //private void ChessBoardMate_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    int xCoord = (int)Mouse.GetPosition(ChessBoardMate).X;
        //    int yCoord = (int)Mouse.GetPosition(ChessBoardMate).Y;
        //    selectedSquare = new Point(xCoord, yCoord);
        //    for (int i = 0; i < 32; i++)
        //        if (PiecesMate2[i].Pos == selectedSquare)
        //        {
        //            k = i;
        //            _pieceType = PiecesMate2[i].Type;
        //            break;
        //        }
        //}

        //private void ChessBoardMate_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    int xCoordDest = (int)Mouse.GetPosition(ChessBoardMateIn2).X;
        //    int yCoordDest = (int)Mouse.GetPosition(ChessBoardMateIn2).Y;
        //    releasedSquare = new Point(xCoordDest, yCoordDest);
        //    if (whichMove < 4)
        //        if (dbc.IsMoveCorrectMate(CurrentId, PiecesMate2[k].Type, releasedSquare, "MateInTwoPuzzle", whichMove, ref compStart, ref compEnd))
        //        {
        //            for (int i = 0; i < PiecesMate2.Count; i++)
        //                if (PiecesMate2[i].Pos == compStart)
        //                    PiecesMate2[i].Pos = compEnd;
        //            whichMove++;
        //        }

        //}

        private void ChessBoardMateIn2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int xCoord = (int)Mouse.GetPosition(ChessBoardMateIn2).X;
            int yCoord = (int)Mouse.GetPosition(ChessBoardMateIn2).Y;
            selectedSquare = new Point(xCoord, yCoord);
            for (int i = 0; i < PiecesMate2.Count(); i++)
                if (PiecesMate2[i].Pos == selectedSquare)
                {
                    k = i;
                    _pieceType = PiecesMate2[i].Type;
                    break;
                }
        }

        private void ChessBoardMateIn2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int xCoordDest = (int)Mouse.GetPosition(ChessBoardMateIn2).X;
            int yCoordDest = (int)Mouse.GetPosition(ChessBoardMateIn2).Y;
            releasedSquare = new Point(xCoordDest, yCoordDest);
            if (whichMove < 4)
                if (dbc.IsMoveCorrectMate(CurrentId, PiecesMate2[k].Type, releasedSquare, "MateInTwoPuzzle", whichMove))
                {
                    if (PiecesMate2[k].IsMovePossible(selectedSquare, releasedSquare, PiecesMate2[k].Type, PiecesMate2[k].Player, ref PiecesMate2, k, ref taken))
                    {
                        if (taken == false)
                            PiecesMate2[k - 1].Pos = releasedSquare;
                        else PiecesMate2[k].Pos = releasedSquare;
                        taken = true;
                        whichMove++;
                        if (whichMove < 4)
                        {
                            dbc.MakeCompMove(CurrentId, "MateInTwoPuzzle", ref whichMove, ref compStart, ref compEnd);
                            //if (PiecesMate2[k].IsMovePossible)
                            for (int i = 0; i < PiecesMate2.Count; i++)
                                if (PiecesMate2[i].Pos == compStart)
                                {
                                    for (int j = 0; j < PiecesMate2.Count; j++)
                                        if (PiecesMate2[j].Pos == compEnd)
                                        {
                                            if (j < i)
                                                taken = false;
                                            PiecesMate2.Remove(PiecesMate2[j]);
                                            break;
                                        }
                                    if (taken == false)
                                        PiecesMate2[i - 1].Pos = compEnd;
                                    else PiecesMate2[i].Pos = compEnd;
                                    taken = true;
                                    //PiecesMate2[i].Pos = compEnd;
                                    break;
                                }
                        }
                        else
                        {
                            Info.AppendText("Верно! Задача решена! Перейдите к следующей.");
                        }
                    }
                }
                else Info.AppendText("Неверный ход! Попробуйте еще! ");
        }
    }
}
