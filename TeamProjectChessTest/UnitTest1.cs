using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using TeamProjectChess;
using TeamProjectChess.ViewModel;
using TeamProjectChess.Model;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace TeamProjectChessTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Point start_point = new Point (2,7);        
            Point end_point = new Point (2, 5);
            Parser pc = new Parser();
            DBConnection dbc = new DBConnection();
            string str = dbc.DisplayCertainPuzzle(2);
            ObservableCollection<ChessPiece> coll = pc.DisplayStartPos(str);
            bool tr = true;
            ChessPiece cp = new ChessPiece();
            bool result= cp.IsMovePossible(start_point, end_point, PieceType.Bishop, Player.White,ref coll, 28, ref tr);
            bool expectation = false;
            Assert.AreEqual(expectation, result);


        }
        [TestMethod]
        public void TestMethod2()
        {

        }
    }
}
