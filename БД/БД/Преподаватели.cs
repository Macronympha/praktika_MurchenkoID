using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace БД
{
    public partial class Преподаватели : Form
    {
        string idClient;
        public Преподаватели(string roleid)
        {
            InitializeComponent();
            this.idClient = roleid;
        }

        private void Преподаватели_Load(object sender, EventArgs e)
        {
            string connectString = "Data Source=.\\SQLEXPRESS;Initial Catalog=MVC;" +
               "Integrated Security=True;";
            SqlConnection myConnection = new SqlConnection(connectString);

            myConnection.Open();
            string query = "SELECT Subjects_Name, Teachers_Surname, Teachers_Name, Teachers_Patronymic, DateOfBirth, Email, Phone FROM Teachers JOIN Subjects ON Teachers.Subjects_ID=Subjects.Subjects_ID";
            SqlCommand command = new SqlCommand(query, myConnection);
            SqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[7]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();
                data[data.Count - 1][6] = reader[6].ToString();
            }

            reader.Close();

            myConnection.Close();

            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Меню_студента M_S = new Меню_студента(idClient);
            M_S.Show();
            this.Hide();
        }
        Point lastPoint;
        private void Преподаватели_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void Преподаватели_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void сохранитьToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

