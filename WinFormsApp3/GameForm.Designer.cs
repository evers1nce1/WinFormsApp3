namespace WinFormsApp3
{
    partial class GameForm
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
            _playerPanel = new Panel();
            _computerPanel = new Panel();
            startGame_button = new Button();
            SuspendLayout();
            // 
            // _playerPanel
            // 
            _playerPanel.BorderStyle = BorderStyle.FixedSingle;
            _playerPanel.Location = new Point(36, 42);
            _playerPanel.Name = "_playerPanel";
            _playerPanel.Size = new Size(300, 300);
            _playerPanel.TabIndex = 0;
            // 
            // _computerPanel
            // 
            _computerPanel.BorderStyle = BorderStyle.FixedSingle;
            _computerPanel.Location = new Point(437, 42);
            _computerPanel.Name = "_computerPanel";
            _computerPanel.Size = new Size(300, 300);
            _computerPanel.TabIndex = 1;
            // 
            // startGame_button
            // 
            startGame_button.Location = new Point(36, 370);
            startGame_button.Name = "startGame_button";
            startGame_button.Size = new Size(98, 50);
            startGame_button.TabIndex = 2;
            startGame_button.Text = "button1";
            startGame_button.UseVisualStyleBackColor = true;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(776, 440);
            Controls.Add(startGame_button);
            Controls.Add(_computerPanel);
            Controls.Add(_playerPanel);
            Name = "GameForm";
            Text = "GameForm";
            ResumeLayout(false);
        }

        #endregion

        private Panel _playerPanel;
        private ListBox listBox1;
        private Panel _computerPanel;
        private Button startGame_button;
    }
}