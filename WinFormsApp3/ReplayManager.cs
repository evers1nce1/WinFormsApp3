using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp3
{
    public class ReplayManager
    {
        private GameRecord _gameRecord;
        private int _currentMoveIndex;
        private Panel _playerPanel;
        private Panel _computerPanel;
        private Label _moveCountLabel;
        private Button _nextMoveButton;
        private Button _prevMoveButton;
        private List<Button> _playerField;
        private List<Button> _computerField;
        private List<Move> _moves;

        public ReplayManager(GameRecord gameRecord, Panel playerPanel, Panel computerPanel,
            Label moveCountLabel, Button nextMoveButton, Button prevMoveButton)
        {
            _gameRecord = gameRecord;
            _playerPanel = playerPanel;
            _computerPanel = computerPanel;
            _moveCountLabel = moveCountLabel;
            _nextMoveButton = nextMoveButton;
            _prevMoveButton = prevMoveButton;
            _currentMoveIndex = -1;
            _moves = gameRecord.GetMovesList();
            _playerField = new List<Button>();
            _computerField = new List<Button>();

            InitializeReplay();
        }

        private void InitializeReplay()
        {
            CreatePlayerGrid();
            CreateComputerGrid();
            _nextMoveButton.Click += (sender, e) => NextMove();
            _prevMoveButton.Click += (sender, e) => PreviousMove();
            InitializeShips();
            UpdateMoveCountLabel();
        }
        public void NextMove()
        {
            if (_currentMoveIndex < _moves.Count - 1)
            {
                _currentMoveIndex++;
                ApplyMove(_moves[_currentMoveIndex]);
                UpdateMoveCountLabel();
            }
        }

        public void PreviousMove()
        {
            if (_currentMoveIndex > -1)
            {
                UndoMove(_moves[_currentMoveIndex]);
                _currentMoveIndex--;
                UpdateMoveCountLabel();
            }
        }
        private void ApplyMove(Move move)
        {
            Button button = FindButtonByCoordinates(move.GetPoint().GetX(), move.GetPoint().GetY(),
                move.IsPlayerMove() ? _computerField : _playerField);

            if (move.IsHit())
            {
                button.BackColor = Color.Red;
                if (move.IsSunk())
                {
                    // Отметьте потопленный корабль
                    MarkSunkShip(move.GetPoint(), move.IsPlayerMove() ? _computerField : _playerField);
                }
            }
            else
            {
                button.BackColor = Color.Gray;
            }

        }
        private void UpdateMoveCountLabel()
        {
            _moveCountLabel.Text = $"Ход {_currentMoveIndex + 1} / {_moves.Count}";
        }

        private void UndoMove(Move move)
        {
            Button button = FindButtonByCoordinates(move.GetPoint().GetX(), move.GetPoint().GetY(),
                move.IsPlayerMove() ? _computerField : _playerField);

            if (!move.IsHit())
            {
                button.BackColor = Color.White;
            }
            else
            {
                if (move.IsSunk())
                {
                    UnmarkSunkShip(move.GetPoint(), move.IsPlayerMove() ? _computerField : _playerField);
                }
                else
                {
                    button.BackColor = Color.Blue;
                }
            }
        }
        private void MarkSunkShip(ShipPoint point, List<Button> field)
        {
            Ship sunkShip = FindShipByPoint(point, field == _playerField ? _gameRecord.GetPlayerShips() : _gameRecord.GetComputerShips());
            if (sunkShip != null)
            {
                foreach (var shipPoint in sunkShip.GetAllPoints())
                {
                    Button button = FindButtonByCoordinates(shipPoint.GetX(), shipPoint.GetY(), field);
                    button.Text = "X";
                }
            }
        }
        private void ResetAllButtons()
        {
            foreach (var button in _playerField.Concat(_computerField))
            {
                button.BackColor = Color.White;
                button.Text = "";
            }
        }

        private void UnmarkSunkShip(ShipPoint point, List<Button> field)
        {
            Ship sunkShip = FindShipByPoint(point, field == _playerField ? _gameRecord.GetPlayerShips() : _gameRecord.GetComputerShips());
            FindButtonByCoordinates(point.GetX(), point.GetY(), field).BackColor = Color.Blue;
            if (sunkShip != null)
            {
                foreach (var shipPoint in sunkShip.GetAllPoints())
                {
                    Button button = FindButtonByCoordinates(shipPoint.GetX(), shipPoint.GetY(), field);
                    button.Text = "";
                }
            }
        }

        private Ship FindShipByPoint(ShipPoint point, List<Ship> ships)
        {
            return ships.FirstOrDefault(ship => ship.GetAllPoints().Any(p => p.GetX() == point.GetX() && p.GetY() == point.GetY()));
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
                        Tag = new Point(x, y),
                        BackColor = Color.White
                    };
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
                        Tag = new Point(x, y),
                        BackColor = Color.White
                    };
                    _computerField.Add(button);
                    _computerPanel.Controls.Add(button);
                }
            }
        }

        private void InitializeShips()
        {
            foreach (Ship ship in _gameRecord.GetPlayerShips())
            {
                PlaceShipOnGrid(ship, _playerField);
            }

            foreach (Ship ship in _gameRecord.GetComputerShips())
            {
                PlaceShipOnGrid(ship, _computerField);
            }
        }

        private void PlaceShipOnGrid(Ship ship, List<Button> field)
        {
            foreach (ShipPoint point in ship.GetAllPoints())
            {
                Button button = FindButtonByCoordinates(point.GetX(), point.GetY(), field);
                if (button != null)
                {
                    button.BackColor = Color.Blue;
                }
            }
        }

        private Button FindButtonByCoordinates(int x, int y, List<Button> field)
        {
            foreach (Button button in field)
            {
                if ((Point)button.Tag == new Point(x, y))
                {
                    return button;
                }
            }
            return null;
        }


    }
}
