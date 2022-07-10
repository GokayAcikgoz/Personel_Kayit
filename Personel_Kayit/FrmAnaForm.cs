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

namespace Personel_Kayit
{
    public partial class FrmAnaForm : Form
    {
        public FrmAnaForm()
        {
            InitializeComponent();
        }
        //Db baglanti adresimizi yazdik.
        SqlConnection baglanti = new SqlConnection("Data Source=GOKAY\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True");

        void temizle()
        {
            TxtId.Text = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            TxtMeslek.Text = "";
            MskMaas.Text = "";
            CmbSehir.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Form açıldığı gibi db deki bilgileri gosterir.
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);

        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            //listeleye basildiginda db deki bilgileri gosterir.
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            //Uygulamadan cikis yapması icin buton.
            Application.Exit();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            //Baglantimizi actik ve kapattık bunlar olmassa olmaz.
            baglanti.Open();

            //Sql komutu yazmak icin.
            SqlCommand sqlCommand = new SqlCommand("insert into Tbl_Personel(PerAd, PerSoyad, PerSehir, PerMaas, PerMeslek, PerDurum) values(@p1, @p2, @p3, @p4, @p5, @p6)", baglanti);

            //@p1 degeri txtaddan gelen degeri alacak. @p2 ise soyaddan gelen degeri alacak.
            sqlCommand.Parameters.AddWithValue("@p1", TxtAd.Text);  
            sqlCommand.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            sqlCommand.Parameters.AddWithValue("@p3", CmbSehir.Text);
            sqlCommand.Parameters.AddWithValue("@p4", MskMaas.Text);
            sqlCommand.Parameters.AddWithValue("@p5", TxtMeslek.Text);
            sqlCommand.Parameters.AddWithValue("@p6", label8.Text);
            //Sql komutunun calismasi icin. insert, update, delete islemlerinde kullanılır.
            sqlCommand.ExecuteNonQuery(); 

            baglanti.Close();

            MessageBox.Show("Personel eklendi");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true)
            {
                label8.Text = "True";
            }
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                label8.Text = "False";
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            TxtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbSehir.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            MskMaas.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            label8.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            TxtMeslek.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void label8_TextChanged(object sender, EventArgs e)
        {
            if(label8.Text == "True")
            {
                radioButton1.Checked = true;
            }
            if(label8.Text == "False")
            {
                radioButton2.Checked = true;
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand sqlCommandSil = new SqlCommand("delete from Tbl_Personel where Perid = @k1", baglanti);
            sqlCommandSil.Parameters.AddWithValue("@k1", TxtId.Text);
            sqlCommandSil.ExecuteNonQuery();

            baglanti.Close();
            MessageBox.Show("Personel Silindi");

            
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand sqlCommandGuncelle = new SqlCommand("update Tbl_Personel set PerAd = @a1, PerSoyad = @a2, PerSehir = @a3, PerMaas = @a4, PerDurum = @a5, PerMeslek = @a6 where Perid = @a7",baglanti);
            sqlCommandGuncelle.Parameters.AddWithValue("@a1", TxtAd.Text);
            sqlCommandGuncelle.Parameters.AddWithValue("@a2", TxtSoyad.Text);
            sqlCommandGuncelle.Parameters.AddWithValue("@a3", CmbSehir.Text);
            sqlCommandGuncelle.Parameters.AddWithValue("@a4", MskMaas.Text);
            sqlCommandGuncelle.Parameters.AddWithValue("@a5", label8.Text);
            sqlCommandGuncelle.Parameters.AddWithValue("@a6", TxtMeslek.Text);
            sqlCommandGuncelle.Parameters.AddWithValue("@a7", TxtId.Text);

            sqlCommandGuncelle.ExecuteNonQuery();

            baglanti.Close();
            MessageBox.Show("Personel Güncellendi");
        }

        private void Btnİstatistik_Click(object sender, EventArgs e)
        {
            Frmİstatistik fr = new Frmİstatistik();
            fr.Show();
        }

        private void BtnGrafik_Click(object sender, EventArgs e)
        {
            FrmGrafikler frm = new FrmGrafikler();
            frm.Show();
        }
    }
}
