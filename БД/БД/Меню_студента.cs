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
    public partial class Меню_студента : Form
    {
        string SurnameS, NameS, PatronomycS;
        string idClient;

        public Меню_студента(string roleid)
        {
            InitializeComponent();
            this.idClient = roleid;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }



        private void button3_Click(object sender, EventArgs e)
        {
            Преподаватели Prepods = new Преподаватели(idClient);
            Prepods.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Информация_о_предстоящих_экзаменах Exsams = new Информация_о_предстоящих_экзаменах(idClient);
            Exsams.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }
        Point lastPoint;
        private void Меню_студента_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void Меню_студента_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void Меню_студента_Load(object sender, EventArgs e)
        {
            try
            {
                string connect = @"Data Source=DESKTOP-CSSH9GB\SQLEXPRESS;Initial Catalog=MVC;Integrated Security=True";
                SqlConnection con = new SqlConnection(connect);
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Surname FROM [Users] WHERE Roleld= '" + idClient + "' ";
                SurnameS = Convert.ToString(cmd.ExecuteScalar());
                string fio = Convert.ToString(cmd.ExecuteScalar());
                this.FIOStudents.Text += fio + " ";
                cmd.CommandText = "SELECT Name FROM [Users] WHERE Roleld= '" + idClient + "' ";
                NameS = Convert.ToString(cmd.ExecuteScalar());
                fio = Convert.ToString(cmd.ExecuteScalar());
                this.FIOStudents.Text += fio + " ";
                cmd.CommandText = "SELECT Patronymic FROM [Users] WHERE Roleld= '" + idClient + "' ";
                PatronomycS = Convert.ToString(cmd.ExecuteScalar());
                fio = Convert.ToString(cmd.ExecuteScalar());
                this.FIOStudents.Text += fio;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Мои_оценки Balls = new Мои_оценки(idClient, SurnameS, NameS, PatronomycS);
            Balls.Show();
            this.Hide();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
