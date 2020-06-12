using System;

namespace OpenMinesweeper.Core
{
    public class MinesweeperCore
    {
        public MinesweeperCore()
        {

        }

        public GameGrid NewGame(uint power)
        {
            GameGrid gg = new GameGrid();
            gg.Load(power);

            return gg;
        }
    }
}
