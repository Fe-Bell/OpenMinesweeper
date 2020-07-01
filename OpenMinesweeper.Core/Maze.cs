using OpenMinesweeper.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenMinesweeper.Core
{
    public class Maze : IGrid
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public List<List<List<bool>>> Cells { get; set; }

        public Maze()
        {
            Width = 0;
            Height = 0;
            Cells = new List<List<List<bool>>>();
        }
    }
}
