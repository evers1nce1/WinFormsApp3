using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp3
{
    public class Move
    {
        private ShipPoint _point;
        private bool _isHit;
        private bool _isSunk;

        public Move(ShipPoint point, bool isHit, bool isSunk)
        {
            _point = point;
            _isHit = isHit;
            _isSunk = isSunk;
        }

        public ShipPoint GetPoint()
        {
            return _point;
        }

        public bool IsHit()
        {
            return _isHit;
        }

        public bool IsSunk()
        {
            return _isSunk;
        }
    }
}
