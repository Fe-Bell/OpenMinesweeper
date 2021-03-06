﻿using GalaSoft.MvvmLight.Ioc;
using OpenMinesweeper.Core;
using System;
using System.IO;

namespace OpenMinesweeper.NET.ViewModel
{
    /// <summary>
    /// Static class that provides static access to all viewmodels in the game.
    /// </summary>
    public class ViewModelLocator
    {
        #region Properties

        /// <summary>
        /// Returns an instance of the MinesweeperCore.
        /// </summary>
        public static MinesweeperCore MinesweeperCore => SimpleIoc.Default.GetInstance<MinesweeperCore>();
        /// <summary>
        /// Returns an instance of the MainViewModel.
        /// </summary>
        public static MainViewModel MainVM => SimpleIoc.Default.GetInstance<MainViewModel>();
        /// <summary>
        /// Returns an instance of the LoadGameStateViewModel.
        /// </summary>
        public static LoadGameStateViewModel LoadGameStateVM => SimpleIoc.Default.GetInstance<LoadGameStateViewModel>();
        /// <summary>
        /// Returns an instance of the NewGameViewModel.
        /// </summary>
        public static NewGameViewModel NewGameVM => SimpleIoc.Default.GetInstance<NewGameViewModel>();
        /// <summary>
        /// Returns an instance of the AboutViewModel.
        /// </summary>
        public static AboutViewModel AboutVM => SimpleIoc.Default.GetInstance<AboutViewModel>();

        #endregion

        /// <summary>
        /// Static constructor.
        /// </summary>
        static ViewModelLocator()
        {
            string config_path = Path.Combine(Environment.CurrentDirectory, SoftwareConfigLoader.DEFAULTCFGFILENAME);
            SimpleIoc.Default.Register(() => new MinesweeperCore(config_path));
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoadGameStateViewModel>();
            SimpleIoc.Default.Register<NewGameViewModel>();
            SimpleIoc.Default.Register<AboutViewModel>();
        }
    }
}
