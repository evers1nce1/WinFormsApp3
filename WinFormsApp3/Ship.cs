using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp3
{
    using System;
    public enum ShipDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    public class Ship
    {
        private ShipPoint _location;
        private int _size;
        private ShipDirection _rotation; //left, right, back, forward
        public Ship(ShipPoint location, int size, ShipDirection rotation)
        {
            _location = location;
            _size = size;
            _rotation = rotation;
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
    }

}
