using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp3
{
    using System;
    using System.Text.Json.Serialization;

    public enum ShipDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    public class Ship
    {
        [JsonInclude]
        private ShipPoint _location;
        [JsonInclude]
        private int _size;
        [JsonInclude]
        private ShipDirection _rotation;
        [JsonInclude]
        private int _hits;

        [JsonConstructor]

        public Ship()
        {
        }

        public Ship(ShipPoint location, int size, ShipDirection rotation)
        {
            _location = location;
            _size = size;
            _rotation = rotation;
            _hits = 0;
        }
        public void SetRotation(ShipDirection rotation)
        {
            _rotation = rotation;
        }
        public ShipDirection GetRotation()
        {
            return _rotation;
        }
        public void SetLocation(ShipPoint location)
        {
            _location = location;
        }
        public ShipPoint GetLocation()
        {
            return _location;
        }
        public int GetSize()
        {
            return _size;
        }
        public void Hit()
        {
            _hits++;
        }
        public List<ShipPoint> GetAllPoints()
        {
            List<ShipPoint> points = new List<ShipPoint>();

            for (int i = 0; i < _size; i++)
            {
                if (_rotation == ShipDirection.Right)
                {
                    points.Add(new ShipPoint(_location.GetX() + i, _location.GetY()));
                }
                else // ShipDirection.Down
                {
                    points.Add(new ShipPoint(_location.GetX(), _location.GetY() + i));
                }
            }
            return points;
        }

        public bool IsSunk()
        {
            return _hits >= _size;
        }

    }

}
