using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp3
{
    public class GameManager
    {
        private Player _player;
        private Player _computer;
        private bool _isPlayerTurn;
        private Panel _playerPanel;
        private Panel _computerPanel;
        private Button _gameStartButton;
        private Random _random;
        private List<ShipPoint> _availableShots = new List<ShipPoint>();
        private List<ShipPoint> _hits = new List<ShipPoint>();
        private ShipPoint _lastHit;

        public GameManager(Player player, Player computer, Panel playerPanel, Panel computerPanel, Button gameStartButton)
        {
            _player = player;
            _computer = computer;
            _playerPanel = playerPanel;
            _computerPanel = computerPanel;
            _gameStartButton = gameStartButton;
            _isPlayerTurn = false;
            _random = new Random();
            InitializeGame();
        }

        private void InitializeGame()
        {
            CreatePlayerGrid();
            CreateComputerGrid();
            _gameStartButton.Click += StartGame;
        }

        private void CreatePlayerGrid()
        {
            _playerPanel.Controls.Clear();
            int buttonSize = Math.Min(_playerPanel.Width / 10, _playerPanel.Height / 10);

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    Button button = new Button
                    {
                        Size = new Size(buttonSize, buttonSize),
                        Location = new Point(x * buttonSize, y * buttonSize),
                        Tag = new Point(x, y)
                    };
                    button.Enabled = false;
                    if (_player.HasShipAt(x, y))
                    {
                        button.BackColor = Color.Blue;
                    }

                    _playerPanel.Controls.Add(button);
                }
            }
        }

        private void CreateComputerGrid()
        {
            _computerPanel.Controls.Clear();
            int buttonSize = Math.Min(_computerPanel.Width / 10, _computerPanel.Height / 10);

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    Button button = new Button
                    {
                        Size = new Size(buttonSize, buttonSize),
                        Location = new Point(x * buttonSize, y * buttonSize),
                        Tag = new Point(x, y)
                    };
                    if (_computer.HasShipAt(x, y))
                    {
                        button.BackColor = Color.Blue;
                    }

                    button.Click += ComputerButton_Click;
                    _computerPanel.Controls.Add(button);
                }
            }
        }

        private void StartGame(object sender, EventArgs e)
        {
            _gameStartButton.Enabled = false;
            _isPlayerTurn = true;
            // Здесь можно добавить дополнительную логику начала игры
        }

        private void ComputerButton_Click(object sender, EventArgs e)
        {
            if (!_isPlayerTurn) return;

            Button clickedButton = (Button)sender;
            Point location = (Point)clickedButton.Tag;

            if (_computer.HasShipAt(location.X, location.Y))
            {
                // Попадание
                clickedButton.BackColor = Color.Red;
                var shipInfo = _computer.GetShipAt(location.X, location.Y);
                if (shipInfo.HasValue)
                {
                    // Можно добавить логику уничтожения корабля
                }
            }
            else
            {
                // Промах
                clickedButton.BackColor = Color.Gray;
                _isPlayerTurn = false;
                ComputerTurn();
            }

            CheckGameEnd();
        }

        private void ComputerTurn()
        {
            // Пример простой реализации:
            Random random = new Random();
            int x, y;
            do
            {
                x = random.Next(10);
                y = random.Next(10);
            } while (_player.GetShipAt(x, y) != null);

            var button = _playerPanel.Controls.Cast<Button>().FirstOrDefault(b =>
                b.Tag is Point p && p.X == x && p.Y == y);
            if (button != null)
            {
                button.PerformClick();
            }
        }

        private void CheckGameEnd()
        {
            // Проверка конца игры
            // Если у одного из игроков не осталось кораблей, то игра окончена
            if (_player.GetShips().All(ship => ship.Ship.GetSize() == 0))
            {
                // Компьютер выиграл
            }
            else if (_computer.GetShips().All(ship => ship.Ship.GetSize() == 0))
            {
                // Игрок выиграл
            }
        }
    }
}
