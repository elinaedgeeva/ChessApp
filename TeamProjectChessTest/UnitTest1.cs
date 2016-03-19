using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using TeamProjectChess;
using TeamProjectChess.ViewModel;
using TeamProjectChess.Model;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using System.Data.SqlClient;

namespace TeamProjectChessTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Point start_point = new Point (2,7);        
            Point end_point = new Point (0, 5);
            Parser pc = new Parser();
            DBConnection dbc = new DBConnection();
            string str = dbc.DisplayCertainPuzzle(2);
            ObservableCollection<ChessPiece> coll = pc.DisplayStartPos(str);
            bool tr = true;
            ChessPiece cp = new ChessPiece();
            bool result= cp.IsMovePossible(start_point, end_point, PieceType.Bishop, Player.White,ref coll, 28, ref tr);
            bool expectation = true;
            Assert.AreEqual(expectation, result);


        }
        [TestMethod]
        public void TestMethod2()
        {
            
            DBConnection dbc = new DBConnection();
            string result= dbc.DisplayCertainPuzzle(2);
            string expect = "1rbq1rk1/1pp1ppbp/p1np1np1/8/2PP4/1PN2NP1/P3PPBP/R1BQ1RK1                                           ";
            Assert.AreEqual(expect, result);
        }
    }
}
