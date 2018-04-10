namespace Metaloks_İş_Takip_Platformu
{
    partial class form_giris
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_giris));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button_giris = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label_bilgi_icerik = new System.Windows.Forms.Label();
            this.groupBox_kullanici_islemleri = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label_bilgi = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox_kullanici_islemleri.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox1.ForeColor = System.Drawing.Color.DimGray;
            this.textBox1.Location = new System.Drawing.Point(67, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(117, 24);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox2.Location = new System.Drawing.Point(67, 68);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(117, 25);
            this.textBox2.TabIndex = 3;
            // 
            // button_giris
            // 
            this.button_giris.BackColor = System.Drawing.Color.Transparent;
            this.button_giris.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_giris.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_giris.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button_giris.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.button_giris.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_giris.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button_giris.ForeColor = System.Drawing.Color.Transparent;
            this.button_giris.Location = new System.Drawing.Point(67, 99);
            this.button_giris.Name = "button_giris";
            this.button_giris.Size = new System.Drawing.Size(117, 25);
            this.button_giris.TabIndex = 6;
            this.button_giris.Text = "LOGIN...";
            this.button_giris.UseVisualStyleBackColor = false;
            this.button_giris.Click += new System.EventHandler(this.button_giris_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(29, 39);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(29, 69);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(22, 24);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // label_bilgi_icerik
            // 
            this.label_bilgi_icerik.AutoSize = true;
            this.label_bilgi_icerik.BackColor = System.Drawing.Color.Transparent;
            this.label_bilgi_icerik.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_bilgi_icerik.ForeColor = System.Drawing.Color.White;
            this.label_bilgi_icerik.Location = new System.Drawing.Point(64, 139);
            this.label_bilgi_icerik.Name = "label_bilgi_icerik";
            this.label_bilgi_icerik.Size = new System.Drawing.Size(41, 15);
            this.label_bilgi_icerik.TabIndex = 8;
            this.label_bilgi_icerik.Text = "label1";
            this.label_bilgi_icerik.Visible = false;
            // 
            // groupBox_kullanici_islemleri
            // 
            this.groupBox_kullanici_islemleri.BackColor = System.Drawing.Color.Transparent;
            this.groupBox_kullanici_islemleri.Controls.Add(this.linkLabel1);
            this.groupBox_kullanici_islemleri.Controls.Add(this.label_bilgi);
            this.groupBox_kullanici_islemleri.Controls.Add(this.textBox2);
            this.groupBox_kullanici_islemleri.Controls.Add(this.label_bilgi_icerik);
            this.groupBox_kullanici_islemleri.Controls.Add(this.textBox1);
            this.groupBox_kullanici_islemleri.Controls.Add(this.button_giris);
            this.groupBox_kullanici_islemleri.Controls.Add(this.pictureBox1);
            this.groupBox_kullanici_islemleri.Controls.Add(this.pictureBox2);
            this.groupBox_kullanici_islemleri.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox_kullanici_islemleri.ForeColor = System.Drawing.Color.White;
            this.groupBox_kullanici_islemleri.Location = new System.Drawing.Point(580, 248);
            this.groupBox_kullanici_islemleri.Name = "groupBox_kullanici_islemleri";
            this.groupBox_kullanici_islemleri.Size = new System.Drawing.Size(212, 171);
            this.groupBox_kullanici_islemleri.TabIndex = 9;
            this.groupBox_kullanici_islemleri.TabStop = false;
            this.groupBox_kullanici_islemleri.Text = "Kullanıcı Girişi";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.DisabledLinkColor = System.Drawing.Color.Transparent;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.linkLabel1.LinkColor = System.Drawing.Color.GhostWhite;
            this.linkLabel1.Location = new System.Drawing.Point(110, 16);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(96, 15);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Şifremi Unuttum";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label_bilgi
            // 
            this.label_bilgi.AutoSize = true;
            this.label_bilgi.BackColor = System.Drawing.Color.Transparent;
            this.label_bilgi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_bilgi.Location = new System.Drawing.Point(26, 139);
            this.label_bilgi.Name = "label_bilgi";
            this.label_bilgi.Size = new System.Drawing.Size(34, 15);
            this.label_bilgi.TabIndex = 9;
            this.label_bilgi.Text = "Bilgi:";
            // 
            // form_giris
            // 
            this.AcceptButton = this.button_giris;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = global::Metaloks_İş_Takip_Platformu.Properties.Resources.arkaplan;
            this.ClientSize = new System.Drawing.Size(1303, 690);
            this.Controls.Add(this.groupBox_kullanici_islemleri);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "form_giris";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "METALOKS İŞ TAKİP PLATFORMU";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.form_giris_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox_kullanici_islemleri.ResumeLayout(false);
            this.groupBox_kullanici_islemleri.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button_giris;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label_bilgi_icerik;
        private System.Windows.Forms.GroupBox groupBox_kullanici_islemleri;
        private System.Windows.Forms.Label label_bilgi;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

