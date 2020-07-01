using System;

namespace OpenMinesweeper.Core
{
    /// <summary>
    /// Defines a 2D cell.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// The location of the cell in a 2D array format.
        /// </summary>
        public Tuple<int, int> Position { get; set; }
        /// <summary>
        /// Checks if the cell is occupied or not.
        /// </summary>
        public bool Occupied { get; set; }
        /// <summary>
        /// Checks if this cell is in a border or not.
        /// </summary>
        public bool IsEdge { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Cell()
        {
            Position = new Tuple<int, int>(0, 0);
            Occupied = false;
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="column"></param>
        /// <param name="occupied"></param>
        public Cell(int line, int column, bool occupied, bool isEdge)
        {
            Position = new Tuple<int, int>(line, column);
            Occupied = occupied;
            IsEdge = isEdge;
        }
    }
}
