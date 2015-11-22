namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Project.NuGet.NuSpec
{
	partial class FilesView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilesView));
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this._uiProjectIdentifierLabel = new System.Windows.Forms.Label();
			this._uiProjectIdentifiers = new System.Windows.Forms.ComboBox();
			this._uiFilesSettings = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this._uiFiles = new System.Windows.Forms.ListBox();
			this._uiCurrentInclude = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this._uiApplyChanges = new System.Windows.Forms.Button();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this._uiRemove = new System.Windows.Forms.Button();
			this._uiAdd = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this._uiCurrentType = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this._uiCurrentName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this._uiCurrentTarget = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this._uiCurrentFolder = new System.Windows.Forms.TextBox();
			this._toolTip = new System.Windows.Forms.ToolTip(this.components);
			this._uiUseFromExistingNuSpec = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel3.SuspendLayout();
			this._uiFilesSettings.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.AutoSize = true;
			this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel3.ColumnCount = 3;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this._uiProjectIdentifierLabel, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this._uiProjectIdentifiers, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this._uiFilesSettings, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 2, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(400, 408);
			this.tableLayoutPanel3.TabIndex = 1;
			// 
			// _uiProjectIdentifierLabel
			// 
			this._uiProjectIdentifierLabel.AutoSize = true;
			this._uiProjectIdentifierLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiProjectIdentifierLabel.Location = new System.Drawing.Point(3, 0);
			this._uiProjectIdentifierLabel.Name = "_uiProjectIdentifierLabel";
			this._uiProjectIdentifierLabel.Size = new System.Drawing.Size(43, 30);
			this._uiProjectIdentifierLabel.TabIndex = 0;
			this._uiProjectIdentifierLabel.Text = "Project:";
			this._uiProjectIdentifierLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _uiProjectIdentifiers
			// 
			this._uiProjectIdentifiers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._uiProjectIdentifiers.FormattingEnabled = true;
			this._uiProjectIdentifiers.Location = new System.Drawing.Point(52, 3);
			this._uiProjectIdentifiers.Name = "_uiProjectIdentifiers";
			this._uiProjectIdentifiers.Size = new System.Drawing.Size(154, 21);
			this._uiProjectIdentifiers.TabIndex = 1;
			this._toolTip.SetToolTip(this._uiProjectIdentifiers, "Determines for which projects typs the configuration is");
			this._uiProjectIdentifiers.SelectedValueChanged += new System.EventHandler(this.OnChange);
			// 
			// _uiFilesSettings
			// 
			this._uiFilesSettings.AutoSize = true;
			this._uiFilesSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel3.SetColumnSpan(this._uiFilesSettings, 3);
			this._uiFilesSettings.Controls.Add(this.tableLayoutPanel1);
			this._uiFilesSettings.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiFilesSettings.Location = new System.Drawing.Point(3, 33);
			this._uiFilesSettings.Name = "_uiFilesSettings";
			this._uiFilesSettings.Size = new System.Drawing.Size(394, 372);
			this._uiFilesSettings.TabIndex = 8;
			this._uiFilesSettings.TabStop = false;
			this._uiFilesSettings.Text = "Elements to use";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this._uiFiles, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this._uiCurrentInclude, 1, 6);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this._uiApplyChanges, 1, 7);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this._uiCurrentType, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this._uiCurrentName, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this._uiCurrentTarget, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this._uiCurrentFolder, 1, 3);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 8;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(388, 353);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// _uiFiles
			// 
			this.tableLayoutPanel1.SetColumnSpan(this._uiFiles, 2);
			this._uiFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiFiles.FormattingEnabled = true;
			this._uiFiles.Location = new System.Drawing.Point(0, 29);
			this._uiFiles.Margin = new System.Windows.Forms.Padding(0);
			this._uiFiles.Name = "_uiFiles";
			this._uiFiles.Size = new System.Drawing.Size(388, 170);
			this._uiFiles.TabIndex = 0;
			this._uiFiles.SelectedValueChanged += new System.EventHandler(this.OnChange);
			// 
			// _uiCurrentInclude
			// 
			this._uiCurrentInclude.AutoSize = true;
			this._uiCurrentInclude.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiCurrentInclude.Enabled = false;
			this._uiCurrentInclude.Location = new System.Drawing.Point(51, 307);
			this._uiCurrentInclude.Name = "_uiCurrentInclude";
			this._uiCurrentInclude.Size = new System.Drawing.Size(334, 14);
			this._uiCurrentInclude.TabIndex = 1;
			this._toolTip.SetToolTip(this._uiCurrentInclude, resources.GetString("_uiCurrentInclude.ToolTip"));
			this._uiCurrentInclude.UseVisualStyleBackColor = true;
			this._uiCurrentInclude.CheckedChanged += new System.EventHandler(this.OnChange);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 304);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(42, 20);
			this.label2.TabIndex = 3;
			this.label2.Text = "Include";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _uiApplyChanges
			// 
			this._uiApplyChanges.AutoSize = true;
			this._uiApplyChanges.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this._uiApplyChanges.Dock = System.Windows.Forms.DockStyle.Right;
			this._uiApplyChanges.Enabled = false;
			this._uiApplyChanges.Location = new System.Drawing.Point(342, 327);
			this._uiApplyChanges.Name = "_uiApplyChanges";
			this._uiApplyChanges.Size = new System.Drawing.Size(43, 23);
			this._uiApplyChanges.TabIndex = 5;
			this._uiApplyChanges.Text = "Apply";
			this._toolTip.SetToolTip(this._uiApplyChanges, "Applies the changes to the currently selected file rule");
			this._uiApplyChanges.UseVisualStyleBackColor = true;
			this._uiApplyChanges.Click += new System.EventHandler(this.OnClick);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this._uiRemove, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this._uiAdd, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(48, 0);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(340, 29);
			this.tableLayoutPanel2.TabIndex = 8;
			// 
			// _uiRemove
			// 
			this._uiRemove.AutoSize = true;
			this._uiRemove.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this._uiRemove.Enabled = false;
			this._uiRemove.Location = new System.Drawing.Point(280, 3);
			this._uiRemove.Name = "_uiRemove";
			this._uiRemove.Size = new System.Drawing.Size(57, 23);
			this._uiRemove.TabIndex = 0;
			this._uiRemove.Text = "Remove";
			this._toolTip.SetToolTip(this._uiRemove, "Removes the currently selected file rule");
			this._uiRemove.UseVisualStyleBackColor = true;
			this._uiRemove.Click += new System.EventHandler(this.OnClick);
			// 
			// _uiAdd
			// 
			this._uiAdd.AutoSize = true;
			this._uiAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this._uiAdd.Dock = System.Windows.Forms.DockStyle.Right;
			this._uiAdd.Location = new System.Drawing.Point(238, 3);
			this._uiAdd.Name = "_uiAdd";
			this._uiAdd.Size = new System.Drawing.Size(36, 23);
			this._uiAdd.TabIndex = 1;
			this._uiAdd.Text = "Add";
			this._toolTip.SetToolTip(this._uiAdd, "Adds a new file rule to the list");
			this._uiAdd.UseVisualStyleBackColor = true;
			this._uiAdd.Click += new System.EventHandler(this.OnClick);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label5.Location = new System.Drawing.Point(3, 199);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(42, 27);
			this.label5.TabIndex = 9;
			this.label5.Text = "Type";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _uiCurrentType
			// 
			this._uiCurrentType.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiCurrentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._uiCurrentType.Enabled = false;
			this._uiCurrentType.FormattingEnabled = true;
			this._uiCurrentType.Location = new System.Drawing.Point(51, 202);
			this._uiCurrentType.Name = "_uiCurrentType";
			this._uiCurrentType.Size = new System.Drawing.Size(334, 21);
			this._uiCurrentType.TabIndex = 10;
			this._toolTip.SetToolTip(this._uiCurrentType, "Determines the type of the item");
			this._uiCurrentType.SelectedIndexChanged += new System.EventHandler(this.OnChange);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(3, 252);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(42, 26);
			this.label4.TabIndex = 6;
			this.label4.Text = "Name";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _uiCurrentName
			// 
			this._uiCurrentName.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiCurrentName.Enabled = false;
			this._uiCurrentName.Location = new System.Drawing.Point(51, 255);
			this._uiCurrentName.Name = "_uiCurrentName";
			this._uiCurrentName.Size = new System.Drawing.Size(334, 20);
			this._uiCurrentName.TabIndex = 7;
			this._toolTip.SetToolTip(this._uiCurrentName, "Determines the name of the file, this is itended to be used for wildcards (e.g. *" +
        ".txt)");
			this._uiCurrentName.TextChanged += new System.EventHandler(this.OnChange);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(3, 278);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(42, 26);
			this.label3.TabIndex = 4;
			this.label3.Text = "Target";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _uiCurrentTarget
			// 
			this._uiCurrentTarget.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiCurrentTarget.Enabled = false;
			this._uiCurrentTarget.Location = new System.Drawing.Point(51, 281);
			this._uiCurrentTarget.Name = "_uiCurrentTarget";
			this._uiCurrentTarget.Size = new System.Drawing.Size(334, 20);
			this._uiCurrentTarget.TabIndex = 2;
			this._toolTip.SetToolTip(this._uiCurrentTarget, "Determines the target under which items of this type\r\nwill appear in the package " +
        "(e.g. lib/content).");
			this._uiCurrentTarget.TextChanged += new System.EventHandler(this.OnChange);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label6.Location = new System.Drawing.Point(3, 226);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(42, 26);
			this.label6.TabIndex = 11;
			this.label6.Text = "Folder";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _uiCurrentFolder
			// 
			this._uiCurrentFolder.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiCurrentFolder.Location = new System.Drawing.Point(51, 229);
			this._uiCurrentFolder.Name = "_uiCurrentFolder";
			this._uiCurrentFolder.Size = new System.Drawing.Size(334, 20);
			this._uiCurrentFolder.TabIndex = 12;
			this._toolTip.SetToolTip(this._uiCurrentFolder, "determines in which folder the item must appear to be used.\r\nThe folders base is " +
        "the project root and also supports wildcards. (e.g /A/*/B)\r\n");
			this._uiCurrentFolder.TextChanged += new System.EventHandler(this.OnChange);
			// 
			// _toolTip
			// 
			this._toolTip.AutoPopDelay = 20000;
			this._toolTip.InitialDelay = 500;
			this._toolTip.ReshowDelay = 100;
			// 
			// _uiUseFromExistingNuSpec
			// 
			this._uiUseFromExistingNuSpec.Appearance = System.Windows.Forms.Appearance.Button;
			this._uiUseFromExistingNuSpec.BackgroundImage = global::Xyanid.VisualStudioExtension.NuGetDeploy.Resources.getFormNuSpecDisabled;
			this._uiUseFromExistingNuSpec.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this._uiUseFromExistingNuSpec.Dock = System.Windows.Forms.DockStyle.Left;
			this._uiUseFromExistingNuSpec.Location = new System.Drawing.Point(139, 3);
			this._uiUseFromExistingNuSpec.Name = "_uiUseFromExistingNuSpec";
			this._uiUseFromExistingNuSpec.Size = new System.Drawing.Size(24, 24);
			this._uiUseFromExistingNuSpec.TabIndex = 1;
			this._toolTip.SetToolTip(this._uiUseFromExistingNuSpec, "Determines whether files for the package will be use from the existing NuSpec fil" +
        "e of the project. ");
			this._uiUseFromExistingNuSpec.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(130, 30);
			this.label1.TabIndex = 0;
			this.label1.Text = "use from existing NuSpec:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.AutoSize = true;
			this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel4.ColumnCount = 2;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this._uiUseFromExistingNuSpec, 1, 0);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(209, 0);
			this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 1;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(191, 30);
			this.tableLayoutPanel4.TabIndex = 0;
			// 
			// FilesView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel3);
			this.Name = "FilesView";
			this.Size = new System.Drawing.Size(400, 408);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this._uiFilesSettings.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Label _uiProjectIdentifierLabel;
		private System.Windows.Forms.GroupBox _uiFilesSettings;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ToolTip _toolTip;
		private System.Windows.Forms.ListBox _uiFiles;
		private System.Windows.Forms.CheckBox _uiCurrentInclude;
		private System.Windows.Forms.TextBox _uiCurrentTarget;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button _uiApplyChanges;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox _uiCurrentName;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button _uiRemove;
		private System.Windows.Forms.Button _uiAdd;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox _uiCurrentType;
		private System.Windows.Forms.ComboBox _uiProjectIdentifiers;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox _uiCurrentFolder;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox _uiUseFromExistingNuSpec;
	}
}
