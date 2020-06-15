using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OpenMinesweeper.Core;
using OpenMinesweeper.NET.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace OpenMinesweeper.NET.ViewModel
{
    /// <summary>
    /// Defines the main data model of the game.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Properties

        private MinesweeperCore core = null;

        private GameGridViewModel gameGridVM = null;
        public GameGridViewModel GameGridVM
        {
            get => gameGridVM;
            set
            {
                gameGridVM = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event raised once the game is over.
        /// </summary>
        public event EventHandler OnGameOver;
        /// <summary>
        /// Event raised once the player wins the game.
        /// </summary>
        public event EventHandler OnGameWon;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="core"></param>
        public MainViewModel(MinesweeperCore core)
        {
            this.core = core;

            GameGridVM = new GameGridViewModel();

            NewGame = new RelayCommand(() => NewGameExecute(), () => true);
            SaveGame = new RelayCommand<object>((obj) => SaveGameExecute(obj), (obj) => true);

            Messenger.Default.Register<SystemMessage>(this, (action) => OnSystemMessageReceived(action));
        }

        #region Methods

        /// <summary>
        /// Handles messages sent over MVVMLight messaging.
        /// </summary>
        /// <param name="message"></param>
        private void OnSystemMessageReceived(SystemMessage message)
        {
            if(message.Target == GetType())
            {
                if(message.Message == "GameOver")
                {
                    OnGameOver?.Invoke(this, null);
                }
                else if (message.Message == "GameWon")
                {
                    OnGameWon?.Invoke(this, null);
                }
                else if (message.Message == "LoadedGameState")
                {
                    GameState gameState = message.ExtendedData as GameState;
                    if(gameState != null)
                    {
                        string state = string.Empty;
                        GameGrid gameGrid = core.FromGameState(gameState, out state);
                        if(gameGrid != null)
                        {
                            GameGridVM.Load(gameGrid);
                            GameGridVM.LoadState(state);
                        }
                    }
                }
                else if (message.Message == "NewGame")
                {
                    GameGrid gameGrid = message.ExtendedData as GameGrid;
                    if (gameGrid != null)
                    {
                        GameGridVM.Load(gameGrid);
                    }
                }
            }
        }

        #endregion

        #region Commands

        public ICommand NewGame { get; private set; }
        private void NewGameExecute()
        {
            var newGameGrid = core.NewGame(4);
            GameGridVM.Load(newGameGrid);
        }

        public ICommand SaveGame { get; private set; }
        private void SaveGameExecute(object parameters)
        {
            if (parameters is null) return;

            object[] input = parameters as object[];
            if (input.Length < 2) return;

            string folder = input[0] as string;
            if (string.IsNullOrEmpty(folder)) return;

            string filename = input[1] as string;
            if (string.IsNullOrEmpty(filename)) return;

            string current_state = GameGridVM.GetUIState();
            if (string.IsNullOrEmpty(current_state)) return;

            GameGrid gameGrid = GameGridVM.ToGameGrid();
            if (gameGrid is null) return;

            core.SaveGame(gameGrid, current_state, folder, filename);
        }

        public ICommand LoadGame { get; private set; }
        private void LoadGameExecute(object parameters)
        {
            //if (parameters is null) return;

            //object[] input = parameters as object[];
            //if (input.Length < 2) return;

            //string folder = input[0] as string;
            //if (string.IsNullOrEmpty(folder)) return;

            //string filename = input[1] as string;
            //if (string.IsNullOrEmpty(filename)) return;

            //string current_state = GameGridVM.GetUIState();
            //if (string.IsNullOrEmpty(current_state)) return;

            //GameGrid gameGrid = GameGridVM.ToGameGrid();
            //if (gameGrid is null) return;

            //core.SaveGame(gameGrid, current_state, folder, filename);
        }

        #endregion
    }
}
