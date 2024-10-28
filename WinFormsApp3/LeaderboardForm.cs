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

        public LeaderboardForm()
        {
            InitializeComponent();
            InitializeLeaderboardView();
            LoadLeaderboard();
        }

        private void InitializeLeaderboardView()
        {

            listView_leaderboard.Columns.Add("Игрок");
            listView_leaderboard.Columns.Add("Ходов");
            listView_leaderboard.Columns.Add("Время");
            listView_leaderboard.Columns.Add("Дата");

        }

        private void LoadLeaderboard()
        {
            _leaderboard = new LeaderboardManager();
            DisplayLeaderboard();
        }

        private void DisplayLeaderboard()
        {
            listView_leaderboard.Items.Clear();
            foreach (var record in _leaderboard.GetRecords())
            {
                var item = new ListViewItem(record.GetPlayerName());
                item.SubItems.Add(record.GetMovesList().Count.ToString());
                item.SubItems.Add(record.GetFormattedTime());
                item.SubItems.Add(record.GetGameDate().ToString("dd.MM.yyyy HH:mm"));
                listView_leaderboard.Items.Add(item);
            }
        }

    }
}
