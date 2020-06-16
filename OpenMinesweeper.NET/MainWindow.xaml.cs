using OpenMinesweeper.NET.ViewModel;
using System;
using System.Windows;

namespace OpenMinesweeper.NET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Fields

        GameStatesWindow gameStatesWindow = null;
        NewGameWindow newGameWindow = null;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            ViewModelLocator.MainVM.OnGameOver += MainWindow_OnGameOver;
            ViewModelLocator.MainVM.OnGameWon += MainWindow_OnGameWon;
            ViewModelLocator.LoadGameStateVM.OnGameLoad += LoadGameStateVM_OnGameLoad;
            ViewModelLocator.NewGameVM.OnNewGame += NewGameVM_OnNewGame;
        }

        #region Methods

        /// <summary>
        /// Kills the application.
        /// </summary>
        private void ExitGame()
        {
            Application.Current.Shutdown();
        }
        /// <summary>
        /// Event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadGameStateVM_OnGameLoad(object sender, EventArgs e)
        {
            gameStatesWindow.Close();
            gameStatesWindow = null;
        }
        /// <summary>
        /// Event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGameVM_OnNewGame(object sender, EventArgs e)
        {
            newGameWindow.Close();
            newGameWindow = null;
        }
        /// <summary>
        /// Event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnGameWon(object sender, EventArgs e)
        {
            var rc = MessageBox.Show(ViewModelLocator.MainVM.LanguageContent["GameWonMsgStr"], "OpenMinesweeper", MessageBoxButton.YesNo);
            if (rc == MessageBoxResult.Yes)
            {
                newGameWindow = new NewGameWindow();
                newGameWindow.ShowDialog();
            }
        }
        /// <summary>
        /// Event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnGameOver(object sender, EventArgs e)
        {
            var rc = MessageBox.Show(ViewModelLocator.MainVM.LanguageContent["GameOverMsgStr"], "OpenMinesweeper", MessageBoxButton.YesNo);
            if(rc == MessageBoxResult.Yes)
            {
                newGameWindow = new NewGameWindow();
                newGameWindow.ShowDialog();
            }
        }
        /// <summary>
        /// Event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ExitGame();
        }
        /// <summary>
        /// Event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Database Files (*.db)|*.db|All files (*.*)|*.*";
            saveFileDialog.InitialDirectory = Environment.CurrentDirectory;

            if (saveFileDialog.ShowDialog() == true)
            {
                object[] parameter = { System.IO.Path.GetDirectoryName(saveFileDialog.FileName), saveFileDialog.SafeFileName };
                (DataContext as MainViewModel).SaveGame.Execute(parameter);
            }
        }
        /// <summary>
        /// Event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Database Files (*.db)|*.db|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;

            if (openFileDialog.ShowDialog() == true)
            {
                ViewModelLocator.LoadGameStateVM.UpdateGameStates(openFileDialog.FileName);

                gameStatesWindow = new GameStatesWindow();
                gameStatesWindow.ShowDialog();
            }
        }
        /// <summary>
        /// Event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGameMenuItem_Click(object sender, RoutedEventArgs e)
        {
            newGameWindow = new NewGameWindow();
            newGameWindow.ShowDialog();
        }
        /// <summary>
        /// Event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

        #endregion
    }
}
