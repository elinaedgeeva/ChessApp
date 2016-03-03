using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TeamProjectChess.Model;

namespace TeamProjectChess.ViewModel
{
    public class Parser
    {
        char letter;
        int coordX, coodrY;
        int i = 0, j = 0;

        public ObservableCollection<ChessPiece> DisplayStartPos(string str)
        {
            ObservableCollection<ChessPiece> StartPos = new ObservableCollection<ChessPiece>();
            while ((j < 64) && (i <= str.Length))
            {
                letter = str.ElementAt(i);
                i++;
                coordX = j % 8;
                coodrY = j / 8;
                switch (letter)
                {
                    case 'p': StartPos.Add(new ChessPiece { Pos = new Point(coordX, coodrY), Type = PieceType.Pawn, Player = Player.Black }); break;
                    case 'r': StartPos.Add(new ChessPiece { Pos = new Point(coordX, coodrY), Type = PieceType.Rook, Player = Player.Black }); break;
                    case 'n': StartPos.Add(new ChessPiece { Pos = new Point(coordX, coodrY), Type = PieceType.Knight, Player = Player.Black }); break;
                    case 'b': StartPos.Add(new ChessPiece { Pos = new Point(coordX, coodrY), Type = PieceType.Bishop, Player = Player.Black }); break;
                    case 'q': StartPos.Add(new ChessPiece { Pos = new Point(coordX, coodrY), Type = PieceType.Queen, Player = Player.Black }); break;
                    case 'k': StartPos.Add(new ChessPiece { Pos = new Point(coordX, coodrY), Type = PieceType.King, Player = Player.Black }); break;
                    case 'P': StartPos.Add(new ChessPiece { Pos = new Point(coordX, coodrY), Type = PieceType.Pawn, Player = Player.White }); break;
                    case 'R': StartPos.Add(new ChessPiece { Pos = new Point(coordX, coodrY), Type = PieceType.Rook, Player = Player.White }); break;
                    case 'N': StartPos.Add(new ChessPiece { Pos = new Point(coordX, coodrY), Type = PieceType.Knight, Player = Player.White }); break;
                    case 'B': StartPos.Add(new ChessPiece { Pos = new Point(coordX, coodrY), Type = PieceType.Bishop, Player = Player.White }); break;
                    case 'Q': StartPos.Add(new ChessPiece { Pos = new Point(coordX, coodrY), Type = PieceType.Queen, Player = Player.White }); break;
                    case 'K': StartPos.Add(new ChessPiece { Pos = new Point(coordX, coodrY), Type = PieceType.King, Player = Player.White }); break;
                    case '/': j--; break;
                    case '1': break;
                    case '2': j++; break;
                    case '3': j += 2; break;
                    case '4': j += 3; break;
                    case '5': j += 4; break;
                    case '6': j += 5; break;
                    case '7': j += 6; break;
                    case '8': j += 7; break;
                }
                j++;
            }
            return StartPos;
        }
    }
}
