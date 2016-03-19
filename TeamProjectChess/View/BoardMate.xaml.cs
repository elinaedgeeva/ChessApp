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
    /// Логика взаимодействия для BoardMate.xaml
    /// </summary>
    public partial class BoardMate : UserControl
    {
        private ObservableCollection<ChessPiece> PiecesMate = new ObservableCollection<ChessPiece>();
        int k;
        bool taken = true;
        Point selectedSquare;
        Point releasedSquare;
        PieceType _pieceType;
        DBConnection dbc = new DBConnection();
        int CurrentId;
        int count = 0;
        
        public BoardMate(string str)
        {
            InitializeComponent();
            Parser pr = new Parser();
            PiecesMate = pr.DisplayStartPos(str);
            ChessBoardMate.ItemsSource = PiecesMate;
            CurrentId = dbc.FindId(str, "MateInOnePuzzle", "MateInOneStartPosition");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PuzzleListMate("MateInOnePuzzle"));
        }

        private void NextPuzzle_Click(object sender, RoutedEventArgs e)
        {
            DBConnection dbc = new DBConnection();
            string str = dbc.DisplayCertainPuzzle(CurrentId + 1, "MateInOnePuzzle", "MateInOneStartPosition");
            Switcher.Switch(new BoardMate(str));
        }

        private void ChessBoardMate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int xCoord = (int)Mouse.GetPosition(ChessBoardMate).X;
            int yCoord = (int)Mouse.GetPosition(ChessBoardMate).Y;
            selectedSquare = new Point(xCoord, yCoord);
            for (int i = 0; i < 32; i++)
                if (PiecesMate[i].Pos == selectedSquare)
                {
                    k = i;
                    _pieceType = PiecesMate[i].Type;
                    break;
                }
        }

        private void ChessBoardMate_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int xCoordDest = (int)Mouse.GetPosition(ChessBoardMate).X;
            int yCoordDest = (int)Mouse.GetPosition(ChessBoardMate).Y;
            releasedSquare = new Point(xCoordDest, yCoordDest);
            //if (PiecesMate[k].IsMovePossible(selectedSquare, releasedSquare, PiecesMate[k].Type, PiecesMate[k].Player, ref PiecesMate, k, ref taken))
            //{
                //if (taken == false)
                //    PiecesMate[k - 1].Pos = releasedSquare;
                //else PiecesMate[k].Pos = releasedSquare;
                //taken = true;
                //if (count != 1)
                //{
                    if (dbc.IsMoveCorrectMate(CurrentId, PiecesMate[k].Type, releasedSquare,"MateInOnePuzzle",1))
                    {
                        {
                            for (int i = 0; i < PiecesMate.Count; i++)
                                if (PiecesMate[i].Pos == releasedSquare)
                                    if (PiecesMate[i].Player != PiecesMate[k].Player)
                                    {
                                        if (i < k)
                                            taken = false;
                                        PiecesMate.Remove(PiecesMate[i]);
                                        break;
                                    }
                                    //else { jump = false; break; }
                            if (taken == false)
                                PiecesMate[k - 1].Pos = releasedSquare;
                            else PiecesMate[k].Pos = releasedSquare;
                            taken = true;
                            Info.AppendText("Верно! Задача решена! Перейдите к следующей.");
                            count++;
                        }

                    }
                    else Info.AppendText("Неверный ход! Попробуйте еще! ");
                //}
            //}
        }
    }
}
