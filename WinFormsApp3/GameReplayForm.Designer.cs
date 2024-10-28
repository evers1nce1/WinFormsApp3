namespace WinFormsApp3
{
    partial class GameReplayForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            _computerPanel = new Panel();
            _playerPanel = new Panel();
            _prevMoveButton = new Button();
            _nextMoveButton = new Button();
            _moveCountLabel = new Label();
            SuspendLayout();
            // 
            // _computerPanel
            // 
            _computerPanel.BorderStyle = BorderStyle.FixedSingle;
            _computerPanel.Location = new Point(448, 35);
            _computerPanel.Name = "_computerPanel";
            _computerPanel.Size = new Size(300, 300);
            _computerPanel.TabIndex = 3;
            // 
            // _playerPanel
            // 
            _playerPanel.BorderStyle = BorderStyle.FixedSingle;
            _playerPanel.Location = new Point(49, 35);
            _playerPanel.Name = "_playerPanel";
            _playerPanel.Size = new Size(300, 300);
            _playerPanel.TabIndex = 2;
            // 
            // _prevMoveButton
            // 
            _prevMoveButton.Location = new Point(318, 341);
            _prevMoveButton.Name = "_prevMoveButton";
            _prevMoveButton.Size = new Size(82, 59);
            _prevMoveButton.TabIndex = 4;
            _prevMoveButton.Text = "<<";
            _prevMoveButton.UseVisualStyleBackColor = true;
            // 
            // _nextMoveButton
            // 
            _nextMoveButton.Location = new Point(406, 341);
            _nextMoveButton.Name = "_nextMoveButton";
            _nextMoveButton.Size = new Size(82, 59);
            _nextMoveButton.TabIndex = 5;
            _nextMoveButton.Text = ">>";
            _nextMoveButton.UseVisualStyleBackColor = true;
            // 
            // _moveCountLabel
            // 
            _moveCountLabel.AutoSize = true;
            _moveCountLabel.Location = new Point(379, 320);
            _moveCountLabel.Name = "_moveCountLabel";
            _moveCountLabel.Size = new Size(38, 15);
            _moveCountLabel.TabIndex = 6;
            _moveCountLabel.Text = "label1";
            // 
            // GameReplayForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(_moveCountLabel);
            Controls.Add(_nextMoveButton);
            Controls.Add(_prevMoveButton);
            Controls.Add(_computerPanel);
            Controls.Add(_playerPanel);
            Name = "GameReplayForm";
            Text = "GameReplayForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel _computerPanel;
        private Panel _playerPanel;
        private Button _prevMoveButton;
        private Button _nextMoveButton;
        private Label _moveCountLabel;
    }
}