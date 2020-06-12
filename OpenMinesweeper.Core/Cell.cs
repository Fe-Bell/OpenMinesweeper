using System;
using System.Collections.Generic;
using System.Text;

namespace OpenMinesweeper.Core
{
    public class Cell
    {
        public Tuple<uint, uint> Position { get; set; }
        public bool Occupied { get; set; }

        public Cell()
        {
            Position = new Tuple<uint, uint>(0, 0);
            Occupied = false;
        }
        public Cell(uint line, uint column, bool occupied)
        {
            Position = new Tuple<uint, uint>(line, column);
            Occupied = occupied;
        }
    }
}
