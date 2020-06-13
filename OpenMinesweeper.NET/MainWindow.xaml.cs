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
    public partial class MainWindow : Window
    {
        GameStatesWindow gameStatesWindow = null;

        public MainWindow()
        {
            InitializeComponent();

            gameStatesWindow = new GameStatesWindow();

            (DataContext as MainViewModel).OnGameOver += MainWindow_OnGameOver;
            (DataContext as MainViewModel).OnGameWon += MainWindow_OnGameWon;

            ViewModelLocator.LoadGameStateVM.PropertyChanged += LoadGameStateVM_PropertyChanged;

            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            gameStatesWindow.Close();
        }

        private void LoadGameStateVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "LoadedGameState")
            {
                gameStatesWindow.Hide();
            }
        }

        private void MainWindow_OnGameWon(object sender, EventArgs e)
        {
            var rc = MessageBox.Show("GAME WON!\nContinue?", "OpenMinesweeper", MessageBoxButton.YesNo);
            if (rc == MessageBoxResult.Yes)
            {
                (DataContext as MainViewModel).NewGame.Execute(null);
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
                (DataContext as MainViewModel).NewGame.Execute(null);
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
                (gameStatesWindow.DataContext as LoadGameStateViewModel).UpdateGameStates(openFileDialog.FileName);
                gameStatesWindow.Show();
            }
        }
    }
}
