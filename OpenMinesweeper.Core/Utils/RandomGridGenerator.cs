using OpenMinesweeper.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenMinesweeper.Core.Utils
{
    public class RandomGridGenerator : IGridGenerator
    {
        public RandomGridGenerator()
        {

        }

        #region Private methods

        /// <summary>
        /// Generates a GameGrid.
        /// </summary>
        /// <param name="lineCount"></param>
        /// <param name="columnCount"></param>
        public IGrid GenerateGameGrid(int lineCount, int columnCount)
        {
            GameGrid grid = new GameGrid();

            //Finds out the number of itens per line and column
            grid.ColumnCount = columnCount;
            grid.LineCount = lineCount;

            //Uses a randomizer to create the table of cells
            Random random = new Random();
            for (int line = 0; line < grid.LineCount; line++)
            {
                for (int column = 0; column < grid.ColumnCount; column++)
                {
                    bool occupied = random.NextDouble() > 0.5;

                    var cell = new Cell(line, column, occupied, false);
                    grid.Cells.Add(cell);
                }
            }

            return grid;
        }

        /// <summary>
        /// Generates a Maze.
        /// </summary>
        /// <param name="lineCount"></param>
        /// <param name="columnCount"></param>
        public IGrid GenerateMaze(int lineCount, int columnCount)
        {
            Maze grid = new Maze();

            //Finds out the number of itens per line and column
            grid.Width = columnCount;
            grid.Height = lineCount;

            //Uses a randomizer to create the table of cells
            Random random = new Random();
            for (int line = 0; line < grid.Height; line++)
            {
                List<List<bool>> line_ = new List<List<bool>>();

                for (int column = 0; column < grid.Width; column++)
                {
                    bool occupied = random.NextDouble() > 0.5;

                    List<bool> data = new List<bool>() { occupied, false };
                    line_.Add(data);
                }

                grid.Cells.Add(line_);
            }

            return grid;
        }

        #endregion

        #region Public methods

        public T Generate<T>(int lineCount, int columnCount) where T : IGrid
        {
            Type t = typeof(T);

            if (t == typeof(Maze))
            {
                return (T)GenerateMaze(lineCount, columnCount);
            }
            else if (t == typeof(GameGrid))
            {
                return (T)GenerateGameGrid(lineCount, columnCount);
            }
            else
            {
                return default;
            }
        }

        #endregion
    }
}
