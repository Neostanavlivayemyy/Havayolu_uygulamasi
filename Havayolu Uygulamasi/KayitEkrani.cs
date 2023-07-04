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
    public partial class KayitEkrani : Form
    {
        public KayitEkrani()
        {
            InitializeComponent();
        }
        static string constring = "Data Source=DESKTOP-QDTIDF2;Initial Catalog=Havaaalani;Integrated Security=True";
        SqlConnection connect = new SqlConnection(constring);
        private void btnKayit_Click(object sender, EventArgs e)
        {
            Form1 girisEkrani = new Form1();
            girisEkrani.Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked) 
            {
                textBox3.PasswordChar = '\0';
            }
            else
            {
                textBox3.PasswordChar = '*';
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text==textBox3.Text)
            {
                try
                {
                    if (connect.State == ConnectionState.Closed)
                    {
                        connect.Open();
                    }
                    string kayit = "insert into users (nickname,sifre) values(@kullaniciAdi,@sifre)";
                    SqlCommand komut = new SqlCommand(kayit, connect);

                    komut.Parameters.AddWithValue("@kullaniciAdi", textBox1.Text);
                    komut.Parameters.AddWithValue("@sifre", textBox2.Text);

                    komut.ExecuteNonQuery();

                    connect.Close();
                    MessageBox.Show("Eklendi");
                }
                catch (Exception hata)
                {
                    MessageBox.Show("Hata Meydana Geldi" + hata.Message);
                    throw;
                }
            }
        }

        private void KayitEkrani_Load(object sender, EventArgs e)
        {

        }
    }
}
