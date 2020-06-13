using System;
using System.Collections.Generic;
using System.Text;

namespace OpenMinesweeper.Core
{
    public class GameGrid
    {
        public uint LineCount { get; set; }
        public uint ColumnCount { get; set; }
        public ICollection<Cell> Cells { get; set; }

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
