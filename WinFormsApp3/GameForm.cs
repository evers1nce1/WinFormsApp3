using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WinFormsApp3
{
    public partial class GameForm : Form
    {

        private GameManager _gameManager;
        Button[,] _placedButtons;

        private string _username;
        public GameForm(string name, Button[,] placedButtons)
        {
            _username = name;
            _placedButtons = placedButtons;
            InitializeComponent();
            _gameManager = new GameManager(_username);
        }


    }
}
