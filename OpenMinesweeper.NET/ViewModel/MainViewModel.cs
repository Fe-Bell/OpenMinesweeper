using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using OpenMinesweeper.Core;
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

        public MainViewModel(MinesweeperCore core)
        {
            this.core = core;

            GameGridVM = new GameGridViewModel();
            GameGridVM.PropertyChanged += GameGridVM_PropertyChanged;

            NewGame = new RelayCommand(() => NewGameExecute(), () => true);
        }

        private void GameGridVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "GameOver")
            {
                OnGameOver?.Invoke(this, null);
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
