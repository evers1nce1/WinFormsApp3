using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinFormsApp3
{
    public class Player
    {
        private string _name;
        private List<(Ship Ship, List<ShipPoint> Points)> _ships;

        public Player(string name)
        {
            _name = name;
            _ships = new List<(Ship Ship, List<ShipPoint> Points)>();
        }

        public void AddShip(Ship ship, List<ShipPoint> Points)
        {
            _ships.Add((ship, Points));
        }

        public List<(Ship Ship, List<ShipPoint> Points)> GetShips()
        {
            return _ships;
        }

        public string GetName()
        {
            return _name;
        }

    public bool HasShipAt(int x, int y)
    {
        foreach (var (_, buttons) in _ships)
        {
            if (buttons.Any(b =>  
                b.GetX() == x && 
                b.GetY() == y))
            {
                return true;
            }
        }
        return false;
    }

        public (Ship Ship, ShipPoint Point)? GetShipAt(int x, int y)
        {
            foreach (var (ship, points) in _ships)
            {
                var point = points.FirstOrDefault(b =>
                {
                    return b.GetX() == x && b.GetY() == y;
                });

                if (point != null)
                {
                    return (ship, point);
                }
            }
            return null;
        }
    }
}