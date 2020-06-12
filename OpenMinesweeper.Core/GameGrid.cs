using System;
using System.Collections.Generic;
using System.Text;

namespace OpenMinesweeper.Core
{
    public class GameGrid
    {
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

            //If Cells is already populated, clears its contents.
            if (Cells.Count != 0)
            {
                Cells.Clear();
            }

            //Uses a randomizer to create the table of cells
            Random random = new Random();
            for(uint line = 0; line < cells_per_line; line++)
            {
                for (uint column = 0; column < cells_per_line; column++)
                {
                    bool occupied = random.NextDouble() > 0.5;
                    var cell = new Cell(line, column, occupied);

                    Cells.Add(cell);
                }
            }
        }
    }
}
