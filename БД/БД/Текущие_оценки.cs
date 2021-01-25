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
    public partial class Текущие_оценки : Form
    {

        string idClient;
        public Текущие_оценки(string roleid)
        {
            InitializeComponent();
            this.idClient = roleid;
        }

        Point lastPoint;
        private void Текущие_оценки_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void Текущие_оценки_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Меню_преподавателя M_P = new Меню_преподавателя(idClient);
            M_P.Show();
            this.Hide();
        }
        private void LoadData()
        {
            //using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-CSSH9GB\SQLEXPRESS;Initial Catalog=MVC;Integrated Security=True"))
            //{
            //    con.Open(); string connectString = "Data Source=.\\SQLEXPRESS;Initial Catalog=MVC;" +
            //   "Integrated Security=True;";
            //    SqlConnection myConnection = new SqlConnection(connectString);

            //    myConnection.Open();
            //    string query = "SELECT Student_Name,Student_Surname,Student_Patronomyc, Group_Name, Group_Number, Subject_Name, Exams_Type, Exams_Date, Ball FROM Balls2";
            //    SqlCommand command = new SqlCommand(query, myConnection);
            //    SqlDataReader reader = command.ExecuteReader();

            //    List<string[]> data = new List<string[]>();

            //    while (reader.Read())
            //    {
            //        data.Add(new string[8]);

            //        data[data.Count - 1][0] = reader[0].ToString() +" " + reader[1].ToString() + " " + reader[2].ToString();
            //        data[data.Count - 1][1] = reader[3].ToString() + " " + reader[4].ToString();
            //        data[data.Count - 1][2] = reader[5].ToString();
            //        data[data.Count - 1][3] = reader[6].ToString();
            //        data[data.Count - 1][4] = reader[7].ToString();
            //        data[data.Count - 1][5] = reader[8].ToString();
            //    }

            //    reader.Close();

            //    myConnection.Close();

            //    foreach (string[] s in data)
            //        dataGridView1.Rows.Add(s);

        }


        private void button1_Click(object sender, EventArgs e)
        {


        }
        private void Текущие_оценки_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "mVCDataSet.Balls2". При необходимости она может быть перемещена или удалена.
            this.balls2TableAdapter.Fill(this.mVCDataSet.Balls2);
            //LoadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //int delete = dataGridView1.SelectedCells[0].RowIndex;
            //dataGridView1.Rows.RemoveAt(delete);
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
            exporttoPDF(balls2dataGridView1, "Текущие оценки студентов");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < balls2dataGridView1.RowCount; i++)
            {
                balls2dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < balls2dataGridView1.ColumnCount; j++)
                    if (balls2dataGridView1.Rows[i].Cells[j].Value != null)
                        if (balls2dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))
                        {
                            balls2dataGridView1.Rows[i].Selected = true;
                            break;
                        }
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {

            }
        
        private void button2_Click(object sender, EventArgs e)
        {
            balls2BindingSource.Filter = "[Subject_Name] LIKE'" + textBox2.Text + "%'";

        }

        private void ballsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void balls2BindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.balls2BindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.mVCDataSet);

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
                        exporttoPDF(balls2dataGridView1, "Текущие оценки студентов");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

        }

        private void сохранитьToolStripButton_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.balls2BindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.mVCDataSet);
        }

        private void balls2BindingNavigator_RefreshItems(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
        }
    }
    }

