using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;

namespace Metaloks_İş_Takip_Platformu
{
    public partial class form_giris : Form
    {
        public form_giris()
        {
            InitializeComponent();
        }
        bool giris_kontrol;
        
        form_islemler f_islemler = new form_islemler();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[1].ConnectionString);
        public void kullanicicagir()
        {
            string kul_adi = textBox1.Text.ToLower();
            string sifre = textBox2.Text;
            try
            {
                SqlCommand cmd = new SqlCommand("Select * from kullanicilar ", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if ((dt.Rows[i][0].ToString() == kul_adi) && (dt.Rows[i][1].ToString() == sifre))
                    { giris_kontrol = true; break; }
                    else
                        giris_kontrol = false;
                }
            
            }
            catch { MessageBox.Show("Hata oluştu.Sunucuyu çalıştırmayı deneyiniz...","Bilgilendirme",MessageBoxButtons.OK,MessageBoxIcon.Warning); }
        
        }

        private void button_giris_Click(object sender, EventArgs e)
        {
            form_giris f_giris = new form_giris();
            kullanicicagir();
            if ((giris_kontrol == true) )
            {   f_islemler.Show();  this.Hide(); }

            else
            { label_bilgi_icerik.Text = "Hatalı giriş..."; label_bilgi_icerik.Visible = true; }
        }

        private void form_giris_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Yöneticinize başvurunuz...", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

      

       
    

