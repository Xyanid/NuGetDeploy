namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.Options.Project.MsBuild
{
	partial class UsagesView
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
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this._uiProjectIdentifierLabel = new System.Windows.Forms.Label();
			this._uiProjectIdentifiers = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this._uiDebugInfoUseage = new System.Windows.Forms.ComboBox();
			this._uiDebugConstantsUseage = new System.Windows.Forms.ComboBox();
			this._uiOptimizeUseage = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this._uiOptimizeValue = new System.Windows.Forms.CheckBox();
			this._uiDebugConstantsValue = new System.Windows.Forms.TextBox();
			this._uiDebugInfoValue = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label7 = new System.Windows.Forms.Label();
			this._imageDebugInfo = new System.Windows.Forms.PictureBox();
			this._toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.tableLayoutPanel3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._imageDebugInfo)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.AutoSize = true;
			this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.Controls.Add(this._uiProjectIdentifierLabel, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this._uiProjectIdentifiers, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.groupBox1, 0, 1);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(500, 408);
			this.tableLayoutPanel3.TabIndex = 1;
			// 
			// _uiProjectIdentifierLabel
			// 
			this._uiProjectIdentifierLabel.AutoSize = true;
			this._uiProjectIdentifierLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiProjectIdentifierLabel.Location = new System.Drawing.Point(3, 0);
			this._uiProjectIdentifierLabel.Name = "_uiProjectIdentifierLabel";
			this._uiProjectIdentifierLabel.Size = new System.Drawing.Size(43, 27);
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
			this._uiProjectIdentifiers.SelectedValueChanged += new System.EventHandler(this.OnChangeIdentifier);
			// 
			// groupBox1
			// 
			this.groupBox1.AutoSize = true;
			this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel3.SetColumnSpan(this.groupBox1, 2);
			this.groupBox1.Controls.Add(this.tableLayoutPanel2);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(3, 30);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(494, 375);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Options to use";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoScroll = true;
			this.tableLayoutPanel2.ColumnCount = 4;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.label5, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this._uiDebugInfoUseage, 1, 2);
			this.tableLayoutPanel2.Controls.Add(this._uiDebugConstantsUseage, 1, 1);
			this.tableLayoutPanel2.Controls.Add(this._uiOptimizeUseage, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.label3, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.label4, 2, 1);
			this.tableLayoutPanel2.Controls.Add(this.label6, 2, 2);
			this.tableLayoutPanel2.Controls.Add(this._uiOptimizeValue, 3, 0);
			this.tableLayoutPanel2.Controls.Add(this._uiDebugConstantsValue, 3, 1);
			this.tableLayoutPanel2.Controls.Add(this._uiDebugInfoValue, 3, 2);
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 2);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 4;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(488, 356);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(86, 27);
			this.label2.TabIndex = 0;
			this.label2.Text = "Optimize";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label5.Location = new System.Drawing.Point(3, 27);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(86, 27);
			this.label5.TabIndex = 2;
			this.label5.Text = "DebugConstants";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _uiDebugInfoUseage
			// 
			this._uiDebugInfoUseage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._uiDebugInfoUseage.FormattingEnabled = true;
			this._uiDebugInfoUseage.Location = new System.Drawing.Point(95, 57);
			this._uiDebugInfoUseage.Name = "_uiDebugInfoUseage";
			this._uiDebugInfoUseage.Size = new System.Drawing.Size(100, 21);
			this._uiDebugInfoUseage.TabIndex = 7;
			this._uiDebugInfoUseage.SelectedValueChanged += new System.EventHandler(this.OnChangeUseage);
			// 
			// _uiDebugConstantsUseage
			// 
			this._uiDebugConstantsUseage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._uiDebugConstantsUseage.FormattingEnabled = true;
			this._uiDebugConstantsUseage.Location = new System.Drawing.Point(95, 30);
			this._uiDebugConstantsUseage.Name = "_uiDebugConstantsUseage";
			this._uiDebugConstantsUseage.Size = new System.Drawing.Size(100, 21);
			this._uiDebugConstantsUseage.TabIndex = 7;
			this._uiDebugConstantsUseage.SelectedValueChanged += new System.EventHandler(this.OnChangeUseage);
			// 
			// _uiOptimizeUseage
			// 
			this._uiOptimizeUseage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._uiOptimizeUseage.FormattingEnabled = true;
			this._uiOptimizeUseage.Location = new System.Drawing.Point(95, 3);
			this._uiOptimizeUseage.Name = "_uiOptimizeUseage";
			this._uiOptimizeUseage.Size = new System.Drawing.Size(100, 21);
			this._uiOptimizeUseage.TabIndex = 7;
			this._uiOptimizeUseage.SelectedValueChanged += new System.EventHandler(this.OnChangeUseage);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(201, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(34, 27);
			this.label3.TabIndex = 8;
			this.label3.Text = "Value";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(201, 27);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(34, 27);
			this.label4.TabIndex = 9;
			this.label4.Text = "Value";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label6.Location = new System.Drawing.Point(201, 54);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(34, 27);
			this.label6.TabIndex = 10;
			this.label6.Text = "Value";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _uiOptimizeValue
			// 
			this._uiOptimizeValue.AutoSize = true;
			this._uiOptimizeValue.Dock = System.Windows.Forms.DockStyle.Left;
			this._uiOptimizeValue.Location = new System.Drawing.Point(241, 3);
			this._uiOptimizeValue.Name = "_uiOptimizeValue";
			this._uiOptimizeValue.Size = new System.Drawing.Size(15, 21);
			this._uiOptimizeValue.TabIndex = 11;
			this._uiOptimizeValue.UseVisualStyleBackColor = true;
			this._uiOptimizeValue.CheckedChanged += new System.EventHandler(this.OnChangeValue);
			// 
			// _uiDebugConstantsValue
			// 
			this._uiDebugConstantsValue.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiDebugConstantsValue.Location = new System.Drawing.Point(241, 30);
			this._uiDebugConstantsValue.Name = "_uiDebugConstantsValue";
			this._uiDebugConstantsValue.Size = new System.Drawing.Size(244, 20);
			this._uiDebugConstantsValue.TabIndex = 12;
			this._uiDebugConstantsValue.TextChanged += new System.EventHandler(this.OnChangeValue);
			// 
			// _uiDebugInfoValue
			// 
			this._uiDebugInfoValue.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiDebugInfoValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._uiDebugInfoValue.FormattingEnabled = true;
			this._uiDebugInfoValue.Location = new System.Drawing.Point(241, 57);
			this._uiDebugInfoValue.Name = "_uiDebugInfoValue";
			this._uiDebugInfoValue.Size = new System.Drawing.Size(244, 21);
			this._uiDebugInfoValue.TabIndex = 13;
			this._uiDebugInfoValue.SelectedValueChanged += new System.EventHandler(this.OnChangeValue);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.label7, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this._imageDebugInfo, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 54);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(92, 27);
			this.tableLayoutPanel1.TabIndex = 14;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label7.Location = new System.Drawing.Point(3, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(60, 27);
			this.label7.TabIndex = 0;
			this.label7.Text = "Debug Info";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _imageDebugInfo
			// 
			this._imageDebugInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this._imageDebugInfo.Image = global::Xyanid.VisualStudioExtension.NuGetDeploy.Resources.warning;
			this._imageDebugInfo.Location = new System.Drawing.Point(68, 2);
			this._imageDebugInfo.Margin = new System.Windows.Forms.Padding(2);
			this._imageDebugInfo.Name = "_imageDebugInfo";
			this._imageDebugInfo.Size = new System.Drawing.Size(22, 23);
			this._imageDebugInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this._imageDebugInfo.TabIndex = 1;
			this._imageDebugInfo.TabStop = false;
			this._toolTip.SetToolTip(this._imageDebugInfo, "Pdb Files will only be added if the NuSpec Files option has been disabled and the" +
        " debug info value is not set to none");
			this._imageDebugInfo.Visible = false;
			// 
			// _toolTip
			// 
			this._toolTip.AutoPopDelay = 20000;
			this._toolTip.InitialDelay = 500;
			this._toolTip.ReshowDelay = 100;
			// 
			// UsagesView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.Controls.Add(this.tableLayoutPanel3);
			this.Name = "UsagesView";
			this.Size = new System.Drawing.Size(500, 408);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._imageDebugInfo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Label _uiProjectIdentifierLabel;
		private System.Windows.Forms.ComboBox _uiProjectIdentifiers;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ToolTip _toolTip;
		private System.Windows.Forms.ComboBox _uiDebugInfoUseage;
		private System.Windows.Forms.ComboBox _uiDebugConstantsUseage;
		private System.Windows.Forms.ComboBox _uiOptimizeUseage;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox _uiOptimizeValue;
		private System.Windows.Forms.TextBox _uiDebugConstantsValue;
		private System.Windows.Forms.ComboBox _uiDebugInfoValue;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.PictureBox _imageDebugInfo;
	}
}
