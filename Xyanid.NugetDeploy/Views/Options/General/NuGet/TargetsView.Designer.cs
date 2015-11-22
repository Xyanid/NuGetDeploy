namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.General.NuGet
{
	partial class TargetsView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TargetsView));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this._uiTargets = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this._uiRemoveTarget = new System.Windows.Forms.Button();
			this._uiAddTarget = new System.Windows.Forms.Button();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this._uiCurrentTargetName = new System.Windows.Forms.TextBox();
			this._uiCurrentTargetMoniker = new System.Windows.Forms.TextBox();
			this._uiApplyChanges = new System.Windows.Forms.Button();
			this._openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this._uiTargets, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 1, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(372, 204);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// _uiTargets
			// 
			this.tableLayoutPanel1.SetColumnSpan(this._uiTargets, 2);
			this._uiTargets.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiTargets.FormattingEnabled = true;
			this._uiTargets.Location = new System.Drawing.Point(3, 30);
			this._uiTargets.Name = "_uiTargets";
			this._uiTargets.Size = new System.Drawing.Size(366, 96);
			this._uiTargets.TabIndex = 4;
			this._uiTargets.SelectedValueChanged += new System.EventHandler(this.OnChange);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(3, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 27);
			this.label3.TabIndex = 5;
			this.label3.Text = "NuGet Targets:";
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
			this.tableLayoutPanel3.Controls.Add(this._uiRemoveTarget, 2, 0);
			this.tableLayoutPanel3.Controls.Add(this._uiAddTarget, 1, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Right;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(271, 0);
			this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.Size = new System.Drawing.Size(101, 27);
			this.tableLayoutPanel3.TabIndex = 6;
			// 
			// _uiRemoveTarget
			// 
			this._uiRemoveTarget.AutoSize = true;
			this._uiRemoveTarget.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this._uiRemoveTarget.Dock = System.Windows.Forms.DockStyle.Right;
			this._uiRemoveTarget.Enabled = false;
			this._uiRemoveTarget.Location = new System.Drawing.Point(42, 2);
			this._uiRemoveTarget.Margin = new System.Windows.Forms.Padding(2);
			this._uiRemoveTarget.Name = "_uiRemoveTarget";
			this._uiRemoveTarget.Size = new System.Drawing.Size(57, 23);
			this._uiRemoveTarget.TabIndex = 0;
			this._uiRemoveTarget.Text = "Remove";
			this.toolTip1.SetToolTip(this._uiRemoveTarget, "Removes the currently selected NuGet target");
			this._uiRemoveTarget.UseVisualStyleBackColor = true;
			this._uiRemoveTarget.Click += new System.EventHandler(this.OnClick);
			// 
			// _uiAddTarget
			// 
			this._uiAddTarget.AutoSize = true;
			this._uiAddTarget.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this._uiAddTarget.Dock = System.Windows.Forms.DockStyle.Right;
			this._uiAddTarget.Location = new System.Drawing.Point(2, 2);
			this._uiAddTarget.Margin = new System.Windows.Forms.Padding(2);
			this._uiAddTarget.Name = "_uiAddTarget";
			this._uiAddTarget.Size = new System.Drawing.Size(36, 23);
			this._uiAddTarget.TabIndex = 1;
			this._uiAddTarget.Text = "Add";
			this.toolTip1.SetToolTip(this._uiAddTarget, "Adds a new NuGet target to the list");
			this._uiAddTarget.UseVisualStyleBackColor = true;
			this._uiAddTarget.Click += new System.EventHandler(this.OnClick);
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.AutoSize = true;
			this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel4.ColumnCount = 2;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Controls.Add(this.label4, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.label5, 0, 1);
			this.tableLayoutPanel4.Controls.Add(this._uiCurrentTargetName, 1, 0);
			this.tableLayoutPanel4.Controls.Add(this._uiCurrentTargetMoniker, 1, 1);
			this.tableLayoutPanel4.Controls.Add(this._uiApplyChanges, 1, 2);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(86, 129);
			this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 3;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.Size = new System.Drawing.Size(286, 75);
			this.tableLayoutPanel4.TabIndex = 7;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(3, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 24);
			this.label4.TabIndex = 0;
			this.label4.Text = "Name:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label5.Location = new System.Drawing.Point(3, 24);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 24);
			this.label5.TabIndex = 1;
			this.label5.Text = "Moniker:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _uiCurrentTargetName
			// 
			this._uiCurrentTargetName.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiCurrentTargetName.Location = new System.Drawing.Point(56, 2);
			this._uiCurrentTargetName.Margin = new System.Windows.Forms.Padding(2);
			this._uiCurrentTargetName.Name = "_uiCurrentTargetName";
			this._uiCurrentTargetName.ReadOnly = true;
			this._uiCurrentTargetName.Size = new System.Drawing.Size(228, 20);
			this._uiCurrentTargetName.TabIndex = 2;
			this.toolTip1.SetToolTip(this._uiCurrentTargetName, resources.GetString("_uiCurrentTargetName.ToolTip"));
			this._uiCurrentTargetName.TextChanged += new System.EventHandler(this.OnChange);
			// 
			// _uiCurrentTargetMoniker
			// 
			this._uiCurrentTargetMoniker.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiCurrentTargetMoniker.Location = new System.Drawing.Point(56, 26);
			this._uiCurrentTargetMoniker.Margin = new System.Windows.Forms.Padding(2);
			this._uiCurrentTargetMoniker.Name = "_uiCurrentTargetMoniker";
			this._uiCurrentTargetMoniker.ReadOnly = true;
			this._uiCurrentTargetMoniker.Size = new System.Drawing.Size(228, 20);
			this._uiCurrentTargetMoniker.TabIndex = 3;
			this.toolTip1.SetToolTip(this._uiCurrentTargetMoniker, resources.GetString("_uiCurrentTargetMoniker.ToolTip"));
			this._uiCurrentTargetMoniker.TextChanged += new System.EventHandler(this.OnChange);
			// 
			// _uiApplyChanges
			// 
			this._uiApplyChanges.AutoSize = true;
			this._uiApplyChanges.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this._uiApplyChanges.Dock = System.Windows.Forms.DockStyle.Right;
			this._uiApplyChanges.Enabled = false;
			this._uiApplyChanges.Location = new System.Drawing.Point(241, 50);
			this._uiApplyChanges.Margin = new System.Windows.Forms.Padding(2);
			this._uiApplyChanges.Name = "_uiApplyChanges";
			this._uiApplyChanges.Size = new System.Drawing.Size(43, 23);
			this._uiApplyChanges.TabIndex = 4;
			this._uiApplyChanges.Text = "Apply";
			this.toolTip1.SetToolTip(this._uiApplyChanges, "Applies the changed made to the currently selected NuGet target");
			this._uiApplyChanges.UseVisualStyleBackColor = true;
			this._uiApplyChanges.Click += new System.EventHandler(this.OnClick);
			// 
			// _openFileDialog
			// 
			this._openFileDialog.DefaultExt = "exe";
			this._openFileDialog.Filter = "Executables (*.exe)|*.exe|All files (*.*)|*.*";
			// 
			// toolTip1
			// 
			this.toolTip1.AutoPopDelay = 20000;
			this.toolTip1.InitialDelay = 500;
			this.toolTip1.ReshowDelay = 100;
			// 
			// TargetsView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "TargetsView";
			this.Size = new System.Drawing.Size(372, 204);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox _uiTargets;
		private System.Windows.Forms.OpenFileDialog _openFileDialog;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Button _uiRemoveTarget;
		private System.Windows.Forms.Button _uiAddTarget;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox _uiCurrentTargetName;
		private System.Windows.Forms.TextBox _uiCurrentTargetMoniker;
		private System.Windows.Forms.Button _uiApplyChanges;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}
