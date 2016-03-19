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
        private ObservableCollection<ChessPiece> PiecesMate = new ObservableCollection<ChessPiece>();
        int k;
        bool taken = true;
        Point selectedSquare;
        Point releasedSquare;
        PieceType _pieceType;
        DBConnection dbc = new DBConnection();
        int CurrentId;
        int count = 0;
        
        public BoardMateInTwo(string str)
        {
            InitializeComponent();
            Parser pr = new Parser();
            PiecesMate = pr.DisplayStartPos(str);
            ChessBoardMate.ItemsSource = PiecesMate;
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
        }
    }
}
