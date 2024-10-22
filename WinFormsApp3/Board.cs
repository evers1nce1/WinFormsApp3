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
        private bool[,] _hits;

        public Board()
        {
            _cells = new Ship[10, 10];
            _hits = new bool[10, 10];
        }

        public bool IsHit(ShipPoint point)
        {
            int x = point.GetX();
            int y = point.GetY();

            if (_hits[x, y])
            {
                return false; // Уже стреляли в эту клетку
            }

            _hits[x, y] = true; // Отмечаем, что по этой клетке стреляли

            if (_cells[x, y] != null)
            {
                _cells[x, y].Hit(); // Отмечаем попадание в корабль
                return true;
            }

            return false;
        }

        public void AddShip(Ship ship)
        {
            int x = ship.GetLocation().GetX();
            int y = ship.GetLocation().GetY();
            int size = ship.GetSize();

            for (int i = 0; i < size; i++)
            {
                if (ship.GetRotation() == ShipDirection.Right)
                {
                    _cells[x + i, y] = ship;
                }
                else
                {
                    _cells[x, y + i] = ship;
                }
            }
        }
        public Button[,] CreateGameField()
        {
            return new Button[10, 10];
        }

        public bool AllShipsSunk()
        {
            foreach (Ship ship in _cells)
            {
                if (ship != null && !ship.IsSunk())
                {
                    return false;
                }
            }
            return true;
        }
    }

}
