using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinFormsApp3
{
    public class Player
    {
        private string _name;
        private List<ShipPoint> _hitPoints;
        private List<Ship> _ships;

        public Player(string name)
        {
            _name = name;
            _ships = new List<Ship>();
            _hitPoints = new List<ShipPoint>();
        }

        public void AddShip(Ship ship, List<ShipPoint> Points)
        {
            _ships.Add(ship);
        }

        public List<Ship> GetShips()
        {
            return _ships;
        }

        public string GetName()
        {
            return _name;
        }
        public bool IsSunk(ShipPoint point)
        {
            Ship ship = FindShipAt(point);
            if (ship != null)
                return ship.IsSunk();
            return false;
        }


        public void RegisterHit(ShipPoint point)
        {
            Ship ship = FindShipAt(point);
            if (ship != null)
            {
                MessageBox.Show(ship.GetLocation().GetX().ToString());
                ship.Hit();
            }
            else
                MessageBox.Show("-");
        }

        public bool HasShipAt(int x, int y)
    {
        foreach (Ship ship in _ships)
        {
            if (ship.GetAllPoints().Any(b =>  
                b.GetX() == x && 
                b.GetY() == y))
            {
                return true;
            }
        }
        return false;
    }
        public Ship FindShipAt(ShipPoint Point)
        {
            foreach (Ship ship in _ships)
            {
                List<ShipPoint> points = ship.GetAllPoints();
                foreach (ShipPoint point in points)
                {
                    if (point.GetX() == Point.GetX() && point.GetY() == Point.GetY())
                    {
                        return ship;
                    }
                }
            }
            return null;
        }
    }
}