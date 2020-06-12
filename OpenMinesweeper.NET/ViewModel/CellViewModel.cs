using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace OpenMinesweeper.NET.ViewModel
{
    public class CellViewModel : ObservableObject
    {
        private uint line = 0;
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
        public string Message
        {
            get => message;
            set
            {
                message = value;
                RaisePropertyChanged();
            }
        }

        public CellViewModel()
        {
            Mark = new RelayCommand(() => MarkExecute(), () => true);
        }
        public CellViewModel(uint line, uint column, bool occupied) : this()
        {
            Line = line;
            Column = column;
            HasMine = occupied;
        }

        #region Commands

        public ICommand Mark { get; private set; }
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
