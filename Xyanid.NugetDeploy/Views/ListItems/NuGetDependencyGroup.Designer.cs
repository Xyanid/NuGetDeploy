namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.ListItems
{
	partial class NuGetDependencyGroup
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tableLayoutPanel1 = new Xyanid.Winforms.Layout.BorderTableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this._uiTargetFramework = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this._uiItemsCount = new System.Windows.Forms.Label();
			this._uiDependencies = new Xyanid.Winforms.Selection.SelectableControlPanel();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this._uiTargetFramework, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this._uiItemsCount, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this._uiDependencies, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.InnerBorderColor = System.Drawing.Color.Black;
			this.tableLayoutPanel1.InnerBorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
			this.tableLayoutPanel1.InnerBorderThickness = 1;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.OuterBorderColor = System.Drawing.Color.Black;
			this.tableLayoutPanel1.OuterBorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
			this.tableLayoutPanel1.OuterBorderThickness = 2;
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(221, 230);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(2, 2);
			this.label1.Margin = new System.Windows.Forms.Padding(2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "Framework:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _uiTargetFramework
			// 
			this._uiTargetFramework.AutoSize = true;
			this._uiTargetFramework.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiTargetFramework.Location = new System.Drawing.Point(68, 2);
			this._uiTargetFramework.Margin = new System.Windows.Forms.Padding(2);
			this._uiTargetFramework.Name = "_uiTargetFramework";
			this._uiTargetFramework.Padding = new System.Windows.Forms.Padding(2);
			this._uiTargetFramework.Size = new System.Drawing.Size(52, 17);
			this._uiTargetFramework.TabIndex = 3;
			this._uiTargetFramework.Text = "45net";
			this._uiTargetFramework.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(124, 2);
			this.label2.Margin = new System.Windows.Forms.Padding(2);
			this.label2.Name = "label2";
			this.label2.Padding = new System.Windows.Forms.Padding(2);
			this.label2.Size = new System.Drawing.Size(39, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "Items:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _uiItemsCount
			// 
			this._uiItemsCount.AutoSize = true;
			this._uiItemsCount.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiItemsCount.Location = new System.Drawing.Point(167, 2);
			this._uiItemsCount.Margin = new System.Windows.Forms.Padding(2);
			this._uiItemsCount.Name = "_uiItemsCount";
			this._uiItemsCount.Padding = new System.Windows.Forms.Padding(2);
			this._uiItemsCount.Size = new System.Drawing.Size(52, 17);
			this._uiItemsCount.TabIndex = 3;
			this._uiItemsCount.Text = "1";
			this._uiItemsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _uiDependencies
			// 
			this._uiDependencies.BackColor = System.Drawing.SystemColors.Control;
			this.tableLayoutPanel1.SetColumnSpan(this._uiDependencies, 4);
			this._uiDependencies.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiDependencies.ItemPadding = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this._uiDependencies.Location = new System.Drawing.Point(2, 21);
			this._uiDependencies.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
			this._uiDependencies.Name = "_uiDependencies";
			this._uiDependencies.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this._uiDependencies.Size = new System.Drawing.Size(217, 207);
			this._uiDependencies.TabIndex = 1;
			this._uiDependencies.WillResizeItems = true;
			// 
			// NuGetDependencyGroup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.tableLayoutPanel1);
			this.MinimumSize = new System.Drawing.Size(200, 23);
			this.Name = "NuGetDependencyGroup";
			this.Size = new System.Drawing.Size(221, 230);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Xyanid.Winforms.Layout.BorderTableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label _uiTargetFramework;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label _uiItemsCount;
		private Xyanid.Winforms.Selection.SelectableControlPanel _uiDependencies;
	}
}
