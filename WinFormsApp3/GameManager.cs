using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp3
{
    public class GameManager
    {
        protected Player _player;
        protected Computer _computer;
        private LeaderboardManager _leaderboard;
        private int _moveCount;
        private Stopwatch _gameTimer;
        private bool _isPlayerTurn;
        private Panel _playerPanel;
        private Panel _computerPanel;
        private Button _gameStartButton;
        private Random _random;
        private List<ShipPoint> _availableShots = new List<ShipPoint>();
        private List<ShipPoint> _hits = new List<ShipPoint>();
        protected List<Button> _playerField = new List<Button>();
        protected List<Button> _computerField = new List<Button>();
        private ShipPoint _lastHit;
        private bool _isGameEnd;
        private Logger _logger;
        public GameManager(Player player, Computer computer, Panel playerPanel, Panel computerPanel, Button gameStartButton)
        {
            _player = player;
            _computer = computer;
            _playerPanel = playerPanel;
            _computerPanel = computerPanel;
            _gameStartButton = gameStartButton;
            _isPlayerTurn = false;
            _isGameEnd = false;
            _logger = new Logger();
            _random = new Random();
            _leaderboard = new LeaderboardManager();
            _gameTimer = new Stopwatch();
            _moveCount = 0;
            InitializeGame();
        }

        private void InitializeGame()
        {
            CreatePlayerGrid();
            CreateComputerGrid();
            _logger.OnNewGame(_player.GetName());
            _gameStartButton.Click += StartGame;
        }

        protected void CreatePlayerGrid()
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

        protected void CreateComputerGrid()
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
                    /*
                    if (_computer.HasShipAt(x, y))
                    {
                        button.BackColor = Color.Blue;
                    }
                    */
                    _computerField.Add(button);
                    button.Click += PlayerTurn;
                    _computerPanel.Controls.Add(button);
                }
            }
        }

        private void StartGame(object sender, EventArgs e)
        {
            _gameStartButton.Enabled = false;
            _isPlayerTurn = true;
            _gameTimer.Restart();
        }

        private void PlayerTurn(object sender, EventArgs e)
        {
            if (!_isPlayerTurn) return;

            Button clickedButton = (Button)sender;
            _moveCount++;
            ShipPoint location = new ShipPoint(((Point)clickedButton.Tag).X, ((Point)clickedButton.Tag).Y);
            bool isHit = _computer.HasShipAt(location.GetX(), location.GetY());
            Ship currentShip = _computer.FindShipAt(location);
            Move move = new Move(location, isHit, false, true);
            if (_computer.HasShipAt(location.GetX(), location.GetY()))
            {
                // Попадание
                _computer.RegisterHit(location);
                clickedButton.BackColor = Color.Red;
                if (currentShip.IsSunk())
                {
                    move.SetShipSunk();
                    OnShipSunk(currentShip, _computer, _computerField);
                }
                _logger.LogMove(move);
                CheckGameEnd();
            }
            else
            {
                // Промах
                clickedButton.BackColor = Color.Gray;
                clickedButton.Enabled = false;
                _isPlayerTurn = false;
                _logger.LogMove(move);
                ComputerTurn();
            }

        }
        public void OnShipSunk(Ship ship, Player player, List<Button> field)
        {
            player.AddSunk();
            // Получаем все точки корабля
            List<ShipPoint> shipPoints = ship.GetAllPoints();

            // Отмечаем окружающие клетки
            PrintSurroundingCells(shipPoints, field);

            // Отмечаем точки самого корабля
            foreach (ShipPoint point in shipPoints)
            {
                Button shipButton = field.First(b =>
                    ((Point)b.Tag).X == point.GetX() &&
                    ((Point)b.Tag).Y == point.GetY());

                shipButton.Text = "X";
                shipButton.BackColor = Color.Red;
            }
        }
        private void ComputerTurn()
        {
            if (_isPlayerTurn || _isGameEnd) 
                return;
            
            ShipPoint shot = _computer.MakeShot();
            Ship currentShip = _player.FindShipAt(shot);
            bool isHit = _player.HasShipAt(shot.GetX(), shot.GetY());
            bool isSunk = false;
            Move move = new Move(shot, isHit, isSunk, false);
            _computer.UpdateLastShot(shot, isHit);
            var buttonData = _playerField.FirstOrDefault(
                s => ((Point)s.Tag).X == shot.GetX() &&
                ((Point)s.Tag).Y == shot.GetY());

            if (isHit)
            {
                _player.RegisterHit(shot);
                isSunk = _player.IsSunk(shot);
                if (isSunk)
                {
                    move.SetShipSunk();
                    _computer.OnShipSunk(currentShip);
                    OnShipSunk(currentShip, _player, _playerField);

                } 
                buttonData.BackColor = Color.Red;
                _logger.LogMove(move);
                if (!isSunk)
                {
                    ComputerTurn(); // Компьютер ходит снова
                }
                else
                {
                    _isPlayerTurn = true;
                }
                CheckGameEnd();
            }
            else
            {
                buttonData.BackColor = Color.Gray;
                _logger.LogMove(move);
                _isPlayerTurn = true;
            }
        }
        private void PrintSurroundingCells(List<ShipPoint> shipPoints, List<Button> field)
        {
            foreach (ShipPoint point in shipPoints)
            {
                // Проходим по окружающим клеткам (включая диагональные)
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int newX = point.GetX() + dx;
                        int newY = point.GetY() + dy;

                        // Проверяем, что точка находится в пределах поля
                        if (newX >= 0 && newX < 10 && newY >= 0 && newY < 10)
                        {
                            // Находим соответствующую кнопку
                            Button surroundingButton = field.First(b =>
                                ((Point)b.Tag).X == newX &&
                                ((Point)b.Tag).Y == newY);
                            surroundingButton.Enabled = false;
                            // Если это не точка самого корабля, красим в серый
                            if (!shipPoints.Any(p => p.GetX() == newX && p.GetY() == newY))
                            {
                                surroundingButton.BackColor = Color.Gray;
                            }
                        }
                    }
                }
            }
        }
        private void OnGameEnd()
        {
            foreach (Button button in _computerField)
            {
                button.Enabled = false;
            }
        }
        private void CheckGameEnd()
        {
            GameRecord record = _logger.GetGameRecord();
            record.SetPlayerShips(_player.GetShips());
            record.SetComputerShips(_computer.GetShips());
            int winner = CheckWinner();
            if (winner == 1)
            {
                _gameTimer.Stop();
                _logger.EndGame(_gameTimer.Elapsed, true);
                _leaderboard.AddRecord(record);
                MessageBox.Show($"Игрок победил!\nХодов: {_moveCount}\nВремя: {_gameTimer.Elapsed.TotalSeconds:F1} сек.");
                OnGameEnd();
            }
            else if (winner == 0)
            {
                _gameTimer.Stop();
                _logger.EndGame(_gameTimer.Elapsed, false);
                MessageBox.Show("Компьютер победил!");
                OnGameEnd();
            }
        }
        private int CheckWinner()
        {
            if (_player.GetSunkCount() == 10)
            {
                _isGameEnd = true;
                return 0;
            }
            else if (_computer.GetSunkCount() == 10)
            {
                _isGameEnd = true;
                return 1;
            }
            return -1;
        }
    }
}
