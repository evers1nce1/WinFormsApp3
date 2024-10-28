namespace WinFormsApp3
{
    partial class LeaderboardForm
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
            listView_leaderboard = new ListView();
            SuspendLayout();
            // 
            // listView_leaderboard
            // 
            listView_leaderboard.Dock = DockStyle.Fill;
            listView_leaderboard.FullRowSelect = true;
            listView_leaderboard.GridLines = true;
            listView_leaderboard.Location = new Point(0, 0);
            listView_leaderboard.Name = "listView_leaderboard";
            listView_leaderboard.Size = new Size(489, 320);
            listView_leaderboard.TabIndex = 0;
            listView_leaderboard.UseCompatibleStateImageBehavior = false;
            listView_leaderboard.View = View.Details;
            // 
            // LeaderboardForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(489, 320);
            Controls.Add(listView_leaderboard);
            Name = "LeaderboardForm";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private ListView listView_leaderboard;
    }
}