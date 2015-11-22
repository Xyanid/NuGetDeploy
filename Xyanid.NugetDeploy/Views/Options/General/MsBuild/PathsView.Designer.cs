namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.General.MsBuild
{
	partial class PathsView
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this._uiMsBuilds = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this._uiRemoveMsBuild = new System.Windows.Forms.Button();
			this._uiAddMsBuild = new System.Windows.Forms.Button();
			this._openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this._toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this._uiMsBuilds, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(332, 129);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// _uiMsBuilds
			// 
			this.tableLayoutPanel1.SetColumnSpan(this._uiMsBuilds, 2);
			this._uiMsBuilds.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiMsBuilds.FormattingEnabled = true;
			this._uiMsBuilds.Location = new System.Drawing.Point(3, 30);
			this._uiMsBuilds.Name = "_uiMsBuilds";
			this._uiMsBuilds.Size = new System.Drawing.Size(326, 96);
			this._uiMsBuilds.TabIndex = 4;
			this._uiMsBuilds.SelectedValueChanged += new System.EventHandler(this.OnChange);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(3, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(77, 27);
			this.label3.TabIndex = 5;
			this.label3.Text = "MsBuild Paths:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.AutoSize = true;
			this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel3.ColumnCount = 3;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.Controls.Add(this._uiRemoveMsBuild, 2, 0);
			this.tableLayoutPanel3.Controls.Add(this._uiAddMsBuild, 1, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Right;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(231, 0);
			this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.Size = new System.Drawing.Size(101, 27);
			this.tableLayoutPanel3.TabIndex = 6;
			// 
			// _uiRemoveMsBuild
			// 
			this._uiRemoveMsBuild.AutoSize = true;
			this._uiRemoveMsBuild.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this._uiRemoveMsBuild.Dock = System.Windows.Forms.DockStyle.Right;
			this._uiRemoveMsBuild.Enabled = false;
			this._uiRemoveMsBuild.Location = new System.Drawing.Point(42, 2);
			this._uiRemoveMsBuild.Margin = new System.Windows.Forms.Padding(2);
			this._uiRemoveMsBuild.Name = "_uiRemoveMsBuild";
			this._uiRemoveMsBuild.Size = new System.Drawing.Size(57, 23);
			this._uiRemoveMsBuild.TabIndex = 0;
			this._uiRemoveMsBuild.Text = "Remove";
			this._toolTip.SetToolTip(this._uiRemoveMsBuild, "Removes the currently selectede msbuild.exe");
			this._uiRemoveMsBuild.UseVisualStyleBackColor = true;
			this._uiRemoveMsBuild.Click += new System.EventHandler(this.OnChange);
			// 
			// _uiAddMsBuild
			// 
			this._uiAddMsBuild.AutoSize = true;
			this._uiAddMsBuild.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this._uiAddMsBuild.Dock = System.Windows.Forms.DockStyle.Right;
			this._uiAddMsBuild.Location = new System.Drawing.Point(2, 2);
			this._uiAddMsBuild.Margin = new System.Windows.Forms.Padding(2);
			this._uiAddMsBuild.Name = "_uiAddMsBuild";
			this._uiAddMsBuild.Size = new System.Drawing.Size(36, 23);
			this._uiAddMsBuild.TabIndex = 1;
			this._uiAddMsBuild.Text = "Add";
			this._toolTip.SetToolTip(this._uiAddMsBuild, "Adds a new msbuild.exe to the list");
			this._uiAddMsBuild.UseVisualStyleBackColor = true;
			this._uiAddMsBuild.Click += new System.EventHandler(this.OnChange);
			// 
			// _openFileDialog
			// 
			this._openFileDialog.DefaultExt = "exe";
			this._openFileDialog.Filter = "Executables (*.exe)|*.exe|All files (*.*)|*.*";
			// 
			// _toolTip
			// 
			this._toolTip.AutoPopDelay = 20000;
			this._toolTip.InitialDelay = 500;
			this._toolTip.ReshowDelay = 100;
			// 
			// PathsView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "PathsView";
			this.Size = new System.Drawing.Size(332, 129);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox _uiMsBuilds;
		private System.Windows.Forms.OpenFileDialog _openFileDialog;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Button _uiRemoveMsBuild;
		private System.Windows.Forms.Button _uiAddMsBuild;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ToolTip _toolTip;
	}
}
