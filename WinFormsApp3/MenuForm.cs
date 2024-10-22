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
    public partial class MenuForm : Form
    {
        private GameManager _gameManager;
        private string _username;

        public MenuForm(string username)
        {
            InitializeComponent();
            _username = username;
        }

        private void startGame_button_Click(object sender, EventArgs e)
        {
            GameManager _gameManager = new GameManager(_username);
            PlacementForm shipPlacementForm = new PlacementForm(_gameManager, _username);
            shipPlacementForm.ShowDialog();
            this.Hide();

        }
    }
}
