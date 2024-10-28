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
        private string _username;
        private Logger _logger;
        public MenuForm(string username)
        {
            InitializeComponent();
            _username = username;
            _logger = new Logger();
        }

        private void startGame_button_Click(object sender, EventArgs e)
        {
            PlacementForm shipPlacementForm = new PlacementForm(_username);
            shipPlacementForm.Show();
            this.Hide();

        }

        private void leadership_Button_Click(object sender, EventArgs e)
        {
            LeaderboardForm leaderboardForm = new LeaderboardForm();
            leaderboardForm.ShowDialog();
        }

        private void replay_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Path.Combine(Application.StartupPath, "saves"),
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                GameRecord gameRecord = _logger.LoadGameRecord(filePath);

                if (gameRecord != null)
                {
                    GameReplayForm replayForm = new GameReplayForm(gameRecord);
                    replayForm.Show();
                }
            }
        }
    }
}
