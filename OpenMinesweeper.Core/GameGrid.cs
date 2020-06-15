using System;
using System.Collections.Generic;

namespace OpenMinesweeper.Core
{
    /// <summary>
    /// Defines a grid filled with cells.
    /// </summary>
    public class GameGrid
    {
        /// <summary>
        /// The count of cell lines. 
        /// </summary>
        public uint LineCount { get; set; }
        /// <summary>
        /// The count of cell columns. 
        /// </summary>
        public uint ColumnCount { get; set; }
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

        /// <summary>
        /// Populates a collection of cells.
        /// </summary>
        /// <param name="max_cell_count"></param>
        public void Load(uint power)
        {
            //Use the power to generate a grid size
            uint gridSize = Convert.ToUInt32(Math.Pow(2, power));

            //Finds out the number of itens per line and column
            var cells_per_line = Convert.ToUInt32(Math.Sqrt(gridSize));
            LineCount = ColumnCount = cells_per_line;

            //If Cells is already populated, clears its contents.
            if (Cells.Count != 0)
            {
                Cells.Clear();
            }

            //Uses a randomizer to create the table of cells
            Random random = new Random();
            for(uint line = 0; line < LineCount; line++)
            {
                for (uint column = 0; column < ColumnCount; column++)
                {
                    bool occupied = random.NextDouble() > 0.5;
                    var cell = new Cell(line, column, occupied);

                    Cells.Add(cell);
                }
            }
        }

        /// <summary>
        /// Populates a collection of cells.
        /// </summary>
        /// <param name="lineCount"></param>
        /// <param name="columnCount"></param>
        public void Load(uint lineCount, uint columnCount)
        {
            //Finds out the number of itens per line and column
            ColumnCount = columnCount;
            LineCount = lineCount;

            //If Cells is already populated, clears its contents.
            if (Cells.Count != 0)
            {
                Cells.Clear();
            }

            //Uses a randomizer to create the table of cells
            Random random = new Random();
            for (uint line = 0; line < LineCount; line++)
            {
                for (uint column = 0; column < ColumnCount; column++)
                {
                    bool occupied = random.NextDouble() > 0.5;
                    var cell = new Cell(line, column, occupied);

                    Cells.Add(cell);
                }
            }
        }
    }
}
