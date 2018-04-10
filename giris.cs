using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Metaloks_İş_Takip_Platformu
{
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
        }
        int sayac = 0;
        private void giris_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
            
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            if (sayac == 3)
            {
                form_giris giris = new form_giris();
                giris.Show();
                this.Hide();
                timer1.Stop();

            }
        }
    }
}
