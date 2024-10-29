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

            _playerField = new List<Button>();
            _computerField = new List<Button>();

            InitializeReplay();
        }

        private void InitializeReplay()
        {
            CreatePlayerGrid();
            CreateComputerGrid();

            InitializeShips();
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
