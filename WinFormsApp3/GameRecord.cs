using Microsoft.VisualBasic.Devices;
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
        private List<Move> _moveList;

        [JsonInclude]
        private TimeSpan _gameDuration;

        [JsonInclude]
        private DateTime _gameDate;

        [JsonInclude]
        private bool _playerWon;
        [JsonInclude]
        private List<Ship> _playerShips;
        [JsonInclude]
        private List<Ship> _computerShips;

        [JsonConstructor]
        public GameRecord()
        {
        }

        public GameRecord(string playerName)
        {
            _playerName = playerName;
            _gameDuration = TimeSpan.Zero;
            _gameDate = DateTime.Now;
            _moveList = new List<Move>();
            _playerShips = new List<Ship>();
            _computerShips = new List<Ship>();
            _playerWon = false;
        }
        public void SetPlayerWon(bool playerWon)
        {
            _playerWon = playerWon;
        }

        public bool GetPlayerWon()
        {
            return _playerWon;
        }
        public void SetPlayerShips(List<Ship> ships)
        {
            _playerShips = ships;
        }

        public void SetComputerShips(List<Ship> ships)
        {
            _computerShips = ships;
        }

        public List<Ship> GetPlayerShips()
        {
            return _playerShips;
        }

        public List<Ship> GetComputerShips()
        {
            return _computerShips;
        }


        public void AddMoveToList(Move move)
        {
            _moveList.Add(move);
        }
        public List<Move> GetMovesList()
        {
            return _moveList;
        }
        public void SetDuration(TimeSpan duration)
        {
            _gameDuration = duration;
        }

        public string GetPlayerName()
        {
            return _playerName;
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
            return $"Игрок: {_playerName}, Ходов: {_moveList.Count}, Время: {GetFormattedTime()}, Дата: {_gameDate}";
        }
    }
}
