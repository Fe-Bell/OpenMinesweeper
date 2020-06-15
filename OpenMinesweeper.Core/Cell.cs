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
        public Tuple<uint, uint> Position { get; set; }
        /// <summary>
        /// Checks if the cell is occupied or not.
        /// </summary>
        public bool Occupied { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Cell()
        {
            Position = new Tuple<uint, uint>(0, 0);
            Occupied = false;
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="column"></param>
        /// <param name="occupied"></param>
        public Cell(uint line, uint column, bool occupied)
        {
            Position = new Tuple<uint, uint>(line, column);
            Occupied = occupied;
        }
    }
}
