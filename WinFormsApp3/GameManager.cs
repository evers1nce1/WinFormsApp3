using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp3
{
    public class GameManager
    {
        private Board _board;
        private Player _player;
        private Player _computer;
        private List<Move> _moveHistory;
        private string _username;

        public GameManager(string username)
        {
            _username = username;
            _board = new Board();
            _player = new Player(username);
            _computer = new Player("Компьютер");
            _moveHistory = new List<Move>();
        }


        public Player GetPlayer()
        {
            return _player;
        }

    }
}
