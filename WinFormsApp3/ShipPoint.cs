using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WinFormsApp3
{
    public class ShipPoint
    {
        [JsonInclude]
        private int _x;
        [JsonInclude]
        private int _y;
        [JsonConstructor]
        public ShipPoint()
        {
        }

        public ShipPoint(int x, int y)
        {
            _x = x;
            _y = y;
        }
        public int GetX()
        {
            return _x;
        }
        public int GetY()
        {
            return _y;
        }
    }

}
