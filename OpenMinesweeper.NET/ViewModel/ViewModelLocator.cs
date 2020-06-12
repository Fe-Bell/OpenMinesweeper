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

        static ViewModelLocator()
        {
            SimpleIoc.Default.Register<MinesweeperCore>();
            SimpleIoc.Default.Register<MainViewModel>();
        }
    }
}
