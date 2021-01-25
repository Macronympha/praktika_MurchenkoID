using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.OleDb;
using System.IO;
using System.Data.SqlClient;

namespace БД
{
    public partial class Информация_о_предстоящих_экзаменах : Form
    {
        string idClient;
        public Информация_о_предстоящих_экзаменах(string roleid)
        {
            InitializeComponent();
            this.idClient = roleid;
        }
        public void exporttoPDF(DataGridView dgv, string filename)
        {
            BaseFont bf = BaseFont.CreateFont(@"C:\Windows\Fonts\Calibri.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            PdfPTable pdftable = new PdfPTable(dgv.Columns.Count);
            pdftable.DefaultCell.Padding = 3;
            pdftable.WidthPercentage = 100;
            pdftable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdftable.DefaultCell.BorderWidth = 1;

            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);

            //добавить шапку
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));

                pdftable.AddCell(cell);
            }
            // добавить строку
            foreach (DataGridViewRow row in dgv.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    pdftable.AddCell(new Phrase(cell.Value.ToString(), text));
                }
            }
            var sfd = new SaveFileDialog();
            sfd.FileName = filename;
            sfd.DefaultExt = ".pdf";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();
                    pdfdoc.Add(pdftable);
                    pdfdoc.Close();
                    stream.Close();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            exporttoPDF(dataGridView1, "Информация о предстоящих экзаменах");
        }
        private void Информация_о_предстоящих_экзаменах_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-CSSH9GB\SQLEXPRESS;Initial Catalog=MVC;Integrated Security=True"))
            {
                con.Open(); string connectString = "Data Source=.\\SQLEXPRESS;Initial Catalog=MVC;" +
               "Integrated Security=True;";
            SqlConnection myConnection = new SqlConnection(connectString);

            myConnection.Open();
            string query = "SELECT Groups_Name, Groups_Number,Exams_Date, Exams_Type, Subjects_Name, Teachers_Surname, Teachers_Name, Teachers_Patronymic FROM Exams INNER JOIN Teachers ON Exams.Subjects_ID = Teachers.Subjects_ID INNER JOIN Subjects ON Exams.Subjects_ID = Subjects.Subjects_ID INNER JOIN Groups ON Exams.Groups_ID = Groups.Groups_ID";
            SqlCommand command = new SqlCommand(query, myConnection);
            SqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[8]);

                    data[data.Count - 1][0] = reader[0].ToString() + " " + reader[1].ToString() + " ";
                data[data.Count - 1][1] = reader[2].ToString();
                data[data.Count - 1][2] = reader[3].ToString();
                data[data.Count - 1][3] = reader[4].ToString();
                data[data.Count - 1][4] = reader[5].ToString()+" "+ reader[6].ToString()+" "+ reader[7].ToString();
            }

            reader.Close();

            myConnection.Close();

            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }
    }
        Point lastPoint;
        private void Информация_о_предстоящих_экзаменах_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void Информация_о_предстоящих_экзаменах_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            switch (idClient)
            {
                case
                "0":
                    {
                        Меню_преподавателя admin = new Меню_преподавателя(idClient);
                        admin.Show();
                        this.Hide();
                    }
                    break;
                case
                "1":
                    {
                        Меню_студента user = new Меню_студента(idClient);
                        user.Show();
                        this.Hide();
                    }
                    break;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
