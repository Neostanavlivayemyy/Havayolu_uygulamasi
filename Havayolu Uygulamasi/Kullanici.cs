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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Havayolu_Uygulamasi
{
    public partial class Kullanici : Form
    {
        public Kullanici()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-QDTIDF2;Initial Catalog=Havaaalani;Integrated Security=True");
        private void Kullanici_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'havaaalaniDataSet10.pilot' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.pilotTableAdapter.Fill(this.havaaalaniDataSet10.pilot);
            // TODO: Bu kod satırı 'havaaalaniDataSet9.flight' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.flightTableAdapter.Fill(this.havaaalaniDataSet9.flight);
            // TODO: Bu kod satırı 'havaaalaniDataSet7.ticked' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.tickedTableAdapter.Fill(this.havaaalaniDataSet7.ticked);

            SqlCommand cmd = new SqlCommand("SELECT flight.id,airline,origin,destination,fligtdate,capacity,isim,surname,city,gender,dateofbirth,marrige FROM flight LEFT JOIN pilot ON flight.pilotID = pilot.id", conn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand= cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Airline";
            dataGridView1.Columns[2].HeaderText = "Origin";
            dataGridView1.Columns[3].HeaderText = "Destination";
            dataGridView1.Columns[4].HeaderText = "Flight Date";
            dataGridView1.Columns[5].HeaderText = "Capacity";
            dataGridView1.Columns[6].HeaderText = "Pilot Name";
            dataGridView1.Columns[7].HeaderText = "Pilot Surname";
            dataGridView1.Columns[8].HeaderText = "Pilot City";
            dataGridView1.Columns[9].HeaderText = "Pilot Gender";
            dataGridView1.Columns[10].HeaderText = "Pilot Dt";
            dataGridView1.Columns[11].HeaderText = "Pilot Marriage";


            dataGridView2.Columns[0].HeaderText = "Flight Id";
            dataGridView2.Columns[1].HeaderText = "Sead No";

            SqlCommand komut = new SqlCommand("SELECT * FROM flight", conn);
            SqlDataReader dr;
            conn.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox3.Items.Add(dr["id"]);
            }
            conn.Close();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;
            dataGridView1.CurrentRow.Selected = true;
            comboBox3.Text = dataGridView1.Rows[secim].Cells[0].Value.ToString();
            txtAirline.Text = dataGridView1.Rows[secim].Cells[1].Value.ToString();
            txtOrigin.Text = dataGridView1.Rows[secim].Cells[2].Value.ToString();
            txtDestintion.Text = dataGridView1.Rows[secim].Cells[3].Value.ToString();
            dateTimePicker1.Value = (DateTime)dataGridView1.Rows[secim].Cells[4].Value;
            txtCapacity.Text = dataGridView1.Rows[secim].Cells[5].Value.ToString();
            txtPilotname.Text = dataGridView1.Rows[secim].Cells[6].Value.ToString();
            txtpilotsurname.Text = dataGridView1.Rows[secim].Cells[7].Value.ToString();
            txtpilotcity.Text = dataGridView1.Rows[secim].Cells[8].Value.ToString();
            label15.Text = dataGridView1.Rows[secim].Cells[9].Value.ToString();
            dateTimePicker2.Value = (DateTime)dataGridView1.Rows[secim].Cells[10].Value;
            label16.Text = dataGridView1.Rows[secim].Cells[11].Value.ToString();
        }

        private void label15_TextChanged(object sender, EventArgs e)
        {
            if (label15.Text == "Erkek")
            {
                radioButtonmale.Checked = true;
            }
            if (label15.Text == "Kadın")
            {
                radioButtonfemale.Checked = true;
            }
        }

        private void label16_TextChanged(object sender, EventArgs e)
        {
            if (label16.Text == "Evli")
            {
                checkBox1.Checked = true;
            }
            if (label16.Text == "Evli Değil")
            {
                checkBox1.Checked = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtCapacity.Text == "20")
            {
                pictureBox20kisi.Visible = true;
                pictureBox40kisi.Visible = false;
            }
            else if (txtCapacity.Text == "40")
            {
                pictureBox40kisi.Visible = true;
                pictureBox20kisi.Visible = false;
            }
        }
        bool durum;
        void mukerrer()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from ticked where flightId=@flightId and seatNo=@seatNo", conn);
            cmd.Parameters.AddWithValue("@flightId", comboBox3.Text);
            cmd.Parameters.AddWithValue("@seatNo", richTextBox1.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                durum = false;
            }
            else
            {
                durum=true;
            }
            conn.Close();

        }
        private void button4_Click(object sender, EventArgs e)
        {
            mukerrer();
            SqlCommand cmd = new SqlCommand("insert into ticked (flightId,seatNo) values (@flightId,@seatNo)", conn);

            if (durum == true)
            {
                if (txtCapacity.Text == "20")
                {
                    if (int.Parse(richTextBox1.Text) > 0 && int.Parse(richTextBox1.Text) <= 20)
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("@flightId", comboBox3.Text);
                        cmd.Parameters.AddWithValue("@seatNo", richTextBox1.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Bilet Alındı");
                    }
                    else
                    {
                        MessageBox.Show("Lütfen 1 ile 20 arasında bi sayı girin");
                    }
                }
                else if (txtCapacity.Text == "40")
                {
                    if (int.Parse(richTextBox1.Text) > 0 && int.Parse(richTextBox1.Text) <= 40)
                    {
                        conn.Open(); 
                        cmd.Parameters.AddWithValue("@flightId", comboBox3.Text);
                        cmd.Parameters.AddWithValue("@seatNo", richTextBox1.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Bilet Alındı");
                    }
                    else
                    {
                        MessageBox.Show("Lütfen 1 ile 40 arasında bi sayı girin");
                    }
                }
            }
            else
            {
                MessageBox.Show("Bu bilet zaten alınmış");
            }


            this.tickedTableAdapter.Fill(this.havaaalaniDataSet7.ticked);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            conn.Open();

            SqlCommand sil = new SqlCommand("delete from ticked where flightId=@flightId and seatNo=@seatNo", conn);
            sil.Parameters.AddWithValue("@flightId", txtticketFlightId.Text);
            sil.Parameters.AddWithValue("@seatNo", txttickeseatno.Text);
            if (sil.ExecuteNonQuery()==0)
            {
                MessageBox.Show("Bilet Bulunamadı");
            }
            else
            {
                MessageBox.Show("Bilet Silindi");
            }
            conn.Close();
            this.tickedTableAdapter.Fill(this.havaaalaniDataSet7.ticked);
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secim = dataGridView2.SelectedCells[0].RowIndex;
            dataGridView2.CurrentRow.Selected = true;
            txtticketFlightId.Text = dataGridView2.Rows[secim].Cells[0].Value.ToString();
            txttickeseatno.Text = dataGridView2.Rows[secim].Cells[1].Value.ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panelhome.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible=true;
            panelhome.Visible=false;
        }
    }
}
