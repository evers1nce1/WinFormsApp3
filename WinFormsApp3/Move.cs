using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WinFormsApp3
{
    public class Move
    {
        [JsonInclude]
        private ShipPoint _point;
        [JsonInclude]
        private bool _isHit;
        [JsonInclude]
        private bool _isSunk;
        [JsonInclude]
        private bool _isPlayerMove;

        [JsonConstructor]
        public Move()
        {
        }

        public Move(ShipPoint point, bool isHit, bool isSunk, bool isPlayerMove)
        {
            _point = point;
            _isHit = isHit;
            _isSunk = isSunk;
            _isPlayerMove = isPlayerMove;
        }

        public ShipPoint GetPoint()
        {
            return _point;
        }

        public bool IsHit()
        {
            return _isHit;
        }
        public void SetShipSunk()
        {
            _isSunk = true;
        }
        public bool IsSunk()
        {
            return _isSunk;
        }
    }
}
