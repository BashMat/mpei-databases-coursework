
namespace DB_MPEI_B4_S1_Coursework
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.modesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editStudentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.studentsByProgramWoConscriptionDelayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.programsWoConscriptionDelayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.studentsByLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.conscriptionDocumentsByYearAndFacultyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.studentsByAgeGroupAndFormOfTrainingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonEdit = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.comboBox3 = new System.Windows.Forms.ComboBox();
			this.labelInfo = new System.Windows.Forms.Label();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.modesToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(982, 28);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
			this.fileToolStripMenuItem.Text = "Файл";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
			this.exitToolStripMenuItem.Text = "Выход";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// modesToolStripMenuItem
			// 
			this.modesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editStudentToolStripMenuItem,
            this.studentsByProgramWoConscriptionDelayToolStripMenuItem,
            this.programsWoConscriptionDelayToolStripMenuItem,
            this.studentsByLevelToolStripMenuItem,
            this.conscriptionDocumentsByYearAndFacultyToolStripMenuItem,
            this.studentsByAgeGroupAndFormOfTrainingToolStripMenuItem});
			this.modesToolStripMenuItem.Name = "modesToolStripMenuItem";
			this.modesToolStripMenuItem.Size = new System.Drawing.Size(137, 24);
			this.modesToolStripMenuItem.Text = "Режимы работы";
			// 
			// editStudentToolStripMenuItem
			// 
			this.editStudentToolStripMenuItem.Name = "editStudentToolStripMenuItem";
			this.editStudentToolStripMenuItem.Size = new System.Drawing.Size(758, 26);
			this.editStudentToolStripMenuItem.Text = "Изменение данных о студенте";
			this.editStudentToolStripMenuItem.Click += new System.EventHandler(this.editStudentToolStripMenuItem_Click);
			// 
			// studentsByProgramWoConscriptionDelayToolStripMenuItem
			// 
			this.studentsByProgramWoConscriptionDelayToolStripMenuItem.Name = "studentsByProgramWoConscriptionDelayToolStripMenuItem";
			this.studentsByProgramWoConscriptionDelayToolStripMenuItem.Size = new System.Drawing.Size(758, 26);
			this.studentsByProgramWoConscriptionDelayToolStripMenuItem.Text = "Формирование списка студентов по направлениям, у которых нет отсрочки от службы в" +
    " ВС РФ";
			this.studentsByProgramWoConscriptionDelayToolStripMenuItem.Click += new System.EventHandler(this.studentsByProgramWoConscriptionDelayToolStripMenuItem_Click);
			// 
			// programsWoConscriptionDelayToolStripMenuItem
			// 
			this.programsWoConscriptionDelayToolStripMenuItem.Name = "programsWoConscriptionDelayToolStripMenuItem";
			this.programsWoConscriptionDelayToolStripMenuItem.Size = new System.Drawing.Size(758, 26);
			this.programsWoConscriptionDelayToolStripMenuItem.Text = "Учет направлений подготовки, по которым не предоставляется отсрочка от армии";
			this.programsWoConscriptionDelayToolStripMenuItem.Click += new System.EventHandler(this.programsWoConscriptionDelayToolStripMenuItem_Click);
			// 
			// studentsByLevelToolStripMenuItem
			// 
			this.studentsByLevelToolStripMenuItem.Name = "studentsByLevelToolStripMenuItem";
			this.studentsByLevelToolStripMenuItem.Size = new System.Drawing.Size(758, 26);
			this.studentsByLevelToolStripMenuItem.Text = "Учет сведений о студентах  ВУЗа различных категорий";
			this.studentsByLevelToolStripMenuItem.Click += new System.EventHandler(this.studentsByLevelToolStripMenuItem_Click);
			// 
			// conscriptionDocumentsByYearAndFacultyToolStripMenuItem
			// 
			this.conscriptionDocumentsByYearAndFacultyToolStripMenuItem.Name = "conscriptionDocumentsByYearAndFacultyToolStripMenuItem";
			this.conscriptionDocumentsByYearAndFacultyToolStripMenuItem.Size = new System.Drawing.Size(758, 26);
			this.conscriptionDocumentsByYearAndFacultyToolStripMenuItem.Text = "Составление отчета по выданным справкам в военкомат (за один уч. год) по факульте" +
    "там";
			this.conscriptionDocumentsByYearAndFacultyToolStripMenuItem.Click += new System.EventHandler(this.conscriptionDocumentsByYearAndFacultyToolStripMenuItem_Click);
			// 
			// studentsByAgeGroupAndFormOfTrainingToolStripMenuItem
			// 
			this.studentsByAgeGroupAndFormOfTrainingToolStripMenuItem.Name = "studentsByAgeGroupAndFormOfTrainingToolStripMenuItem";
			this.studentsByAgeGroupAndFormOfTrainingToolStripMenuItem.Size = new System.Drawing.Size(758, 26);
			this.studentsByAgeGroupAndFormOfTrainingToolStripMenuItem.Text = "Составление списка студентов по различным возрастным группам, типам подготовки ";
			this.studentsByAgeGroupAndFormOfTrainingToolStripMenuItem.Click += new System.EventHandler(this.studentsByAgeGroupAndFormOfTrainingToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
			this.helpToolStripMenuItem.Text = "Справка";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
			this.aboutToolStripMenuItem.Text = "О программе";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(12, 31);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersWidth = 51;
			this.dataGridView1.RowTemplate.Height = 29;
			this.dataGridView1.Size = new System.Drawing.Size(712, 410);
			this.dataGridView1.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(730, 31);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "Поле 1:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(730, 85);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60, 20);
			this.label2.TabIndex = 4;
			this.label2.Text = "Поле 2:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(730, 139);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 20);
			this.label3.TabIndex = 6;
			this.label3.Text = "Поле 3:";
			// 
			// buttonEdit
			// 
			this.buttonEdit.Enabled = false;
			this.buttonEdit.Location = new System.Drawing.Point(730, 298);
			this.buttonEdit.Name = "buttonEdit";
			this.buttonEdit.Size = new System.Drawing.Size(240, 29);
			this.buttonEdit.TabIndex = 8;
			this.buttonEdit.Text = "Изменить данные";
			this.buttonEdit.UseVisualStyleBackColor = true;
			this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Enabled = false;
			this.buttonUpdate.Location = new System.Drawing.Point(730, 333);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(240, 29);
			this.buttonUpdate.TabIndex = 9;
			this.buttonUpdate.Text = "Обновить список";
			this.buttonUpdate.UseVisualStyleBackColor = true;
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(730, 54);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(240, 28);
			this.comboBox1.TabIndex = 10;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// comboBox2
			// 
			this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Location = new System.Drawing.Point(730, 108);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(240, 28);
			this.comboBox2.TabIndex = 11;
			this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// comboBox3
			// 
			this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox3.FormattingEnabled = true;
			this.comboBox3.Location = new System.Drawing.Point(730, 162);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new System.Drawing.Size(240, 28);
			this.comboBox3.TabIndex = 12;
			this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// labelInfo
			// 
			this.labelInfo.ForeColor = System.Drawing.Color.Red;
			this.labelInfo.Location = new System.Drawing.Point(730, 365);
			this.labelInfo.Name = "labelInfo";
			this.labelInfo.Size = new System.Drawing.Size(240, 76);
			this.labelInfo.TabIndex = 13;
			this.labelInfo.Text = "T";
			this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labelInfo.Visible = false;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(982, 453);
			this.Controls.Add(this.labelInfo);
			this.Controls.Add(this.comboBox3);
			this.Controls.Add(this.comboBox2);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.buttonUpdate);
			this.Controls.Add(this.buttonEdit);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "DB MPEI B4 S1 Coursework";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem modesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editStudentToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem studentsByProgramWoConscriptionDelayToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem programsWoConscriptionDelayToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem studentsByLevelToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem conscriptionDocumentsByYearAndFacultyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem studentsByAgeGroupAndFormOfTrainingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonEdit;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.ComboBox comboBox3;
		private System.Windows.Forms.Label labelInfo;
	}
}
