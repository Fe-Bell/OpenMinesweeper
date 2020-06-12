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

namespace OpenMinesweeper.NET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            (DataContext as MainViewModel).OnGameOver += MainWindow_OnGameOver;
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
                Application.Current.Shutdown();
            }
        }
    }
}
