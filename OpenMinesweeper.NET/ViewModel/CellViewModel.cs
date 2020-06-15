using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace OpenMinesweeper.NET.ViewModel
{
    /// <summary>
    /// DEfines a GUI 2D cell.
    /// </summary>
    public class CellViewModel : ObservableObject
    {
        #region Properties

        private uint line = 0;
        /// <summary>
        /// The line position of the cell.
        /// </summary>
        public uint Line
        {
            get => line;
            set
            {
                line = value;
                RaisePropertyChanged();
            }
        }

        private uint column = 0;
        /// <summary>
        /// The column position of the cell.
        /// </summary>
        public uint Column
        {
            get => column;
            set
            {
                column = value;
                RaisePropertyChanged();
            }
        }

        private bool hasMine = false;
        /// <summary>
        /// Returns if the cell has a mine or not.
        /// </summary>
        public bool HasMine
        {
            get => hasMine;
            set
            {
                hasMine = value;
                RaisePropertyChanged();
            }
        }

        private bool visited = false;
        /// <summary>
        /// Returns if the cell has been visited or not.
        /// </summary>
        public bool Visited
        {
            get => visited;
            set
            {
                visited = value;
                RaisePropertyChanged();
            }
        }

        private bool clicked = false;
        /// <summary>
        /// Returns if the cell has been selected in the GUI.
        /// </summary>
        public bool Clicked
        {
            get => clicked;
            set
            {
                clicked = value;
                RaisePropertyChanged();
            }
        }

        private string message = "?";
        /// <summary>
        /// A message displayed by the cell.
        /// </summary>
        public string Message
        {
            get => message;
            set
            {
                message = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Stores the adjacent neighbors of the cell.
        /// </summary>
        public ICollection<CellViewModel> Neighbors { get; set; }

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public CellViewModel()
        {
            Mark = new RelayCommand(() => MarkExecute(), () => true);
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="column"></param>
        /// <param name="occupied"></param>
        public CellViewModel(uint line, uint column, bool occupied) : this()
        {
            Line = line;
            Column = column;
            HasMine = occupied;
        }

        #region Commands

        /// <summary>
        /// Command to select a cell.
        /// </summary>
        public ICommand Mark { get; private set; }
        /// <summary>
        /// Logic for the cell selection.
        /// </summary>
        private void MarkExecute()
        {
            if(!Clicked && !Visited)
            {
                Clicked = true;
            }
        }

        #endregion
    }
}
