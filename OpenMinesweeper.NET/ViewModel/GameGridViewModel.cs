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
            foreach (var cell in Cells)
            {
                //Searches the neighbors collection for the neighbors that have mines in them
                //When a mine is found it increments a counter that is shown inside the cell                
                cell.Neighbors = FindNeighbors(cell);
                var minesAroundCell = cell.Neighbors.Where(c => c.HasMine).Count();

                cell.Message = Convert.ToString(minesAroundCell);
            }

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

                if (col < ColumnNumber - 1)
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
                    cell.Message = "#";
                    //Game Over
                    Messenger.Default.Send(new SystemMessage(this, typeof(MainViewModel), "GameOver"));
                }
                //Otherwise, go through its neighbors and continue
                else
                {
                    cell.Visited = true;
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

            //This takes the current cell and adds i and k to the Line and Column
            //so we can find the adjacent cells
            for(int i = -1; i < 2; i++)
            {
                for (int k = -1; k < 2; k++)
                {
                    //Ignore itself
                    if(i == 0 && k == 0)
                    {
                        continue;
                    }

                    //This line searches for the adjacent cell, if the cell does not exist, the linq expression returns null
                    var neighbor = Cells.FirstOrDefault(c => ((int)c.Line == (int)cell.Line + i) && ((int)c.Column == (int)cell.Column + k));
                    if(neighbor != null)
                    {
                        _cells.Add(neighbor);
                    }
                }
            }

            return _cells;
        }
    }
}
