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
        private List<ShipPoint> _availableShots;
        private ShipPoint _lastHit;
        private bool _hasLastHit;

        public Computer(string name) : base(name)
        {
            _availableShots = new List<ShipPoint>();
            // Заполняем список доступных выстрелов всеми точками поля
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    _availableShots.Add(new ShipPoint(x, y));
                }
            }
            _hasLastHit = false;
        }

        public ShipPoint MakeShot()
        {
            ShipPoint shot;

            if (_hasLastHit)
            {
                // Если было попадание, стреляем вокруг этой точки
                shot = GetShotAroundPoint(_lastHit);
            }
            else
            {
                // Иначе случайный выстрел
                int index = _random.Next(_availableShots.Count);
                shot = _availableShots[index];
            }

            // Удаляем использованную точку из доступных
            _availableShots.RemoveAll(p =>
                p.GetX() == shot.GetX() &&
                p.GetY() == shot.GetY());

            return shot;
        }

        private ShipPoint GetShotAroundPoint(ShipPoint point)
        {
            // Проверяем все соседние клетки
            List<ShipPoint> possibleShots = new List<ShipPoint>
        {
            new ShipPoint(point.GetX() + 1, point.GetY()),
            new ShipPoint(point.GetX() - 1, point.GetY()),
            new ShipPoint(point.GetX(), point.GetY() + 1),
            new ShipPoint(point.GetX(), point.GetY() - 1)
        };

            // Фильтруем только доступные выстрелы
            var validShots = possibleShots.Where(p =>
                p.GetX() >= 0 && p.GetX() < 10 &&
                p.GetY() >= 0 && p.GetY() < 10 &&
                _availableShots.Any(a =>
                    a.GetX() == p.GetX() &&
                    a.GetY() == p.GetY())
            ).ToList();

            if (validShots.Count > 0)
            {
                // Выбираем случайную точку из доступных
                return validShots[_random.Next(validShots.Count)];
            }

            // Если нет доступных точек вокруг, делаем случайный выстрел
            int index = _random.Next(_availableShots.Count);
            return _availableShots[index];
        }

        public void OnShipSunk(Ship ship)
        {
            List<ShipPoint> pointsToRemove = new List<ShipPoint>();
            List<ShipPoint> shipPoints = ship.GetAllPoints();

            foreach (ShipPoint point in shipPoints)
            {
                // Добавляем окружающие клетки
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int newX = point.GetX() + dx;
                        int newY = point.GetY() + dy;

                        if (newX >= 0 && newX < 10 && newY >= 0 && newY < 10)
                        {
                            pointsToRemove.Add(new ShipPoint(newX, newY));
                        }
                    }
                }
            }

            // Удаляем все эти точки из доступных выстрелов
            _availableShots.RemoveAll(shot =>
                pointsToRemove.Any(p => p.GetX() == shot.GetX() && p.GetY() == shot.GetY()));
        }

        // Метод для обновления состояния после выстрела
        public void UpdateLastShot(ShipPoint shot, bool isHit)
        {
            if (isHit)
            {
                _lastHit = shot;
                _hasLastHit = true;
            }
            else
            {
                _hasLastHit = false;
            }
        }
    }
}
