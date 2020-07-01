using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OpenMinesweeper.Core;
using OpenMinesweeper.NET.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Shapes;

namespace OpenMinesweeper.NET.ViewModel
{
    /// <summary>
    /// Provides data for creating a new game.
    /// </summary>
    public class NewGameViewModel :ViewModelBase
    {
        #region Constants

        /// <summary>
        /// Maximun grid size.
        /// </summary>
        public const uint MAX_LINES = 20u;
        /// <summary>
        /// Minimum grid size.
        /// </summary>
        public const uint MIN_LINES = 1u;

        #endregion

        #region Properties

        /// <summary>
        /// Internal instance of the MinesweeperCore.
        /// </summary>
        private MinesweeperCore core = null;

        private string lineCount = null;
        /// <summary>
        /// The number of rows in the grid.
        /// </summary>
        public string LineCount
        {
            get => lineCount;
            set
            {
                lineCount = value;
                RaisePropertyChanged();
            }
        }

        private string columnCount = null;
        /// <summary>
        /// The number of columns of the grid.
        /// </summary>
        public string ColumnCount
        {
            get => columnCount;
            set
            {
                columnCount = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Events

        public event EventHandler OnNewGame = null;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="core"></param>
        public NewGameViewModel(MinesweeperCore core)
        {
            this.core = core;

            PlayGame = new RelayCommand(() => PlayGameExecute(), () => true);

            PropertyChanged += NewGameViewModel_PropertyChanged;
        }

        #region Private methods

        private void NewGameViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "ColumnCount")
            {
                if(!string.IsNullOrEmpty(ColumnCount))
                {
                    //Checks if it is a valid number
                    uint number = 0;
                    if (uint.TryParse(ColumnCount, out number) || number < MIN_LINES || number > MAX_LINES)
                    {
                        if (number < MIN_LINES)
                        {
                            ColumnCount = "1";
                        }
                        else if (number > MAX_LINES)
                        {
                            ColumnCount = MAX_LINES.ToString();
                        }
                        else
                        {
                            //Success
                        }
                    }
                    else
                    {
                        ColumnCount = string.Empty;
                    }
                }
            }

            if(e.PropertyName == "LineCount")
            {
                if (!string.IsNullOrEmpty(LineCount))
                {
                    //Checks if it is a valid number
                    uint number = 0;
                    if (uint.TryParse(LineCount, out number) || number < MIN_LINES || number > MAX_LINES)
                    {
                        if (number < MIN_LINES)
                        {
                            LineCount = "1";
                        }
                        else if (number > MAX_LINES)
                        {
                            LineCount = MAX_LINES.ToString();
                        }
                        else
                        {
                            //Success
                        }
                    }
                    else
                    {
                        LineCount = string.Empty;
                    }
                }
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Starts a new game.
        /// </summary>
        public ICommand PlayGame { get; private set; }
        /// <summary>
        /// Logic for the StartGame command. 
        /// </summary>
        public void PlayGameExecute()
        {
            int column_count = 0;
            if (!int.TryParse(ColumnCount, out column_count) || column_count == 0 || column_count > MAX_LINES) return;

            int line_count = 0;
            if (!int.TryParse(LineCount, out line_count) || line_count == 0 || line_count > MAX_LINES) return;

            var gameGrid = core.NewRandomGridGame(line_count, column_count);
            if(gameGrid != null)
            {
                OnNewGame?.Invoke(this, null);
                Messenger.Default.Send(new SystemMessage(this, typeof(MainViewModel), "NewGame", gameGrid));
            }
        }

        #endregion
    }
}
