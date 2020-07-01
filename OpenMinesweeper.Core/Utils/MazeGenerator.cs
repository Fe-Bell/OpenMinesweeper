using OpenMinesweeper.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenMinesweeper.Core.Utils
{
    public class MazeGenerator : IGridGenerator
	{
        #region Private fields

        private const uint UP = 0;
        private const uint DOWN = 1;
        private const uint LEFT = 2;
        private const uint RIGHT = 3;

        private int m_start_axis;
        private int m_start_side;
		private Random random;
		private Queue<List<int>> dfs_queue;

        #endregion

        #region Public properties

       
		#endregion

		public MazeGenerator()
        {
			Reset();
        }

        #region Private methods

		/// <summary>
		/// Resets the generator.
		/// </summary>
		private void Reset()
        {
			m_start_axis = 0;
			m_start_side = 0;
			random = new Random();
			dfs_queue = new Queue<List<int>>();
		}

        /// <summary>
        /// Select a random direction based on our options, append it to the current path, and move there
        /// </summary>
        /// <param name="first_move"></param>
        /// <returns></returns>
        private bool RandomMove(Maze maze, bool first_move)
		{
			int random_neighbor;
			List<List<int>> unvisited_neighbors = new List<List<int>>();

			for (int direction = 0; direction < 4; direction++)
			{
				int[] possible_pmd = new int[] { 0, 0 };

				if (direction == UP)
				{
					possible_pmd[1] = -1;
				}
				else if (direction == DOWN)
				{
					possible_pmd[1] = 1;
				}
				else if (direction == LEFT)
				{
					possible_pmd[0] = -1;
				}
				else
				{
					possible_pmd[0] = 1;
				}

				if (dfs_queue.LastOrDefault()[0] + possible_pmd[0] * 2 > 0 &&
					dfs_queue.LastOrDefault()[0] + possible_pmd[0] * 2 < maze.Width - 1 &&
					dfs_queue.LastOrDefault()[1] + possible_pmd[1] * 2 > 0 &&
					dfs_queue.LastOrDefault()[1] + possible_pmd[1] * 2 < maze.Height - 1)
				{
					var column = dfs_queue.LastOrDefault()[1] + possible_pmd[1] * 2;
					var line = dfs_queue.LastOrDefault()[0] + possible_pmd[0] * 2;

					var cell = maze.Cells.ElementAt(column).ElementAt(line)[1]; 

					if (!cell)
					{
						List<int> possible_move_delta = new List<int>(possible_pmd);

						unvisited_neighbors.Add(possible_move_delta);
					}
				}
			}

			if (unvisited_neighbors.Count() > 0)
			{
				random_neighbor = random.Next() % unvisited_neighbors.Count;

				for (int a = 0; a < Convert.ToInt32(!first_move) + 1; a++)
				{
					List<int> new_location = new List<int>();

					new_location.Add(dfs_queue.LastOrDefault()[0] + unvisited_neighbors[random_neighbor][0]);
					new_location.Add(dfs_queue.LastOrDefault()[1] + unvisited_neighbors[random_neighbor][1]);

					dfs_queue.Enqueue(new_location);

					maze.Cells[dfs_queue.LastOrDefault()[1]][dfs_queue.LastOrDefault()[0]][0] = false;
					maze.Cells[dfs_queue.LastOrDefault()[1]][dfs_queue.LastOrDefault()[0]][1] = true;
				}

				return true;
			}
			else
			{
				return false;
			}
		}

		private bool ValidInteger(string str)
		{
			foreach (char c in str)
			{
				if (!char.IsDigit(c))
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Initialize the maze vector with a completely-filled grid with the size the user specified
		/// </summary>
		private void InitializeMaze(Maze maze)
		{
			for (int a = 0; a < maze.Height; a++)
			{
				for (int b = 0; b < maze.Width; b++)
				{
					bool is_border;

					if (a == 0 || a == maze.Height - 1 ||
						b == 0 || b == maze.Width - 1)
					{
						is_border = true;
					}
					else
					{
						is_border = false;
					}

					List<bool> new_cell = new List<bool>() { true, is_border };

					if ((uint )a + 1 > maze.Cells.Count()) 
					{
						List<List<bool>> new_row = new List<List<bool>>();
						new_row.Add(new_cell);

						maze.Cells.Add(new_row);
					}
					else
					{
						maze.Cells[a].Add(new_cell);
					}
				}
			}
		}

		/// <summary>
		/// Set a random point (start or end)
		/// </summary>
		/// <param name="part"></param>
		private void RandomPoint(Maze maze, bool part)
		{
			int axis = 0;
			int side = 0;

			if (!part)
			{
				axis = random.Next() % 2;
				side = random.Next() % 2;

				m_start_axis = axis;
				m_start_side = side;
			}
			else
			{
				bool done = false;

				while (!done)
				{
					axis = random.Next() % 2;
					side = random.Next() % 2;

					if (axis != m_start_axis || side != m_start_side)
					{
						done = true;
					}
				}
			}

			List<int> location = new List<int>() { 0, 0 };

			if (!Convert.ToBoolean(side))
			{
				var _axis = Convert.ToInt32(!Convert.ToBoolean(axis));
				location[_axis] = 0;
			}
			else
			{
				var axis_ = !Convert.ToBoolean(axis);
				location[axis_ ? 0 : 1] = axis_ ? maze.Width - 1 : maze.Height - 1;
			}

			location[axis] = 2 * (random.Next() % ((axis != 0 ? maze.Width + 1 : maze.Height + 1) / 2 - 2)) + 1;

			if (!part)
			{
				dfs_queue.Enqueue(location);
			}

			maze.Cells[location[1]][location[0]][0] = false;
			maze.Cells[location[1]][location[0]][1] = true;
		}

		/// <summary>
		/// Generates a maze.
		/// </summary>
		/// <param name=""></param>
		/// <param name="width"></param>
		/// <param name=""></param>
		/// <param name="height"></param>
		/// <returns></returns>
		private IGrid GenerateGridAsMaze(int width, int height)
		{
			var m_maze_size = new int[] { 0, 0 };
			m_maze_size[0] = width;
			m_maze_size[1] = height;

			if (width < 5 || height < 5)
			{
				return null;
			}

			// The width and height must be greater than or equal to 5 or it won't work
			// The width and height must be odd or else we will have extra walls
			for (int a = 0; a < 2; a++)
			{
				if (m_maze_size[a] < 5)
				{
					m_maze_size[a] = 5;
				}
				else if (m_maze_size[a] % 2 == 0)
				{
					m_maze_size[a]--;
				}
			}

			Maze maze = new Maze();
			maze.Width = m_maze_size[0];
			maze.Height = m_maze_size[1];

			InitializeMaze(maze);
			RandomPoint(maze, false);
			RandomPoint(maze, true);

			bool first_move = true;
			bool success = true;

			while (dfs_queue.Count() > 1 - Convert.ToInt32(first_move))
			{
				if (!success)
				{
					dfs_queue.Dequeue();

					if (!first_move && dfs_queue.Count() > 2)
					{
						dfs_queue.Dequeue();
					}
					else
					{
						break;
					}

					success = true;
				}

				while (success)
				{
					success = RandomMove(maze, first_move);

					if (first_move)
					{
						first_move = false;
					}
				}
			}

			Reset();

			return maze;
		}

		private IGrid GenerateAsGameGrid(int width, int height)
		{
			var maze = GenerateGridAsMaze(width, height);

			return MazeToGameGrid(maze as Maze);
		}

		private GameGrid MazeToGameGrid(Maze maze)
		{
			GameGrid grid = new GameGrid();
			grid.ColumnCount = maze.Width;
			grid.LineCount = maze.Height;

			for (int line = 0; line < maze.Cells.Count(); line++)
			{
				for (int column = 0; column < maze.Cells[line].Count(); column++)
				{
					var cell = new Cell(line, column, maze.Cells[line][column][0], maze.Cells[line][column][1]);
					grid.Cells.Add(cell);
				}
			}

			return grid;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Returns a maze of the specified type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="columnCount"></param>
		/// <param name="lineCount"></param>
		/// <returns></returns>
		public T Generate<T>(int lineCount, int columnCount) where T : IGrid
		{
			Type t = typeof(T);

			if (t == typeof(Maze))
            {
				return (T)GenerateGridAsMaze(columnCount, lineCount);
			}
			else if (t == typeof(GameGrid))
			{
				return (T)GenerateAsGameGrid(columnCount, lineCount);
			}
            else
            {
				return default;
            }
		}

		/// <summary>
		/// Gets the maze as a string.
		/// </summary>
		/// <returns>The maze as a string.</returns>
		public string PrintMaze<T>(T maze) where T : IGrid
		{
			string mazeStr = string.Empty;

			Type t = typeof(T);

			if (t == typeof(Maze))
			{
				var _maze = maze as Maze;

				for (int line = 0; line < _maze.Cells.Count(); line++)
				{
					for (int column = 0; column < _maze.Cells[line].Count(); column++)
					{
						mazeStr += _maze.Cells[line][column][0] ? "||" : "  ";
					}

					mazeStr += "\n";
				}
			}
			else if (t == typeof(GameGrid))
			{
				var _maze = maze as GameGrid;

				for (int line = 0; line < _maze.LineCount; line++)
				{
					for (int column = 0; column < _maze.ColumnCount; column++)
					{
						var cell = _maze.Cells.FirstOrDefault(c => ((int)c.Position.Item1 == line) && ((int)c.Position.Item2 == column));
						if(cell != null)
                        {
							mazeStr += cell.Occupied ? "||" : "  ";
						}						
					}

					mazeStr += "\n";
				}
			}

			return mazeStr;
		}

        #endregion
    }
}
