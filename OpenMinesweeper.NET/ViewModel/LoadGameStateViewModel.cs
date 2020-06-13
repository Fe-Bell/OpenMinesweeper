using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OpenMinesweeper.Core;
using OpenMinesweeper.NET.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace OpenMinesweeper.NET.ViewModel
{
    public class LoadGameStateViewModel : ViewModelBase
    {
        private MinesweeperCore core = null;

        private ObservableCollection<GameState> gameStates;
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
        public GameState SelectedGameState
        {
            get => selectedGameState;
            set
            {
                selectedGameState = value;
                RaisePropertyChanged();
            }
        }

        public LoadGameStateViewModel(MinesweeperCore core)
        {
            this.core = core;
            GameStates = new ObservableCollection<GameState>();

            LoadGameState = new RelayCommand(() => LoadGameStateExecute(), () => true);
        }

        public void UpdateGameStates(string filename)
        {
            ICollection<GameState> gameStates_;
            if(core.GetSavedGames(filename, Environment.CurrentDirectory, out gameStates_))
            {
                GameStates = new ObservableCollection<GameState>(gameStates_);
            }
        }

        public ICommand LoadGameState { get; private set; }
        private void LoadGameStateExecute()
        {
            if(SelectedGameState != null)
            {
                RaisePropertyChanged("LoadedGameState");
                Messenger.Default.Send(new SystemMessage(this, typeof(MainViewModel), "LoadedGameState", SelectedGameState));
            }
        }
    }
}
