using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp3
{
    public partial class LeaderboardForm : Form
    {
        private LeaderboardManager _leaderboard;
        private ListView _leaderboardListView;

        public LeaderboardForm()
        {
            InitializeComponent();
            InitializeLeaderboardView();
            LoadLeaderboard();
        }

        private void InitializeLeaderboardView()
        {
            _leaderboardListView = new ListView
            {
                View = View.Details,
                Dock = DockStyle.Fill,
                FullRowSelect = true,
                GridLines = true
            };

            _leaderboardListView.Columns.Add("Игрок", 150);
            _leaderboardListView.Columns.Add("Ходов", 100);
            _leaderboardListView.Columns.Add("Время", 100);
            _leaderboardListView.Columns.Add("Дата", 150);

            Controls.Add(_leaderboardListView);
        }

        private void LoadLeaderboard()
        {
            _leaderboard = new LeaderboardManager();
            DisplayLeaderboard();
        }

        private void DisplayLeaderboard()
        {
            _leaderboardListView.Items.Clear();
            foreach (var record in _leaderboard.GetRecords())
            {
                var item = new ListViewItem(record.GetPlayerName());
                item.SubItems.Add(record.GetMoves().ToString());
                item.SubItems.Add(record.GetFormattedTime());
                item.SubItems.Add(record.GetGameDate().ToString("dd.MM.yyyy HH:mm"));
                _leaderboardListView.Items.Add(item);
            }
        }
    }
}
