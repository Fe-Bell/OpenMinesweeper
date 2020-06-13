using GalaSoft.MvvmLight.Ioc;
using OpenMinesweeper.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenMinesweeper.NET.ViewModel
{
    public class ViewModelLocator
    {
        public static MinesweeperCore MinesweeperCore => SimpleIoc.Default.GetInstance<MinesweeperCore>();
        public static MainViewModel MainVM => SimpleIoc.Default.GetInstance<MainViewModel>();
        public static LoadGameStateViewModel LoadGameStateVM => SimpleIoc.Default.GetInstance<LoadGameStateViewModel>();
        public static NewGameViewModel NewGameVM => SimpleIoc.Default.GetInstance<NewGameViewModel>();

        static ViewModelLocator()
        {
            SimpleIoc.Default.Register<MinesweeperCore>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoadGameStateViewModel>();
            SimpleIoc.Default.Register<NewGameViewModel>();
        }
    }
}
