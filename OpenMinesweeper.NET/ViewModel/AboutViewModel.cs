using GalaSoft.MvvmLight;
using System.Reflection;

namespace OpenMinesweeper.NET.ViewModel
{
    /// <summary>
    /// Provides data for program information.
    /// </summary>
    public class AboutViewModel : ViewModelBase
    {
        /// <summary>
        /// Returns name and version information for the about window.
        /// </summary>
        public string ProductDetails
        {
            get => string.Format("OpenMinesweeper v{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }

        /// <summary>
        /// Creates a new instance of AboutViewModel.
        /// </summary>
        public AboutViewModel()
        {

        }
    }
}
