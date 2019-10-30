namespace Tetris
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.score = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.highscore = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.levelDisplay = new System.Windows.Forms.Label();
            this.controls = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 500;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.score);
            this.panel1.Location = new System.Drawing.Point(622, 50);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(160, 70);
            this.panel1.TabIndex = 0;
            // 
            // score
            // 
            this.score.AutoSize = true;
            this.score.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.score.Location = new System.Drawing.Point(2, 7);
            this.score.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.score.Name = "score";
            this.score.Size = new System.Drawing.Size(42, 46);
            this.score.TabIndex = 0;
            this.score.Text = "0";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel2.Controls.Add(this.highscore);
            this.panel2.Location = new System.Drawing.Point(622, 170);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(160, 70);
            this.panel2.TabIndex = 1;
            // 
            // highscore
            // 
            this.highscore.AutoSize = true;
            this.highscore.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.highscore.Location = new System.Drawing.Point(2, 7);
            this.highscore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.highscore.Name = "highscore";
            this.highscore.Size = new System.Drawing.Size(42, 46);
            this.highscore.TabIndex = 0;
            this.highscore.Text = "0";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.Controls.Add(this.levelDisplay);
            this.panel3.Location = new System.Drawing.Point(100, 10);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(400, 30);
            this.panel3.TabIndex = 2;
            // 
            // levelDisplay
            // 
            this.levelDisplay.AutoSize = true;
            this.levelDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelDisplay.Location = new System.Drawing.Point(4, 4);
            this.levelDisplay.Name = "levelDisplay";
            this.levelDisplay.Size = new System.Drawing.Size(71, 20);
            this.levelDisplay.TabIndex = 0;
            this.levelDisplay.Text = "Level: 1";
            this.levelDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // controls
            // 
            this.controls.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.controls.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.controls.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.controls.DetectUrls = false;
            this.controls.Enabled = false;
            this.controls.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controls.Location = new System.Drawing.Point(582, 470);
            this.controls.Name = "controls";
            this.controls.ReadOnly = true;
            this.controls.ShortcutsEnabled = false;
            this.controls.Size = new System.Drawing.Size(240, 196);
            this.controls.TabIndex = 3;
            this.controls.TabStop = false;
            this.controls.Text = "Controls:\nLeft Arrow/Right Arrow/A/D -> Shift Direction\nUp Arrow/W -> Rotate\nDown" +
    " Arrow/S -> Speed Downwards\nP -> Pause\nR -> Restart\nZ -> Change Rotation Directi" +
    "on\nM -> Unmute/Mute Song";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::Tetris.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(902, 911);
            this.Controls.Add(this.controls);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(306, 317);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Tetris By: Elite Future/Kevin Kim";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label score;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label highscore;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label levelDisplay;
        private System.Windows.Forms.RichTextBox controls;
    }
}

