namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Dialogs
{
	partial class DeploymentProcess
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
			if(disposing && (components != null))
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeploymentProcess));
			this._openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this._uiProgress = new System.Windows.Forms.ProgressBar();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this._uiOk = new System.Windows.Forms.Button();
			this._uiCancel = new System.Windows.Forms.Button();
			this._uiMessages = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// _openFileDialog
			// 
			this._openFileDialog.Filter = "Executables (*.exe)|*.exe|All files (*.*)|*.*";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this._uiProgress, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this._uiMessages, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(584, 461);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// _uiProgress
			// 
			this._uiProgress.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiProgress.Location = new System.Drawing.Point(10, 379);
			this._uiProgress.Margin = new System.Windows.Forms.Padding(10);
			this._uiProgress.Name = "_uiProgress";
			this._uiProgress.Size = new System.Drawing.Size(564, 23);
			this._uiProgress.Step = 1;
			this._uiProgress.TabIndex = 1;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this._uiOk, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this._uiCancel, 2, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(10, 422);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(10);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(564, 29);
			this.tableLayoutPanel2.TabIndex = 4;
			// 
			// _uiOk
			// 
			this._uiOk.Enabled = false;
			this._uiOk.Location = new System.Drawing.Point(3, 3);
			this._uiOk.Name = "_uiOk";
			this._uiOk.Size = new System.Drawing.Size(75, 23);
			this._uiOk.TabIndex = 0;
			this._uiOk.Text = "OK";
			this._uiOk.UseVisualStyleBackColor = true;
			this._uiOk.Click += new System.EventHandler(this.OnChange);
			// 
			// _uiCancel
			// 
			this._uiCancel.Location = new System.Drawing.Point(486, 3);
			this._uiCancel.Name = "_uiCancel";
			this._uiCancel.Size = new System.Drawing.Size(75, 23);
			this._uiCancel.TabIndex = 1;
			this._uiCancel.Text = "Cancel";
			this._uiCancel.UseVisualStyleBackColor = true;
			this._uiCancel.Click += new System.EventHandler(this.OnChange);
			// 
			// _uiMessages
			// 
			this._uiMessages.AcceptsReturn = true;
			this._uiMessages.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiMessages.Location = new System.Drawing.Point(3, 3);
			this._uiMessages.Multiline = true;
			this._uiMessages.Name = "_uiMessages";
			this._uiMessages.ReadOnly = true;
			this._uiMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this._uiMessages.Size = new System.Drawing.Size(578, 363);
			this._uiMessages.TabIndex = 5;
			this._uiMessages.UseWaitCursor = true;
			this._uiMessages.WordWrap = false;
			// 
			// DeploymentProcess
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 461);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(350, 250);
			this.Name = "DeploymentProcess";
			this.Text = "Title";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
			this.Shown += new System.EventHandler(this.OnShown);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog _openFileDialog;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ProgressBar _uiProgress;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button _uiOk;
		private System.Windows.Forms.Button _uiCancel;
		private System.Windows.Forms.TextBox _uiMessages;
	}
}