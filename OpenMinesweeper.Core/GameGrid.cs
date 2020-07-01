using OpenMinesweeper.Core.Interface;
using System;
using System.Collections.Generic;

namespace OpenMinesweeper.Core
{
    /// <summary>
    /// Defines a grid filled with cells.
    /// </summary>
    public class GameGrid : IGrid
    {
        /// <summary>
        /// The count of cell lines. 
        /// </summary>
        public int LineCount { get; set; }
        /// <summary>
        /// The count of cell columns. 
        /// </summary>
        public int ColumnCount { get; set; }
        /// <summary>
        /// A collection of cells in the grid.
        /// </summary>
        public ICollection<Cell> Cells { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public GameGrid()
        {
            Cells = new List<Cell>();
        }
    }
}
