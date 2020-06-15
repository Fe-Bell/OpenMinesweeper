using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OpenMinesweeper.Core;
using OpenMinesweeper.NET.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace OpenMinesweeper.NET.ViewModel
{
    /// <summary>
    /// Defines the data model for the Load Game page.
    /// </summary>
    public class LoadGameStateViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// Local instance of the core.
        /// </summary>
        private MinesweeperCore core = null;

        private ObservableCollection<GameState> gameStates;
        /// <summary>
        /// Stores all the save games loaded by the player.
        /// </summary>
        public ObservableCollection<GameState> GameStates
        {
            get => gameStates;
            set
            {
                gameStates = value;
                RaisePropertyChanged();
            }
        }

        private GameState selectedGameState = null;
        /// <summary>
        /// The save game selected by the player.
        /// </summary>
        public GameState SelectedGameState
        {
            get => selectedGameState;
            set
            {
                selectedGameState = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Game Load event. Fired once the player loads a saved game.
        /// </summary>
        public event EventHandler OnGameLoad = null;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="core"></param>
        public LoadGameStateViewModel(MinesweeperCore core)
        {
            this.core = core;
            GameStates = new ObservableCollection<GameState>();

            LoadGameState = new RelayCommand(() => LoadGameStateExecute(), () => true);
        }

        #region Methods

        /// <summary>
        /// Loads a collection of saved games.
        /// </summary>
        /// <param name="filename"></param>
        public void UpdateGameStates(string filename)
        {
            ICollection<GameState> gameStates_;
            if(core.GetSavedGames(filename, Environment.CurrentDirectory, out gameStates_))
            {
                GameStates = new ObservableCollection<GameState>(gameStates_);
            }
        }

        #endregion

        #region Commands

        public ICommand LoadGameState { get; private set; }
        private void LoadGameStateExecute()
        {
            if(SelectedGameState != null)
            {
                OnGameLoad?.Invoke(this, null);
                Messenger.Default.Send(new SystemMessage(this, typeof(MainViewModel), "LoadedGameState", SelectedGameState));
            }
        }

        #endregion
    }
}
