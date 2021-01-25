using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace БД
{
    public partial class Авторизация : Form
    {
        int ColPopitok = 3;
        public Авторизация()
        {
            InitializeComponent();
            TbLogin.UseSystemPasswordChar = false;
        }


        private void TbLogin_Click(object sender, EventArgs e)
        {
            TbLogin.Clear();
        }

        private void TbPassword_Click(object sender, EventArgs e)
        {
            TbLogin.Clear();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            //Меню_студента M_S = new Меню_студента();
            //M_S.Show();
            //this.Hide();
        }

        private void TbPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void TbLogin_TextChanged(object sender, EventArgs e)
        {

        }

        private void Авторизация_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (TbLogin.Text != "" && TbPass.Text != "")
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-CSSH9GB\SQLEXPRESS;Initial Catalog=MVC;Integrated Security=True"))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandText = "select Password from [Users] where Email ='" + TbLogin.Text + "'";
                        string password = Convert.ToString(cmd.ExecuteScalar());

                        if (password != "")
                        {
                            if (password == TbPass.Text)
                            {
                                MessageBox.Show("Авторизация прошла успешно");
                                cmd.CommandText = "select Roleld from [Users] where Email ='" + TbLogin.Text + "'";
                                string roleid = Convert.ToString(cmd.ExecuteScalar());
                                switch (roleid)
                                {
                                    case
                                    "0":
                                        {
                                            Меню_преподавателя admin = new Меню_преподавателя(roleid);
                                            admin.Show();
                                            this.Hide();
                                        }
                                        break;
                                    case
                                    "1":
                                        {
                                            Меню_студента user = new Меню_студента(roleid);
                                            user.Show();
                                            this.Hide();
                                        }
                                        break;
                                }
                            }
                            else { MessageBox.Show("Неверный пароль!"); ColPopitok--; }
                        }
                        else { MessageBox.Show("Неверный Email!"); ColPopitok--; }

                        if (ColPopitok == 0)
                        {
                            MessageBox.Show("Попробуйте позже!");
                            Application.Exit();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Convert.ToString(ex));
                    }
                    finally
                    {
                        con.Close();
                    }

                }

            }
            else
                MessageBox.Show("Введите данные!");
        }
        Point lastPoint;

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Авторизация_MouseDown_1(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void Авторизация_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
    }
}
