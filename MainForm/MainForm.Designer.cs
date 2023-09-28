/*
PROJECT C#
*/
namespace Doodle_Jump
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button RestartButton;
		private System.Windows.Forms.Button OverCloseButton;
		private System.Windows.Forms.Button ContinueButton;
		private System.Windows.Forms.Button PlayButton;
		private System.Windows.Forms.Button ShutdownButton;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem rulesToolStripMenuItem;
		private System.Windows.Forms.Button recordsButton;
		private System.Windows.Forms.Button recordsCancel;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		public void InitializeComponent()
		{
			this.RestartButton = new System.Windows.Forms.Button();
			this.OverCloseButton = new System.Windows.Forms.Button();
			this.ContinueButton = new System.Windows.Forms.Button();
			this.PlayButton = new System.Windows.Forms.Button();
			this.ShutdownButton = new System.Windows.Forms.Button();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.rulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.recordsButton = new System.Windows.Forms.Button();
			this.recordsCancel = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// RestartButton
			// 
			this.RestartButton.BackColor = System.Drawing.Color.Transparent;
			this.RestartButton.BackgroundImage = global::Doodle_Jump.Resources.play_again_on;
			this.RestartButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.RestartButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.RestartButton.FlatAppearance.BorderSize = 0;
			this.RestartButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.RestartButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.RestartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.RestartButton.ForeColor = System.Drawing.Color.Transparent;
			this.RestartButton.Location = new System.Drawing.Point(127, 489);
			this.RestartButton.Name = "RestartButton";
			this.RestartButton.Size = new System.Drawing.Size(126, 49);
			this.RestartButton.TabIndex = 2;
			this.RestartButton.UseVisualStyleBackColor = false;
			this.RestartButton.Visible = false;
			this.RestartButton.Click += new System.EventHandler(this.RestartButtonClick);
			// 
			// OverCloseButton
			// 
			this.OverCloseButton.BackColor = System.Drawing.Color.Transparent;
			this.OverCloseButton.BackgroundImage = global::Doodle_Jump.Resources.menu;
			this.OverCloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.OverCloseButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OverCloseButton.FlatAppearance.BorderSize = 0;
			this.OverCloseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.OverCloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.OverCloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.OverCloseButton.ForeColor = System.Drawing.Color.Transparent;
			this.OverCloseButton.Location = new System.Drawing.Point(127, 544);
			this.OverCloseButton.Name = "OverCloseButton";
			this.OverCloseButton.Size = new System.Drawing.Size(126, 49);
			this.OverCloseButton.TabIndex = 3;
			this.OverCloseButton.UseVisualStyleBackColor = false;
			this.OverCloseButton.Visible = false;
			this.OverCloseButton.Click += new System.EventHandler(this.OverCloseButtonClick);
			this.OverCloseButton.MouseEnter += new System.EventHandler(this.OverCloseButtonMouseEnter);
			this.OverCloseButton.MouseLeave += new System.EventHandler(this.OverCloseButtonMouseLeave);
			// 
			// ContinueButton
			// 
			this.ContinueButton.BackColor = System.Drawing.Color.Transparent;
			this.ContinueButton.BackgroundImage = global::Doodle_Jump.Resources.done;
			this.ContinueButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ContinueButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ContinueButton.FlatAppearance.BorderSize = 0;
			this.ContinueButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.ContinueButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.ContinueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ContinueButton.ForeColor = System.Drawing.Color.Transparent;
			this.ContinueButton.Location = new System.Drawing.Point(127, 489);
			this.ContinueButton.Name = "ContinueButton";
			this.ContinueButton.Size = new System.Drawing.Size(126, 49);
			this.ContinueButton.TabIndex = 4;
			this.ContinueButton.UseVisualStyleBackColor = false;
			this.ContinueButton.Visible = false;
			this.ContinueButton.Click += new System.EventHandler(this.ContinueButtonClick);
			this.ContinueButton.MouseEnter += new System.EventHandler(this.ContinueButtonMouseEnter);
			this.ContinueButton.MouseLeave += new System.EventHandler(this.ContinueButtonMouseLeave);
			// 
			// PlayButton
			// 
			this.PlayButton.BackColor = System.Drawing.Color.Transparent;
			this.PlayButton.BackgroundImage = global::Doodle_Jump.Resources.play;
			this.PlayButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.PlayButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PlayButton.FlatAppearance.BorderSize = 0;
			this.PlayButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.PlayButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.PlayButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.PlayButton.ForeColor = System.Drawing.Color.Transparent;
			this.PlayButton.Location = new System.Drawing.Point(127, 298);
			this.PlayButton.Name = "PlayButton";
			this.PlayButton.Size = new System.Drawing.Size(126, 49);
			this.PlayButton.TabIndex = 5;
			this.PlayButton.UseVisualStyleBackColor = false;
			this.PlayButton.Visible = false;
			this.PlayButton.Click += new System.EventHandler(this.PlayButtonClick);
			this.PlayButton.MouseEnter += new System.EventHandler(this.PlayButtonMouseEnter);
			this.PlayButton.MouseLeave += new System.EventHandler(this.PlayButtonMouseLeave);
			// 
			// ShutdownButton
			// 
			this.ShutdownButton.BackColor = System.Drawing.Color.Transparent;
			this.ShutdownButton.BackgroundImage = global::Doodle_Jump.Resources.cancel;
			this.ShutdownButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ShutdownButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ShutdownButton.FlatAppearance.BorderSize = 0;
			this.ShutdownButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.ShutdownButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.ShutdownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ShutdownButton.ForeColor = System.Drawing.Color.Transparent;
			this.ShutdownButton.Location = new System.Drawing.Point(127, 489);
			this.ShutdownButton.Name = "ShutdownButton";
			this.ShutdownButton.Size = new System.Drawing.Size(126, 49);
			this.ShutdownButton.TabIndex = 6;
			this.ShutdownButton.UseVisualStyleBackColor = false;
			this.ShutdownButton.Visible = false;
			this.ShutdownButton.Click += new System.EventHandler(this.ShutdownButtonClick);
			this.ShutdownButton.MouseEnter += new System.EventHandler(this.ShutdownButtonMouseEnter);
			this.ShutdownButton.MouseLeave += new System.EventHandler(this.ShutdownButtonMouseLeave);
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(434, 33);
			this.menuStrip1.TabIndex = 7;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.rulesToolStripMenuItem,
			this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(61, 29);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// rulesToolStripMenuItem
			// 
			this.rulesToolStripMenuItem.Name = "rulesToolStripMenuItem";
			this.rulesToolStripMenuItem.Size = new System.Drawing.Size(146, 30);
			this.rulesToolStripMenuItem.Text = "Rules";
			this.rulesToolStripMenuItem.Click += new System.EventHandler(this.RulesToolStripMenuItemClick);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(146, 30);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
			// 
			// recordsButton
			// 
			this.recordsButton.BackColor = System.Drawing.Color.Transparent;
			this.recordsButton.BackgroundImage = global::Doodle_Jump.Resources.records;
			this.recordsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.recordsButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.recordsButton.FlatAppearance.BorderSize = 0;
			this.recordsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.recordsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.recordsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.recordsButton.ForeColor = System.Drawing.Color.Transparent;
			this.recordsButton.Location = new System.Drawing.Point(127, 489);
			this.recordsButton.Name = "recordsButton";
			this.recordsButton.Size = new System.Drawing.Size(126, 49);
			this.recordsButton.TabIndex = 8;
			this.recordsButton.UseVisualStyleBackColor = false;
			this.recordsButton.Visible = false;
			this.recordsButton.Click += new System.EventHandler(this.RecordsButtonClick);
			this.recordsButton.MouseEnter += new System.EventHandler(this.RecordsButtonMouseEnter);
			this.recordsButton.MouseLeave += new System.EventHandler(this.RecordsButtonMouseLeave);
			// 
			// recordsCancel
			// 
			this.recordsCancel.BackColor = System.Drawing.Color.Transparent;
			this.recordsCancel.BackgroundImage = global::Doodle_Jump.Resources.cancel;
			this.recordsCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.recordsCancel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.recordsCancel.FlatAppearance.BorderSize = 0;
			this.recordsCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.recordsCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.recordsCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.recordsCancel.ForeColor = System.Drawing.Color.Transparent;
			this.recordsCancel.Location = new System.Drawing.Point(127, 544);
			this.recordsCancel.Name = "recordsCancel";
			this.recordsCancel.Size = new System.Drawing.Size(126, 49);
			this.recordsCancel.TabIndex = 9;
			this.recordsCancel.UseVisualStyleBackColor = false;
			this.recordsCancel.Visible = false;
			this.recordsCancel.Click += new System.EventHandler(this.RecordsCancelButtonClick);
			this.recordsCancel.MouseEnter += new System.EventHandler(this.RecordsCancelButtonMouseEnter);
			this.recordsCancel.MouseLeave += new System.EventHandler(this.RecordsCancelButtonMouseLeave);
			// 
			// MainForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
			this.BackColor = System.Drawing.Color.Black;
			this.BackgroundImage = global::Doodle_Jump.Resources.background;
			this.ClientSize = new System.Drawing.Size(434, 661);
			this.Controls.Add(this.recordsCancel);
			this.Controls.Add(this.recordsButton);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.ShutdownButton);
			this.Controls.Add(this.PlayButton);
			this.Controls.Add(this.ContinueButton);
			this.Controls.Add(this.OverCloseButton);
			this.Controls.Add(this.RestartButton);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = global::Doodle_Jump.Resources.Icon;
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Doodle Jump";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainFormPaint);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainFormKeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainFormKeyUp);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
