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
	public partial class FormAccountProgramsWOConscript : Form
	{
		Form1 f;
		int i;
		ProgramConscripted currentProgram;

		public FormAccountProgramsWOConscript(Form1 form)
		{
			InitializeComponent();

			f = form;

			dataGridView1.Columns.Add("ProgramID", "Номер направления");
			dataGridView1.Columns.Add("ProgramFacultyID", "Номер факультета");
			dataGridView1.Columns.Add("ProgramName", "Название направления");
			dataGridView1.Columns.Add("ProgramCode", "Код направления");
			dataGridView1.Columns.Add("ProgramLevel", "Уровень образования");
			dataGridView1.Columns.Add("ProgramFormOfTraining", "Форма обучения");
			dataGridView1.Columns.Add("ProgramBudget", "Бюджет");
			dataGridView1.Columns.Add("ProgramCommerce", "Коммерция");
			dataGridView1.Columns.Add("ProgramContract", "Целевое");
			currentProgram = null;
			i = 0;
			FillGrid();
		}

		void FillGrid()
		{
			dataGridView1.Rows.Clear();

			if (f.programs.Count > 0)
			{
				buttonPrev.Enabled = i != 0;
				buttonNext.Enabled = i != f.programs.Count - 1;
				buttonDelete.Enabled = true;
				currentProgram = f.programs[i];
				dataGridView1.Rows.Add(currentProgram.id, currentProgram.idFaculty, currentProgram.name, currentProgram.code, 
					currentProgram.level, currentProgram.form);

				if (currentProgram.budget)
				{
					dataGridView1[6, 0].Value = "да";
				}
				else
				{
					dataGridView1[6, 0].Value = "нет";
				}
				if (currentProgram.commerce)
				{
					dataGridView1[7, 0].Value = "да";
				}
				else
				{
					dataGridView1[7, 0].Value = "нет";
				}
				if (currentProgram.contract)
				{
					dataGridView1[8, 0].Value = "да";
				}
				else
				{
					dataGridView1[8, 0].Value = "нет";
				}
			}
			else
			{
				buttonPrev.Enabled = false;
				buttonNext.Enabled = false;
				buttonDelete.Enabled = false;
				buttonSave.Enabled = false;
				dataGridView1.Rows.Add("", "", "", "", "", "", "", "", "");
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

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			int id, idFaculty;
			if (int.TryParse(dataGridView1[0, 0].Value.ToString(), out id) && id > 0 &&
				int.TryParse(dataGridView1[1, 0].Value.ToString(), out idFaculty) && idFaculty > 0 &&
				(dataGridView1[4, 0].Value.ToString() == "бакалавриат" || dataGridView1[4, 0].Value.ToString() == "магистратура" ||
				dataGridView1[4, 0].Value.ToString() == "аспирантура") &&
				(dataGridView1[5, 0].Value.ToString() == "очное" || dataGridView1[5, 0].Value.ToString() == "очно-заочное" ||
				dataGridView1[5, 0].Value.ToString() == "заочное") &&
				(dataGridView1[6, 0].Value.ToString() == "да" || dataGridView1[6, 0].Value.ToString() == "нет") &&
				(dataGridView1[7, 0].Value.ToString() == "да" || dataGridView1[7, 0].Value.ToString() == "нет") &&
				(dataGridView1[8, 0].Value.ToString() == "да" || dataGridView1[8, 0].Value.ToString() == "нет")
				)
			{
				ProgramConscripted program = new ProgramConscripted(id, idFaculty, dataGridView1[2, 0].Value.ToString(),
					dataGridView1[3, 0].Value.ToString(), dataGridView1[4, 0].Value.ToString(),
					dataGridView1[5, 0].Value.ToString(), dataGridView1[6, 0].Value.ToString() == "да",
					dataGridView1[7, 0].Value.ToString() == "да", dataGridView1[8, 0].Value.ToString() == "да");

				SqlCommand command;
				SqlDataReader sdr;
				// Доработать через процедуру проверки корректности связи направления и группы (а также типа обучения?)
				//string query = "INSERT INTO EducationalProgram(ProgramID, ProgramFacultyID, ProgramName, " +
				//	"ProgramCode, ProgramNotConscripted, ProgramLevel, ProgramFormOfTraining) VALUES " +					
				//	"(" + program.id + ", " + program.idFaculty + ", '" + program.name + "', '" + program.code + "', 'нет', '" +
				//	program.level + "', '" + program.form + "'";
				//command = new SqlCommand(query, f.connection);
				//sdr = command.ExecuteReader();

				//f.programs.Add(program);
				//i = f.programs.Count - 1;
				//currentProgram = f.programs[i];

				//sdr.Close();
				//FillGrid();

				//buttonNext.Enabled = false;
				//buttonPrev.Enabled = (f.programs.Count > 1);

				string query = "DECLARE @Res INT; EXECUTE @Res = Add_Program " + program.id + ", " + program.idFaculty + ", '"
					+ program.name + "', '" + program.code + "', '" +
					program.level + "', '" + program.form + "'; SELECT @Res;";
				command = new SqlCommand(query, f.connection);
				sdr = command.ExecuteReader();

				int procRes = 1;
				if (sdr.Read())
				{
					procRes = (int)sdr.GetValue(0);
				}
				sdr.Close();

				bool budget = dataGridView1[6, 0].Value.ToString() == "да";
				bool comm = dataGridView1[7, 0].Value.ToString() == "да";
				bool contr = dataGridView1[8, 0].Value.ToString() == "да";

				if ((procRes == 0) && (budget || comm || contr))
				{
					f.programs.Add(program);
					i = f.programs.Count - 1;
					currentProgram = f.programs[i];

					string query2 = "DECLARE @MaxID INT; SELECT @MaxID = Max(PrOptID) FROM ProgramsAndOptions; SELECT @MaxID";
					command = new SqlCommand(query2, f.connection);
					sdr = command.ExecuteReader();
					int maxID = 1;
					if (sdr.Read())
					{
						maxID = (int)sdr.GetValue(0) + 1;
					}
					sdr.Close();

					string query3 = "";
					if (budget)
					{
						query3 += "INSERT INTO ProgramsAndOptions(PrOptID, PrOptProgramID, PrOptOptionID) VALUES " +
							"(" + maxID + ", " + program.id + ", " + 1 + ");";
						maxID++;
					}
					if (comm)
					{
						query3 += "INSERT INTO ProgramsAndOptions(PrOptID, PrOptProgramID, PrOptOptionID) VALUES " +
							"(" + maxID + ", " + program.id + ", " + 2 + ");";
						maxID++;
					}
					if (contr)
					{
						query3 += "INSERT INTO ProgramsAndOptions(PrOptID, PrOptProgramID, PrOptOptionID) VALUES " +
							"(" + maxID + ", " + program.id + ", " + 3 + ");";
					}
					command = new SqlCommand(query3, f.connection);
					sdr = command.ExecuteReader();
					sdr.Close();	
				}
				else
				{
					MessageBox.Show("Некорректное изменение", "Сообщение");
					if (currentProgram != null)
					{
						dataGridView1[0, 0].Value = currentProgram.id;
						dataGridView1[1, 0].Value = currentProgram.idFaculty;
						dataGridView1[2, 0].Value = currentProgram.name;
						dataGridView1[3, 0].Value = currentProgram.code;
						dataGridView1[4, 0].Value = currentProgram.level;
						dataGridView1[5, 0].Value = currentProgram.form;

						if (currentProgram.budget)
						{
							dataGridView1[6, 0].Value = "да";
						}
						else
						{
							dataGridView1[6, 0].Value = "нет";
						}
						if (currentProgram.commerce)
						{
							dataGridView1[7, 0].Value = "да";
						}
						else
						{
							dataGridView1[7, 0].Value = "нет";
						}
						if (currentProgram.contract)
						{
							dataGridView1[8, 0].Value = "да";
						}
						else
						{
							dataGridView1[8, 0].Value = "нет";
						}
					}
				}
				FillGrid();
			}
			else
			{
				MessageBox.Show("Некорректный формат значений", "Сообщение");

				if (currentProgram != null)
				{
					dataGridView1[0, 0].Value = currentProgram.id;
					dataGridView1[1, 0].Value = currentProgram.idFaculty;
					dataGridView1[2, 0].Value = currentProgram.name;
					dataGridView1[3, 0].Value = currentProgram.code;
					dataGridView1[4, 0].Value = currentProgram.level;
					dataGridView1[5, 0].Value = currentProgram.form;

					if (currentProgram.budget)
					{
						dataGridView1[6, 0].Value = "да";
					}
					else
					{
						dataGridView1[6, 0].Value = "нет";
					}
					if (currentProgram.commerce)
					{
						dataGridView1[7, 0].Value = "да";
					}
					else
					{
						dataGridView1[7, 0].Value = "нет";
					}
					if (currentProgram.contract)
					{
						dataGridView1[8, 0].Value = "да";
					}
					else
					{
						dataGridView1[8, 0].Value = "нет";
					}
				}
			}
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			SqlCommand command;
			SqlDataReader sdr;
			// Доработать через процедуру проверки корректности связи направления и группы (а также типа обучения?)
			string query = "DELETE EducationalProgram WHERE ProgramID = " + currentProgram.id;
			command = new SqlCommand(query, f.connection);
			sdr = command.ExecuteReader();

			f.programs.RemoveAt(i);

			//if (i == f.programs.Count && f.programs.Count > 0)
			//{
			//	i--;
			//	buttonNext.Enabled = false;
			//}
			//else if (i == f.programs.Count - 1)
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

			if (i == f.programs.Count && f.programs.Count > 0)
			{
				i--;
			}

			sdr.Close();

			string query2 = "DELETE ProgramsAndOptions WHERE PrOptProgramID = " + currentProgram.id;
			command = new SqlCommand(query2, f.connection);
			sdr = command.ExecuteReader();
			sdr.Close();
			FillGrid();
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			int id, idFaculty;
			if (int.TryParse(dataGridView1[0, 0].Value.ToString(), out id) && id > 0 && 
				int.TryParse(dataGridView1[1, 0].Value.ToString(), out idFaculty) && idFaculty > 0 &&
				(dataGridView1[4, 0].Value.ToString() == "бакалавриат" || dataGridView1[4, 0].Value.ToString() == "магистратура" ||
				dataGridView1[4, 0].Value.ToString() == "аспирантура") &&
				(dataGridView1[5, 0].Value.ToString() == "очное" || dataGridView1[5, 0].Value.ToString() == "очно-заочное" ||
				dataGridView1[5, 0].Value.ToString() == "заочное") &&
				(dataGridView1[6, 0].Value.ToString() == "да" || dataGridView1[6, 0].Value.ToString() == "нет") &&
				(dataGridView1[7, 0].Value.ToString() == "да" || dataGridView1[7, 0].Value.ToString() == "нет") &&
				(dataGridView1[8, 0].Value.ToString() == "да" || dataGridView1[8, 0].Value.ToString() == "нет")
				)
			{
				ProgramConscripted program = new ProgramConscripted(id, idFaculty, dataGridView1[2, 0].Value.ToString(),
					dataGridView1[3, 0].Value.ToString(), dataGridView1[4, 0].Value.ToString(),
					dataGridView1[5, 0].Value.ToString(), dataGridView1[6, 0].Value.ToString() == "да",
					dataGridView1[7, 0].Value.ToString() == "да", dataGridView1[8, 0].Value.ToString() == "да");

				SqlCommand command;
				SqlDataReader sdr;
				// Доработать через процедуру проверки корректности связи направления и группы (а также типа обучения?)
				//string query = "UPDATE EducationalProgram SET ProgramID = " + id +
				//	", ProgramFacultyID = " + idFaculty +
				//	", ProgramName = '" + dataGridView1[2, 0].Value.ToString() +
				//	"', ProgramCode = '" + dataGridView1[3, 0].Value.ToString() +
				//	"', ProgramLevel = '" + dataGridView1[4, 0].Value.ToString() +
				//	"', ProgramForm = '" + dataGridView1[5, 0].Value.ToString() +
				//	"' WHERE ProgramID = " + currentProgram.id;
				//command = new SqlCommand(query, f.connection);
				//sdr = command.ExecuteReader();

				//f.programs[i].id = id;
				//f.programs[i].idFaculty = idFaculty;
				//f.programs[i].name = dataGridView1[2, 0].Value.ToString();
				//f.programs[i].code = dataGridView1[3, 0].Value.ToString();
				//f.programs[i].level = dataGridView1[4, 0].Value.ToString();
				//f.programs[i].form = dataGridView1[5, 0].Value.ToString();

				//sdr.Close();
				//FillGrid();

				string query = "DECLARE @Res INT; EXECUTE @Res = Edit_Program " + currentProgram.id + ", " + id + ", " + idFaculty + ", '" 
					+ dataGridView1[2, 0].Value.ToString() + "', '" + dataGridView1[3, 0].Value.ToString() + "', '" + 
					dataGridView1[4, 0].Value.ToString() + "', '" + dataGridView1[5, 0].Value.ToString() + "'; SELECT @Res;";
				command = new SqlCommand(query, f.connection);
				sdr = command.ExecuteReader();

				int procRes = 1;
				if (sdr.Read())
				{
					procRes = (int)sdr.GetValue(0);
				}
				sdr.Close();

				bool budget = dataGridView1[6, 0].Value.ToString() == "да";
				bool comm = dataGridView1[7, 0].Value.ToString() == "да";
				bool contr = dataGridView1[8, 0].Value.ToString() == "да";

				if ((procRes == 0) && (budget || comm || contr))
				{
					f.programs[i].id = id;
					f.programs[i].idFaculty = idFaculty;
					f.programs[i].name = dataGridView1[2, 0].Value.ToString();
					f.programs[i].code = dataGridView1[3, 0].Value.ToString();
					f.programs[i].level = dataGridView1[4, 0].Value.ToString();
					f.programs[i].form = dataGridView1[5, 0].Value.ToString();
					f.programs[i].budget = dataGridView1[6, 0].Value.ToString() == "да";
					f.programs[i].commerce = dataGridView1[7, 0].Value.ToString() == "да";
					f.programs[i].contract = dataGridView1[8, 0].Value.ToString() == "да";

					string query2 = "DELETE ProgramsAndOptions WHERE PrOptProgramID = " + currentProgram.id;
					command = new SqlCommand(query2, f.connection);
					sdr = command.ExecuteReader();
					sdr.Close();

					string query3 = "DECLARE @MaxID INT; SELECT @MaxID = Max(PrOptID) FROM ProgramsAndOptions; SELECT @MaxID";
					command = new SqlCommand(query3, f.connection);
					sdr = command.ExecuteReader();
					int maxID = 1;
					if (sdr.Read())
					{
						maxID = (int)sdr.GetValue(0) + 1;
					}
					sdr.Close();

					string query4 = "";
					if (budget)
					{
						query4 += "INSERT INTO ProgramsAndOptions(PrOptID, PrOptProgramID, PrOptOptionID) VALUES " +
							"(" + maxID + ", " + program.id + ", " + 1 + ");";
						maxID++;
					}
					if (comm)
					{
						query4 += "INSERT INTO ProgramsAndOptions(PrOptID, PrOptProgramID, PrOptOptionID) VALUES " +
							"(" + maxID + ", " + program.id + ", " + 2 + ");";
						maxID++;
					}
					if (contr)
					{
						query4 += "INSERT INTO ProgramsAndOptions(PrOptID, PrOptProgramID, PrOptOptionID) VALUES " +
							"(" + maxID + ", " + program.id + ", " + 3 + ");";
					}
					command = new SqlCommand(query4, f.connection);
					sdr = command.ExecuteReader();
					sdr.Close();
				}
				else
				{
					MessageBox.Show("Некорректное изменение", "Сообщение");
					if (currentProgram != null)
					{
						dataGridView1[0, 0].Value = currentProgram.id;
						dataGridView1[1, 0].Value = currentProgram.idFaculty;
						dataGridView1[2, 0].Value = currentProgram.name;
						dataGridView1[3, 0].Value = currentProgram.code;
						dataGridView1[4, 0].Value = currentProgram.level;
						dataGridView1[5, 0].Value = currentProgram.form;

						if (currentProgram.budget)
						{
							dataGridView1[6, 0].Value = "да";
						}
						else
						{
							dataGridView1[6, 0].Value = "нет";
						}
						if (currentProgram.commerce)
						{
							dataGridView1[7, 0].Value = "да";
						}
						else
						{
							dataGridView1[7, 0].Value = "нет";
						}
						if (currentProgram.contract)
						{
							dataGridView1[8, 0].Value = "да";
						}
						else
						{
							dataGridView1[8, 0].Value = "нет";
						}
					}
				}

				FillGrid();
			}
			else
			{
				MessageBox.Show("Некорректный формат значений", "Сообщение");
				if (currentProgram != null)
				{
					dataGridView1[0, 0].Value = currentProgram.id;
					dataGridView1[1, 0].Value = currentProgram.idFaculty;
					dataGridView1[2, 0].Value = currentProgram.name;
					dataGridView1[3, 0].Value = currentProgram.code;
					dataGridView1[4, 0].Value = currentProgram.level;
					dataGridView1[5, 0].Value = currentProgram.form;

					if (currentProgram.budget)
					{
						dataGridView1[6, 0].Value = "да";
					}
					else
					{
						dataGridView1[6, 0].Value = "нет";
					}
					if (currentProgram.commerce)
					{
						dataGridView1[7, 0].Value = "да";
					}
					else
					{
						dataGridView1[7, 0].Value = "нет";
					}
					if (currentProgram.contract)
					{
						dataGridView1[8, 0].Value = "да";
					}
					else
					{
						dataGridView1[8, 0].Value = "нет";
					}
				}
			}
		}
	}
}
