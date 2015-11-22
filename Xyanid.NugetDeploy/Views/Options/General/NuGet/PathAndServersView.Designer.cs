namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.General.NuGet
{
	partial class PathAndServersView
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
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this._uiNuGetExePath = new System.Windows.Forms.TextBox();
			this._uiSearchNuGetExe = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this._uiNuGetServers = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this._uiRemoveNuGetServer = new System.Windows.Forms.Button();
			this._uiAddNuGetServer = new System.Windows.Forms.Button();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this._uiCurrentNuGetServerUrl = new System.Windows.Forms.TextBox();
			this._uiCurrentNuGetServerApiKey = new System.Windows.Forms.TextBox();
			this._uiApplyChanges = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this._uiCurrentNuGetServerIsPreferred = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this._uiNuGetServerUsage = new System.Windows.Forms.ComboBox();
			this._openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this._toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
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
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this._uiNuGetServers, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this._uiNuGetServerUsage, 1, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(406, 278);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this._uiNuGetExePath, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this._uiSearchNuGetExe, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(115, 0);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(291, 27);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// _uiNuGetExePath
			// 
			this._uiNuGetExePath.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiNuGetExePath.Location = new System.Drawing.Point(2, 2);
			this._uiNuGetExePath.Margin = new System.Windows.Forms.Padding(2);
			this._uiNuGetExePath.Name = "_uiNuGetExePath";
			this._uiNuGetExePath.Size = new System.Drawing.Size(257, 20);
			this._uiNuGetExePath.TabIndex = 0;
			this._toolTip.SetToolTip(this._uiNuGetExePath, "Determine the path to the NuGet.exe to use for packaing the project");
			this._uiNuGetExePath.TextChanged += new System.EventHandler(this.OnChange);
			// 
			// _uiSearchNuGetExe
			// 
			this._uiSearchNuGetExe.AutoSize = true;
			this._uiSearchNuGetExe.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this._uiSearchNuGetExe.Dock = System.Windows.Forms.DockStyle.Left;
			this._uiSearchNuGetExe.Location = new System.Drawing.Point(263, 2);
			this._uiSearchNuGetExe.Margin = new System.Windows.Forms.Padding(2);
			this._uiSearchNuGetExe.Name = "_uiSearchNuGetExe";
			this._uiSearchNuGetExe.Size = new System.Drawing.Size(26, 23);
			this._uiSearchNuGetExe.TabIndex = 1;
			this._uiSearchNuGetExe.Text = "...";
			this._toolTip.SetToolTip(this._uiSearchNuGetExe, "Opens up a directory dialog to select the NuGet.exe");
			this._uiSearchNuGetExe.UseVisualStyleBackColor = true;
			this._uiSearchNuGetExe.Click += new System.EventHandler(this.OnClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(109, 27);
			this.label1.TabIndex = 1;
			this.label1.Text = "NuGet Exe:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _uiNuGetServers
			// 
			this.tableLayoutPanel1.SetColumnSpan(this._uiNuGetServers, 2);
			this._uiNuGetServers.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiNuGetServers.FormattingEnabled = true;
			this._uiNuGetServers.Location = new System.Drawing.Point(3, 84);
			this._uiNuGetServers.Name = "_uiNuGetServers";
			this._uiNuGetServers.Size = new System.Drawing.Size(400, 96);
			this._uiNuGetServers.TabIndex = 4;
			this._uiNuGetServers.SelectedValueChanged += new System.EventHandler(this.OnChange);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(3, 54);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(109, 27);
			this.label3.TabIndex = 5;
			this.label3.Text = "NuGet Servers:";
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
			this.tableLayoutPanel3.Controls.Add(this._uiRemoveNuGetServer, 2, 0);
			this.tableLayoutPanel3.Controls.Add(this._uiAddNuGetServer, 1, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Right;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(305, 54);
			this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.Size = new System.Drawing.Size(101, 27);
			this.tableLayoutPanel3.TabIndex = 6;
			// 
			// _uiRemoveNuGetServer
			// 
			this._uiRemoveNuGetServer.AutoSize = true;
			this._uiRemoveNuGetServer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this._uiRemoveNuGetServer.Dock = System.Windows.Forms.DockStyle.Right;
			this._uiRemoveNuGetServer.Enabled = false;
			this._uiRemoveNuGetServer.Location = new System.Drawing.Point(42, 2);
			this._uiRemoveNuGetServer.Margin = new System.Windows.Forms.Padding(2);
			this._uiRemoveNuGetServer.Name = "_uiRemoveNuGetServer";
			this._uiRemoveNuGetServer.Size = new System.Drawing.Size(57, 23);
			this._uiRemoveNuGetServer.TabIndex = 0;
			this._uiRemoveNuGetServer.Text = "Remove";
			this._toolTip.SetToolTip(this._uiRemoveNuGetServer, "Removes the currently selected NuGet Server from the list");
			this._uiRemoveNuGetServer.UseVisualStyleBackColor = true;
			this._uiRemoveNuGetServer.Click += new System.EventHandler(this.OnClick);
			// 
			// _uiAddNuGetServer
			// 
			this._uiAddNuGetServer.AutoSize = true;
			this._uiAddNuGetServer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this._uiAddNuGetServer.Dock = System.Windows.Forms.DockStyle.Right;
			this._uiAddNuGetServer.Location = new System.Drawing.Point(2, 2);
			this._uiAddNuGetServer.Margin = new System.Windows.Forms.Padding(2);
			this._uiAddNuGetServer.Name = "_uiAddNuGetServer";
			this._uiAddNuGetServer.Size = new System.Drawing.Size(36, 23);
			this._uiAddNuGetServer.TabIndex = 1;
			this._uiAddNuGetServer.Text = "Add";
			this._toolTip.SetToolTip(this._uiAddNuGetServer, "Adds a new NuGet server to the list");
			this._uiAddNuGetServer.UseVisualStyleBackColor = true;
			this._uiAddNuGetServer.Click += new System.EventHandler(this.OnClick);
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
			this.tableLayoutPanel4.Controls.Add(this._uiCurrentNuGetServerUrl, 1, 0);
			this.tableLayoutPanel4.Controls.Add(this._uiCurrentNuGetServerApiKey, 1, 1);
			this.tableLayoutPanel4.Controls.Add(this._uiApplyChanges, 1, 3);
			this.tableLayoutPanel4.Controls.Add(this.label2, 0, 2);
			this.tableLayoutPanel4.Controls.Add(this._uiCurrentNuGetServerIsPreferred, 1, 2);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(115, 183);
			this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 4;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.Size = new System.Drawing.Size(291, 95);
			this.tableLayoutPanel4.TabIndex = 7;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(3, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(53, 24);
			this.label4.TabIndex = 0;
			this.label4.Text = "Url:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label5.Location = new System.Drawing.Point(3, 24);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(53, 24);
			this.label5.TabIndex = 1;
			this.label5.Text = "Key:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _uiCurrentNuGetServerUrl
			// 
			this._uiCurrentNuGetServerUrl.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiCurrentNuGetServerUrl.Location = new System.Drawing.Point(61, 2);
			this._uiCurrentNuGetServerUrl.Margin = new System.Windows.Forms.Padding(2);
			this._uiCurrentNuGetServerUrl.Name = "_uiCurrentNuGetServerUrl";
			this._uiCurrentNuGetServerUrl.Size = new System.Drawing.Size(228, 20);
			this._uiCurrentNuGetServerUrl.TabIndex = 2;
			this._toolTip.SetToolTip(this._uiCurrentNuGetServerUrl, "Determines the url of the currently selected NuGet server");
			this._uiCurrentNuGetServerUrl.TextChanged += new System.EventHandler(this.OnChange);
			// 
			// _uiCurrentNuGetServerApiKey
			// 
			this._uiCurrentNuGetServerApiKey.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiCurrentNuGetServerApiKey.Location = new System.Drawing.Point(61, 26);
			this._uiCurrentNuGetServerApiKey.Margin = new System.Windows.Forms.Padding(2);
			this._uiCurrentNuGetServerApiKey.Name = "_uiCurrentNuGetServerApiKey";
			this._uiCurrentNuGetServerApiKey.Size = new System.Drawing.Size(228, 20);
			this._uiCurrentNuGetServerApiKey.TabIndex = 3;
			this._toolTip.SetToolTip(this._uiCurrentNuGetServerApiKey, "Determines the key of the currently selected NuGet server");
			this._uiCurrentNuGetServerApiKey.TextChanged += new System.EventHandler(this.OnChange);
			// 
			// _uiApplyChanges
			// 
			this._uiApplyChanges.AutoSize = true;
			this._uiApplyChanges.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this._uiApplyChanges.Dock = System.Windows.Forms.DockStyle.Right;
			this._uiApplyChanges.Enabled = false;
			this._uiApplyChanges.Location = new System.Drawing.Point(246, 70);
			this._uiApplyChanges.Margin = new System.Windows.Forms.Padding(2);
			this._uiApplyChanges.Name = "_uiApplyChanges";
			this._uiApplyChanges.Size = new System.Drawing.Size(43, 23);
			this._uiApplyChanges.TabIndex = 4;
			this._uiApplyChanges.Text = "Apply";
			this._uiApplyChanges.UseVisualStyleBackColor = true;
			this._uiApplyChanges.Click += new System.EventHandler(this.OnClick);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 20);
			this.label2.TabIndex = 5;
			this.label2.Text = "Preferred:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _uiCurrentNuGetServerIsPreferred
			// 
			this._uiCurrentNuGetServerIsPreferred.AutoSize = true;
			this._uiCurrentNuGetServerIsPreferred.Dock = System.Windows.Forms.DockStyle.Left;
			this._uiCurrentNuGetServerIsPreferred.Location = new System.Drawing.Point(62, 51);
			this._uiCurrentNuGetServerIsPreferred.Name = "_uiCurrentNuGetServerIsPreferred";
			this._uiCurrentNuGetServerIsPreferred.Size = new System.Drawing.Size(15, 14);
			this._uiCurrentNuGetServerIsPreferred.TabIndex = 6;
			this._toolTip.SetToolTip(this._uiCurrentNuGetServerIsPreferred, "Determines whether the currently selected NuGet server is the preferred NuGet ser" +
        "ver.\r\nNote: there can only be ony preferred NuGet server, applying this option w" +
        "ill unprefer the previous server");
			this._uiCurrentNuGetServerIsPreferred.UseVisualStyleBackColor = true;
			this._uiCurrentNuGetServerIsPreferred.CheckedChanged += new System.EventHandler(this.OnChange);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label6.Location = new System.Drawing.Point(3, 27);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(109, 27);
			this.label6.TabIndex = 8;
			this.label6.Text = "NuGet Server Usage:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _uiNuGetServerUsage
			// 
			this._uiNuGetServerUsage.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiNuGetServerUsage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._uiNuGetServerUsage.FormattingEnabled = true;
			this._uiNuGetServerUsage.Location = new System.Drawing.Point(118, 30);
			this._uiNuGetServerUsage.Name = "_uiNuGetServerUsage";
			this._uiNuGetServerUsage.Size = new System.Drawing.Size(285, 21);
			this._uiNuGetServerUsage.TabIndex = 9;
			this._toolTip.SetToolTip(this._uiNuGetServerUsage, "Determines which NuGet server is selected when the dialog to deploy a projects op" +
        "ens");
			this._uiNuGetServerUsage.SelectedValueChanged += new System.EventHandler(this.OnChange);
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
			// PathAndServersView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "PathAndServersView";
			this.Size = new System.Drawing.Size(406, 278);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TextBox _uiNuGetExePath;
		private System.Windows.Forms.Button _uiSearchNuGetExe;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox _uiNuGetServers;
		private System.Windows.Forms.OpenFileDialog _openFileDialog;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Button _uiRemoveNuGetServer;
		private System.Windows.Forms.Button _uiAddNuGetServer;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox _uiCurrentNuGetServerUrl;
		private System.Windows.Forms.TextBox _uiCurrentNuGetServerApiKey;
		private System.Windows.Forms.Button _uiApplyChanges;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox _uiCurrentNuGetServerIsPreferred;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox _uiNuGetServerUsage;
		private System.Windows.Forms.ToolTip _toolTip;
	}
}
