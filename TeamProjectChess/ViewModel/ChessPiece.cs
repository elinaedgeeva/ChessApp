using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using TeamProjectChess.Model;

namespace TeamProjectChess.ViewModel
{
    public class ChessPiece : ViewModelBase
    {
        private Point _Pos;
        public Point Pos
        {
            get { return this._Pos; }
            set { this._Pos = value; RaisePropertyChanged(() => this.Pos); }
        }

        //private IPiece piece;
        //public IPiece Piece
        //{
        //    get { return piece; }
        //    set { piece = value; }
        //}


        private PieceType _Type;
        public PieceType Type
        {
            get { return this._Type; }
            set { this._Type = value; RaisePropertyChanged(() => this.Type); }
        }

        private Player _Player;
        public Player Player
        {
            get { return this._Player; }
            set { this._Player = value; RaisePropertyChanged(() => this.Player); }
        }

        public void SpecDirection(Vector vec1, Vector constVec, Point st, Point dest, ObservableCollection<ChessPiece> _Pieces, ref bool jump)
        {
            if (Equals(vec1, constVec))
            {
                Point pt = Vector.Add(vec1, st);
                do
                {
                    foreach (var item in _Pieces)
                        if (item._Pos == pt)
                        {
                            jump = false;
                            break;
                        }
                    pt = Vector.Add(constVec, pt);
                } while (pt != dest);
            }
        }

        public bool IsMovePossible(Point start, Point destination, PieceType pieceType, Player player, ref ObservableCollection<ChessPiece> _Pieces)
        {
            Vector up = new Vector(0, -1);
            Vector down = new Vector(0, 1);
            Vector left = new Vector(-1, 0);
            Vector right = new Vector(1, 0);
            Vector up_left = new Vector(-1, -1);
            Vector up_right = new Vector(1, -1);
            Vector down_left = new Vector(-1, 1);
            Vector down_right = new Vector(1, 1);
            switch (pieceType)
            {
                #region Bishop
                case PieceType.Bishop:
                    bool jump = true;
                    bool suit = false;
                    if (Math.Abs(start.X - destination.X) == Math.Abs(start.Y - destination.Y))
                    {
                        suit = true;
                        //Checking move for not jumping over pieces
                        //Point pt = start;
                        Vector vec = destination - start;
                        Vector vecDiv = Vector.Divide(vec, Math.Abs(vec.X));

                        SpecDirection(vecDiv, up_left, start, destination, _Pieces, ref jump);
                        SpecDirection(vecDiv, up_right, start, destination, _Pieces, ref jump);
                        SpecDirection(vecDiv, down_left, start, destination, _Pieces, ref jump);
                        SpecDirection(vecDiv, down_right, start, destination, _Pieces, ref jump);

                        #region "if-s"
                        //if (Equals(vecDiv, up_left))
                        //{
                        //    Point pt = Vector.Add(vecDiv, start);
                        //    do
                        //    {
                        //        foreach (var item in _Pieces)
                        //            if (item._Pos == pt)
                        //            {
                        //                jump = false;
                        //                break;
                        //            }
                        //        pt = Vector.Add(up_left, pt);
                        //    } while (pt != destination);
                        //}

                        //if (Equals(vecDiv, up_right))
                        //{
                        //    Point pt = Vector.Add(vecDiv, start);
                        //    do
                        //    {
                        //        foreach (var item in _Pieces)
                        //            if (item._Pos == pt)
                        //            {
                        //                jump = false;
                        //                break;
                        //            }
                        //        pt = Vector.Add(up_right, pt);
                        //    } 
                        //    while (pt != destination);
                        //}

                        //if (Equals(vecDiv, down_left))
                        //{
                        //    Point pt = Vector.Add(vecDiv, start);
                        //    do
                        //    {
                        //        foreach (var item in _Pieces)
                        //            if (item._Pos == pt)
                        //            {
                        //                jump = false;
                        //                break;
                        //            }
                        //        pt = Vector.Add(down_left, pt);
                        //    } while (pt != destination);
                        //}

                        //if (Equals(vecDiv, down_right))
                        //{
                        //    Point pt = Vector.Add(vecDiv, start);
                        //    do
                        //    {
                        //        foreach (var item in _Pieces)
                        //            if (item._Pos == pt)
                        //            {
                        //                jump = false;
                        //                break;
                        //            }
                        //        pt = Vector.Add(down_right, pt);
                        //    } while (pt != destination);
                        //}
                        #endregion
                        if (jump)
                            for (int i = 0; i < _Pieces.Count; i++)
                                if (_Pieces[i]._Pos == destination)
                                    if (_Pieces[i].Player!=player)
                                    {
                                        _Pieces.Remove(_Pieces[i]);
                                        break;
                                    }
                                    else { jump = false; break; }
                    }

                    if (suit && jump)
                        return true;
                    else return false;
                    break;
                #endregion

                #region King
                case PieceType.King:
                    bool kingAble = false;
                    if (((start.X == destination.X + 1) || (start.X == destination.X - 1) || (start.X == destination.X))
                && ((start.Y == destination.Y) || (start.Y == destination.Y - 1) || (start.Y == destination.Y + 1)))
                    {
                        kingAble = true;
                        for (int i = 0; i < _Pieces.Count; i++)
                            if (_Pieces[i]._Pos == destination)
                                if (_Pieces[i].Player != player)
                                {
                                    _Pieces.Remove(_Pieces[i]);
                                    break;
                                }
                                else kingAble=false;
                    }
                    return kingAble;
                    break;
                #endregion

                #region Knight
                case PieceType.Knight:
                    bool knightAble = false;
                    if (((Math.Abs(start.X - destination.X) == 1) && (Math.Abs(start.Y - destination.Y) == 2))
                || ((Math.Abs(start.X - destination.X) == 2) && (Math.Abs(start.Y - destination.Y) == 1)))
                    {
                        knightAble = true;
                        for (int i = 0; i < _Pieces.Count; i++)
                            if (_Pieces[i]._Pos == destination)
                                if (_Pieces[i].Player != player)
                                {
                                    _Pieces.Remove(_Pieces[i]);
                                    break;
                                }
                                else knightAble = false;
                    }
                    return knightAble;
                    break;
                #endregion

                #region Pawn
                case PieceType.Pawn:
                    //logic for white pawns
                    //bool suitable=false;
                    bool pawnsTake = false;
                    if (((start.X == destination.X - 1) || (start.X == destination.X + 1)) && (start.Y == destination.Y + 1))
                    {
                        for (int i = 0; i < _Pieces.Count; i++)
                            if (_Pieces[i]._Pos == destination)
                                if (_Pieces[i].Player != player)
                                {
                                    _Pieces.Remove(_Pieces[i]);
                                    pawnsTake = true;
                                    break;
                                }
                    }

                    if (((start.X == destination.X) && (start.Y - 1 == destination.Y))
                        || ((start.Y == 6) && (start.Y - 2 == destination.Y)) || (pawnsTake))
                        return true;
                    else return false;

                    break;
                #endregion

                #region Queen
                case PieceType.Queen:
                    bool queensBool = false;
                    bool jumpQueen = true;
                    if ((Math.Abs(start.X - destination.X) == Math.Abs(start.Y - destination.Y)) || (start.X == destination.X) || (start.Y == destination.Y))
                    {
                        queensBool = true;
                        //Point pt = start;
                        Vector vec = destination - start;
                        Vector vecDiv = Vector.Divide(vec, Math.Abs(vec.X));
                        Vector vecDiv1 = Vector.Divide(vec, Math.Abs(vec.Y));
                        SpecDirection(vecDiv1, up, start, destination, _Pieces, ref jumpQueen);
                        SpecDirection(vecDiv1, down, start, destination, _Pieces, ref jumpQueen);
                        SpecDirection(vecDiv, left, start, destination, _Pieces, ref jumpQueen);
                        SpecDirection(vecDiv, right, start, destination, _Pieces, ref jumpQueen);
                        SpecDirection(vecDiv, up_left, start, destination, _Pieces, ref jumpQueen);
                        SpecDirection(vecDiv, up_right, start, destination, _Pieces, ref jumpQueen);
                        SpecDirection(vecDiv, down_left, start, destination, _Pieces, ref jumpQueen);
                        SpecDirection(vecDiv, down_right, start, destination, _Pieces, ref jumpQueen);
                        #region "if-s"
                        //if (Equals(vecDiv1, up))
                        //{
                        //    Point pt = Vector.Add(vecDiv1, start); 
                        //    do
                        //    {
                        //        foreach (var item in _Pieces)
                        //            if (item._Pos == pt)
                        //            {
                        //                jumpQueen = false;
                        //                break;
                        //            }
                        //        pt = Vector.Add(up, pt);
                        //    } while (pt != destination);
                        //}

                        //if (Equals(vecDiv1, down))
                        //{
                        //    Point pt = Vector.Add(vecDiv1, start); 
                        //    do
                        //    {
                        //        foreach (var item in _Pieces)
                        //            if (item._Pos == pt)
                        //            {
                        //                jumpQueen = false;
                        //                break;
                        //            }
                        //        pt = Vector.Add(down, pt);
                        //    } while (pt != destination);
                        //}

                        //if (Equals(vecDiv, left))
                        //{
                        //    Point pt = Vector.Add(vecDiv, start);
                        //    do
                        //    {
                        //        foreach (var item in _Pieces)
                        //            if (item._Pos == pt)
                        //            {
                        //                jumpQueen = false;
                        //                break;
                        //            }
                        //        pt = Vector.Add(left, pt);
                        //    } while (pt != destination);
                        //}

                        //if (Equals(vecDiv, right))
                        //{
                        //    Point pt = Vector.Add(vecDiv, start);
                        //    do
                        //    {
                        //        foreach (var item in _Pieces)
                        //            if (item._Pos == pt)
                        //            {
                        //                jumpQueen = false;
                        //                break;
                        //            }
                        //        pt = Vector.Add(right, pt);
                        //    } while (pt != destination);
                        //}

                        //if (Equals(vecDiv, up_left))
                        //{
                        //    Point pt = Vector.Add(vecDiv, start);
                        //    do
                        //    {

                        //        foreach (var item in _Pieces)
                        //            if (item._Pos == pt)
                        //            {
                        //                jumpQueen = false;
                        //                break;
                        //            }
                        //        pt = Vector.Add(up_left, pt);
                        //    } while (pt != destination);
                        //}

                        //if (Equals(vecDiv, up_right))
                        //{
                        //    Point pt = Vector.Add(vecDiv, start);
                        //    do
                        //    {
                        //        foreach (var item in _Pieces)
                        //            if (item._Pos == pt)
                        //            {
                        //                jumpQueen = false;
                        //                break;
                        //            }
                        //        pt = Vector.Add(up_right, pt);
                        //    } while (pt != destination);
                        //}

                        //if (Equals(vecDiv, down_left))
                        //{
                        //    Point pt = Vector.Add(vecDiv, start);
                        //    do
                        //    {
                        //        foreach (var item in _Pieces)
                        //            if (item._Pos == pt)
                        //            {
                        //                jumpQueen = false;
                        //                break;
                        //            }
                        //        pt = Vector.Add(down_left, pt);
                        //    } while (pt != destination);
                        //}

                        //if (Equals(vecDiv, down_right))
                        //{
                        //    Point pt = Vector.Add(vecDiv, start);
                        //    do
                        //    {
                        //        foreach (var item in _Pieces)
                        //            if (item._Pos == pt)
                        //            {
                        //                jumpQueen = false;
                        //                break;
                        //            }
                        //        pt = Vector.Add(down_right, pt);
                        //    } while (pt != destination);
                        //}

                        //if (jumpQueen)
                        //    foreach (var item in _Pieces)
                        //        if (item._Pos == destination)
                        //            _Pieces.Remove(item);
                        #endregion

                        if (jumpQueen)
                            for (int i = 0; i < _Pieces.Count; i++)
                                if (_Pieces[i]._Pos == destination)
                                    if (_Pieces[i].Player != player)
                                    {
                                        _Pieces.Remove(_Pieces[i]);
                                        break;
                                    }
                                    else { jumpQueen = false; break; }
                    }

                    if (queensBool && jumpQueen)
                        return true;
                    else return false;
                    break;
                #endregion

                #region Rook
                case PieceType.Rook:
                    bool rooksBool = false;
                    bool jumpRook = true;
                    if ((start.X == destination.X) || (start.Y == destination.Y))
                    {
                        rooksBool = true;
                        //Point pt = start;
                        Vector vec = destination - start;
                        Vector vecDiv = Vector.Divide(vec, Math.Abs(vec.X));
                        Vector vecDiv1 = Vector.Divide(vec, Math.Abs(vec.Y));
                        SpecDirection(vecDiv1, up, start, destination, _Pieces, ref jumpRook);
                        SpecDirection(vecDiv1, down, start, destination, _Pieces, ref jumpRook);
                        SpecDirection(vecDiv, left, start, destination, _Pieces, ref jumpRook);
                        SpecDirection(vecDiv, right, start, destination, _Pieces, ref jumpRook);
                        #region "if-s"
                        //if (Equals(vecDiv1, up))
                        //{
                        //    Point pt = Vector.Add(vecDiv1, start);
                        //    do
                        //    {
                        //        foreach (var item in _Pieces)
                        //            if (item._Pos == pt)
                        //            {
                        //                jumpRook = false;
                        //                break;
                        //            }
                        //        pt = Vector.Add(up, pt);
                        //    }
                        //    while (pt != destination);
                        //}

                        //if (Equals(vecDiv1, down))
                        //{
                        //    Point pt = Vector.Add(vecDiv1, start); 
                        //    do
                        //    {
                        //        foreach (var item in _Pieces)
                        //            if (item._Pos == pt)
                        //            {
                        //                jumpRook = false;
                        //                break;
                        //            }
                        //        pt = Vector.Add(down, pt);
                        //    } while (pt != destination);
                        //}

                        //if (Equals(vecDiv, left))
                        //{
                        //    Point pt = Vector.Add(vecDiv, start);
                        //    do
                        //    {
                        //        foreach (var item in _Pieces)
                        //            if (item._Pos == pt)
                        //            {
                        //                jumpRook = false;
                        //                break;
                        //            }
                        //        pt = Vector.Add(left, pt);
                        //    } while (pt != destination);
                        //}

                        //if (Equals(vecDiv, right))
                        //{
                        //    Point pt = Vector.Add(vecDiv, start);
                        //    do
                        //    {
                        //        foreach (var item in _Pieces)
                        //            if (item._Pos == pt)
                        //            {
                        //                jumpRook = false;
                        //                break;
                        //            }
                        //        pt = Vector.Add(right, pt);
                        //    } while (pt != destination);
                        //}

                        //if (jumpRook)
                        //    foreach (var item in _Pieces)
                        //        if (item._Pos == destination)
                        //            _Pieces.Remove(item);
                        #endregion

                        if (jumpRook)
                            for (int i = 0; i < _Pieces.Count; i++)
                                if (_Pieces[i]._Pos == destination)
                                    if (_Pieces[i].Player != player)
                                    {
                                        _Pieces.Remove(_Pieces[i]);
                                        break;
                                    }
                                    else { jumpRook = false; break; }
                    }

                    if (rooksBool && jumpRook)
                        return true;
                    else return false;
                    break;
                #endregion

                default: return false;

            }

        }
    }
}
