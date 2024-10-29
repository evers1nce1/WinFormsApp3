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
        private MenuForm _menuForm;
        public GameForm(Player player, Computer computer, MenuForm menuForm)
        {
            InitializeComponent();
            _gameManager = new GameManager(player, computer, _playerPanel, _computerPanel, startGame_button);
            _menuForm = menuForm;
            this.FormClosed += OnFormClosed;
        }
        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            _menuForm.Show();
        }

    }
}