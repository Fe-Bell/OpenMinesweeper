using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OpenMinesweeper.NET.ViewModel
{
    public class AboutViewModel : ViewModelBase
    {
        public string ProductDetails
        {
            get => string.Format("OpenMinesweeper v{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }

        public AboutViewModel()
        {

        }
    }
}
