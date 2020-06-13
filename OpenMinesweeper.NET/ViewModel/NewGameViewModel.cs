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
    public class NewGameViewModel :ViewModelBase
    {
        public const uint MAX_LINES = 20u;
        public const uint MIN_LINES = 0u;

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

        public event EventHandler OnNewGame = null;

        public NewGameViewModel(MinesweeperCore core)
        {
            this.core = core;

            PlayGame = new RelayCommand(() => PlayGameExecute(), () => true);

            PropertyChanged += NewGameViewModel_PropertyChanged;
        }

        private void NewGameViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "ColumnCount")
            {
                if(!string.IsNullOrEmpty(ColumnCount))
                {
                    //Checks if it is a valid number
                    uint number = 0;
                    if (uint.TryParse(ColumnCount, out number) || number == MIN_LINES || number > MAX_LINES)
                    {
                        if (number == MIN_LINES)
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
                    if (uint.TryParse(LineCount, out number) || number == MIN_LINES || number > MAX_LINES)
                    {
                        if (number == MIN_LINES)
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

        public ICommand PlayGame { get; private set; }
        public void PlayGameExecute()
        {
            uint column_count = 0;
            if (!uint.TryParse(ColumnCount, out column_count) || column_count == 0 || column_count > MAX_LINES) return;

            uint line_count = 0;
            if (!uint.TryParse(LineCount, out line_count) || line_count == 0 || line_count > MAX_LINES) return;

            var gameGrid = core.NewGame(line_count, column_count);
            if(gameGrid != null)
            {
                OnNewGame?.Invoke(this, null);
                Messenger.Default.Send(new SystemMessage(this, typeof(MainViewModel), "NewGame", gameGrid));
            }
        }
    }
}
