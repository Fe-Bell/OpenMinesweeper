﻿using OpenMinesweeper.Core.Interface;
using OpenMinesweeper.Core.Utils;
using ReflectXMLDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OpenMinesweeper.Core
{
    /// <summary>
    /// The core of the minesweeper game.
    /// </summary>
    public class MinesweeperCore
    {
        /// <summary>
        /// An internal instance of the configuration loaderr. 
        /// </summary>
        public SoftwareConfigLoader ConfigurationLoader { get; private set; }

        /// <summary>
        /// Gets/Sets the current path of the configuration file.
        /// </summary>
        public string ConfigurationFilePath
        {
            get
            {
                return ConfigurationLoader != null ? ConfigurationLoader.FilePath : string.Empty;
            }
            set
            {
                if (ConfigurationLoader != null)
                {
                    ConfigurationLoader.Initialize(value);
                }
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MinesweeperCore()
        {
            ConfigurationLoader = new SoftwareConfigLoader();
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filePath"></param>
        public MinesweeperCore(string filePath) : this()
        {
            ConfigurationFilePath = filePath;
        }

        #region Methods

        /// <summary>
        /// Creates a new game.
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        public GameGrid NewRandomGridGame(int lineCount, int columnCount)
        {
            IGridGenerator gridGenerator = new RandomGridGenerator();

            GameGrid gg = gridGenerator.Generate<GameGrid>(lineCount, columnCount);

            return gg;
        }

        /// <summary>
        /// Creates a new game.
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        public GameGrid NewMazeGame(int lineCount, int columnCount)
        {
            IGridGenerator gridGenerator = new MazeGenerator();

            GameGrid gg = gridGenerator.Generate<GameGrid>(lineCount, columnCount);

            return gg;
        }

        /// <summary>
        /// Saves the current game to a database.
        /// </summary>
        /// <param name="gameGrid"></param>
        /// <param name="folder"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool SaveGame(GameGrid gameGrid, string state, string folder, string filename)
        {
            DatabaseHandler dh = new DatabaseHandler();

            Type[] databaseTypes = new Type[] { typeof(GameStateDatabase) };
            dh.SetWorkspace(folder, databaseTypes);

            GameState gameState = ToGameState(gameGrid, state);

            //If the file already exists, just update the database.         
            string pathToDB = Path.Combine(folder, filename);
            if(File.Exists(pathToDB))
            {
                dh.ImportDatabase(pathToDB, folder);       
            }
            //Else, creates a new database
            else
            {
                dh.CreateDatabase<GameStateDatabase>();
            }

            dh.Insert(new GameState[] { gameState });
            
            dh.ExportDatabase(folder, System.IO.Path.GetFileNameWithoutExtension(filename));

            return File.Exists(pathToDB);
        }

        /// <summary>
        /// Returns all saved GameStates.
        /// </summary>
        /// <param name="fileToPath"></param>
        /// <param name="workingDir"></param>
        /// <param name="gameStates"></param>
        /// <returns></returns>
        public bool GetSavedGames(string fileToPath, string workingDir, out ICollection<GameState> gameStates)
        {
            DatabaseHandler dh = new DatabaseHandler();

            Type[] databaseTypes = new Type[] { typeof(GameStateDatabase) };
            dh.SetWorkspace(workingDir, databaseTypes);

            dh.ImportDatabase(fileToPath, workingDir);

            gameStates = dh.Get<GameState>();

            return gameStates.Any();
        }

        /// <summary>
        /// Converts a GameState to a GameGrid.
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns></returns>
        public GameGrid FromGameState(GameState gameState, out string state)
        {
            string line_count_str = gameState.Grid.Substring(0, 8);
            string column_count_str = gameState.Grid.Substring(8, 8);
            string cells_str = gameState.Grid.Substring(16);
            state = gameState.State;

            GameGrid gameGrid = new GameGrid();
            gameGrid.LineCount = Convert.ToInt32(line_count_str, 2);
            gameGrid.ColumnCount = Convert.ToInt32(column_count_str, 2);
             
            //Translate cells binary string to 2D array of cells.
            int ln = 0, col = 0;
            foreach(var c in cells_str)
            {
                Cell cell = new Cell();
                cell.Occupied = c != '0';
                cell.Position = new Tuple<int, int>(ln, col);
                gameGrid.Cells.Add(cell);

                if (col < gameGrid.ColumnCount - 1)
                {
                    col++;
                }
                else
                {
                    ln++;
                    col = 0;
                }
            }

            return gameGrid;
        }

        /// <summary>
        /// Converts a GameGrid tp a GameState.
        /// </summary>
        /// <param name="gameGrid"></param>
        /// <returns></returns>
        private GameState ToGameState(GameGrid gameGrid, string state)
        {
            string line_count = Convert.ToString(gameGrid.LineCount, 2).PadLeft(8, '0');
            string column_count = Convert.ToString(gameGrid.ColumnCount, 2).PadLeft(8, '0');
            string cells = string.Empty;
            gameGrid.Cells.ForEach(cell => cells += cell.Occupied ? "1" : "0");

            GameState gameState = new GameState();
            gameState.State = state;
            gameState.Grid = line_count + column_count + cells;
            gameState.Date = string.Format("{0}_{1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());

            return gameState;
        }

        #endregion
    }
}
