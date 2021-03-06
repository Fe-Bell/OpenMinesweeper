﻿using OpenMinesweeper.NET.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OpenMinesweeper.NET.View
{
    /// <summary>
    /// Interaction logic for GameGridControl.xaml
    /// </summary>
    public partial class GameGridControl : UserControl
    {
        public GameGridControl()
        {
            InitializeComponent();
        }

        private void CellButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border border = (sender as Border);
            if(border != null)
            {
                (border.DataContext as CellViewModel).Mark.Execute(null);
            }
        }
    }
}
