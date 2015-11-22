namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Project.NuGet.General
{
	partial class GeneralView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralView));
			this._toolTip = new System.Windows.Forms.ToolTip(this.components);
			this._uiDependencyUsage = new System.Windows.Forms.ComboBox();
			this._uiProjectIdentifiers = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this._uiProjectIdentifierLabel = new System.Windows.Forms.Label();
			this.groupbox1 = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this._uiHasTargetSpecificDependencyGroups = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupbox1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// _toolTip
			// 
			this._toolTip.AutoPopDelay = 20000;
			this._toolTip.InitialDelay = 500;
			this._toolTip.ReshowDelay = 100;
			// 
			// _uiDependencyUsage
			// 
			this._uiDependencyUsage.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiDependencyUsage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._uiDependencyUsage.FormattingEnabled = true;
			this._uiDependencyUsage.Location = new System.Drawing.Point(186, 3);
			this._uiDependencyUsage.Name = "_uiDependencyUsage";
			this._uiDependencyUsage.Size = new System.Drawing.Size(199, 21);
			this._uiDependencyUsage.TabIndex = 2;
			this._toolTip.SetToolTip(this._uiDependencyUsage, resources.GetString("_uiDependencyUsage.ToolTip"));
			this._uiDependencyUsage.SelectedValueChanged += new System.EventHandler(this.OnChange);
			// 
			// _uiProjectIdentifiers
			// 
			this._uiProjectIdentifiers.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiProjectIdentifiers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._uiProjectIdentifiers.FormattingEnabled = true;
			this._uiProjectIdentifiers.Location = new System.Drawing.Point(52, 3);
			this._uiProjectIdentifiers.Name = "_uiProjectIdentifiers";
			this._uiProjectIdentifiers.Size = new System.Drawing.Size(345, 21);
			this._uiProjectIdentifiers.TabIndex = 5;
			this._toolTip.SetToolTip(this._uiProjectIdentifiers, "Determines for which projects typs the configuration is");
			this._uiProjectIdentifiers.SelectedValueChanged += new System.EventHandler(this.OnChange);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this._uiProjectIdentifierLabel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this._uiProjectIdentifiers, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.groupbox1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(400, 300);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// _uiProjectIdentifierLabel
			// 
			this._uiProjectIdentifierLabel.AutoSize = true;
			this._uiProjectIdentifierLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiProjectIdentifierLabel.Location = new System.Drawing.Point(3, 0);
			this._uiProjectIdentifierLabel.Name = "_uiProjectIdentifierLabel";
			this._uiProjectIdentifierLabel.Size = new System.Drawing.Size(43, 27);
			this._uiProjectIdentifierLabel.TabIndex = 4;
			this._uiProjectIdentifierLabel.Text = "Project:";
			this._uiProjectIdentifierLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupbox1
			// 
			this.groupbox1.AutoSize = true;
			this.groupbox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.SetColumnSpan(this.groupbox1, 2);
			this.groupbox1.Controls.Add(this.tableLayoutPanel2);
			this.groupbox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupbox1.Location = new System.Drawing.Point(3, 30);
			this.groupbox1.Name = "groupbox1";
			this.groupbox1.Size = new System.Drawing.Size(394, 267);
			this.groupbox1.TabIndex = 6;
			this.groupbox1.TabStop = false;
			this.groupbox1.Text = "Settings";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this._uiDependencyUsage, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this._uiHasTargetSpecificDependencyGroups, 1, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 3;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(388, 248);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(177, 27);
			this.label1.TabIndex = 0;
			this.label1.Text = "DependencyUsage:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 27);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(177, 20);
			this.label2.TabIndex = 3;
			this.label2.Text = "Target specific dependency groups:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _uiHasTargetSpecificDependencyGroups
			// 
			this._uiHasTargetSpecificDependencyGroups.AutoSize = true;
			this._uiHasTargetSpecificDependencyGroups.Location = new System.Drawing.Point(186, 30);
			this._uiHasTargetSpecificDependencyGroups.Name = "_uiHasTargetSpecificDependencyGroups";
			this._uiHasTargetSpecificDependencyGroups.Size = new System.Drawing.Size(15, 14);
			this._uiHasTargetSpecificDependencyGroups.TabIndex = 4;
			this._toolTip.SetToolTip(this._uiHasTargetSpecificDependencyGroups, "Determines if other NuGet dependencies will be put into groups specific to their " +
        "framework or\r\nif they will all be put into a single group.");
			this._uiHasTargetSpecificDependencyGroups.UseVisualStyleBackColor = true;
			// 
			// GeneralView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "GeneralView";
			this.Size = new System.Drawing.Size(400, 300);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.groupbox1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolTip _toolTip;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label _uiProjectIdentifierLabel;
		private System.Windows.Forms.ComboBox _uiProjectIdentifiers;
		private System.Windows.Forms.GroupBox groupbox1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox _uiDependencyUsage;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox _uiHasTargetSpecificDependencyGroups;
	}
}
