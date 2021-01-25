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
    public partial class Мои_оценки : Form
    {
        string idClient;
        string SurnameS;
        string NameS;
        string PatronomycS;
        public Мои_оценки(string roleid, string surname, string name, string patronomyc)
        {
            InitializeComponent();
            this.idClient = roleid;
            this.SurnameS = surname;
            this.NameS = name;
            this.PatronomycS = patronomyc;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Меню_студента M_S = new Меню_студента(idClient);
            M_S.Show();
            this.Hide();
        }

        private void Мои_оценки_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-CSSH9GB\SQLEXPRESS;Initial Catalog=MVC;Integrated Security=True"))
            {
                con.Open(); string connectString = "Data Source=.\\SQLEXPRESS;Initial Catalog=MVC;" +
               "Integrated Security=True;";
                SqlConnection myConnection = new SqlConnection(connectString);

                myConnection.Open();
                string query = "SELECT  Subject_Name, Exams_Type, Exams_Date, Ball FROM Balls2 WHERE Student_Surname = '" + SurnameS + "'";
                SqlCommand command = new SqlCommand(query, myConnection);
                SqlDataReader reader = command.ExecuteReader();

                List<string[]> data = new List<string[]>();

                while (reader.Read())
                {
                    data.Add(new string[4]);

                    data[data.Count - 1][0] = reader[0].ToString();
                    data[data.Count - 1][1] = reader[1].ToString();
                    data[data.Count - 1][2] = reader[2].ToString();
                    data[data.Count - 1][3] = reader[3].ToString();
                }

                reader.Close();

                myConnection.Close();

                foreach (string[] s in data)
                    dataGridView1.Rows.Add(s);
            }
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
        private void button6_Click(object sender, EventArgs e)
        {
            exporttoPDF(dataGridView1, "Мои оценки");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
