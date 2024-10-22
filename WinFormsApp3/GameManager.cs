using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp3
{
    public class GameManager
    {
        private Player _player;
        private Computer _computer;
        private bool _isPlayerTurn;
        private Panel _playerPanel;
        private Panel _computerPanel;
        private Button _gameStartButton;
        private Random _random;
        private List<ShipPoint> _availableShots = new List<ShipPoint>();
        private List<ShipPoint> _hits = new List<ShipPoint>();
        private List<Button> _playerField = new List<Button>();
        private List<Button> _computerField = new List<Button>();
        private ShipPoint _lastHit;

        public GameManager(Player player, Computer computer, Panel playerPanel, Panel computerPanel, Button gameStartButton)
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
                    _playerField.Add(button);
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
                    _computerField.Add(button);
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

        public Move ComputerMove()
        {
            ShipPoint point = _computer.MakeShot();
            bool isHit = _player.HasShipAt(point.GetX(), point.GetY());
            bool isSunk = _player.IsSunk(point);
            Move move = new Move(point, isHit, isSunk);
            return move;
        }

        private void ComputerButton_Click(object sender, EventArgs e)
        {
            if (!_isPlayerTurn) return;

            Button clickedButton = (Button)sender;
            ShipPoint location = new ShipPoint(((Point)clickedButton.Tag).X, ((Point)clickedButton.Tag).Y);
            bool isHit = _computer.HasShipAt(location.GetX(), location.GetY());
            Ship currentShip = _computer.FindShipAt(location);
            bool isSunk = _computer.IsSunk(location);
            if (isSunk)
                MessageBox.Show("1");
            Move move = new Move(location, isHit, isSunk);
            if (isHit)
            {
                // Попадание
                _player.RegisterHit(location);
                clickedButton.BackColor = Color.Red;
                if (currentShip.IsSunk())
                {
                    MessageBox.Show("1");
                    OnShipSunk(currentShip);
                }
                CheckGameEnd();
            }
            else
            {
                // Промах
                clickedButton.BackColor = Color.Gray;
                _isPlayerTurn = false;
                ComputerTurn();
            }
        }
        public void OnShipSunk(Ship ship)
        {
            // Получаем все точки корабля
            List<ShipPoint> shipPoints = ship.GetAllPoints();

            // Помечаем все клетки корабля крестиком
            foreach (ShipPoint point in shipPoints)
            {
                // Находим соответствующую кнопку
                Button button = _computerField.First(b => ((Point)b.Tag).X == point.GetX() && ((Point)b.Tag).Y == point.GetY());

                // Меняем текст кнопки на "X"
                button.Text = "X";

                // Можно также изменить цвет для визуального выделения
                button.BackColor = Color.Red;
            }

        }
        private void ComputerTurn()
        {
            if (_isPlayerTurn) return;
            Move computerMove = ComputerMove();
            
            var buttonData = _playerField.FirstOrDefault(s => ((Point)s.Tag).X == computerMove.GetPoint().GetX() && ((Point)s.Tag).Y == computerMove.GetPoint().GetY());
            if (computerMove.IsHit())
            {
                // Попадание
                _computer.RegisterHit(computerMove.GetPoint());
                buttonData.BackColor = Color.Red;
                ComputerTurn();
                CheckGameEnd();
            }
            else
            {
                // Промах
                buttonData.BackColor = Color.Gray;
                _isPlayerTurn = true;
            }
        }

        private void CheckGameEnd()
        {
            if (CheckWinner() == 1)
                MessageBox.Show("Игрок победил");
            else if(CheckWinner() == 0)
                MessageBox.Show("Компьютер победил");
        }
        private int CheckWinner()
        {
            // Проверка конца игры
            // Если у одного из игроков не осталось кораблей, то игра окончена
            if (_player.GetShips().Count == 0)
            {
                return 0;
            }
            else if (_computer.GetShips().Count() == 0)
            {
                return 1;
            }
            return -1;
        }
    }
}
