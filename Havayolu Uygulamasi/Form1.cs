using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;

namespace Havayolu_Uygulamasi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnKayit_Click(object sender, EventArgs e)
        {
            KayitEkrani kayitEkrani = new KayitEkrani();
            kayitEkrani.Show();
            this.Hide();
        }
        

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            

            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        private void btnGiris_Click(object sender, EventArgs e)
        {
            AdminEkrani adminekran = new AdminEkrani();
            if (textBox1.Text != "admin" && textBox2.Text != "123")
            {
                string sorgu = "SELECT * FROM users where nickname=@nickname AND sifre=@sifre";
                con = new SqlConnection("Data Source=DESKTOP-QDTIDF2;Initial Catalog=Havaaalani;Integrated Security=True");
                cmd = new SqlCommand(sorgu, con);

                cmd.Parameters.AddWithValue("@nickname", textBox1.Text);
                cmd.Parameters.AddWithValue("@sifre", textBox2.Text);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Tebrikler! Başarılı bir şekilde giriş yaptınız. ");
                    Kullanici kullanici = new Kullanici();
                    kullanici.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adını ve şifrenizi kontrol ediniz.");
                }
                con.Close();
            }
            if (textBox1.Text=="admin" && textBox2.Text=="123")
            {
                MessageBox.Show("Tebrikler! Başarılı bir şekilde giriş yaptınız. ");
                adminekran.Show();
                this.Hide();
            }
        }
    }
}
