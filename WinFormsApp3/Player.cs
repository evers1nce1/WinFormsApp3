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
        private int _sunkCount;
        public Player(string name)
        {
            _name = name;
            _ships = new List<Ship>();
            _hitPoints = new List<ShipPoint>();
            _sunkCount = 0;
        }
        public void RegisterHit(ShipPoint point)
        {
            Ship currentShip = FindShipAt(point);
            currentShip.Hit();
        }
        public void AddShip(Ship ship, List<ShipPoint> Points)
        {
            if (ship != null)
            {
                _ships.Add(ship);
            }
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
            else
                return false;
        }
        public void AddSunk()
        {
            _sunkCount++;
        }
        public int GetSunkCount()
        {
            return _sunkCount;
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
            Ship ret = _ships.FirstOrDefault(ship => ship.GetAllPoints().Any(p => p.GetX() == Point.GetX() && p.GetY() == Point.GetY()));

            return ret;
        }
    }
}