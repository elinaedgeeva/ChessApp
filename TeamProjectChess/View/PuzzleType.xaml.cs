﻿using System;
using System.Collections.Generic;
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

namespace TeamProjectChess.View
{
    /// <summary>
    /// Логика взаимодействия для PuzzleType.xaml
    /// </summary>
    public partial class PuzzleType : UserControl, ISwitchable
    {
        public PuzzleType()
        {
            InitializeComponent();
        }

        private void Debuts_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PuzzleLevels());
        }

        private void BestMove_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PuzzleLevels());
        }

        private void MateInMoves_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PuzzleLevels());
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }
    }
}