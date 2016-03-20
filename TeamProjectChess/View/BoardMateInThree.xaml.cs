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
    /// Логика взаимодействия для BoardMateInThree.xaml
    /// </summary>
    public partial class BoardMateInThree : UserControl
    {
        private ObservableCollection<ChessPiece> PiecesMate3 = new ObservableCollection<ChessPiece>();
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

        public BoardMateInThree(string str)
        {
            InitializeComponent();
            Parser pr = new Parser();
            PiecesMate3 = pr.DisplayStartPos(str);
            ChessBoardMateIn3.ItemsSource = PiecesMate3;
            CurrentId = dbc.FindId(str, "MateInThreePuzzle", "MateInThreeStartPosition");
        }

        private void ChessBoardMateIn3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int xCoord = (int)Mouse.GetPosition(ChessBoardMateIn3).X;
            int yCoord = (int)Mouse.GetPosition(ChessBoardMateIn3).Y;
            selectedSquare = new Point(xCoord, yCoord);
            for (int i = 0; i < PiecesMate3.Count(); i++)
                if (PiecesMate3[i].Pos == selectedSquare)
                {
                    k = i;
                    _pieceType = PiecesMate3[i].Type;
                    break;
                }
        }

        private void ChessBoardMateIn3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int xCoordDest = (int)Mouse.GetPosition(ChessBoardMateIn3).X;
            int yCoordDest = (int)Mouse.GetPosition(ChessBoardMateIn3).Y;
            releasedSquare = new Point(xCoordDest, yCoordDest);
            if (whichMove < 6)
                if (dbc.IsMoveCorrectMate(CurrentId, PiecesMate3[k].Type, releasedSquare, "MateInThreePuzzle", whichMove))
                {
                    if (PiecesMate3[k].IsMovePossible(selectedSquare, releasedSquare, PiecesMate3[k].Type, PiecesMate3[k].Player, ref PiecesMate3, k, ref taken))
                    {
                        if (taken == false)
                            PiecesMate3[k - 1].Pos = releasedSquare;
                        else PiecesMate3[k].Pos = releasedSquare;
                        taken = true;
                        whichMove++;
                        if (whichMove < 6)
                        {
                            dbc.MakeCompMove(CurrentId, "MateInThreePuzzle", ref whichMove, ref compStart, ref compEnd);
                            //if (PiecesMate2[k].IsMovePossible)
                            for (int i = 0; i < PiecesMate3.Count; i++)
                                if (PiecesMate3[i].Pos == compStart)
                                {
                                    for (int j = 0; j < PiecesMate3.Count; j++)
                                        if (PiecesMate3[j].Pos == compEnd)
                                        {
                                            if (j < i)
                                                taken = false;
                                            PiecesMate3.Remove(PiecesMate3[j]);
                                            break;
                                        }
                                    if (taken == false)
                                        PiecesMate3[i - 1].Pos = compEnd;
                                    else PiecesMate3[i].Pos = compEnd;
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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PuzzleListMate("MateInThreePuzzle"));
        }

        private void NextPuzzle_Click(object sender, RoutedEventArgs e)
        {
            DBConnection dbc = new DBConnection();
            string str = dbc.DisplayCertainPuzzle(CurrentId + 1, "MateInThreePuzzle", "MateInThreeStartPosition");
            Switcher.Switch(new BoardMateInThree(str));
        }
    }
}
