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
	public partial class FormEditStudent : Form
	{
		Form1 f;
		int i;
		Student currentStudent;

		public FormEditStudent(Form1 form)
		{
			InitializeComponent();
			f = form;

			dataGridView1.Columns.Add("StudentId", "Номер");
			dataGridView1.Columns.Add("StudentFirstName", "Имя");
			dataGridView1.Columns.Add("StudentLastName", "Фамилия");
			dataGridView1.Columns.Add("StudentProgramID", "Направление");
			dataGridView1.Columns.Add("StudentGroupID", "Группа");
			dataGridView1.Columns.Add("StudentMarried", "Состоит в браке");

			buttonPrev.Enabled = false;
			buttonNext.Enabled = f.students.Count > 1;
			i = 0;
			FillGrid();
		}

		void FillGrid()
		{
			dataGridView1.Rows.Clear();
			currentStudent = f.students[i];
			dataGridView1.Rows.Add(currentStudent.id, currentStudent.firstName, currentStudent.lastName,
				currentStudent.idProgram, currentStudent.idGroup, currentStudent.isMarried);
		}

		private void buttonPrev_Click(object sender, EventArgs e)
		{
			i--;
			if (i == 0)
			{
				buttonPrev.Enabled = false;
			}
			else
			{
				buttonPrev.Enabled = true;
			}
			buttonNext.Enabled = true;
			FillGrid();
		}

		private void buttonNext_Click(object sender, EventArgs e)
		{
			i++;
			if (i == f.students.Count - 1)
			{
				buttonNext.Enabled = false;
			}
			else
			{
				buttonNext.Enabled = true;
			}
			buttonPrev.Enabled = true;
			FillGrid();
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			int res;
			//StringBuilder sb = new StringBuilder();
			//sb.Append(dataGridView1[1, 0].Value);
			//string firstName = sb.ToString();
			//sb.Clear();
			//sb.Append(dataGridView1[2, 0].Value);
			//string lastName = sb.ToString();
			//sb.Clear();
			if (int.TryParse(dataGridView1[0, 0].Value.ToString(), out res) && res == currentStudent.id &&
				dataGridView1[1, 0].Value.ToString() == currentStudent.firstName &&
				dataGridView1[2, 0].Value.ToString() == currentStudent.lastName)
			{
				int prog, group;
				if (int.TryParse(dataGridView1[3, 0].Value.ToString(), out prog) && 
					int.TryParse(dataGridView1[4, 0].Value.ToString(), out group) && 
					(dataGridView1[5, 0].Value.ToString() == "да" || dataGridView1[5, 0].Value.ToString() == "нет"))
				{
					SqlCommand command;
					SqlDataReader sdr;
					// Доработать через процедуру проверки корректности связи направления и группы (а также типа обучения?)
					//string query = "UPDATE Student SET Student.StudentProgramID = " + prog + ", Student.StudentGroupID = " + group +
					//	", Student.StudentMarried = '" + dataGridView1[5, 0].Value.ToString() + "' WHERE Student.StudentID = " +
					//	currentStudent.id;

					string date;

					f.TryParseDate(currentStudent.birth, out date);

					string query = "DECLARE @Res INT; EXECUTE @Res = EDIT_STUDENT " + currentStudent.id + ", " + res + ", " + prog + ", " + 
						group + ", " + currentStudent.idOption + ", '" + currentStudent.firstName + "', '" + currentStudent.lastName + "', '" 
						+ date + "', '" + dataGridView1[5, 0].Value.ToString() + "', '" + currentStudent.sex + 
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
						f.students[i].idProgram = int.Parse(dataGridView1[3, 0].Value.ToString());
						f.students[i].idGroup = int.Parse(dataGridView1[4, 0].Value.ToString());
						f.students[i].isMarried = dataGridView1[5, 0].Value.ToString();
					}
					else
					{
						MessageBox.Show("Некорректное изменение", "Сообщение");
						dataGridView1[0, 0].Value = currentStudent.id;
						dataGridView1[1, 0].Value = currentStudent.firstName;
						dataGridView1[2, 0].Value = currentStudent.lastName;
						dataGridView1[3, 0].Value = currentStudent.idProgram;
						dataGridView1[4, 0].Value = currentStudent.idGroup;
						dataGridView1[5, 0].Value = currentStudent.isMarried;
					}

					sdr.Close();
					FillGrid();
				}
				else
				{
					MessageBox.Show("Поля с номерами групп и направления должны быть натуральными, а поле 'Состоит в браке'" +
						"может содержать значения 'да' или 'нет'", "Сообщение");
					dataGridView1[3, 0].Value = currentStudent.idProgram;
					dataGridView1[4, 0].Value = currentStudent.idGroup;
					dataGridView1[5, 0].Value = currentStudent.isMarried;
				}
			}
			else
			{
				MessageBox.Show("Изменять значения номера студента, его имени и фамилии запрещено", "Сообщение");
				dataGridView1[0, 0].Value = currentStudent.id;
				dataGridView1[1, 0].Value = currentStudent.firstName;
				dataGridView1[2, 0].Value = currentStudent.lastName;
			}
		}
	}
}
