using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp3
{

    public class Board
    {
        private Ship[,] _cells;

        public Board()
        {
            _cells = new Ship[10, 10];
        }

        public void Initialize()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    _cells[i, j] = null;
                }
            }
        }
        public Button[,] CreateGameField()
        {
           return new Button[10, 10];
        }
        public void SetCells(Button[,] buttons)
        {
            _cells = new Ship[10, 10];

            for (int x = 0; x < buttons.GetLength(0); x++)
            {
                for (int y = 0; y < buttons.GetLength(1); y++)
                {
                    if (buttons[x, y].Tag != null)
                    {
                        _cells[x, y] = (Ship)buttons[x, y].Tag;
                    }
                    else
                    {
                        _cells[x, y] = null;
                    }
                }
            }
        }

        public Ship[,] GetBoard()
        {
            return _cells;
        }

        public bool IsHit(ShipPoint point)
        {
            int x = point.GetX();
            int y = point.GetY();

            if (_cells[x, y] != null)
            {
                return true;
            }

            return false;
        }
    }

}
