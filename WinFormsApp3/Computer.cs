using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WinFormsApp3
{
    public class Computer : Player
    {
        private Random _random = new Random();
        private List<ShipPoint> _availableShots = new List<ShipPoint>();
        private List<ShipPoint> _hits = new List<ShipPoint>();
        private ShipPoint _lastHit;

        public Computer(string name) : base(name)
        {
            // Инициализация доступных выстрелов
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    _availableShots.Add(new ShipPoint(x, y));
                }
            }
        }

        public ShipPoint MakeShot()
        {
            ShipPoint shot;

            if (_hits.Count > 0)
            {
                // Если есть попадание, пытаемся угадать направление корабля
                shot = PredictNextShot();
            }
            else
            {
                // Если нет попаданий, выбираем случайную клетку
                int index = _random.Next(_availableShots.Count);
                shot = _availableShots[index];
            }

            _availableShots.Remove(shot);
            return shot;
        }

        public void RegisterShotResult(ShipPoint shot, bool isHit)
        {
            if (isHit)
            {
                _hits.Add(shot);
                _lastHit = shot;
            }
        }

        public void RegisterShipSunk(List<ShipPoint> shipPoints)
        {
            foreach (var point in shipPoints)
            {
                _hits.Remove(point);
            }
            _lastHit = null;
        }

        private ShipPoint PredictNextShot()
        {
            List<ShipPoint> possibleShots = new List<ShipPoint>();

            if (_hits.Count == 1)
            {
                // Проверяем все соседние клетки
                AddPossibleShot(possibleShots, _lastHit.GetX() + 1, _lastHit.GetY());
                AddPossibleShot(possibleShots, _lastHit.GetX() - 1, _lastHit.GetY());
                AddPossibleShot(possibleShots, _lastHit.GetX(), _lastHit.GetY() + 1);
                AddPossibleShot(possibleShots, _lastHit.GetX(), _lastHit.GetY() - 1);
            }
            else
            {
                // Если есть больше одного попадания, стреляем в том же направлении
                int dx = _hits[1].GetX() - _hits[0].GetX();
                int dy = _hits[1].GetY() - _hits[0].GetY();

                AddPossibleShot(possibleShots, _lastHit.GetX() + dx, _lastHit.GetY() + dy);
                AddPossibleShot(possibleShots, _hits[0].GetX() - dx, _hits[0].GetY() - dy);
            }

            if (possibleShots.Count > 0)
            {
                return possibleShots[_random.Next(possibleShots.Count)];
            }
            else
            {
                // Если нет возможных выстрелов, выбираем случайную клетку
                int index = _random.Next(_availableShots.Count);
                return _availableShots[index];
            }
        }

        private void AddPossibleShot(List<ShipPoint> possibleShots, int x, int y)
        {
            ShipPoint shot = new ShipPoint(x, y);
            if (_availableShots.Contains(shot))
            {
                possibleShots.Add(shot);
            }
        }
    }
}
