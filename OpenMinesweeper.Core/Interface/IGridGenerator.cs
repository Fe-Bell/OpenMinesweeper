using System;
using System.Collections.Generic;
using System.Text;

namespace OpenMinesweeper.Core.Interface
{
    public interface IGridGenerator
    {
        T Generate<T>(int lineCount, int columnCount) where T : IGrid;
    }
}
