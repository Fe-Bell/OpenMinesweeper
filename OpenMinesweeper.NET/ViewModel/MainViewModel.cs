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
    public class MainViewModel : ViewModelBase
    {
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

        public event EventHandler OnGameOver;
        public event EventHandler OnGameWon;

        public MainViewModel(MinesweeperCore core)
        {
            this.core = core;

            GameGridVM = new GameGridViewModel();

            NewGame = new RelayCommand(() => NewGameExecute(), () => true);

            Messenger.Default.Register<SystemMessage>(this, (action) => OnSystemMessageReceived(action));
        }

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
            }
        }

        #region Commands

        public ICommand NewGame { get; private set; }
        private void NewGameExecute()
        {
            var newGameGrid = core.NewGame(4);
            GameGridVM.Load(newGameGrid);
        }

        #endregion
    }
}
