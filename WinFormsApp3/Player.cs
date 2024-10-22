using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
+
namespace WinFormsApp3
{
    public class Player
    {
        private List<Ship> _ships;

        public Player(string name)
        {
            _ships = new List<Ship>();
        }

        public void AddShip(Ship ship)
        {
            _ships.Add(ship);
        }

        public List<Ship> GetShips()
        {
            return _ships;
        }

    }
}
