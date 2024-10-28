using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WinFormsApp3
{
    public class GameRecord
    {
        [JsonInclude]
        private string _playerName;

        [JsonInclude]
        private int _moves;

        [JsonInclude]
        private TimeSpan _gameDuration;

        [JsonInclude]
        private DateTime _gameDate;

        [JsonConstructor]
        public GameRecord()
        {
        }

        public GameRecord(string playerName)
        {
            _playerName = playerName;
            _moves = 0;
            _gameDuration = TimeSpan.Zero;
            _gameDate = DateTime.Now;
        }


        public void SetMoves(int moves)
        {
            _moves = moves;
        }

        public void SetDuration(TimeSpan duration)
        {
            _gameDuration = duration;
        }

        public string GetPlayerName()
        {
            return _playerName;
        }

        public int GetMoves()
        {
            return _moves;
        }

        public TimeSpan GetDuration()
        {
            return _gameDuration;
        }

        public DateTime GetGameDate()
        {
            return _gameDate;
        }

        public string GetFormattedTime()
        {
            return $"{_gameDuration.Minutes}:{_gameDuration.Seconds}";
        }

        public override string ToString()
        {
            return $"Игрок: {_playerName}, Ходов: {_moves}, Время: {GetFormattedTime()}, Дата: {_gameDate}";
        }
    }
}
