using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DB_MPEI_B4_S1_Coursework
{
	public partial class FormAccountStudentsByLevel : Form
	{
		Form1 f;
		int i;
		Student currentStudent;

		public FormAccountStudentsByLevel(Form1 form)
		{
			InitializeComponent();

			f = form;

			dataGridView1.Columns.Add("StudentId", "Номер");
			dataGridView1.Columns.Add("StudentProgramID", "Направление");
			dataGridView1.Columns.Add("StudentGroupID", "Группа");
			dataGridView1.Columns.Add("StudentOptionID", "Форма");
			dataGridView1.Columns.Add("StudentFirstName", "Имя");
			dataGridView1.Columns.Add("StudentLastName", "Фамилия");
			dataGridView1.Columns.Add("StudentBirthday", "Дата рождения");
			dataGridView1.Columns.Add("StudentMarried", "Состоит в браке");
			dataGridView1.Columns.Add("StudentSex", "Пол");

			i = 0;
			currentStudent = null;
			FillGrid();
		}

		void FillGrid()
		{
			dataGridView1.Rows.Clear();
			
			if (f.students.Count > 0)
			{
				buttonPrev.Enabled = i != 0;
				buttonNext.Enabled = i != f.students.Count - 1;
				buttonDelete.Enabled = true;
				buttonSave.Enabled = true;
				currentStudent = f.students[i];
				dataGridView1.Rows.Add(currentStudent.id, currentStudent.idProgram, currentStudent.idGroup, currentStudent.idOption,
					currentStudent.firstName, currentStudent.lastName, currentStudent.birth, currentStudent.isMarried, currentStudent.sex);
			}
			else
			{
				buttonPrev.Enabled = false;
				buttonNext.Enabled = false;
				buttonDelete.Enabled = false;
				buttonSave.Enabled = false;
				dataGridView1.Rows.Add();
			}
		}

		private void buttonPrev_Click(object sender, EventArgs e)
		{
			i--;
			FillGrid();
		}

		private void buttonNext_Click(object sender, EventArgs e)
		{
			i++;
			FillGrid();
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			int id, idProg, idGr, idOpt;
			string date;
			if (int.TryParse(dataGridView1[0, 0].Value.ToString(), out id) && id > 0 &&
				int.TryParse(dataGridView1[1, 0].Value.ToString(), out idProg) && idProg > 0 &&
				int.TryParse(dataGridView1[2, 0].Value.ToString(), out idGr) && (idGr >= 0) &&
				int.TryParse(dataGridView1[3, 0].Value.ToString(), out idOpt) && idOpt > 0 &&
				f.TryParseDate(dataGridView1[6, 0].Value.ToString(), out date) && (int.Parse(date[0..4]) >= 1900) &&
				(dataGridView1[7, 0].Value.ToString() == "да" || dataGridView1[7, 0].Value.ToString() == "нет") &&
				(dataGridView1[8, 0].Value.ToString() == "м" || dataGridView1[8, 0].Value.ToString() == "ж")
				)
			{
				SqlCommand command;
				SqlDataReader sdr;
				// Доработать через процедуру проверки корректности связи направления и группы (а также типа обучения?)
				//string query = "UPDATE Student SET Student.StudentID = " + id +
				//	", Student.StudentProgramID = " + idProg +
				//	", Student.StudentGroupID = " + idGr +
				//	", Student.StudentOptionID = " + idOpt +
				//	", Student.StudentFirstName = '" + dataGridView1[4, 0].Value.ToString() +
				//	"', Student.StudentLastName = '" + dataGridView1[5, 0].Value.ToString() +
				//	"', Student.StudentBirthday = '" + date +
				//	"', Student.StudentMarried = '" + dataGridView1[7, 0].Value.ToString() +
				//	"', Student.StudentSex = '" + dataGridView1[8, 0].Value.ToString() +
				//	"' WHERE Student.StudentID = " + currentStudent.id;
				//command = new SqlCommand(query, f.connection);
				//sdr = command.ExecuteReader();

				//f.students[i].id = id;
				//f.students[i].idProgram = idProg;
				//f.students[i].idGroup = idGr;
				//f.students[i].idOption = idOpt;
				//f.students[i].firstName = dataGridView1[4, 0].Value.ToString();
				//f.students[i].lastName = dataGridView1[5, 0].Value.ToString();
				//f.students[i].birth = dataGridView1[6, 0].Value.ToString();
				//f.students[i].isMarried = dataGridView1[7, 0].Value.ToString();
				//f.students[i].sex = dataGridView1[8, 0].Value.ToString()[0];

				string query = "DECLARE @Res INT; EXECUTE @Res = EDIT_STUDENT " + currentStudent.id + ", " + id + ", " + idProg + ", " +
						idGr + ", " + idOpt + ", '" + dataGridView1[4, 0].Value.ToString() + "', '" + dataGridView1[5, 0].Value.ToString()
						+ "', '" + date + "', '" + dataGridView1[7, 0].Value.ToString() + "', '" + dataGridView1[8, 0].Value +
						"'; SELECT @Res;";
				command = new SqlCommand(query, f.connection);
				sdr = command.ExecuteReader();

				int procRes = 1;
				if (sdr.Read())
				{
					procRes = (int)sdr.GetValue(0);
				}

				if (procRes == 0)
				{
					f.students[i].id = id;
					f.students[i].idProgram = idProg;
					f.students[i].idGroup = idGr;
					f.students[i].idOption = idOpt;
					f.students[i].firstName = dataGridView1[4, 0].Value.ToString();
					f.students[i].lastName = dataGridView1[5, 0].Value.ToString();
					f.students[i].birth = dataGridView1[6, 0].Value.ToString();
					f.students[i].isMarried = dataGridView1[7, 0].Value.ToString();
					f.students[i].sex = dataGridView1[8, 0].Value.ToString()[0];
				}
				else
				{
					MessageBox.Show("Некорректное изменение", "Сообщение");
					dataGridView1[0, 0].Value = currentStudent.id;
					dataGridView1[1, 0].Value = currentStudent.idProgram;
					dataGridView1[2, 0].Value = currentStudent.idGroup;
					dataGridView1[3, 0].Value = currentStudent.idOption;
					dataGridView1[4, 0].Value = currentStudent.firstName;
					dataGridView1[5, 0].Value = currentStudent.lastName;
					dataGridView1[6, 0].Value = currentStudent.birth;
					dataGridView1[7, 0].Value = currentStudent.isMarried;
					dataGridView1[8, 0].Value = currentStudent.sex;

				}

				sdr.Close();
				FillGrid();
			}
			else
			{
				MessageBox.Show("Некорректный формат значений", "Сообщение");
				if (currentStudent != null)
				{
					dataGridView1[0, 0].Value = currentStudent.id;
					dataGridView1[1, 0].Value = currentStudent.idProgram;
					dataGridView1[2, 0].Value = currentStudent.idGroup;
					dataGridView1[3, 0].Value = currentStudent.idOption;
					dataGridView1[4, 0].Value = currentStudent.firstName;
					dataGridView1[5, 0].Value = currentStudent.lastName;
					dataGridView1[6, 0].Value = currentStudent.birth;
					dataGridView1[7, 0].Value = currentStudent.isMarried;
					dataGridView1[8, 0].Value = currentStudent.sex;
				}
			}
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			SqlCommand command;
			SqlDataReader sdr;
			// Доработать через процедуру проверки корректности связи направления и группы (а также типа обучения?)
			string query = "DELETE Student WHERE Student.StudentID = " + currentStudent.id;
			command = new SqlCommand(query, f.connection);
			sdr = command.ExecuteReader();

			f.students.RemoveAt(i);

			if (i == f.students.Count && f.students.Count > 0)
			{
				i--;
				//buttonNext.Enabled = false;
			}
			//else if (i == f.students.Count - 1)
			//{
			//	buttonNext.Enabled = false;
			//}

			//if (i == 0)
			//{
			//	buttonPrev.Enabled = false;
			//}
			//else
			//{
			//	buttonPrev.Enabled = true;
			//}

			sdr.Close();
			FillGrid();
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			int id, idProg, idGr, idOpt;
			string date;
			if (int.TryParse(dataGridView1[0, 0].Value.ToString(), out id) && (id > 0) && 
				int.TryParse(dataGridView1[1, 0].Value.ToString(), out idProg) && (idProg > 0) &&
				int.TryParse(dataGridView1[2, 0].Value.ToString(), out idGr) && (idGr >= 0) &&
				int.TryParse(dataGridView1[3, 0].Value.ToString(), out idOpt) && (idOpt > 0) &&
				f.TryParseDate(dataGridView1[6, 0].Value.ToString(), out date) && (int.Parse(date[0..4]) >= 1900) &&
				(dataGridView1[7, 0].Value.ToString() == "да" || dataGridView1[7, 0].Value.ToString() == "нет") &&
				(dataGridView1[8, 0].Value.ToString() == "м" || dataGridView1[8, 0].Value.ToString() == "ж")
				)
			{
				Student student = new Student(id, idProg, idGr, idOpt, dataGridView1[4, 0].Value.ToString(),
					dataGridView1[5, 0].Value.ToString(), dataGridView1[6, 0].Value.ToString(), dataGridView1[7, 0].Value.ToString(),
					dataGridView1[8, 0].Value.ToString()[0]);

				SqlCommand command;
				SqlDataReader sdr;
				// Доработать через процедуру проверки корректности связи направления и группы (а также типа обучения?)
				//string query = "INSERT INTO Student(StudentID, StudentProgramID, StudentGroupID, StudentOptionID, StudentFirstName, " +
				//	"StudentLastName, StudentBirthday, StudentMarried, StudentSex) VALUES " +
				//	"(" + student.id + ", " + student.idProgram + ", " + student.idGroup + ", " + student.idOption + ", '" + 
				//	student.firstName + "', '" + student.lastName + "', '" + date + "', '" + student.isMarried +
				//	"', '" + student.sex.ToString() + "')";
				//command = new SqlCommand(query, f.connection);
				//sdr = command.ExecuteReader();

				//f.students.Add(student);
				//i = f.students.Count - 1;
				//currentStudent = f.students[i];

				//sdr.Close();
				//FillGrid();

				//buttonNext.Enabled = false;
				//buttonPrev.Enabled = (f.students.Count > 1);

				string query = "DECLARE @Res INT; EXECUTE @Res = Add_Student " + student.id + ", " + 
					student.idProgram + ", " + student.idGroup + ", " + student.idOption + ", '" + student.firstName + "', '" +
					student.lastName + "', '" + date + "', '" + student.isMarried + "', '" + student.sex +
					"'; SELECT @Res;";
				command = new SqlCommand(query, f.connection);
				sdr = command.ExecuteReader();

				int procRes = 1;
				if (sdr.Read())
				{
					procRes = (int)sdr.GetValue(0);
				}

				if (procRes == 0)
				{
					f.students.Add(student);
					i = f.students.Count - 1;
					currentStudent = f.students[i];
				}
				else
				{
					MessageBox.Show("Некорректное изменение", "Сообщение");
					dataGridView1[0, 0].Value = currentStudent.id;
					dataGridView1[1, 0].Value = currentStudent.idProgram;
					dataGridView1[2, 0].Value = currentStudent.idGroup;
					dataGridView1[3, 0].Value = currentStudent.idOption;
					dataGridView1[4, 0].Value = currentStudent.firstName;
					dataGridView1[5, 0].Value = currentStudent.lastName;
					dataGridView1[6, 0].Value = currentStudent.birth;
					dataGridView1[7, 0].Value = currentStudent.isMarried;
					dataGridView1[8, 0].Value = currentStudent.sex;
				}

				sdr.Close();
				FillGrid();
			}
			else
			{
				MessageBox.Show("Некорректный формат значений", "Сообщение");
				if (currentStudent != null)
				{
					dataGridView1[0, 0].Value = currentStudent.id;
					dataGridView1[1, 0].Value = currentStudent.idProgram;
					dataGridView1[2, 0].Value = currentStudent.idGroup;
					dataGridView1[3, 0].Value = currentStudent.idOption;
					dataGridView1[4, 0].Value = currentStudent.firstName;
					dataGridView1[5, 0].Value = currentStudent.lastName;
					dataGridView1[6, 0].Value = currentStudent.birth;
					dataGridView1[7, 0].Value = currentStudent.isMarried;
					dataGridView1[8, 0].Value = currentStudent.sex;
				}
			}
		}
	}
}
