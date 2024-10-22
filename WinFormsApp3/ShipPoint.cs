using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp3
{
    public class ShipPoint
    {
        private int _x;
        private int _y;

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
