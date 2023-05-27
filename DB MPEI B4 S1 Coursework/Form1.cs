using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DB_MPEI_B4_S1_Coursework
{
	public partial class Form1 : Form
	{
		Mode mode;
		string conStr = @"Data Source=DESKTOP-OMPTSKO\SQLEXPRESS;Initial Catalog=DB_MPEI_Coursework;Integrated Security=true;";
		public SqlConnection connection;

		public List<Student> students;
		public List<ProgramConscripted> programs;
		List<string> faculties;

		string comboValue1;
		string comboValue2;
		string comboValue3;

		public Form1()
		{
			InitializeComponent();
			SetLabels("", "", "");
			connection = new SqlConnection(conStr);
			connection.Open();

			comboValue1 = comboBox1.Text;
			comboValue2 = comboBox2.Text;
			comboValue3 = comboBox3.Text;

			students = new List<Student>();
			programs = new List<ProgramConscripted>();
		}

		public bool TryParseDate(string s, out string date)
		{
			int day, month, year;
			if (int.TryParse(s[0..2], out day) && int.TryParse(s[3..5], out month) && int.TryParse(s[6..], out year))
			{
				if (day >= 1 && day <= 31 && month >= 1 && month <= 12 && year >= 0)
				{
					date = s[6..] + s[3..5] + s[0..2];
					return true;
				}
			}
			//date = "20010914";
			date = "";
			return false;
		}

		void SetLabels(string l1, string l2, string l3)
		{
			if (l1 != "")
			{
				label1.Text = l1;
				label1.Visible = true;
				comboBox1.Visible = true;
			}
			else
			{
				label1.Visible = false;
				comboBox1.Visible = false;
			}
			if (l2 != "")
			{
				label2.Text = l2;
				label2.Visible = true;
				comboBox2.Visible = true;
			}
			else
			{
				label2.Visible = false;
				comboBox2.Visible = false;
			}
			if (l3 != "")
			{
				label3.Text = l3;
				label3.Visible = true;
				comboBox3.Visible = true;
			}
			else
			{
				label3.Visible = false;
				comboBox3.Visible = false;
			}

			comboValue1 = comboBox1.Text;
			comboValue2 = comboBox2.Text;
			comboValue3 = comboBox3.Text;
		}

		void UpdateEditStudent()
		{
			dataGridView1.Columns.Clear();
			dataGridView1.Rows.Clear();
			dataGridView1.Refresh();

			SqlCommand command;
			SqlDataReader sdr;
			string query = "SELECT StudentID, StudentProgramID, StudentGroupID, StudentOptionID, StudentFirstName, " +
				"StudentLastName, StudentBirthday, StudentMarried, StudentSex FROM Student";
			command = new SqlCommand(query, connection);
			sdr = command.ExecuteReader();

			dataGridView1.Columns.Add("StudentId", "Номер");
			dataGridView1.Columns.Add("StudentProgramID", "Направление");
			dataGridView1.Columns.Add("StudentGroupID", "Группа");
			dataGridView1.Columns.Add("StudentOptionID", "Форма");
			dataGridView1.Columns.Add("StudentFirstName", "Имя");
			dataGridView1.Columns.Add("StudentLastName", "Фамилия");
			dataGridView1.Columns.Add("StudentBirthday", "Дата рождения");
			dataGridView1.Columns.Add("StudentMarried", "Состоит в браке");
			dataGridView1.Columns.Add("StudentSex", "Пол");

			students.Clear();

			if (sdr.HasRows)
			{
				while (sdr.Read())
				{
					DateTime obj = (DateTime)sdr.GetValue(6);
					string res = obj.ToString("dd.MM.yyyy");

					int group = 0;
					if (!(sdr.GetValue(2) is System.DBNull))
					{
						group = (int)sdr.GetValue(2);
					}

					Student item = new Student((int)sdr.GetValue(0), (int)sdr.GetValue(1), group,
						(int)sdr.GetValue(3), (string)sdr.GetValue(4), (string)sdr.GetValue(5), res,
						(string)sdr.GetValue(7), ((string)sdr.GetValue(8))[0]);
					students.Add(item);

					dataGridView1.Rows.Add(item.id, item.idProgram, item.idGroup, item.idOption, item.firstName,
						item.lastName, item.birth, item.isMarried, item.sex);
				}
			}
			sdr.Close();
		}

		void UpdateStudentsByProgramWOConscript()
		{
			dataGridView1.Columns.Clear();
			dataGridView1.Rows.Clear();
			dataGridView1.Refresh();

			SqlCommand command;
			SqlDataReader sdr;
			string query = "SELECT StudentID, StudentFirstName, StudentLastName, ProgramID, ProgramName FROM Student " +
				"JOIN EducationalProgram ON Student.StudentProgramID = EducationalProgram.ProgramID " +
				"WHERE EducationalProgram.ProgramNotConscripted = 'нет'";
			command = new SqlCommand(query, connection);
			sdr = command.ExecuteReader();

			dataGridView1.Columns.Add("StudentId", "Номер студента");
			dataGridView1.Columns.Add("StudentFirstName", "Имя");
			dataGridView1.Columns.Add("StudentLastName", "Фамилия");
			dataGridView1.Columns.Add("ProgramId", "Номер направления");
			dataGridView1.Columns.Add("ProgramName", "Название направления");

			if (sdr.HasRows)
			{
				while (sdr.Read())
				{
					dataGridView1.Rows.Add((int)sdr.GetValue(0), (string)sdr.GetValue(1), (string)sdr.GetValue(2), (int)sdr.GetValue(3),
						(string)sdr.GetValue(4));
				}
			}
			sdr.Close();
		}

		void UpdateProgramsWOConscript()
		{
			dataGridView1.Columns.Clear();
			dataGridView1.Rows.Clear();
			dataGridView1.Refresh();

			SqlCommand command;
			SqlDataReader sdr;
			string query = "SELECT ProgramID, ProgramFacultyID, ProgramName, ProgramCode, ProgramLevel, ProgramFormOfTraining, " +
				"PrOptOptionId FROM EducationalProgram JOIN ProgramsAndOptions ON ProgramID = PrOptProgramID " +
				"WHERE EducationalProgram.ProgramNotConscripted = 'нет' ORDER BY ProgramID, PrOptOptionId";
			command = new SqlCommand(query, connection);
			sdr = command.ExecuteReader();

			dataGridView1.Columns.Add("ProgramID", "Номер направления");
			dataGridView1.Columns.Add("ProgramFacultyID", "Номер факультета");
			dataGridView1.Columns.Add("ProgramName", "Название направления");
			dataGridView1.Columns.Add("ProgramCode", "Код направления");
			dataGridView1.Columns.Add("ProgramLevel", "Уровень образования");
			dataGridView1.Columns.Add("ProgramFormOfTraining", "Форма обучения");
			dataGridView1.Columns.Add("ProgramBudget", "Бюджет");
			dataGridView1.Columns.Add("ProgramCommerce", "Коммерция");
			dataGridView1.Columns.Add("ProgramContract", "Целевое");

			programs.Clear();

			if (sdr.HasRows)
			{
				int i = -1;
				while (sdr.Read())
				{
					int idOpt = (int)sdr.GetValue(6);
					if (programs.Count > 0 && programs[programs.Count - 1].id == (int)sdr.GetValue(0))
					{
						programs[programs.Count - 1].budget = programs[programs.Count - 1].budget || idOpt == 1;
						programs[programs.Count - 1].commerce = programs[programs.Count - 1].commerce || idOpt == 2;
						programs[programs.Count - 1].contract = programs[programs.Count - 1].contract || idOpt == 3;


						if (programs[programs.Count - 1].budget)
						{
							dataGridView1[6, i].Value = "да";
						}
						else
						{
							dataGridView1[6, i].Value = "нет";
						}
						if (programs[programs.Count - 1].commerce)
						{
							dataGridView1[7, i].Value = "да";
						}
						else
						{
							dataGridView1[7, i].Value = "нет";
						}
						if (programs[programs.Count - 1].contract)
						{
							dataGridView1[8, i].Value = "да";
						}
						else
						{
							dataGridView1[8, i].Value = "нет";
						}
					}
					else
					{
						i++;
						ProgramConscripted prog = new ProgramConscripted((int)sdr.GetValue(0), (int)sdr.GetValue(1),
						(string)sdr.GetValue(2), (string)sdr.GetValue(3), (string)sdr.GetValue(4), (string)sdr.GetValue(5),
						idOpt == 1, idOpt == 2, idOpt == 3);
						programs.Add(prog);

						string b, co, cn;
						if (prog.budget)
						{
							b = "да";
						}
						else
						{
							b = "нет";
						}
						if (prog.commerce)
						{
							co = "да";
						}
						else
						{
							co = "нет";
						}
						if (prog.contract)
						{
							cn = "да";
						}
						else
						{
							cn = "нет";
						}
						dataGridView1.Rows.Add(prog.id, prog.idFaculty, prog.name, prog.code, prog.level, prog.form, b, co, cn);
					}
				}
			}
			sdr.Close();
		}

		void UpdateStudentsByLevel()
		{
			if (comboBox1.Text == "")
			{
				MessageBox.Show("Укажите значения поля для выполнения запроса.", "Сообщение");
				return;
			}

			comboValue1 = comboBox1.Text;
			comboValue2 = comboBox2.Text;
			comboValue3 = comboBox3.Text;
			buttonEdit.Enabled = true;
			labelInfo.Visible = false;

			dataGridView1.Columns.Clear();
			dataGridView1.Rows.Clear();
			dataGridView1.Refresh();

			SqlCommand command;
			SqlDataReader sdr;
			string query = "SELECT StudentID, StudentProgramID, StudentGroupID, StudentOptionID, StudentFirstName, " + 
				"StudentLastName, StudentBirthday, StudentMarried, StudentSex FROM Student JOIN EducationalProgram " + 
				"ON Student.StudentProgramID = EducationalProgram.ProgramID WHERE EducationalProgram.ProgramLevel = '" + 
				comboBox1.Text + "'";
			command = new SqlCommand(query, connection);
			sdr = command.ExecuteReader();

			dataGridView1.Columns.Add("StudentId", "Номер");
			dataGridView1.Columns.Add("StudentProgramID", "Направление");
			dataGridView1.Columns.Add("StudentGroupID", "Группа");
			dataGridView1.Columns.Add("StudentOptionID", "Форма");
			dataGridView1.Columns.Add("StudentFirstName", "Имя");
			dataGridView1.Columns.Add("StudentLastName", "Фамилия");
			dataGridView1.Columns.Add("StudentBirthday", "Дата рождения");
			dataGridView1.Columns.Add("StudentMarried", "Состоит в браке");
			dataGridView1.Columns.Add("StudentSex", "Пол");

			students.Clear();

			if (sdr.HasRows)
			{
				while (sdr.Read())
				{
					DateTime obj = (DateTime)sdr.GetValue(6);
					string res = obj.ToString("dd.MM.yyyy");

					int group = 0;
					if (!(sdr.GetValue(2) is System.DBNull))
					{
						group = (int)sdr.GetValue(2);
					}

					Student item = new Student((int)sdr.GetValue(0), (int)sdr.GetValue(1), group,
						(int)sdr.GetValue(3), (string)sdr.GetValue(4), (string)sdr.GetValue(5), res,
						(string)sdr.GetValue(7), ((string)sdr.GetValue(8))[0]);
					students.Add(item);

					dataGridView1.Rows.Add(item.id, item.idProgram, item.idGroup,
						item.idOption, item.firstName, item.lastName, res,
						item.isMarried, item.sex);
				}
			}
			sdr.Close();
		}

		void UpdateConDocs()
		{
			if (comboBox1.Text == "" || comboBox2.Text == "")
			{
				MessageBox.Show("Укажите значения полей для выполнения запроса.", "Сообщение");
				return;
			}

			int strIndex = comboBox2.Text.IndexOf(")");

			comboValue1 = comboBox1.Text;
			comboValue2 = comboBox2.Text.Substring(0, strIndex);
			comboValue3 = comboBox3.Text;

			labelInfo.Visible = false;

			dataGridView1.Columns.Clear();
			dataGridView1.Rows.Clear();
			dataGridView1.Refresh();

			SqlCommand command;
			SqlDataReader sdr;
			string query = "SELECT ConscriptionID, ConscriptionFacultyId, ConscriptionStudentID, ConscriptionYear FROM " + 
				"ConscriptionDocument JOIN Faculty ON ConscriptionDocument.ConscriptionFacultyID = Faculty.FacultyID " + 
				"WHERE ConscriptionYear = " + comboBox1.Text.Substring(0, 4) + " AND FacultyID = '" + int.Parse(comboValue2) + "'";
			command = new SqlCommand(query, connection);
			sdr = command.ExecuteReader();

			dataGridView1.Columns.Add("ConscriptionID", "Номер справки");
			dataGridView1.Columns.Add("ConscriptionFacultyId", "Номер факультета");
			dataGridView1.Columns.Add("ConscriptionStudentID", "Номер студента");
			dataGridView1.Columns.Add("ConscriptionYear", "Год на начало действия");

			if (sdr.HasRows)
			{
				while (sdr.Read())
				{
					dataGridView1.Rows.Add((int)sdr.GetValue(0), (int)sdr.GetValue(1), (int)sdr.GetValue(2),
						(int)sdr.GetValue(3));
				}
			}
			sdr.Close();
		}

		void UpdateStudentsByAgeForm()
		{
			if (comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "")
			{
				MessageBox.Show("Укажите значения полей для выполнения запроса.", "Сообщение");
				return;
			}
			else if (int.Parse(comboBox1.Text) > int.Parse(comboBox2.Text))
			{
				MessageBox.Show("Минимальный возраст не может быть больше максимального.", "Сообщение");
				return;
			}

			dataGridView1.Columns.Clear();
			dataGridView1.Rows.Clear();
			dataGridView1.Refresh();

			labelInfo.Visible = false;

			comboValue1 = comboBox1.Text;
			comboValue2 = comboBox2.Text;
			comboValue3 = comboBox3.Text;

			SqlCommand command;
			SqlDataReader sdr;
			string query = "SELECT StudentID, StudentProgramID, StudentGroupID, StudentOptionID, StudentFirstName, " + 
				"StudentLastName, StudentBirthday, StudentMarried, StudentSex FROM Student JOIN ProgramPaymentOption " + 
				"ON Student.StudentOptionID = ProgramPaymentOption.OptionID " + 
				"WHERE ((0 + Convert(Char(8), (SELECT CAST(GETDATE() AS Date)), 112) - Convert(Char(8), StudentBirthday, 112)) " + 
				"/ 10000) BETWEEN " + comboBox1.Text + " AND " + comboBox2.Text + " AND ProgramPaymentOption.OptionName = " + 
				"'" + comboBox3.Text + "'";
			command = new SqlCommand(query, connection);
			sdr = command.ExecuteReader();

			dataGridView1.Columns.Add("StudentId", "Номер");
			dataGridView1.Columns.Add("StudentProgramID", "Направление");
			dataGridView1.Columns.Add("StudentGroupID", "Группа");
			dataGridView1.Columns.Add("StudentOptionID", "Форма");
			dataGridView1.Columns.Add("StudentFirstName", "Имя");
			dataGridView1.Columns.Add("StudentLastName", "Фамилия");
			dataGridView1.Columns.Add("StudentBirthday", "Дата рождения");
			dataGridView1.Columns.Add("StudentMarried", "Состоит в браке");
			dataGridView1.Columns.Add("StudentSex", "Пол");

			if (sdr.HasRows)
			{
				while (sdr.Read())
				{
					DateTime obj = (DateTime)sdr.GetValue(6);
					string res = obj.ToString("dd.MM.yyyy");

					int group = (sdr.GetValue(2) is DBNull) ? 0 : (int)sdr.GetValue(2);
					dataGridView1.Rows.Add((int)sdr.GetValue(0), (int)sdr.GetValue(1), group,
						(int)sdr.GetValue(3), (string)sdr.GetValue(4), (string)sdr.GetValue(5), res,
						(string)sdr.GetValue(7), ((string)sdr.GetValue(8))[0]);
				}
			}
			sdr.Close();
		}

		void EnableButtons(bool isEditEnabled, bool isUpdateEnabled)
		{
			buttonEdit.Enabled = isEditEnabled;
			buttonUpdate.Enabled = isUpdateEnabled;
		}

		void UpdateUI()
		{
			dataGridView1.Columns.Clear();
			dataGridView1.Rows.Clear();
			dataGridView1.Refresh();

			labelInfo.Visible = false;

			students.Clear();
			programs.Clear();

			switch (mode)
			{
				case Mode.EditStudent:
					{
						SetLabels("", "", "");
						EnableButtons(true, false);
						UpdateEditStudent();
					}
					break;
				case Mode.StudentsByProgramWOConscript:
					{
						SetLabels("", "", "");
						EnableButtons(false, false);
						UpdateStudentsByProgramWOConscript();
					}
					break;
				case Mode.ProgramsWOConscript:
					{
						SetLabels("", "", "");
						EnableButtons(true, false);
						UpdateProgramsWOConscript();
					}
					break;
				case Mode.StudentsByLevel:
					{
						SetLabels("Уровень образования", "", "");

						comboBox1.Items.Clear();
						comboBox1.Items.Add("Бакалавриат");
						comboBox1.Items.Add("Магистратура");
						comboBox1.Items.Add("Аспирантура");

						EnableButtons(true, true);
					}
					break;
				case Mode.ConDocs:
					{
						SetLabels("Учебный год", "Факультет", "");

						comboBox1.Items.Clear();
						for (int i = 1999; i < 2022; i++)
						{
							comboBox1.Items.Add(i + "/" + (i + 1));
						}

						comboBox2.Items.Clear();

						if (faculties == null)
						{

							SqlCommand command;
							SqlDataReader sdr;
							string query = "SELECT FacultyID, FacultyName FROM Faculty";
							command = new SqlCommand(query, connection);
							sdr = command.ExecuteReader();
							faculties = new List<string>();
							if (sdr.HasRows)
							{
								while (sdr.Read())
								{
									faculties.Add(sdr.GetValue(0).ToString() + ") " + (string)sdr.GetValue(1));
								}
							}
							else
							{
								MessageBox.Show("В базе данных отсутствуют данные о факультетах. Данная функция не может быть выполнена.",
									"Сообщение");
								UpdateUI();
								return;
							}
							sdr.Close();
						}
						else if (faculties.Count == 0)
						{
							MessageBox.Show("В базе данных отсутствуют данные о факультетах. Данная функция не может быть выполнена.",
									"Сообщение");
							UpdateUI();
							return;
						}
						
						foreach (string faculty in faculties)
						{
							comboBox2.Items.Add(faculty);
						}
						
						EnableButtons(false, true);
					}
					break;
				case Mode.StudentsByAgeForm:
					{
						SetLabels("Минимальный возраст", "Максимальный возраст", "Тип подготовки");

						comboBox1.Items.Clear();
						comboBox2.Items.Clear();
						for (int i = 16; i < 100; i++)
						{
							comboBox1.Items.Add(i.ToString());
							comboBox2.Items.Add(i.ToString());
						}

						//comboBox2.Items.Clear();
						//for (int i = 16; i < 100; i++)
						//{
						//	comboBox2.Items.Add(i.ToString());
						//}

						comboBox3.Items.Clear();
						comboBox3.Items.Add("бюджетное");
						comboBox3.Items.Add("коммерческое");
						comboBox3.Items.Add("целевое");

						EnableButtons(false, true);
					}
					break;
				case Mode.Default:
					{
						dataGridView1.Columns.Clear();
						dataGridView1.Rows.Clear();
						dataGridView1.Refresh();

						labelInfo.Visible = false;
						comboBox1.Items.Clear();
						comboBox2.Items.Clear();
						comboBox3.Items.Clear();

						SetLabels("", "", "");
						EnableButtons(false, false);

						students.Clear();
						programs.Clear();
					}
					break;
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormAbout fa = new FormAbout();
			fa.ShowDialog();
		}

		private void editStudentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mode = Mode.EditStudent;
			UpdateUI();
		}

		private void studentsByProgramWoConscriptionDelayToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mode = Mode.StudentsByProgramWOConscript;
			UpdateUI();
		}

		private void programsWoConscriptionDelayToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mode = Mode.ProgramsWOConscript;
			UpdateUI();
		}

		private void studentsByLevelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mode = Mode.StudentsByLevel;
			UpdateUI();
		}

		private void conscriptionDocumentsByYearAndFacultyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mode = Mode.ConDocs;
			UpdateUI();
		}

		private void studentsByAgeGroupAndFormOfTrainingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mode = Mode.StudentsByAgeForm;
			UpdateUI();
		}

		//private void buttonAdd_Click(object sender, EventArgs e)
		//{
			
		//}

		private void buttonUpdate_Click(object sender, EventArgs e)
		{
			if (mode == Mode.Default)
			{
				MessageBox.Show("Выберите режим работы!", "Предупреждение");
			}
			else
			{
				switch (mode)
				{
					case Mode.StudentsByLevel:
						{
							UpdateStudentsByLevel();
						}
						break;
					case Mode.ConDocs:
						{
							UpdateConDocs();
						}
						break;
					case Mode.StudentsByAgeForm:
						{
							UpdateStudentsByAgeForm();
						}
						break;
				}
			}
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			connection.Close();
		}

		private void buttonEdit_Click(object sender, EventArgs e)
		{
			if (mode == Mode.Default)
			{
				MessageBox.Show("Выберите режим работы!", "Предупреждение");
			}
			else
			{
				switch (mode)
				{
					case Mode.EditStudent:
						{
							FormEditStudent fes = new FormEditStudent(this);
							fes.ShowDialog();
							UpdateEditStudent();
						}
						break;
					case Mode.ProgramsWOConscript:
						{
							FormAccountProgramsWOConscript fapwoc = new FormAccountProgramsWOConscript(this);
							fapwoc.ShowDialog();
							UpdateProgramsWOConscript();
						}
						break;
					case Mode.StudentsByLevel:
						{
							FormAccountStudentsByLevel fasl = new FormAccountStudentsByLevel(this);
							fasl.ShowDialog();
							UpdateStudentsByLevel();
						}
						break;
				}
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			string changed = "";
			if (comboValue1 != comboBox1.Text)
			{
				changed += "1, ";
			}
			if (comboValue2 != comboBox2.Text)
			{
				changed += "2, ";
			}
			if (comboValue3 != comboBox3.Text)
			{
				changed += "3, ";
			}

			if (changed.Length > 0)
			{
				changed = changed[0..^2];
				labelInfo.Text = "Были изменены поля " + changed + ". Нажмите \"Обновить\", чтобы внести изменения";
				labelInfo.Visible = true;
				buttonEdit.Enabled = false;
			}
			else
			{
				labelInfo.Visible = false;
				buttonEdit.Enabled = true;
			}
		}
	}

	public class Student
	{
		public int id;
		public int idProgram;
		public int idGroup;
		public int idOption;
		public string firstName;
		public string lastName;
		public string birth;
		public string isMarried;
		public char sex;

		public Student(int nId, int nIdProgram, int nIdGroup, int nIdOption, string nFirstName, string nLastName, string nBirth,
			string nIsMarried, char nSex)
		{
			id = nId;
			idProgram = nIdProgram;
			idGroup = nIdGroup;
			idOption = nIdOption;
			firstName = nFirstName;
			lastName = nLastName;
			birth = nBirth;
			isMarried = nIsMarried;
			sex = nSex;
		}
	}

	public class ProgramConscripted
	{
		public int id;
		public int idFaculty;
		public string name;
		public string code;
		public string level;
		public string form;
		public bool budget;
		public bool commerce;
		public bool contract;

		public ProgramConscripted(int nId, int nIdFaculty, string nName, string nCode, string nLevel, string nForm, 
			bool nBudget, bool nComm, bool nContr)
		{
			id = nId;
			idFaculty = nIdFaculty;
			name = nName;
			code = nCode;
			level = nLevel;
			form = nForm;
			budget = nBudget;
			commerce = nComm;
			contract = nContr;
		}
	}

	enum Mode
	{
		Default,
		EditStudent,
		StudentsByProgramWOConscript,
		ProgramsWOConscript,
		StudentsByLevel,
		ConDocs,
		StudentsByAgeForm
	}
}
