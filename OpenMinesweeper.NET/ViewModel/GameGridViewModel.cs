using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using OpenMinesweeper.Core;
using OpenMinesweeper.NET.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using OpenMinesweeper.Core.Utils;

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
            //Detach property changed event handling.
            Cells.ForEach(c => c.PropertyChanged -= Cell_PropertyChanged);

            //Creates new UI collection of cells.
            LineNumber = gameGrid.LineCount;
            ColumnNumber = gameGrid.ColumnCount;
            Cells = new ObservableCollection<CellViewModel>(gameGrid.Cells.Select(x => new CellViewModel(x.Position.Item1, x.Position.Item2, x.Occupied)));

            //Attach property changed event handling.
            Cells.ForEach(c => c.PropertyChanged += Cell_PropertyChanged);
        }

        /// <summary>
        /// Load a game state.
        /// </summary>
        /// <param name="state">Binary string representing which cells were previously clicked by the player.</param>
        public void LoadState(string state)
        {
            //Translate cells binary string to 2D array of cells.
            uint ln = 0, col = 0;
            foreach (var c in state)
            {
                var cell = Cells.FirstOrDefault(x => x.Line == ln && x.Column == col);
                if(cell != null)
                {
                    //cell.Visited = c != '0';
                    if(c != '0')
                    {
                        cell.Mark.Execute(null);
                    }                 
                }

                if (col < ColumnNumber)
                {
                    col++;
                }
                else
                {
                    ln++;
                    col = 0;
                }
            }
        }

        /// <summary>
        /// Returns a string representing if the cells have been visited or not.
        /// </summary>
        /// <returns></returns>
        public string GetUIState()
        {
            string state = string.Empty;
            Cells.ForEach(c => state += c.Visited ? "1" : "0");

            return state;
        }

        /// <summary>
        /// Converts this viewmodel into its model (GameGrid).
        /// </summary>
        /// <returns></returns>
        public GameGrid ToGameGrid()
        {
            GameGrid gameGrid = new GameGrid();
            gameGrid.ColumnCount = ColumnNumber;
            gameGrid.LineCount = LineNumber;
            gameGrid.Cells = new List<Cell>(Cells.Select(c => new Cell(c.Line, c.Column, c.HasMine)));

            return gameGrid;
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

                //Checks if all the cells that don't have mines have already been visited.
                //If this is true, the player won the game.
                if (Cells.Where(c => !c.HasMine).All(c => c.Visited))
                {
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
