using GalaSoft.MvvmLight;
using OpenMinesweeper.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Ink;
using System.Windows.Shapes;

namespace OpenMinesweeper.NET.ViewModel
{
    public class GameGridViewModel : ObservableObject
    {
        private ObservableCollection<CellViewModel> cells = new ObservableCollection<CellViewModel>();
        public ObservableCollection<CellViewModel> Cells 
        { 
            get => cells; 
            set
            {
                cells = value;
                RaisePropertyChanged();
            }
        }

        private uint lineNumber = 0;
        public uint LineNumber
        {
            get => lineNumber;
            set
            {
                lineNumber = value;
                RaisePropertyChanged();
            }
        }

        private uint columnNumber = 0;
        public uint ColumnNumber
        {
            get => columnNumber;
            set
            {
                columnNumber = value;
                RaisePropertyChanged();
            }
        }

        private bool gameOver = false;
        public bool GameOver
        {
            get => gameOver;
            set
            {
                gameOver = value;
                RaisePropertyChanged();
            }
        }

        public GameGridViewModel()
        {
            Cells = new ObservableCollection<CellViewModel>();
        }

        public void Load(GameGrid gameGrid)
        {
            foreach(var cell in Cells)
            {
                cell.PropertyChanged -= Cell_PropertyChanged;
            }

            LineNumber = Convert.ToUInt32(Math.Sqrt(gameGrid.Cells.Count()));
            ColumnNumber = LineNumber;

            Cells = new ObservableCollection<CellViewModel>(gameGrid.Cells.Select(x => new CellViewModel(x.Position.Item1, x.Position.Item2, x.Occupied)));
            foreach (var cell in Cells)
            {
                cell.PropertyChanged += Cell_PropertyChanged;
            }
        }

        private void Cell_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Clicked")
            {
                CellViewModel cell = sender as CellViewModel;
                if(cell.HasMine)
                {
                    cell.Message = "BOOM!";
                    //Game Over
                    GameOver = true;
                }
                else
                {
                    cell.Visited = true;

                    uint minesAroundCell = 0;
                    var neighbors = FindNeighbors(cell);
                    foreach (var c in neighbors)
                    {
                        if (c.HasMine)
                        {
                            minesAroundCell++;
                        }
                    }

                    cell.Message = Convert.ToString(minesAroundCell);
                }
            }
        }

        private ICollection<CellViewModel> FindNeighbors(CellViewModel cell)
        {
            List<CellViewModel> _cells = new List<CellViewModel>();

            var right = Cells.FirstOrDefault(c => c.Column == cell.Column + 1 && c.Line == cell.Line);
            if (right != null) _cells.Add(right);
            var left = Cells.FirstOrDefault(c => c.Column == cell.Column - 1 && c.Line == cell.Line);
            if (left != null) _cells.Add(left);
            var bottom = Cells.FirstOrDefault(c => c.Column == cell.Column && c.Line == cell.Line + 1);
            if (bottom != null) _cells.Add(bottom);
            var top = Cells.FirstOrDefault(c => c.Column == cell.Column && c.Line == cell.Line - 1);
            if (top != null) _cells.Add(top);
            var top_right = Cells.FirstOrDefault(c => c.Column == cell.Column + 1 && c.Line == cell.Line - 1);
            if (top_right != null) _cells.Add(top_right);
            var top_left = Cells.FirstOrDefault(c => c.Column == cell.Column - 1 && c.Line == cell.Line - 1);
            if (top_left != null) _cells.Add(top_left);
            var bottom_right = Cells.FirstOrDefault(c => c.Column == cell.Column + 1 && c.Line == cell.Line + 1);
            if (bottom_right != null) _cells.Add(bottom_right);
            var bottom_left = Cells.FirstOrDefault(c => c.Column == cell.Column - 1 && c.Line == cell.Line + 1);
            if (bottom_left != null) _cells.Add(bottom_left);

            return _cells;
        }
    }
}
