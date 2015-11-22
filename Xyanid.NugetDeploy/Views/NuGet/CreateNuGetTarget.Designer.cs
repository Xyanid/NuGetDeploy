namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.NuGet
{
	partial class CreateNuGetTarget
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateNuGetTarget));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this._uiName = new System.Windows.Forms.TextBox();
			this._uiMoniker = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this._uiOk = new System.Windows.Forms.Button();
			this._uiCancel = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this._uiToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this._uiName, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this._uiMoniker, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(284, 132);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// _uiName
			// 
			this._uiName.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiName.Location = new System.Drawing.Point(57, 54);
			this._uiName.Name = "_uiName";
			this._uiName.Size = new System.Drawing.Size(224, 20);
			this._uiName.TabIndex = 0;
			this._uiToolTip.SetToolTip(this._uiName, "This value will be used as the directoty within the package under which content w" +
        "ill be placed.\r\nE.g. net4.0 will result put all the content under \\net4.0 within" +
        " the package.");
			this._uiName.TextChanged += new System.EventHandler(this.OnChange);
			// 
			// _uiMoniker
			// 
			this._uiMoniker.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiMoniker.Location = new System.Drawing.Point(57, 80);
			this._uiMoniker.Name = "_uiMoniker";
			this._uiMoniker.ReadOnly = true;
			this._uiMoniker.Size = new System.Drawing.Size(224, 20);
			this._uiMoniker.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 51);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 26);
			this.label1.TabIndex = 2;
			this.label1.Text = "Name:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 77);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 26);
			this.label2.TabIndex = 3;
			this.label2.Text = "Moniker:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			this.tableLayoutPanel2.Location = new System.Drawing.Point(54, 103);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(230, 29);
			this.tableLayoutPanel2.TabIndex = 4;
			// 
			// _uiOk
			// 
			this._uiOk.Dock = System.Windows.Forms.DockStyle.Top;
			this._uiOk.Enabled = false;
			this._uiOk.Location = new System.Drawing.Point(3, 3);
			this._uiOk.Name = "_uiOk";
			this._uiOk.Size = new System.Drawing.Size(75, 23);
			this._uiOk.TabIndex = 0;
			this._uiOk.Text = "Ok";
			this._uiOk.UseVisualStyleBackColor = true;
			this._uiOk.Click += new System.EventHandler(this.OnChange);
			// 
			// _uiCancel
			// 
			this._uiCancel.Dock = System.Windows.Forms.DockStyle.Top;
			this._uiCancel.Location = new System.Drawing.Point(152, 3);
			this._uiCancel.Name = "_uiCancel";
			this._uiCancel.Size = new System.Drawing.Size(75, 23);
			this._uiCancel.TabIndex = 1;
			this._uiCancel.Text = "Cancel";
			this._uiCancel.UseVisualStyleBackColor = true;
			this._uiCancel.Click += new System.EventHandler(this.OnChange);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.label3, 2);
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(3, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(278, 51);
			this.label3.TabIndex = 5;
			this.label3.Text = "The project moniker listed below is currently not known, would you like to create" +
    " the moniker and then retry?";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// CreateNuGetTarget
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 132);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(300, 170);
			this.Name = "CreateNuGetTarget";
			this.Text = "Unknown Project Moniker";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TextBox _uiMoniker;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button _uiOk;
		private System.Windows.Forms.Button _uiCancel;
		private System.Windows.Forms.TextBox _uiName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ToolTip _uiToolTip;
	}
}