using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace WinFormsApp3
{
    public class Logger
    {
        private const string LOG_DIR = "saves";
        private GameRecord _currentGameRecord;

        public Logger()
        {
            if (!Directory.Exists(LOG_DIR))
            {
                Directory.CreateDirectory(LOG_DIR);
            }
        }
        public GameRecord GetGameRecord()
        { 
            return _currentGameRecord; 
        }
        public void OnNewGame(string playerName)
        {
            _currentGameRecord = new GameRecord(playerName);
        }

        public void LogMove(Move move)
        {
            if (_currentGameRecord != null)
            {
                _currentGameRecord.AddMoveToList(move);
            }
        }

        public void EndGame(TimeSpan duration, bool playerWon)
        {
            if (_currentGameRecord != null)
            {
                _currentGameRecord.SetDuration(duration);
                _currentGameRecord.SetPlayerWon(playerWon);
                SaveGameRecord(_currentGameRecord);
            }
        }

        private void SaveGameRecord(GameRecord record)
        {
            string fileName = $"{record.GetPlayerName()}{record.GetGameDate():yyyyMMdd_HHmmss}.json";
            string filePath = Path.Combine(LOG_DIR, fileName);

            string jsonString = JsonSerializer.Serialize(record, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonString);
        }


        public GameRecord LoadGameRecord(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    string jsonString = File.ReadAllText(filePath);
                    var options = new JsonSerializerOptions
                    {
                        IncludeFields = true
                    };
                    GameRecord record = JsonSerializer.Deserialize<GameRecord>(jsonString, options);
                    return record;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}");
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Файл не найден.");
                return null;
            }
        }


    }
}