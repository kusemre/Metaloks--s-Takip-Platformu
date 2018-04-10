using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;


using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace Metaloks_İş_Takip_Platformu
{
    public partial class form_islemler : Form
    {
        public form_islemler()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[1].ConnectionString);
        int tick1 = 59;
        int tick2 = 59;
        int tick3 = 59;
        int tut1;
        string islem1_zaman = "";
        string islem2_zaman = "";
        string islem3_zaman = "";
        int islem1_saat;
        int islem1_dakika;
        int islem2_saat;
        int islem2_dakika;
        int islem3_saat;
        int islem3_dakika;

       
        private void Form2_Load(object sender, EventArgs e)
        {
            textBox_aciklama.AutoSize = false; 
            textBox_aciklama.Height = 25;
            textBox_aciklama.Width = 200;


            label_tarih_icerik.Text = DateTime.Now.ToString();
            is_sil.Visible = false;
            biteni_alinan_ise_aktar.Visible = false;
            biten_is_sil.Visible = false;
            biten_isleri_goster.Visible = false;
            biten_islere_aktar.Visible = false;
            secilen_islemi_sil.Visible = false;
            islemeal.Visible = false;
            timer1.Interval = 1000;
            timer2.Interval = 1000;
            timer3.Interval = 1000;
            firmagetir();
            malzemegetir();
            onaylayangetir();
            comboBox3.SelectedIndex = 0;
            dataGridView1.AllowUserToAddRows = false;
            groupBox_buton_sure.Visible = false;
        }     
        void kaydet1()
        {
            double kalinlik = double.Parse(textBox2.Text);
            double uzunluk = double.Parse(textBox3.Text);
            double genislik = double.Parse(textBox5.Text);
            double sonuc = (double)(kalinlik * uzunluk * genislik) / (double)125000;
            string str = sonuc.ToString();
            string sonucc = str.Replace(',', '.');
            string sorgu = "Insert into alinanisler(Firma_Adi,Teklif_Tarihi,Onay_Tarihi,Onay_Alan,Malzeme_Cinsi,Kilogram,Teslim_Zamani,Makina,Verilen_Sure,Aciklama) Values('" + comboBox1.Text + "','" + dateTimePicker1.Text.ToString() + "','" + dateTimePicker2.Text.ToString() + "','" + onay_alan.Text + "','" + comboBox2.Text + "',  '" + sonucc + "' ,'" + dateTimePicker3.Text.ToString() + "','" + comboBox3.SelectedItem.ToString() + "','"+textbox_bitis_saat.Text+":"+textbox_bitis_dk.Text+"','"+ textBox_aciklama.Text +"')";
            con.Open();
            SqlCommand cmd = new SqlCommand(sorgu, con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            label10.Visible = false;
            islerigetir();
        }
        private void kaydet_Click(object sender, EventArgs e)
        {
            try
            {
                bool genel_drm = false;
                bool saat_drm = false;
                bool dk_drm = false;

                if (comboBox1.Text.Trim() == "")
                { label10.Visible = true; label10.Text = "*Firma adı boş girilemez."; }
                else if (onay_alan.Text.Trim() == "")
                { label10.Visible = true; label10.Text = "*Onay alan boş girilemez."; }
                else if (comboBox2.Text.Trim() == "")
                { label10.Visible = true; label10.Text = "*Malzeme cinsi boş girilemez."; }
                else if (comboBox3.Text.Trim() == "")
                { label10.Visible = true; label10.Text = "*Makina türü boş girilemez."; }
                else if (textbox_bitis_saat.Text.Trim() == "")
                { label10.Visible = true; label10.Text = "*Bitiş saati boş girilemez."; }
                else if (textbox_bitis_dk.Text.Trim() == "")
                { label10.Visible = true; label10.Text = "*Bitiş dakikası boş girilemez."; }
                else if (textBox_aciklama.Text.Trim() == "")
                { label10.Visible = true; label10.Text = "*Açıklama boş girilemez."; }
                else genel_drm = true;

                if (int.Parse(textbox_bitis_saat.Text) != -1)
                    saat_drm = true;
                else saat_drm = false;

                if (int.Parse(textbox_bitis_dk.Text) != -1)
                    dk_drm = true;
                else dk_drm = false;

                if (genel_drm && saat_drm && dk_drm) { kaydet1(); is_getir_Click(sender, e); }
            }
            catch (Exception ex)
            {
                label10.Visible = true; label10.Text = "*Lütfen malzemenin uzunluklarını veya \n işlem süresini doğru biçimde giriniz...";
                
            }
        }
        void islerigetir()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from alinanisler ", con);
            SqlDataReader dr = cmd.ExecuteReader();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            con.Close();
           
        }
        int islemdekilerigetir()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select id,Firma_Adi,Teklif_Tarihi,Onay_Tarihi,Onay_Alan,Malzeme_Cinsi,Kilogram,Teslim_Zamani,Makina,Verilen_Sure,Aciklama from islemdekiisler ORDER BY islem_id asc ", con);
            SqlDataReader dr = cmd.ExecuteReader();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            con.Close();
            return dt.Rows.Count;
        }

        private void sil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DialogResult secenek = MessageBox.Show("Kaydı silmek istediğinize emin misiniz?","DİKKAT!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (secenek == DialogResult.Yes)
                {
                    kayit_sil("alinanisler", dataGridView1.SelectedRows[0].Cells[0].Value);
                    islerigetir();
                    biten_islere_aktar.Visible = false;
                    biten_is_sil.Visible = false;
                    alinan_isleri_sec();
                }
            }
            else MessageBox.Show("Kayıt Bulunamadı.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void islemeal_Click(object sender, EventArgs e)
        {
            bool varmi = false;
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {

                    object id = dataGridView1.SelectedRows[i].Cells[0].Value;
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("Select id from islemdekiisler ORDER BY id", con);
                    SqlDataReader dr = cmd1.ExecuteReader();
                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt.Load(dr);
                    con.Close();

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (id.ToString() == dt.Rows[j][0].ToString())
                        { varmi = true; break; }
                        else varmi = false;

                    }

                    if (dt.Rows.Count + 1 <= 3)
                        if (varmi)
                            MessageBox.Show("ID si '" + id + "' olan zaten işlemdedir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("INSERT INTO islemdekiisler VALUES('" + dataGridView1.SelectedRows[i].Cells[0].Value + "','" + dataGridView1.SelectedRows[i].Cells[1].Value + "','" + dataGridView1.SelectedRows[i].Cells[2].Value + "','" + dataGridView1.SelectedRows[i].Cells[3].Value + "','" + dataGridView1.SelectedRows[i].Cells[4].Value + "','" + dataGridView1.SelectedRows[i].Cells[5].Value + "','" + dataGridView1.SelectedRows[i].Cells[6].Value + "','" + dataGridView1.SelectedRows[i].Cells[7].Value + "','" + dataGridView1.SelectedRows[i].Cells[8].Value + "','" + dataGridView1.SelectedRows[i].Cells[9].Value + "','" + dataGridView1.SelectedRows[i].Cells[10].Value + "')", con);
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            con.Close();
                            MessageBox.Show("Seçilen İş İşleme Alınmıştır...", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    else MessageBox.Show("Maksimum işleme sayısına ulaşılmıştır....!", "İşleme Alınamadı...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    biten_islere_aktar.Visible = false;
                    biten_is_sil.Visible = false;
                    biteni_alinan_ise_aktar.Visible = false;
                    alinan_isleri_sec();
                }
            }
            else MessageBox.Show("Kayıt Bulunamadı.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void firmagetir()
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("Select * from firmalar", con);
            SqlDataReader dr = cmd1.ExecuteReader();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Load(dr);
            con.Close();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i][0].ToString());
            }
        }
       void malzemegetir()
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("Select * from malzeme_cinsi",con);
            SqlDataReader dr = cmd1.ExecuteReader();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Load(dr);
            con.Close();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox2.Items.Add(dt.Rows[i][0].ToString());
            }
        }
        void onaylayangetir()
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("Select * from Onaylayan",con);
            SqlDataReader dr = cmd1.ExecuteReader();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Load(dr);
            con.Close();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                onay_alan.Items.Add(dt.Rows[i][0].ToString());
            }
        }
        private void firma_ekle_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Trim() == "")

            { label10.Visible = true; label10.Text = "*Firma adı boş girilemez."; }
            else
            {
                bool firma_drm = false;
                con.Open();
                SqlCommand cmd1 = new SqlCommand("Select * from firmalar", con);
                SqlDataReader dr = cmd1.ExecuteReader();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Load(dr);
                con.Close();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == comboBox1.Text.ToUpper())
                    { firma_drm = true; break; }
                    else firma_drm = false;
                }

                if (firma_drm == false)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO firmalar VALUES('" + comboBox1.Text.ToUpper() + "')", con);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();
                    comboBox1.Items.Clear();
                    firmagetir();
                    label10.Visible = false;
                }
                else MessageBox.Show("Firma Mevcut", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void firma_cikar_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from firmalar where firma_adi='"+comboBox1.Text+"' ", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            comboBox1.Items.Clear();
            firmagetir();
            

        }

        private void malzeme_ekle_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text.Trim() == "")
            { label10.Visible = true; label10.Text = "*Malzeme cinsi boş girilemez."; }

            else
            {
                bool malzeme_cinsi_drm = false;
                con.Open();
                SqlCommand cmd1 = new SqlCommand("Select * from malzeme_cinsi", con);
                SqlDataReader dr = cmd1.ExecuteReader();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Load(dr);
                con.Close();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == comboBox2.Text.ToUpper())
                    { malzeme_cinsi_drm = true; break; }
                    else malzeme_cinsi_drm = false;
                }

                if (malzeme_cinsi_drm == false)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO malzeme_cinsi VALUES('" + comboBox2.Text.ToUpper() + "')", con);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();
                    comboBox2.Items.Clear();
                    malzemegetir();
                    label10.Visible = false;
                }
                else MessageBox.Show("Malzeme Mevcut", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void malzeme_cikar_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from malzeme_cinsi where malzeme_cinsi='" + comboBox2.Text + "' ", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            comboBox2.Items.Clear();
            malzemegetir();
        }

        private void onay_ekle_Click(object sender, EventArgs e)
        {
            if (onay_alan.Text.Trim() == "")
            { label10.Visible = true; label10.Text = "*Onay alan boş girilemez."; }
            else
            {
                

                bool onaylayan_drm = false;
                con.Open();
                SqlCommand cmd1 = new SqlCommand("Select * from Onaylayan", con);
                SqlDataReader dr = cmd1.ExecuteReader();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Load(dr);
                con.Close();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == onay_alan.Text.ToUpper())
                    { onaylayan_drm = true; break; }
                    else onaylayan_drm = false;
                }

                if (onaylayan_drm == false)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Onaylayan VALUES('" + onay_alan.Text.ToUpper() + "')", con);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();
                    onay_alan.Items.Clear();
                    onaylayangetir();
                    label10.Visible = false;
                }
                else MessageBox.Show("Bu Kişi Mevcut", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void onay_cikar_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from Onaylayan where Onaylayan='" + onay_alan.Text + "' ", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            onay_alan.Items.Clear();
            onaylayangetir();
        }
        
        public void alinan_isleri_sec()
        {
            groupBox_islemdeki_isler.BackColor = Color.Transparent;
            groupBox_biten_isler.BackColor = Color.Transparent;
            groupBox_alinan_isler.BackColor = Color.Maroon;
        }
        public void islemdekileri_sec()
        {
          
            groupBox_biten_isler.BackColor = Color.Transparent;
            groupBox_alinan_isler.BackColor = Color.Transparent;
            groupBox_islemdeki_isler.BackColor = Color.Maroon;
        }
        public void biten_isleri_sec()
        {
            
            groupBox_biten_isler.BackColor = Color.Maroon;
            groupBox_alinan_isler.BackColor = Color.Transparent;
            groupBox_islemdeki_isler.BackColor = Color.Transparent;
        }

        private void is_getir_Click(object sender, EventArgs e)
        {
            biteni_alinan_ise_aktar.Visible = false;
            biten_is_sil.Visible = false;
            islemeal.Visible = true;
            biten_islere_aktar.Visible = false;
            secilen_islemi_sil.Visible = false;
            is_sil.Visible = true;
            islerigetir();
            alinan_isleri_sec();
            groupBox_buton_sure.Visible = false;
        }
        
        private void islemdekileri_goster_Click(object sender, EventArgs e)
        {
            biteni_alinan_ise_aktar.Visible = false;
            biten_is_sil.Visible = false;
            biten_isleri_goster.Visible = true;
            biten_islere_aktar.Visible = true;
            islemeal.Visible = false;
            is_sil.Visible = false;
            biten_islere_aktar.Visible = true;
            secilen_islemi_sil.Visible = true;
            tut1 = islemdekilerigetir();
            islemdekileri_sec();
            groupBox_buton_sure.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            if (tick1 == 0)
            {
                if (islem1_dakika == 0)
                {
                    if (islem1_saat == 0)
                    { timer1.Stop(); }
                    tick1 = 59; islem1_dakika = 59; islem1_saat = islem1_saat - 1; }
                else
                { tick1 = 59; islem1_dakika = islem1_dakika - 1; }
            }

            if (tick1 == 1)
                if (islem1_dakika == 0)
                    if (islem1_saat == 0)
                    { timer1.Stop(); }
            if (islem1_saat == 0) label_sure1.ForeColor = Color.Red;
            else if (islem1_saat == 1) label_sure1.ForeColor = Color.Yellow;

            label_sure1.Text = islem1_saat + ":" + islem1_dakika + ":" +  tick1.ToString();

            tick1--;

        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (tick2 == 0)
            {
                if (islem2_dakika == 0)
                {
                    if (islem2_saat == 0)
                    { timer2.Stop(); }
                    tick2 = 59; islem2_dakika = 59; islem2_saat = islem2_saat - 1;
                }
                else
                { tick2 = 59; islem2_dakika = islem2_dakika - 1; }
            }

            if (tick2 == 1)
                if (islem2_dakika == 0)
                    if (islem2_saat == 0)
                    { timer2.Stop(); }
            if (islem2_saat == 0) label_sure2.ForeColor = Color.Red;
            else if (islem2_saat == 1) label_sure2.ForeColor = Color.Yellow;
            label_sure2.Text = islem2_saat + ":" + islem2_dakika + ":" + tick2.ToString();

            tick2--;
        }
         private void timer3_Tick(object sender, EventArgs e)
        {
            if (tick3 == 0)
            {
                if (islem3_dakika == 0)
                {
                    if (islem3_saat == 0)
                    { timer3.Stop(); }
                    tick3 = 59; islem3_dakika = 59; islem3_saat = islem3_saat - 1;
                }
                else
                { tick3 = 59; islem3_dakika = islem3_dakika - 1; }
            }

            if (tick3 == 1)
                if (islem3_dakika == 0)
                    if (islem3_saat == 0)
                    { timer3.Stop(); }
            if (islem3_saat == 0) label_sure3.ForeColor = Color.Red;
            else if (islem3_saat == 1) label_sure3.ForeColor = Color.Yellow;
            label_sure3.Text = islem3_saat + ":" + islem3_dakika + ":" + tick3.ToString();

            tick3--;
          
        }

        private void yazdir_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.AllowOrientation = false;
            DialogResult yazdirmaislemi;
            yazdirmaislemi = printDialog1.ShowDialog();
            if (yazdirmaislemi == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
     
        

        private void secilen_islemi_sil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DialogResult secenek = MessageBox.Show("Kaydı silmek istediğinize emin misiniz?", "DİKKAT!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (secenek == DialogResult.Yes)
                    if (button_islem1_baslat.Enabled && button_islem2_baslat.Enabled && button_islem3_baslat.Enabled)
                    {
                        kayit_sil("islemdekiisler", dataGridView1.SelectedRows[0].Cells[0].Value);
                        islemdekilerigetir();
                        biten_islere_aktar.Visible = true;
                        biten_is_sil.Visible = false;
                        islemdekileri_sec();
                    }
                    else
                    {
                        MessageBox.Show("İşleme başlandığı için kayıt silinemez...", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
            }
            else MessageBox.Show("Kayıt Bulunamadı.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
       
        private void biten_islere_aktar_Click(object sender, EventArgs e)
        {
            string bitis_suresi = "";
            groupBox_buton_sure.Visible = true;
            if (dataGridView1.Rows.Count > 0)
            {

                if ((dataGridView1.CurrentCell.RowIndex + 1) == 1 && dataGridView1.RowCount == 3)
                {
                    string[] islem_aktar = dataGridView1.SelectedRows[0].Cells[9].Value.ToString().Split(':');
                    int islem_aktar_saat = int.Parse(islem_aktar[0]);
                    int islem_aktar_dk = int.Parse(islem_aktar[1]);

                    if (button_islem1_baslat.Enabled == false)
                    {
                        timer1.Stop();
                        button_islem1_baslat.Enabled = true;


                        label_sure1.Text = "Bekleniyor...";
                        label_sure2.Text = "Bekleniyor...";
                        if (islem1_dakika > islem_aktar_dk)
                        {
                            islem_aktar_dk = (60 - islem1_dakika) + islem_aktar_dk; islem_aktar_saat--;
                            bitis_suresi = islem_aktar_saat.ToString() + ":" + islem_aktar_dk.ToString();
                        }
                        else
                            bitis_suresi = (islem_aktar_saat - islem1_saat).ToString() + ":" + (islem_aktar_dk - islem1_dakika).ToString();
                    }
                    else if (button_islem1_baslat.Enabled != false)
                    {
                        button1_Click(sender, e);
                        timer1.Stop();
                        button_islem1_baslat.Enabled = true;
                        if (islem1_dakika > islem_aktar_dk)
                        {
                            islem_aktar_dk = (60 - islem1_dakika) + islem_aktar_dk; islem_aktar_saat--;
                            bitis_suresi = islem_aktar_saat.ToString() + ":" + islem_aktar_dk.ToString();
                        }
                        else
                            bitis_suresi = (islem_aktar_saat - islem1_saat).ToString() + ":" + (islem_aktar_dk - islem1_dakika).ToString();
                    }


                    if (button_islem2_baslat.Enabled == false)
                    {

                        islem1_saat = islem2_saat;
                        islem1_dakika = islem2_dakika;

                        button2_Click(sender, e);
                        timer2.Stop();
                        button_islem1_baslat.Enabled = false;
                        button_islem2_baslat.Enabled = true;

                        timer1.Start();

                        label_sure2.Text = "Bekleniyor...";


                    }
                    else if ((button_islem2_baslat.Enabled != false))
                    {
                        button2_Click(sender, e);
                        islem1_saat = islem2_saat;
                        islem1_dakika = islem2_dakika;
                        timer1.Stop();
                        timer2.Stop();

                        button_islem1_baslat.Enabled = true;
                        button_islem2_baslat.Enabled = true;
                        label_sure2.Text = "Bekleniyor...";

                    }
                    if (button_islem3_baslat.Enabled == false)
                    {

                        islem2_saat = islem3_saat;
                        islem2_dakika = islem3_dakika;

                        button3_Click(sender, e);
                        timer3.Stop();
                        button_islem2_baslat.Enabled = false;
                        button_islem3_baslat.Enabled = true;

                        timer2.Start();

                        label_sure3.Text = "Bekleniyor...";


                    }
                    else if ((button_islem3_baslat.Enabled != false))
                    {
                        button3_Click(sender, e);
                        islem2_saat = islem3_saat;
                        islem2_dakika = islem3_dakika;
                        timer2.Stop();
                        timer3.Stop();

                        button_islem2_baslat.Enabled = true;
                        button_islem3_baslat.Enabled = true;
                        label_sure3.Text = "Bekleniyor...";

                    }
                    
                    kayit_ekle("biten_isler", bitis_suresi);
                    kayit_sil("islemdekiisler", dataGridView1.SelectedRows[0].Cells[0].Value);
                    kayit_sil("alinanisler", dataGridView1.SelectedRows[0].Cells[0].Value);

                }
                else if ((dataGridView1.CurrentCell.RowIndex + 1) == 2 && dataGridView1.RowCount == 3)
                {
                    string[] islem_aktar = dataGridView1.SelectedRows[0].Cells[9].Value.ToString().Split(':');
                    int islem_aktar_saat = int.Parse(islem_aktar[0]);
                    int islem_aktar_dk = int.Parse(islem_aktar[1]);

                    if (button_islem2_baslat.Enabled == false)
                    {
                        timer2.Stop();
                        button_islem2_baslat.Enabled = true;
                        label_sure2.Text = "Bekleniyor...";
                        if (islem2_dakika > islem_aktar_dk)
                        {
                            islem_aktar_dk = (60 - islem2_dakika) + islem_aktar_dk; islem_aktar_saat--;
                            bitis_suresi = islem_aktar_saat.ToString() + ":" + islem_aktar_dk.ToString();
                        }
                        else
                            bitis_suresi = (islem_aktar_saat - islem2_saat).ToString() + ":" + (islem_aktar_dk - islem2_dakika).ToString();

                    }
                    else if (button_islem2_baslat.Enabled != false)
                    {
                        button2_Click(sender, e);
                        timer2.Stop();
                        button_islem2_baslat.Enabled = true;
                        if (islem2_dakika > islem_aktar_dk)
                        {
                            islem_aktar_dk = (60 - islem2_dakika) + islem_aktar_dk; islem_aktar_saat--;
                            bitis_suresi = islem_aktar_saat.ToString() + ":" + islem_aktar_dk.ToString();
                        }
                        else
                            bitis_suresi = (islem_aktar_saat - islem2_saat).ToString() + ":" + (islem_aktar_dk - islem2_dakika).ToString();
                    }
                   
                    if (button_islem3_baslat.Enabled == false)
                    {

                        islem2_saat = islem3_saat;
                        islem2_dakika = islem3_dakika;

                        button3_Click(sender, e);
                        timer3.Stop();
                        button_islem2_baslat.Enabled = false;
                        button_islem3_baslat.Enabled = true;

                        timer2.Start();

                        label_sure3.Text = "Bekleniyor...";


                    }
                    else if ((button_islem3_baslat.Enabled != false))
                    {
                        button3_Click(sender, e);
                        islem2_saat = islem3_saat;
                        islem2_dakika = islem3_dakika;
                        timer2.Stop();
                        timer3.Stop();

                        button_islem2_baslat.Enabled = true;
                        button_islem3_baslat.Enabled = true;
                        label_sure3.Text = "Bekleniyor...";

                    }
                    kayit_ekle("biten_isler", bitis_suresi);
                    kayit_sil("islemdekiisler", dataGridView1.SelectedRows[0].Cells[0].Value);
                    kayit_sil("alinanisler", dataGridView1.SelectedRows[0].Cells[0].Value);
                }
                else if ((dataGridView1.CurrentCell.RowIndex + 1) == 3 && dataGridView1.RowCount == 3)
                {
                    string[] islem_aktar = dataGridView1.SelectedRows[0].Cells[9].Value.ToString().Split(':');
                    int islem_aktar_saat = int.Parse(islem_aktar[0]);
                    int islem_aktar_dk = int.Parse(islem_aktar[1]);


                    if (button_islem3_baslat.Enabled == false)
                    {
                        timer3.Stop();
                        button_islem3_baslat.Enabled = true;
                        label_sure3.Text = "Bekleniyor...";
                        if (islem3_dakika > islem_aktar_dk)
                        {
                            islem_aktar_dk = (60 - islem3_dakika) + islem_aktar_dk; islem_aktar_saat--;
                            bitis_suresi = islem_aktar_saat.ToString() + ":" + islem_aktar_dk.ToString();
                        }
                        else
                            bitis_suresi = (islem_aktar_saat - islem3_saat).ToString() + ":" + (islem_aktar_dk - islem3_dakika).ToString();

                    }
                    else if (button_islem3_baslat.Enabled != false)
                    {
                        button3_Click(sender, e);
                        timer3.Stop();
                        button_islem3_baslat.Enabled = true;
                        if (islem3_dakika > islem_aktar_dk)
                        {
                            islem_aktar_dk = (60 - islem3_dakika) + islem_aktar_dk; islem_aktar_saat--;
                            bitis_suresi = islem_aktar_saat.ToString() + ":" + islem_aktar_dk.ToString();
                        }
                        else
                            bitis_suresi = (islem_aktar_saat - islem3_saat).ToString() + ":" + (islem_aktar_dk - islem3_dakika).ToString();
                    }

                    kayit_ekle("biten_isler", bitis_suresi);
                    kayit_sil("islemdekiisler", dataGridView1.SelectedRows[0].Cells[0].Value);
                    kayit_sil("alinanisler", dataGridView1.SelectedRows[0].Cells[0].Value);
                  


                }
                else if ((dataGridView1.CurrentCell.RowIndex + 1) == 1 && dataGridView1.RowCount == 2)
                {
                    string[] islem_aktar = dataGridView1.SelectedRows[0].Cells[9].Value.ToString().Split(':');
                    int islem_aktar_saat = int.Parse(islem_aktar[0]);
                    int islem_aktar_dk = int.Parse(islem_aktar[1]);
                    if (button_islem1_baslat.Enabled == false)
                    {
                        timer1.Stop();
                        button_islem1_baslat.Enabled = true;


                        label_sure1.Text = "Bekleniyor...";
                        label_sure2.Text = "Bekleniyor...";
                        if (islem1_dakika > islem_aktar_dk)
                        {
                            islem_aktar_dk = (60 - islem1_dakika) + islem_aktar_dk; islem_aktar_saat--;
                            bitis_suresi = islem_aktar_saat.ToString() + ":" + islem_aktar_dk.ToString();
                        }
                        else
                            bitis_suresi = (islem_aktar_saat - islem1_saat).ToString() + ":" + (islem_aktar_dk - islem1_dakika).ToString();
                    }
                    else if (button_islem1_baslat.Enabled != false)
                    {
                        button1_Click(sender, e);
                        timer1.Stop();
                        button_islem1_baslat.Enabled = true;
                        if (islem1_dakika > islem_aktar_dk)
                        {
                            islem_aktar_dk = (60 - islem1_dakika) + islem_aktar_dk; islem_aktar_saat--;
                            bitis_suresi = islem_aktar_saat.ToString() + ":" + islem_aktar_dk.ToString();
                        }
                        else
                            bitis_suresi = (islem_aktar_saat - islem1_saat).ToString() + ":" + (islem_aktar_dk - islem1_dakika).ToString();
                    }


                    if (button_islem2_baslat.Enabled == false)
                    {

                        islem1_saat = islem2_saat;
                        islem1_dakika = islem2_dakika;

                        button2_Click(sender, e);
                        timer2.Stop();
                        button_islem1_baslat.Enabled = false;
                        button_islem2_baslat.Enabled = true;

                        timer1.Start();

                        label_sure2.Text = "Bekleniyor...";


                    }
                    else if ((button_islem2_baslat.Enabled != false))
                    {
                        button2_Click(sender, e);
                        islem1_saat = islem2_saat;
                        islem1_dakika = islem2_dakika;
                        timer1.Stop();
                        timer2.Stop();

                        button_islem1_baslat.Enabled = true;
                        button_islem2_baslat.Enabled = true;
                        label_sure2.Text = "Bekleniyor...";

                    }
                    kayit_ekle("biten_isler", bitis_suresi);
                    kayit_sil("islemdekiisler", dataGridView1.SelectedRows[0].Cells[0].Value);
                    kayit_sil("alinanisler", dataGridView1.SelectedRows[0].Cells[0].Value);
                }
                else if ((dataGridView1.CurrentCell.RowIndex + 1) == 2 && dataGridView1.RowCount == 2)
                {
                    string[] islem_aktar = dataGridView1.SelectedRows[0].Cells[9].Value.ToString().Split(':');
                    int islem_aktar_saat = int.Parse(islem_aktar[0]);
                    int islem_aktar_dk = int.Parse(islem_aktar[1]);
                    if (button_islem2_baslat.Enabled == false)
                    {
                        timer2.Stop();
                        button_islem2_baslat.Enabled = true;
                        label_sure2.Text = "Bekleniyor...";
                        if (islem2_dakika > islem_aktar_dk)
                        {
                            islem_aktar_dk = (60 - islem2_dakika) + islem_aktar_dk; islem_aktar_saat--;
                            bitis_suresi = islem_aktar_saat.ToString() + ":" + islem_aktar_dk.ToString();
                        }
                        else
                            bitis_suresi = (islem_aktar_saat - islem2_saat).ToString() + ":" + (islem_aktar_dk - islem2_dakika).ToString();

                    }
                    else if (button_islem2_baslat.Enabled != false)
                    {
                        button2_Click(sender, e);
                        timer2.Stop();
                        button_islem2_baslat.Enabled = true;
                        if (islem2_dakika > islem_aktar_dk)
                        {
                            islem_aktar_dk = (60 - islem2_dakika) + islem_aktar_dk; islem_aktar_saat--;
                            bitis_suresi = islem_aktar_saat.ToString() + ":" + islem_aktar_dk.ToString();
                        }
                        else
                            bitis_suresi = (islem_aktar_saat - islem2_saat).ToString() + ":" + (islem_aktar_dk - islem2_dakika).ToString();
                    }
                    kayit_ekle("biten_isler", bitis_suresi);
                    kayit_sil("islemdekiisler", dataGridView1.SelectedRows[0].Cells[0].Value);
                    kayit_sil("alinanisler", dataGridView1.SelectedRows[0].Cells[0].Value);

                }
                else if ((dataGridView1.CurrentCell.RowIndex + 1) == 1 && dataGridView1.RowCount == 1)
                {
                    string[] islem_aktar = dataGridView1.SelectedRows[0].Cells[9].Value.ToString().Split(':');
                    
                    int islem_aktar_saat = int.Parse(islem_aktar[0]);
                    int islem_aktar_dk = int.Parse(islem_aktar[1]);
                    if (button_islem1_baslat.Enabled == false)
                    {
                        timer1.Stop();
                        button_islem1_baslat.Enabled = true;
                        label_sure1.Text = "Bekleniyor...";

                        if (islem1_dakika > islem_aktar_dk)
                        {
                            islem_aktar_dk = (60 - islem1_dakika) + islem_aktar_dk; islem_aktar_saat--;
                            bitis_suresi = islem_aktar_saat.ToString() + ":" + islem_aktar_dk.ToString();
                        }
                        else
                            bitis_suresi = (islem_aktar_saat - islem1_saat).ToString() + ":" + (islem_aktar_dk - islem1_dakika).ToString();
                    }
                    else if (button_islem1_baslat.Enabled != false)
                    {
                        button1_Click(sender, e);
                        timer1.Stop();
                        button_islem1_baslat.Enabled = true;
                        if (islem1_dakika > islem_aktar_dk)
                        {
                            islem_aktar_dk = (60 - islem1_dakika) + islem_aktar_dk; islem_aktar_saat--;
                            bitis_suresi = islem_aktar_saat.ToString() + ":" + islem_aktar_dk.ToString();
                        }
                        else
                            bitis_suresi = (islem_aktar_saat - islem1_saat).ToString() + ":" + (islem_aktar_dk - islem1_dakika).ToString();
                    }
                    kayit_ekle("biten_isler", bitis_suresi);
                    kayit_sil("islemdekiisler", dataGridView1.SelectedRows[0].Cells[0].Value);
                    kayit_sil("alinanisler", dataGridView1.SelectedRows[0].Cells[0].Value);
                  
                }
                MessageBox.Show("Biten işlere aktarılmıştır...", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                islemdekilerigetir();
                label_sure1.ForeColor = Color.White;
                label_sure2.ForeColor = Color.White;
                label_sure3.ForeColor = Color.White;


            }
            else MessageBox.Show("Kayıt Bulunamadı.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
           
           
            
           
        }
        void biten_isler()
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("Select id,Firma_Adi,Teklif_Tarihi,Onay_Tarihi,Onay_Alan,Malzeme_Cinsi,Kilogram,Teslim_Zamani,Makina,Verilen_Sure,Bitis_Suresi,Aciklama AS Aciklama_Metni From biten_isler ORDER BY id", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            System.Data.DataTable dt1 = new System.Data.DataTable();
            dt1.Load(dr1);
            dataGridView1.DataSource = dt1;
            con.Close();

        
        }
       
        private void biten_isleri_goster_Click(object sender, EventArgs e)
        {

            groupBox_buton_sure.Visible = false;
            biten_isler();
            biten_isleri_sec();

            is_sil.Visible = false;
            islemeal.Visible = false;
            secilen_islemi_sil.Visible = false;
            biteni_alinan_ise_aktar.Visible = true;
            biten_islere_aktar.Visible = false;
            biten_is_sil.Visible = true;
        }
        public void kayit_sil(string tablo_adi, object id)
        {
            try
            {
                string sorgu = "Delete From " + tablo_adi + " where id=" + id.ToString();
                SqlCommand cmd = new SqlCommand(sorgu, con);
                con.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
            }
            catch { MessageBox.Show("Kayıt Silinemedi....!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        private void biten_is_sil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DialogResult secenek = MessageBox.Show("Kaydı silmek istediğinize emin misiniz?", "DİKKAT!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (secenek == DialogResult.Yes)
                {
                    kayit_sil("biten_isler", dataGridView1.SelectedRows[0].Cells[0].Value);
                    biten_isler();
                    biten_isleri_sec();
                }
              
                
            }
            else
                MessageBox.Show("Kayıt Silinemedi....!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
        public void kayit_ekle(string tablo_adi)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO " + tablo_adi + "(Firma_Adi,Teklif_Tarihi,Onay_Tarihi,Onay_Alan,Malzeme_Cinsi,Kilogram,Teslim_Zamani,Makina,Verilen_Sure,Aciklama) VALUES('" + dataGridView1.SelectedRows[0].Cells[1].Value + "','" + dataGridView1.SelectedRows[0].Cells[2].Value + "','" + dataGridView1.SelectedRows[0].Cells[3].Value + "','" + dataGridView1.SelectedRows[0].Cells[4].Value + "','" + dataGridView1.SelectedRows[0].Cells[5].Value + "','" + dataGridView1.SelectedRows[0].Cells[6].Value + "','" + dataGridView1.SelectedRows[0].Cells[7].Value + "','" + dataGridView1.SelectedRows[0].Cells[8].Value + "','" + dataGridView1.SelectedRows[0].Cells[9].Value + "','" + dataGridView1.SelectedRows[0].Cells[11].Value + "') ", con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
            }
            catch { MessageBox.Show("Kayıt Eklenemedi!...", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        public void kayit_ekle(string tablo_adi, string bitis_suresi)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO " + tablo_adi + "(id,Firma_Adi,Teklif_Tarihi,Onay_Tarihi,Onay_Alan,Malzeme_Cinsi,Kilogram,Teslim_Zamani,Makina,Verilen_Sure,Bitis_Suresi,Aciklama) VALUES('" + dataGridView1.SelectedRows[0].Cells[0].Value + "','" + dataGridView1.SelectedRows[0].Cells[1].Value + "','" + dataGridView1.SelectedRows[0].Cells[2].Value + "','" + dataGridView1.SelectedRows[0].Cells[3].Value + "','" + dataGridView1.SelectedRows[0].Cells[4].Value + "','" + dataGridView1.SelectedRows[0].Cells[5].Value + "','" + dataGridView1.SelectedRows[0].Cells[6].Value + "','" + dataGridView1.SelectedRows[0].Cells[7].Value + "','" + dataGridView1.SelectedRows[0].Cells[8].Value + "','" + dataGridView1.SelectedRows[0].Cells[9].Value + "', '" + bitis_suresi + "', '" + dataGridView1.SelectedRows[0].Cells[10].Value + "')", con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
            }
            catch { MessageBox.Show("Kayıt Eklenemedi...." , "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        private void biteni_alinan_ise_aktar_Click(object sender, EventArgs e)
        {
            groupBox_buton_sure.Visible = false;
            if (dataGridView1.Rows.Count > 0)
            {
                kayit_ekle("alinanisler");
                kayit_sil("biten_isler",dataGridView1.SelectedRows[0].Cells[0].Value);
                biten_isler();
                biten_isleri_sec();
                MessageBox.Show("Alınan işlere tekrar aktarıldı.","Bilgilendirme",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else MessageBox.Show("Kayıt Bulunamadı.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            timer1.Stop();
        }
        public System.Data.DataTable islemdeki_isler()
        {
            SqlCommand islemdeki_isler = new SqlCommand("SELECT id,Verilen_Sure FROM islemdekiisler ORDER BY id", con);
            con.Open();
            SqlDataReader islem1_reader = islemdeki_isler.ExecuteReader();
            System.Data.DataTable islemdeki_isler_table = new System.Data.DataTable();
            islemdeki_isler_table.Load(islem1_reader);
            con.Close();
            return (System.Data.DataTable) islemdeki_isler_table;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            System.Data.DataTable islemdeki_isler_table = islemdeki_isler();
            if (islemdeki_isler_table.Rows.Count >= 1)
            {
                islem1_zaman = islemdeki_isler_table.Rows[0][1].ToString();
                label_sure1.Visible = true;
                string[] islem1 = islem1_zaman.Split(':');
                islem1_saat = int.Parse(islem1[0]);
                islem1_dakika = int.Parse(islem1[1]);
                timer1.Start();
                button_islem1_baslat.Enabled = false;
            }
            else
                MessageBox.Show("İşleme ait veri bulunamadı...", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            biten_islere_aktar.Visible = true;
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Data.DataTable islemdeki_isler_table = (System.Data.DataTable) islemdeki_isler();
            if (islemdeki_isler_table.Rows.Count >= 2)
            {
                islem2_zaman = islemdeki_isler_table.Rows[1][1].ToString();
                label_sure2.Visible = true;
                string[] islem2 = islem2_zaman.Split(':');
                islem2_saat = int.Parse(islem2[0]);
                islem2_dakika = int.Parse(islem2[1]);
    
                timer2.Start();
                button_islem2_baslat.Enabled = false;
            }
            else
                MessageBox.Show("İşleme ait veri bulunamadı...", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            biten_islere_aktar.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Data.DataTable islemdeki_isler_table = islemdeki_isler();
             if (islemdeki_isler_table.Rows.Count == 3)
             {
                 islem3_zaman = islemdeki_isler_table.Rows[2][1].ToString();
                 label_sure3.Visible = true;
                 string[] islem3 = islem3_zaman.Split(':');
                 islem3_saat = int.Parse(islem3[0]);
                 islem3_dakika = int.Parse(islem3[1]);
                 timer3.Start();
                 button_islem3_baslat.Enabled = false;
             }
             else
                MessageBox.Show("İşleme ait veri bulunamadı...", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            biten_islere_aktar.Visible = true;
        }

        private void button_excele_aktar_Click(object sender, EventArgs e)
        {
            Excel._Application excel = new Excel.Application();
            excel.Visible = true;
            object missing = Type.Missing;
            Workbook workbook = excel.Workbooks.Add(missing);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            int startcol = 1;
            int startrow = 1;

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                Range myRange = (Range)sheet1.Cells[startrow, startcol + i];
                myRange.Value2 = dataGridView1.Columns[i].HeaderText;
            }
            startrow++;
            for (int j = 0; j < dataGridView1.Rows.Count; j++)
            {
                for (int t = 0; t < dataGridView1.Columns.Count; t++)
                {
                    try
                    {
                        Range myRange = (Range)sheet1.Cells[startrow + j, startcol + t];
                        myRange.Value2 = dataGridView1[t, j].Value == null ? "" : dataGridView1[t, j].Value;
                    }
                    catch { MessageBox.Show("Lütfen aktarım bitmeden dosyayı  kapatmayınız...İşleme devam edebilmek için yeniden aktarınız.","UYARI",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        
                    }

                }
            }
        }

        private void temizle_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox5.Clear();
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            onay_alan.Text = "";
            textbox_bitis_dk.Clear();
            textbox_bitis_saat.Clear();
            textBox_aciklama.Clear();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            bool bir_sayimi = false;
            bool iki_sayimi = false;
            string malzeme_cinsi = comboBox2.Text;
       
            for (int i = 0; i < 9; i++)
            {
                try
                {
                    if (malzeme_cinsi[0] != 0)
                    {
                        if (int.Parse(malzeme_cinsi[0].ToString()) == i)
                            bir_sayimi = true;
                        if (int.Parse(malzeme_cinsi[1].ToString()) == i)
                            iki_sayimi = true;
                    }    
                }
                catch (Exception ex) {  }
            }
           
             if (bir_sayimi && iki_sayimi)
                textBox2.Text = comboBox2.Text[0].ToString() + comboBox2.Text[1].ToString();
             else if (bir_sayimi)
                 textBox2.Text = comboBox2.Text[0].ToString();
           


        }

       
    }
}
