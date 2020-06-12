using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using OpenMinesweeper.Core;
using OpenMinesweeper.NET.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace OpenMinesweeper.NET.ViewModel
{
    /// <summary>
    /// Represents a game grid for the GUI.
    /// </summary>
    public class GameGridViewModel : ObservableObject
    {
        private ObservableCollection<CellViewModel> cells = new ObservableCollection<CellViewModel>();
        /// <summary>
        /// A collection of cells in the current game.
        /// </summary>
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
        /// <summary>
        /// The number of rows in the grid.
        /// </summary>
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
        /// <summary>
        /// The number of columns of the grid.
        /// </summary>
        public uint ColumnNumber
        {
            get => columnNumber;
            set
            {
                columnNumber = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Creates a new instance of GameGridViewModel. 
        /// </summary>
        public GameGridViewModel()
        {
            Cells = new ObservableCollection<CellViewModel>();
        }

        /// <summary>
        /// Reloads the GameGridViewModel with a new game grid.
        /// </summary>
        /// <param name="gameGrid">A base game grid from the OpenMinesweeper.Core</param>
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

        /// <summary>
        /// Property changed handler for each cell.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cell_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Clicked")
            {
                CellViewModel cell = sender as CellViewModel;

                //Checks if there are still mines in the game
                if (Cells.Any(c => c.HasMine))
                {
                    //If the clicked cell has a mine, then it is game over
                    if (cell.HasMine)
                    {
                        cell.Message = "BOOM!";
                        //Game Over
                        Messenger.Default.Send(new SystemMessage(this, typeof(MainViewModel), "GameOver"));
                    }
                    //Otherwise, go through its neighbors and continue
                    else
                    {
                        cell.Visited = true;

                        //Searches the neighbors collection for the neighbors that have mines in them
                        //When a mine is found it increments a counter that is shown inside the cell                
                        var neighbors = FindNeighbors(cell);
                        var minesAroundCell = neighbors.Where(c => c.HasMine).Count();

                        cell.Message = Convert.ToString(minesAroundCell);
                    }
                }
                //If no mines were found, the player has won the game.
                else
                {
                    cell.Visited = true;

                    //Game Won
                    Messenger.Default.Send(new SystemMessage(this, typeof(MainViewModel), "GameWon"));
                }
            }
        }

        /// <summary>
        /// Returns a maximun of 8 surrounding neighbors of a cell.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
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
