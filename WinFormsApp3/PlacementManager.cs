using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp3
{
    public class PlacementManager
    {
        private bool[,] _occupiedCells;
        private Random _random;
        private int _shipCount1 = 4;
        private int _shipCount2 = 3;
        private int _shipCount3 = 2;
        private int _shipCount4 = 1;
        private Ship _selectedShip;
        private Button[,] _buttons;
        private Board _board;
        private string _username;
        private Dictionary<Point, Color> _previousButtonColors = new Dictionary<Point, Color>();
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private RadioButton radioButton4;
        private Label labelCount1;
        private Label labelCount2;
        private Label labelCount3;
        private Label labelCount4;
        private Panel panel1;
        private Button playButton;
        private Player _player;
        private PlacementForm form;
        private Computer _computer;
        private MenuForm _menuForm;
        public PlacementManager(MenuForm menuForm, PlacementForm form, string username, RadioButton radioButton1, RadioButton radioButton2, RadioButton radioButton3, RadioButton radioButton4, Label labelCount1, Label labelCount2, Label labelCount3, Label labelCount4, Panel panel1, Button playButton)
        {
            _menuForm = menuForm;
            _username = username;
            this.radioButton1 = radioButton1;
            this.radioButton2 = radioButton2;
            this.radioButton3 = radioButton3;
            this.radioButton4 = radioButton4;
            this.labelCount1 = labelCount1;
            this.labelCount2 = labelCount2;
            this.labelCount3 = labelCount3;
            this.labelCount4 = labelCount4;
            this.panel1 = panel1;
            this.form = form;
            _computer = new Computer("Компьютер");
            this.playButton = playButton;
            _board = new Board();
            _player = new Player(username);
            _occupiedCells = new bool[10, 10];
            _random = new Random();
        }

        public void InitializeForm(PlacementForm form)
        {
            form.Load += new EventHandler(InitializeGameField);
            radioButton1.CheckedChanged += new EventHandler(SelectShipSize1);
            radioButton2.CheckedChanged += new EventHandler(SelectShipSize2);
            radioButton3.CheckedChanged += new EventHandler(SelectShipSize3);
            radioButton4.CheckedChanged += new EventHandler(SelectShipSize4);
            playButton.Click += new EventHandler(StartGame);
            form.MouseWheel += new MouseEventHandler(ChangeShipRotation);
        }

        private void StartGame(object? sender, EventArgs e)
        {
            PlaceComputerShips();
            GameForm gameForm = new GameForm(_player, _computer, _menuForm);
            if (_shipCount1 + _shipCount2 + _shipCount3 + _shipCount4 == 0)
            {
                form.Hide();
                gameForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Перед началом игры необходимо расставить все корабли.");
            }

        }

        private void InitializeGameField(object sender, EventArgs e)
        {
            // создать поле кнопок
            _buttons = _board.CreateGameField();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int buttonWidth = panel1.Width / 10;
                    int buttonHeight = panel1.Height / 10;
                    Button button = new Button();
                    button.Location = new Point(i * buttonWidth, j * buttonHeight);
                    button.BackColor = Color.White;
                    button.Size = new Size(buttonWidth, buttonHeight);
                    button.Tag = new Point(i, j); // сохранить координаты кнопки
                    button.Click += new EventHandler(PlaceShip);
                    button.MouseLeave += RestoreColors;
                    button.MouseHover += ResetPreview;
                    panel1.Controls.Add(button);
                    _buttons[i, j] = button;
                }
            }
        }

        private void ChangeShipRotation(object sender, MouseEventArgs e)
        {
            if (_selectedShip != null)
            {
                // прокрутка колесика мыши
                switch (_selectedShip.GetRotation())
                {
                    case ShipDirection.Right:
                        _selectedShip.SetRotation(ShipDirection.Down);
                        break;
                    case ShipDirection.Down:
                        _selectedShip.SetRotation(ShipDirection.Right);
                        break;
                }
            }
        }

        private void SelectShipSize1(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                if (_shipCount1 > 0)
                {
                    _selectedShip = new Ship(new ShipPoint(0, 0), 1, ShipDirection.Down);
                }
                else
                {
                    MessageBox.Show("Вы уже разместили все корабли размером 1 клетка.");
                }
            }
        }

        private void SelectShipSize2(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                if (_shipCount2 > 0)
                {
                    _selectedShip = new Ship(new ShipPoint(0, 0), 2, ShipDirection.Down);
                }
                else
                {
                    MessageBox.Show("Вы уже разместили все корабли размером 2 клетки.");
                }
            }
        }

        private void SelectShipSize3(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                if (_shipCount3 > 0)
                {
                    _selectedShip = new Ship(new ShipPoint(0, 0), 3, ShipDirection.Down);
                }
                else
                {
                    MessageBox.Show("Вы уже разместили все корабли размером 3 клетки.");
                }
            }
        }

        private void SelectShipSize4(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                if (_shipCount4 > 0)
                {
                    _selectedShip = new Ship(new ShipPoint(0, 0), 4, ShipDirection.Down);
                }
                else
                {
                    MessageBox.Show("Вы уже разместили все корабли размером 4 клетки.");
                }
            }
        }

        private void PlaceShip(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Point point = (Point)button.Tag;
            if (_selectedShip != null)
            {
                _selectedShip.SetLocation(new ShipPoint(point.X, point.Y));
                if (CanPlaceShip(_selectedShip))
                {

                    switch (_selectedShip.GetSize())
                    {
                        case 1:
                            _shipCount1--;
                            labelCount1.Text = _shipCount1.ToString();
                            break;
                        case 2:
                            _shipCount2--;
                            labelCount2.Text = _shipCount2.ToString();
                            break;
                        case 3:
                            _shipCount3--;
                            labelCount3.Text = _shipCount3.ToString();
                            break;
                        case 4:
                            _shipCount4--;
                            labelCount4.Text = _shipCount4.ToString();
                            break;
                    }

                    PaintSurroundingCells(_selectedShip, Color.Gray);
                    // синий цвет - занятые кораблями
                    AddShipCells(_selectedShip);
                    PaintShipCells(_selectedShip, Color.Blue);

                    // серый цвет - нельзя разместить корабль
                }
                else
                {
                    MessageBox.Show("Невозможно разместить корабль в этом месте.");
                }
            }
            _selectedShip = null;
            ResetShipChoice();
        }

        private void ResetShipChoice()
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
        }

        private void PaintShipCells(Ship ship, Color color)
        {
            for (int i = 0; i < ship.GetSize(); i++)
            {
                int shipX = ship.GetLocation().GetX() + (i * (ship.GetRotation() == ShipDirection.Right ? 1 : 0));
                int shipY = ship.GetLocation().GetY() + (i * (ship.GetRotation() == ShipDirection.Down ? 1 : 0));
                _buttons[shipX, shipY].BackColor = color;
                _previousButtonColors[new Point(shipX, shipY)] = color;
            }
        }
        private void AddShipCells(Ship ship)
        {
            List<ShipPoint> shipPoints = new List<ShipPoint>();
            for (int i = 0; i < ship.GetSize(); i++)
            {
                int shipX = ship.GetLocation().GetX() + (i * (ship.GetRotation() == ShipDirection.Right ? 1 : 0));
                int shipY = ship.GetLocation().GetY() + (i * (ship.GetRotation() == ShipDirection.Down ? 1 : 0));
                ShipPoint shipPoint = new ShipPoint(shipX, shipY);
                shipPoints.Add(shipPoint);
            }
            _player.AddShip(ship, shipPoints);
        }

        private void PaintSurroundingCells(Ship ship, Color color)
        {
            for (int i = 0; i < ship.GetSize(); i++)
            {
                int shipX = ship.GetLocation().GetX() + (i * (ship.GetRotation() == ShipDirection.Right ? 1 : 0));
                int shipY = ship.GetLocation().GetY() + (i * (ship.GetRotation() == ShipDirection.Down ? 1 : 0));

                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        int buttonX = shipX + x;
                        int buttonY = shipY + y;

                        if (buttonX >= 0 && buttonX < 10 && buttonY >= 0 && buttonY < 10 && (x != 0 || y != 0))
                        {
                            _buttons[buttonX, buttonY].BackColor = color;
                        }
                    }
                }
            }
        }

        private void PreviewShipPosition(Ship ship)
        {
            int[] coords = CalcShipCoords(ship);
            int shipX_start = coords[0];
            int shipY_start = coords[2];
            int shipX_end = coords[1];
            int shipY_end = coords[3];

            // Очистить предыдущий предпросмотр
            foreach (var point in _previousButtonColors.Keys.ToList())
            {
                _buttons[point.X, point.Y].BackColor = _previousButtonColors[point];
            }
            _previousButtonColors.Clear();

            // Запомнить предыдущий цвет кнопки
            for (int x = shipX_start; x <= shipX_end; x++)
            {
                for (int y = shipY_start; y <= shipY_end; y++)
                {
                    if (x >= 0 && x < 10 && y >= 0 && y < 10)
                    {
                        _previousButtonColors[new Point(x, y)] = _buttons[x, y].BackColor;
                    }
                }
            }

            // Вывести предпросмотр корабля
            for (int x = shipX_start; x <= shipX_end; x++)
            {
                for (int y = shipY_start; y <= shipY_end; y++)
                {
                    if (x >= 0 && x < 10 && y >= 0 && y < 10)
                    {
                        _buttons[x, y].BackColor = Color.Green;
                    }
                }
            }
        }

        private void RestoreColors(object sender, EventArgs e)
        {
            // Восстановить предыд ущий цвет всех кнопок
            foreach (var point in _previousButtonColors.Keys.ToList())
            {
                _buttons[point.X, point.Y].BackColor = _previousButtonColors[point];
            }
            _previousButtonColors.Clear();
        }

        private void ResetPreview(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Point point = (Point)button.Tag;

            if (_selectedShip != null)
            {
                _selectedShip.SetLocation(new ShipPoint(point.X, point.Y));
                if (CanPlaceShip(_selectedShip))
                    PreviewShipPosition(_selectedShip);
            }
        }

        private int[] CalcShipCoords(Ship ship)
        {
            int shipX_start = ship.GetLocation().GetX();
            int shipY_start = ship.GetLocation().GetY();
            int shipX_end = 0;
            int shipY_end = 0;
            switch (ship.GetRotation())
            {
                case ShipDirection.Right:
                    shipX_end = shipX_start + ship.GetSize() - 1;
                    shipY_end = shipY_start;
                    break;
                case ShipDirection.Down:
                    shipX_end = shipX_start;
                    shipY_end = shipY_start + ship.GetSize() - 1;
                    break;
            }
            return new int[4] { shipX_start, shipX_end, shipY_start, shipY_end };
        }

        private bool CanPlaceShip(Ship ship)
        {
            int[] coords = CalcShipCoords(ship);

            if (coords[0] < 0 || coords[1] >= 10 || coords[2] < 0 || coords[3] >= 10)
            {
                return false;
            }

            if (_buttons[coords[0], coords[2]].BackColor == Color.Blue || _buttons[coords[0], coords[2]].BackColor == Color.Gray)
            {
                return false;
            }
            if (_buttons[coords[1], coords[3]].BackColor == Color.Blue || _buttons[coords[1], coords[3]].BackColor == Color.Gray)
            {
                return false;
            }

            return true;
        }
        public void PlaceComputerShips()
        {
            PlaceShipForComputer(4, 1);
            PlaceShipForComputer(3, 2);
            PlaceShipForComputer(2, 3);
            PlaceShipForComputer(1, 4);
        }

        private void PlaceShipForComputer(int size, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Dictionary<ShipPoint, ShipDirection> possiblePositions = CalculatePossiblePositions(size);
                if (possiblePositions.Count > 0)
                {
                    int randomIndex = _random.Next(possiblePositions.Count);
                    KeyValuePair<ShipPoint, ShipDirection> selectedPosition = possiblePositions.ElementAt(randomIndex);
                    Ship ship = new Ship(selectedPosition.Key, size, selectedPosition.Value);
                    PlaceComputerShipOnGrid(selectedPosition.Key, ship);
                }
                else
                {
                    Console.WriteLine($"Не удалось разместить корабль размера {size}");
                }
            }
        }

        private Dictionary<ShipPoint, ShipDirection> CalculatePossiblePositions(int size)
        {
            Dictionary<ShipPoint, ShipDirection> possiblePositions = new Dictionary<ShipPoint, ShipDirection>();

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (CanPlaceHorizontally(x, y, size))
                    {
                        possiblePositions.Add(new ShipPoint(x, y), ShipDirection.Right);
                    }
                    else if (CanPlaceVertically(x, y, size))
                    {
                        possiblePositions.Add(new ShipPoint(x, y), ShipDirection.Down);
                    }
                }
            }

            return possiblePositions;
        }

        private bool CanPlaceHorizontally(int x, int y, int size)
        {
            if (x + size > 10) return false;

            for (int i = Math.Max(0, x - 1); i <= Math.Min(9, x + size); i++)
            {
                for (int j = Math.Max(0, y - 1); j <= Math.Min(9, y + 1); j++)
                {
                    if (_occupiedCells[i, j]) return false;
                }
            }

            return true;
        }

        private bool CanPlaceVertically(int x, int y, int size)
        {
            if (y + size > 10) return false;

            for (int i = Math.Max(0, x - 1); i <= Math.Min(9, x + 1); i++)
            {
                for (int j = Math.Max(0, y - 1); j <= Math.Min(9, y + size); j++)
                {
                    if (_occupiedCells[i, j]) return false;
                }
            }
            return true;
        }
            private void PlaceComputerShipOnGrid(ShipPoint location, Ship ship)
        {
            List< ShipPoint > points = new List<ShipPoint> ();
            for (int i = 0; i < ship.GetSize(); i++)
            {
                int x = ship.GetRotation() == ShipDirection.Right ? location.GetX() + i : location.GetX();
                int y = ship.GetRotation() == ShipDirection.Down ? location.GetY() + i : location.GetY();
                points.Add(new ShipPoint(x, y));
                _occupiedCells[x, y] = true;
            }

            _computer.AddShip(ship, points);
        }
    }
}
