namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Project.General
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
			this._uiIncrement = new System.Windows.Forms.ComboBox();
			this._uiIncrementOverflow = new System.Windows.Forms.CheckBox();
			this._uiProjectIdentifiers = new System.Windows.Forms.ComboBox();
			this._uiVersionAttribute = new System.Windows.Forms.ComboBox();
			this._uiStorage = new System.Windows.Forms.ComboBox();
			this._uiFilename = new System.Windows.Forms.TextBox();
			this._uiSaveAllVersions = new System.Windows.Forms.CheckBox();
			this._uiInformationalSeparator = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this._uiProjectIdentifierLabel = new System.Windows.Forms.Label();
			this.groupbox1 = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
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
			// _uiIncrement
			// 
			this._uiIncrement.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiIncrement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._uiIncrement.FormattingEnabled = true;
			this._uiIncrement.Location = new System.Drawing.Point(123, 30);
			this._uiIncrement.Name = "_uiIncrement";
			this._uiIncrement.Size = new System.Drawing.Size(85, 21);
			this._uiIncrement.TabIndex = 2;
			this._toolTip.SetToolTip(this._uiIncrement, "Determines if the version shall be increased on deploy and which part will be inc" +
        "reased");
			this._uiIncrement.SelectedValueChanged += new System.EventHandler(this.OnChange);
			// 
			// _uiIncrementOverflow
			// 
			this._uiIncrementOverflow.AutoSize = true;
			this._uiIncrementOverflow.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiIncrementOverflow.Location = new System.Drawing.Point(299, 30);
			this._uiIncrementOverflow.Name = "_uiIncrementOverflow";
			this._uiIncrementOverflow.Size = new System.Drawing.Size(86, 21);
			this._uiIncrementOverflow.TabIndex = 8;
			this._toolTip.SetToolTip(this._uiIncrementOverflow, resources.GetString("_uiIncrementOverflow.ToolTip"));
			this._uiIncrementOverflow.UseVisualStyleBackColor = true;
			this._uiIncrementOverflow.CheckedChanged += new System.EventHandler(this.OnChange);
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
			// _uiVersionAttribute
			// 
			this.tableLayoutPanel2.SetColumnSpan(this._uiVersionAttribute, 3);
			this._uiVersionAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiVersionAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._uiVersionAttribute.FormattingEnabled = true;
			this._uiVersionAttribute.Location = new System.Drawing.Point(123, 57);
			this._uiVersionAttribute.Name = "_uiVersionAttribute";
			this._uiVersionAttribute.Size = new System.Drawing.Size(262, 21);
			this._uiVersionAttribute.TabIndex = 10;
			this._toolTip.SetToolTip(this._uiVersionAttribute, resources.GetString("_uiVersionAttribute.ToolTip"));
			this._uiVersionAttribute.SelectedValueChanged += new System.EventHandler(this.OnChange);
			// 
			// _uiStorage
			// 
			this._uiStorage.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._uiStorage.FormattingEnabled = true;
			this._uiStorage.Location = new System.Drawing.Point(123, 3);
			this._uiStorage.Name = "_uiStorage";
			this._uiStorage.Size = new System.Drawing.Size(85, 21);
			this._uiStorage.TabIndex = 12;
			this._toolTip.SetToolTip(this._uiStorage, resources.GetString("_uiStorage.ToolTip"));
			this._uiStorage.SelectedValueChanged += new System.EventHandler(this.OnChange);
			// 
			// _uiFilename
			// 
			this._uiFilename.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiFilename.Enabled = false;
			this._uiFilename.Location = new System.Drawing.Point(299, 3);
			this._uiFilename.MaxLength = 50;
			this._uiFilename.Name = "_uiFilename";
			this._uiFilename.Size = new System.Drawing.Size(86, 20);
			this._uiFilename.TabIndex = 13;
			this._toolTip.SetToolTip(this._uiFilename, "Determines the filename of the settings file that will be added to the project.\r\n" +
        "This option is only used when stroage has be set to project bases\r\n");
			this._uiFilename.TextChanged += new System.EventHandler(this.OnChange);
			this._uiFilename.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
			// 
			// _uiSaveAllVersions
			// 
			this._uiSaveAllVersions.AutoSize = true;
			this._uiSaveAllVersions.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiSaveAllVersions.Location = new System.Drawing.Point(123, 110);
			this._uiSaveAllVersions.Name = "_uiSaveAllVersions";
			this._uiSaveAllVersions.Size = new System.Drawing.Size(85, 14);
			this._uiSaveAllVersions.TabIndex = 16;
			this._toolTip.SetToolTip(this._uiSaveAllVersions, resources.GetString("_uiSaveAllVersions.ToolTip"));
			this._uiSaveAllVersions.UseVisualStyleBackColor = true;
			this._uiSaveAllVersions.CheckedChanged += new System.EventHandler(this.OnChange);
			// 
			// _uiInformationalSeparator
			// 
			this._uiInformationalSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiInformationalSeparator.Location = new System.Drawing.Point(123, 84);
			this._uiInformationalSeparator.MaxLength = 25;
			this._uiInformationalSeparator.Name = "_uiInformationalSeparator";
			this._uiInformationalSeparator.Size = new System.Drawing.Size(85, 20);
			this._uiInformationalSeparator.TabIndex = 18;
			this._toolTip.SetToolTip(this._uiInformationalSeparator, resources.GetString("_uiInformationalSeparator.ToolTip"));
			this._uiInformationalSeparator.TextChanged += new System.EventHandler(this.OnChange);
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
			this.tableLayoutPanel2.ColumnCount = 4;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this._uiIncrement, 1, 1);
			this.tableLayoutPanel2.Controls.Add(this.label5, 2, 1);
			this.tableLayoutPanel2.Controls.Add(this._uiIncrementOverflow, 3, 1);
			this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this._uiVersionAttribute, 1, 2);
			this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this._uiStorage, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this._uiFilename, 3, 0);
			this.tableLayoutPanel2.Controls.Add(this.label4, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.label6, 0, 4);
			this.tableLayoutPanel2.Controls.Add(this._uiSaveAllVersions, 1, 4);
			this.tableLayoutPanel2.Controls.Add(this.label7, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this._uiInformationalSeparator, 1, 3);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 6;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
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
			this.label1.Location = new System.Drawing.Point(3, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(114, 27);
			this.label1.TabIndex = 0;
			this.label1.Text = "Version Increment:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label5.Location = new System.Drawing.Point(214, 27);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(79, 27);
			this.label5.TabIndex = 7;
			this.label5.Text = "allow Overflow:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(114, 27);
			this.label2.TabIndex = 9;
			this.label2.Text = "Version Attribute:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(3, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(114, 27);
			this.label3.TabIndex = 11;
			this.label3.Text = "Storage:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(214, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(79, 27);
			this.label4.TabIndex = 14;
			this.label4.Text = "Filename:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label6.Location = new System.Drawing.Point(3, 107);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(114, 20);
			this.label6.TabIndex = 15;
			this.label6.Text = "Save all Versions:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label7.Location = new System.Drawing.Point(3, 81);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(114, 26);
			this.label7.TabIndex = 17;
			this.label7.Text = "Informational separator";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
		private System.Windows.Forms.ComboBox _uiIncrement;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox _uiIncrementOverflow;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox _uiVersionAttribute;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox _uiStorage;
		private System.Windows.Forms.TextBox _uiFilename;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox _uiSaveAllVersions;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox _uiInformationalSeparator;
	}
}
