using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using Task = System.Threading.Tasks.Task;
using Application = System.Windows.Forms.Application;
using System.Data.OleDb;

namespace БД
{
    public partial class Меню_преподавателя : Form
    {
        string idClient;
        public Меню_преподавателя(string roleid)
        {

            InitializeComponent();
            this.idClient = roleid;
        }
        
       
        private void Меню_преподавателя_Load(object sender, EventArgs e)
        {
            try
            {
                string connect = @"Data Source=DESKTOP-CSSH9GB\SQLEXPRESS;Initial Catalog=MVC;Integrated Security=True";
                SqlConnection con = new SqlConnection(connect);
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Surname FROM [Users] WHERE Roleld= '" + idClient + "' ";
                string fio = Convert.ToString(cmd.ExecuteScalar());
                this.FIOPrepods.Text += fio + " ";
                cmd.CommandText = "SELECT Name FROM [Users] WHERE Roleld= '" + idClient + "' ";
                fio = Convert.ToString(cmd.ExecuteScalar());
                this.FIOPrepods.Text += fio + " ";
                cmd.CommandText = "SELECT Patronymic FROM [Users] WHERE Roleld= '" + idClient + "' ";
                fio = Convert.ToString(cmd.ExecuteScalar());
                this.FIOPrepods.Text += fio;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Текущие_оценки Balls = new Текущие_оценки(idClient);
            Balls.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }
        Point lastPoint;


        private void Меню_преподавателя_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void Меню_преподавателя_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }



        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void FIOPrepods_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Информация_о_предстоящих_экзаменах Inf = new Информация_о_предстоящих_экзаменах(idClient);
            Inf.Show();
            this.Hide();
        }
    }
}
