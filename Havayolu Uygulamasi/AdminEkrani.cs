using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Havayolu_Uygulamasi
{
    public partial class AdminEkrani : Form
    {
        public AdminEkrani()
        {
            InitializeComponent();
        }
        
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-QDTIDF2;Initial Catalog=Havaaalani;Integrated Security=True");


        private void AdminEkrani_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'havaaalaniDataSet4.pilot' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.pilotTableAdapter.Fill(this.havaaalaniDataSet4.pilot);

            // TODO: Bu kod satırı 'havaaalaniDataSet1.flight' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.flightTableAdapter.Fill(this.havaaalaniDataSet1.flight);

            SqlCommand komut = new SqlCommand("SELECT * FROM pilot", conn);
            SqlDataReader dr;
            conn.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr["id"]);
            }
            conn.Close();

            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Airline";
            dataGridView1.Columns[2].HeaderText = "Origin";
            dataGridView1.Columns[3].HeaderText = "Destination";
            dataGridView1.Columns[4].HeaderText = "Flight Date";
            dataGridView1.Columns[5].HeaderText = "Capacity";
            dataGridView1.Columns[6].HeaderText = "Pilot Id";

            dataGridView2.Columns[0].HeaderText = "Pilot Id";
            dataGridView2.Columns[1].HeaderText = "Pilot Name";
            dataGridView2.Columns[2].HeaderText = "Pilot Surname";
            dataGridView2.Columns[3].HeaderText = "Pilot City";
            dataGridView2.Columns[4].HeaderText = "Pilot Gender";
            dataGridView2.Columns[5].HeaderText = "Pilot Date of Birth";
            dataGridView2.Columns[6].HeaderText = "Pilot Marrige";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into flight (airline,origin,destination,fligtdate,capacity,pilotID) values (@airline,@origin,@destination,@flightdate,@capacity,@pilotID)", conn);
            
            cmd.Parameters.AddWithValue("@airline", txtAirline.Text);
            cmd.Parameters.AddWithValue("@origin", txtOrigin.Text);
            cmd.Parameters.AddWithValue("@destination", txtDestintion.Text);
            cmd.Parameters.AddWithValue("@flightdate", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@capacity", comboBox1.SelectedItem);
            cmd.Parameters.AddWithValue("@pilotID", comboBox2.SelectedItem);
            cmd.ExecuteNonQuery();  
            conn.Close();
            MessageBox.Show("Uçuş ayarlandı");
            this.flightTableAdapter.Fill(this.havaaalaniDataSet1.flight);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into pilot (isim,surname,city,gender,dateofbirth,marrige) values (@isim,@surname,@city,@gender,@dateofbirth,@marrige)", conn);

            cmd.Parameters.AddWithValue("@isim", txtname.Text);
            cmd.Parameters.AddWithValue("@surname", txtsurname.Text);
            cmd.Parameters.AddWithValue("@city", txtcity.Text);
            if (radioButtonfemale.Checked)
            {
                cmd.Parameters.AddWithValue("@gender", "Kadın");
            }
            else
            {
                cmd.Parameters.AddWithValue("@gender", "Erkek");
            }
            cmd.Parameters.AddWithValue("@dateofbirth", dateTimePicker2.Value);
            if (chcmarried.Checked)
            {
                cmd.Parameters.AddWithValue("@marrige", "Evli");
            }
            else
            {
                cmd.Parameters.AddWithValue("@marrige", "Evli Değil");
            }
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Pilot Eklendi");
            this.pilotTableAdapter.Fill(this.havaaalaniDataSet4.pilot);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secim = dataGridView2.SelectedCells[0].RowIndex;
            dataGridView2.CurrentRow.Selected = true;
            txtId.Text = dataGridView2.Rows[secim].Cells[0].Value.ToString();
            txtname.Text = dataGridView2.Rows[secim].Cells[1].Value.ToString();
            txtsurname.Text = dataGridView2.Rows[secim].Cells[2].Value.ToString();
            txtcity.Text = dataGridView2.Rows[secim].Cells[3].Value.ToString();
            label8.Text = dataGridView2.Rows[secim].Cells[4].Value.ToString();
            dateTimePicker2.Value = (DateTime)dataGridView2.Rows[secim].Cells[5].Value;
            label9.Text = dataGridView2.Rows[secim].Cells[6].Value.ToString();
        }

        private void label8_TextChanged(object sender, EventArgs e)
        {
            if (label8.Text=="Erkek")
            {
                radioButtonmale.Checked = true;
            }
            if (label8.Text=="Kadın")
            {
                radioButtonfemale.Checked = true;
            }
        }

        private void radioButtonmale_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonmale.Checked==true)
            {
                label8.Text = "True";
            }
        }

        private void radioButtonfemale_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonfemale.Checked == true)
            {
                label8.Text = "False";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand sil=new SqlCommand("delete from pilot where id=@id",conn);
            sil.Parameters.AddWithValue("@id", txtId.Text);
            sil.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Kayıt silindi");
            this.pilotTableAdapter.Fill(this.havaaalaniDataSet4.pilot);
        }

        private void label9_TextChanged(object sender, EventArgs e)
        {
            if (label9.Text == "Evli")
            {
                chcmarried.Checked = true;
            }
            if (label9.Text == "Evli Değil")
            {
                chcmarried.Checked = false;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand guncelle = new SqlCommand("update pilot set isim=@isim,surname=@surname,city=@city,gender=@gender,dateofbirth=@dateofbirth,marrige=@marrige where id=@id",conn);
            guncelle.Parameters.AddWithValue("isim",txtname.Text);
            guncelle.Parameters.AddWithValue("surname",txtsurname.Text);
            guncelle.Parameters.AddWithValue("city",txtcity.Text);
            if(radioButtonfemale.Checked)
            {
                guncelle.Parameters.AddWithValue("gender", "Kadın");
            }
            else
            {
                guncelle.Parameters.AddWithValue("gender", "Erkek");
            }
            guncelle.Parameters.AddWithValue("dateofbirth", dateTimePicker2.Value);
            if (chcmarried.Checked)
            {
                guncelle.Parameters.AddWithValue("marrige", "Evli");
            }
            else
            {
                guncelle.Parameters.AddWithValue("marrige", "Evli Değil");
            }
            guncelle.Parameters.AddWithValue("id", txtId.Text);
            guncelle.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Personel Güncellemesi Tamamlandı");
            this.pilotTableAdapter.Fill(this.havaaalaniDataSet4.pilot);
        }
        
        void temizle()
        {
            txtId.Text = "";
            txtname.Text = "";
            txtsurname.Text = "";
            txtcity.Text = "";
            radioButtonfemale.Checked = false;
            radioButtonmale.Checked = false;
            dateTimePicker2.Value = DateTime.Now;
            chcmarried.Checked = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand guncelle = new SqlCommand("update flight set airline=@airline,origin=@origin,destination=@destination,fligtdate=@fligtdate,capacity=@capacity,pilotID=@pilotID where id=@id", conn);
            guncelle.Parameters.AddWithValue("airline", txtAirline.Text);
            guncelle.Parameters.AddWithValue("origin", txtOrigin.Text);
            guncelle.Parameters.AddWithValue("destination", txtDestintion.Text);            
            guncelle.Parameters.AddWithValue("fligtdate", dateTimePicker1.Value);
            guncelle.Parameters.AddWithValue("capacity", comboBox1.Text);
            guncelle.Parameters.AddWithValue("pilotID", comboBox2.Text);
            guncelle.Parameters.AddWithValue("id", txtfgID.Text);
            guncelle.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Uçuş Güncellemesi Tamamlandı");
            this.flightTableAdapter.Fill(this.havaaalaniDataSet1.flight);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;
            dataGridView1.CurrentRow.Selected = true;
            txtfgID.Text = dataGridView1.Rows[secim].Cells[0].Value.ToString();
            txtAirline.Text = dataGridView1.Rows[secim].Cells[1].Value.ToString();
            txtOrigin.Text = dataGridView1.Rows[secim].Cells[2].Value.ToString();
            txtDestintion.Text = dataGridView1.Rows[secim].Cells[3].Value.ToString();
            dateTimePicker1.Value = (DateTime)dataGridView1.Rows[secim].Cells[4].Value;
            comboBox1.Text = dataGridView1.Rows[secim].Cells[5].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[secim].Cells[6].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand sil = new SqlCommand("delete from flight where id=@id", conn);
            sil.Parameters.AddWithValue("@id", txtfgID.Text);
            sil.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Uçuş silindi");
            this.flightTableAdapter.Fill(this.havaaalaniDataSet1.flight);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtfgID.Text = "";
            txtAirline.Text = "";
            txtOrigin.Text = "";
            txtDestintion.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            comboBox1.Text = "";
            comboBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelUcus.Visible = true;
            panelPilot.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelUcus.Visible = false;
            panelPilot.Visible = true;
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtsearchflight.Text))
                {
                    flightBindingSource.Filter = string.Empty;  
                }
                else
                {
                    flightBindingSource.Filter=string.Format("{0}='{1}'",cmbColumn.Text,txtsearchflight.Text);
                }
            }
        }

        private void txtsearchPilot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtsearchPilot.Text))
                {
                    pilotBindingSource.Filter = string.Empty;
                }
                else
                {
                    pilotBindingSource.Filter = string.Format("{0}='{1}'", comboBoxpilot.Text, txtsearchPilot.Text);
                }
            }
        }

        private void cmbColumn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }
    }
}
