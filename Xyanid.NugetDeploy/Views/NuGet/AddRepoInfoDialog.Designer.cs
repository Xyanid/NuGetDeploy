namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.NuGet
{
	partial class AddRepoInfoDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddRepoInfoDialog));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this._uiUrl = new System.Windows.Forms.TextBox();
			this._uiApiKey = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this._uiOk = new System.Windows.Forms.Button();
			this._uiCancel = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this._uiUrl, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this._uiApiKey, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(284, 81);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// _uiUrl
			// 
			this._uiUrl.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiUrl.Location = new System.Drawing.Point(52, 3);
			this._uiUrl.Name = "_uiUrl";
			this._uiUrl.Size = new System.Drawing.Size(229, 20);
			this._uiUrl.TabIndex = 0;
			this._uiUrl.TextChanged += new System.EventHandler(this.OnChange);
			// 
			// _uiApiKey
			// 
			this._uiApiKey.Dock = System.Windows.Forms.DockStyle.Fill;
			this._uiApiKey.Location = new System.Drawing.Point(52, 29);
			this._uiApiKey.Name = "_uiApiKey";
			this._uiApiKey.Size = new System.Drawing.Size(229, 20);
			this._uiApiKey.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 26);
			this.label1.TabIndex = 2;
			this.label1.Text = "Url:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(43, 26);
			this.label2.TabIndex = 3;
			this.label2.Text = "ApiKey:";
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
			this.tableLayoutPanel2.Location = new System.Drawing.Point(49, 52);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(235, 37);
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
			this._uiCancel.Location = new System.Drawing.Point(157, 3);
			this._uiCancel.Name = "_uiCancel";
			this._uiCancel.Size = new System.Drawing.Size(75, 23);
			this._uiCancel.TabIndex = 1;
			this._uiCancel.Text = "Cancel";
			this._uiCancel.UseVisualStyleBackColor = true;
			this._uiCancel.Click += new System.EventHandler(this.OnChange);
			// 
			// AddRepoInfoDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(284, 81);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(300, 120);
			this.Name = "AddRepoInfoDialog";
			this.Text = "Add Repository Information";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TextBox _uiApiKey;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button _uiOk;
		private System.Windows.Forms.Button _uiCancel;
		private System.Windows.Forms.TextBox _uiUrl;
	}
}