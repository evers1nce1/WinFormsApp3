using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WinFormsApp3
{
    public class LeaderboardManager
    {
        private const string LEADERBOARD_FILE = "leaderboard.json";
        private List<GameRecord> _records;

        public LeaderboardManager()
        {
            _records = new List<GameRecord>();
            LoadLeaderboard();
        }

        public void AddRecord(GameRecord record)
        {
            _records.Add(record);
            _records = _records
                .OrderBy(r => r.GetMoves())
                .ThenBy(r => r.GetDuration())
                .Take(10) 
                .ToList();
            SaveLeaderboard();
        }
        private void SaveLeaderboard()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    IncludeFields = true
                };

                string jsonString = JsonSerializer.Serialize(_records, options);
                File.WriteAllText(LEADERBOARD_FILE, jsonString);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
        }

        private void LoadLeaderboard()
        {
            try
            {
                if (File.Exists(LEADERBOARD_FILE))
                {
                    string jsonString = File.ReadAllText(LEADERBOARD_FILE);
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        IncludeFields = true
                    };

                    var recordsFromJson = JsonSerializer.Deserialize<List<GameRecord>>(jsonString, options);
                    if (recordsFromJson != null)
                    {
                        _records = recordsFromJson.Where(r => r.GetPlayerName() != null).ToList(); // Фильтруем null записи
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке: {ex.Message}");
                _records = new List<GameRecord>();
            }
        }

        public List<GameRecord> GetRecords()
        {
            return _records;
        }
    }
}
