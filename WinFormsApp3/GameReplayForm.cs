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
    public partial class GameReplayForm : Form
    {
        private ReplayManager _replayManager;

        public GameReplayForm(GameRecord gameRecord)
        {
            InitializeComponent();
            _replayManager = new ReplayManager(
                gameRecord,
                _playerPanel,
                _computerPanel,
                _moveCountLabel,
                _nextMoveButton,
                _prevMoveButton
            );
        }
    }
}
