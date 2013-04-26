namespace ORMPerformanceTest
{
	partial class FormMain
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.btnExit = new System.Windows.Forms.Button();
			this.pnlFooter = new System.Windows.Forms.Panel();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabSelection = new System.Windows.Forms.TabPage();
			this.checkSecondLevelCaching = new System.Windows.Forms.CheckBox();
			this.checkContextCaching = new System.Windows.Forms.CheckBox();
			this.checkTracking = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cboxTestSelect = new System.Windows.Forms.ComboBox();
			this.tabModification = new System.Windows.Forms.TabPage();
			this.label4 = new System.Windows.Forms.Label();
			this.cboxTestModify = new System.Windows.Forms.ComboBox();
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.txtExecutionNumber = new System.Windows.Forms.TextBox();
			this.btnRun = new System.Windows.Forms.Button();
			this.cboxTool = new System.Windows.Forms.ComboBox();
			this.pnlResults = new System.Windows.Forms.Panel();
			this.gridViewResults = new System.Windows.Forms.DataGridView();
			this.colNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTest = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colModificators = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colAvgTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TimeCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.programBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.pnlFooter.SuspendLayout();
			this.pnlMain.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabSelection.SuspendLayout();
			this.tabModification.SuspendLayout();
			this.pnlHeader.SuspendLayout();
			this.pnlResults.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridViewResults)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.programBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// btnExit
			// 
			this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExit.Location = new System.Drawing.Point(817, 4);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(75, 23);
			this.btnExit.TabIndex = 1;
			this.btnExit.Text = "Exit";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// pnlFooter
			// 
			this.pnlFooter.Controls.Add(this.btnExit);
			this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlFooter.Location = new System.Drawing.Point(0, 424);
			this.pnlFooter.Name = "pnlFooter";
			this.pnlFooter.Size = new System.Drawing.Size(904, 34);
			this.pnlFooter.TabIndex = 1;
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.tabControl);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(0, 40);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(904, 184);
			this.pnlMain.TabIndex = 2;
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabSelection);
			this.tabControl.Controls.Add(this.tabModification);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(904, 184);
			this.tabControl.TabIndex = 2;
			// 
			// tabSelection
			// 
			this.tabSelection.Controls.Add(this.checkSecondLevelCaching);
			this.tabSelection.Controls.Add(this.checkContextCaching);
			this.tabSelection.Controls.Add(this.checkTracking);
			this.tabSelection.Controls.Add(this.label1);
			this.tabSelection.Controls.Add(this.cboxTestSelect);
			this.tabSelection.Location = new System.Drawing.Point(4, 22);
			this.tabSelection.Name = "tabSelection";
			this.tabSelection.Padding = new System.Windows.Forms.Padding(3);
			this.tabSelection.Size = new System.Drawing.Size(896, 158);
			this.tabSelection.TabIndex = 0;
			this.tabSelection.Text = "Data Selection";
			this.tabSelection.UseVisualStyleBackColor = true;
			// 
			// checkSecondLevelCaching
			// 
			this.checkSecondLevelCaching.AutoSize = true;
			this.checkSecondLevelCaching.Enabled = false;
			this.checkSecondLevelCaching.Location = new System.Drawing.Point(9, 92);
			this.checkSecondLevelCaching.Name = "checkSecondLevelCaching";
			this.checkSecondLevelCaching.Size = new System.Drawing.Size(272, 17);
			this.checkSecondLevelCaching.TabIndex = 12;
			this.checkSecondLevelCaching.Text = "cache entities between contexts (2nd level caching)";
			this.checkSecondLevelCaching.UseVisualStyleBackColor = true;
			// 
			// checkContextCaching
			// 
			this.checkContextCaching.AutoSize = true;
			this.checkContextCaching.Checked = true;
			this.checkContextCaching.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkContextCaching.Location = new System.Drawing.Point(9, 69);
			this.checkContextCaching.Name = "checkContextCaching";
			this.checkContextCaching.Size = new System.Drawing.Size(264, 17);
			this.checkContextCaching.TabIndex = 11;
			this.checkContextCaching.Text = "cache context between queries (1st level caching)";
			this.checkContextCaching.UseVisualStyleBackColor = true;
			// 
			// checkTracking
			// 
			this.checkTracking.AutoSize = true;
			this.checkTracking.Checked = true;
			this.checkTracking.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkTracking.Location = new System.Drawing.Point(9, 45);
			this.checkTracking.Name = "checkTracking";
			this.checkTracking.Size = new System.Drawing.Size(86, 17);
			this.checkTracking.TabIndex = 10;
			this.checkTracking.Text = "track entities";
			this.checkTracking.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(28, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Test";
			// 
			// cboxTestSelect
			// 
			this.cboxTestSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxTestSelect.FormattingEnabled = true;
			this.cboxTestSelect.Location = new System.Drawing.Point(115, 11);
			this.cboxTestSelect.Name = "cboxTestSelect";
			this.cboxTestSelect.Size = new System.Drawing.Size(428, 21);
			this.cboxTestSelect.TabIndex = 8;
			// 
			// tabModification
			// 
			this.tabModification.Controls.Add(this.label4);
			this.tabModification.Controls.Add(this.cboxTestModify);
			this.tabModification.Location = new System.Drawing.Point(4, 22);
			this.tabModification.Name = "tabModification";
			this.tabModification.Padding = new System.Windows.Forms.Padding(3);
			this.tabModification.Size = new System.Drawing.Size(896, 158);
			this.tabModification.TabIndex = 1;
			this.tabModification.Text = "Data Modification";
			this.tabModification.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 15);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(28, 13);
			this.label4.TabIndex = 14;
			this.label4.Text = "Test";
			// 
			// cboxTestModify
			// 
			this.cboxTestModify.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxTestModify.FormattingEnabled = true;
			this.cboxTestModify.Location = new System.Drawing.Point(115, 12);
			this.cboxTestModify.Name = "cboxTestModify";
			this.cboxTestModify.Size = new System.Drawing.Size(437, 21);
			this.cboxTestModify.TabIndex = 13;
			// 
			// pnlHeader
			// 
			this.pnlHeader.Controls.Add(this.txtExecutionNumber);
			this.pnlHeader.Controls.Add(this.btnRun);
			this.pnlHeader.Controls.Add(this.cboxTool);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(904, 40);
			this.pnlHeader.TabIndex = 3;
			// 
			// txtExecutionNumber
			// 
			this.txtExecutionNumber.Location = new System.Drawing.Point(204, 9);
			this.txtExecutionNumber.Name = "txtExecutionNumber";
			this.txtExecutionNumber.Size = new System.Drawing.Size(56, 20);
			this.txtExecutionNumber.TabIndex = 2;
			this.txtExecutionNumber.Text = "1";
			// 
			// btnRun
			// 
			this.btnRun.Location = new System.Drawing.Point(266, 7);
			this.btnRun.Name = "btnRun";
			this.btnRun.Size = new System.Drawing.Size(84, 23);
			this.btnRun.TabIndex = 1;
			this.btnRun.Text = "Run";
			this.btnRun.UseVisualStyleBackColor = true;
			this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
			// 
			// cboxTool
			// 
			this.cboxTool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxTool.FormattingEnabled = true;
			this.cboxTool.Location = new System.Drawing.Point(12, 9);
			this.cboxTool.Name = "cboxTool";
			this.cboxTool.Size = new System.Drawing.Size(186, 21);
			this.cboxTool.TabIndex = 0;
			// 
			// pnlResults
			// 
			this.pnlResults.Controls.Add(this.gridViewResults);
			this.pnlResults.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlResults.Location = new System.Drawing.Point(0, 224);
			this.pnlResults.Name = "pnlResults";
			this.pnlResults.Size = new System.Drawing.Size(904, 200);
			this.pnlResults.TabIndex = 4;
			// 
			// gridViewResults
			// 
			this.gridViewResults.AllowUserToAddRows = false;
			this.gridViewResults.AllowUserToDeleteRows = false;
			this.gridViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridViewResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNumber,
            this.colTest,
            this.colModificators,
            this.colAvgTime,
            this.TimeCost});
			this.gridViewResults.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridViewResults.Location = new System.Drawing.Point(0, 0);
			this.gridViewResults.Name = "gridViewResults";
			this.gridViewResults.ReadOnly = true;
			this.gridViewResults.Size = new System.Drawing.Size(904, 200);
			this.gridViewResults.TabIndex = 0;
			// 
			// colNumber
			// 
			this.colNumber.HeaderText = "#";
			this.colNumber.Name = "colNumber";
			this.colNumber.ReadOnly = true;
			this.colNumber.Width = 30;
			// 
			// colTest
			// 
			this.colTest.DataPropertyName = "Test.Name";
			this.colTest.HeaderText = "Test";
			this.colTest.Name = "colTest";
			this.colTest.ReadOnly = true;
			this.colTest.Width = 400;
			// 
			// colModificators
			// 
			this.colModificators.HeaderText = "Modificators";
			this.colModificators.Name = "colModificators";
			this.colModificators.ReadOnly = true;
			this.colModificators.Width = 250;
			// 
			// colAvgTime
			// 
			this.colAvgTime.HeaderText = "Avg Time";
			this.colAvgTime.Name = "colAvgTime";
			this.colAvgTime.ReadOnly = true;
			this.colAvgTime.Width = 80;
			// 
			// TimeCost
			// 
			this.TimeCost.HeaderText = "Total Time";
			this.TimeCost.Name = "TimeCost";
			this.TimeCost.ReadOnly = true;
			this.TimeCost.Width = 80;
			// 
			// programBindingSource
			// 
			this.programBindingSource.DataSource = typeof(ORMPerformanceTest.Program);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(904, 458);
			this.Controls.Add(this.pnlMain);
			this.Controls.Add(this.pnlResults);
			this.Controls.Add(this.pnlFooter);
			this.Controls.Add(this.pnlHeader);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "FormMain";
			this.Text = "Data Access Performance Test";
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.pnlFooter.ResumeLayout(false);
			this.pnlMain.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.tabSelection.ResumeLayout(false);
			this.tabSelection.PerformLayout();
			this.tabModification.ResumeLayout(false);
			this.tabModification.PerformLayout();
			this.pnlHeader.ResumeLayout(false);
			this.pnlHeader.PerformLayout();
			this.pnlResults.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridViewResults)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.programBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Panel pnlFooter;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.Panel pnlHeader;
		private System.Windows.Forms.Panel pnlResults;
		private System.Windows.Forms.DataGridView gridViewResults;
		private System.Windows.Forms.ComboBox cboxTool;
		private System.Windows.Forms.Button btnRun;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabSelection;
		private System.Windows.Forms.TabPage tabModification;
		private System.Windows.Forms.CheckBox checkContextCaching;
		private System.Windows.Forms.CheckBox checkTracking;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cboxTestSelect;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cboxTestModify;
		private System.Windows.Forms.BindingSource programBindingSource;
		private System.Windows.Forms.TextBox txtExecutionNumber;
		private System.Windows.Forms.CheckBox checkSecondLevelCaching;
		private System.Windows.Forms.DataGridViewTextBoxColumn colNumber;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTest;
		private System.Windows.Forms.DataGridViewTextBoxColumn colModificators;
		private System.Windows.Forms.DataGridViewTextBoxColumn colAvgTime;
		private System.Windows.Forms.DataGridViewTextBoxColumn TimeCost;
	}
}

