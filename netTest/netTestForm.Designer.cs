namespace netTest
{
	partial class netTestForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(netTestForm));
			this.Resolution = new System.Windows.Forms.TextBox();
			this.labelHeader = new System.Windows.Forms.Label();
			this.progress = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// Resolution
			// 
			this.Resolution.Location = new System.Drawing.Point(15, 34);
			this.Resolution.Multiline = true;
			this.Resolution.Name = "Resolution";
			this.Resolution.ReadOnly = true;
			this.Resolution.Size = new System.Drawing.Size(429, 275);
			this.Resolution.TabIndex = 0;
			// 
			// labelHeader
			// 
			this.labelHeader.AutoSize = true;
			this.labelHeader.Location = new System.Drawing.Point(12, 9);
			this.labelHeader.Name = "labelHeader";
			this.labelHeader.Size = new System.Drawing.Size(161, 13);
			this.labelHeader.TabIndex = 1;
			this.labelHeader.Text = "Diagnose your internet problems!";
			// 
			// progress
			// 
			this.progress.Location = new System.Drawing.Point(179, 9);
			this.progress.Name = "progress";
			this.progress.Size = new System.Drawing.Size(265, 13);
			this.progress.Step = 1;
			this.progress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progress.TabIndex = 2;
			this.progress.Visible = false;
			// 
			// netTestForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Cyan;
			this.ClientSize = new System.Drawing.Size(456, 321);
			this.Controls.Add(this.progress);
			this.Controls.Add(this.labelHeader);
			this.Controls.Add(this.Resolution);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "netTestForm";
			this.Text = "NetTest";
			this.Load += new System.EventHandler(this.netTestForm_Load);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.netTestForm_KeyUp);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox Resolution;
		private System.Windows.Forms.Label labelHeader;
		private System.Windows.Forms.ProgressBar progress;
	}
}

