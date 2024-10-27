namespace WinFormsApp3
{
    partial class MenuForm
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
            startGame_button = new Button();
            label1 = new Label();
            leadership_Button = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // startGame_button
            // 
            startGame_button.Font = new Font("Segoe UI Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            startGame_button.Location = new Point(333, 91);
            startGame_button.Name = "startGame_button";
            startGame_button.Size = new Size(120, 100);
            startGame_button.TabIndex = 0;
            startGame_button.Text = "Играть";
            startGame_button.UseVisualStyleBackColor = true;
            startGame_button.Click += startGame_button_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Comic Sans MS", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(227, 9);
            label1.Name = "label1";
            label1.Size = new Size(335, 67);
            label1.TabIndex = 1;
            label1.Text = "Морской Бой";
            // 
            // leadership_Button
            // 
            leadership_Button.Location = new Point(333, 214);
            leadership_Button.Name = "leadership_Button";
            leadership_Button.Size = new Size(120, 100);
            leadership_Button.TabIndex = 2;
            leadership_Button.Text = "Таблица лидеров";
            leadership_Button.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(333, 338);
            button1.Name = "button1";
            button1.Size = new Size(120, 100);
            button1.TabIndex = 3;
            button1.Text = "Таблица лидеров";
            button1.UseVisualStyleBackColor = true;
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(leadership_Button);
            Controls.Add(label1);
            Controls.Add(startGame_button);
            Name = "MenuForm";
            Text = "MenuForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button startGame_button;
        private Label label1;
        private Button leadership_Button;
        private Button button1;
    }
}