using OpenMinesweeper.NET.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using OpenMinesweeper.Core;

namespace OpenMinesweeper.NET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        GameStatesWindow gameStatesWindow = null;
        NewGameWindow newGameWindow = null;

        public MainWindow()
        {
            InitializeComponent();

            ViewModelLocator.MainVM.OnGameOver += MainWindow_OnGameOver;
            ViewModelLocator.MainVM.OnGameWon += MainWindow_OnGameWon;
            ViewModelLocator.LoadGameStateVM.OnGameLoad += LoadGameStateVM_OnGameLoad;
            ViewModelLocator.NewGameVM.OnNewGame += NewGameVM_OnNewGame;
        }

        private void LoadGameStateVM_OnGameLoad(object sender, EventArgs e)
        {
            gameStatesWindow.Close();
            gameStatesWindow = null;
        }

        private void NewGameVM_OnNewGame(object sender, EventArgs e)
        {
            newGameWindow.Close();
            newGameWindow = null;
        }

        private void MainWindow_OnGameWon(object sender, EventArgs e)
        {
            var rc = MessageBox.Show("CONGRATULATIONS, YOU WON!\nContinue?", "OpenMinesweeper", MessageBoxButton.YesNo);
            if (rc == MessageBoxResult.Yes)
            {
                newGameWindow = new NewGameWindow();
                newGameWindow.ShowDialog();
            }
            else
            {
                ExitGame();
            }
        }

        private void MainWindow_OnGameOver(object sender, EventArgs e)
        {
            var rc = MessageBox.Show("GAME OVER!\nContinue?", "OpenMinesweeper", MessageBoxButton.YesNo);
            if(rc == MessageBoxResult.Yes)
            {
                newGameWindow = new NewGameWindow();
                newGameWindow.ShowDialog();
            }
            else
            {
                ExitGame();
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ExitGame();
        }

        private void ExitGame()
        {
            Application.Current.Shutdown();
        }

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

        private void NewGameMenuItem_Click(object sender, RoutedEventArgs e)
        {
            newGameWindow = new NewGameWindow();
            newGameWindow.ShowDialog();
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }
    }
}
