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

namespace GorselP3Ders4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection bag = new SqlConnection("Data Source=DESKTOP-MJGGV3B;Initial Catalog=anket;Integrated Security=True;Encrypt=False;");
        private void Form1_Load(object sender, EventArgs e)
        {
            //combobox a vt den kayıtları yükleyen kodu yazınız
            string sql = "select * from sorular";
            SqlDataAdapter da = new SqlDataAdapter(sql, bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "soru";
            comboBox1.ValueMember = "soruno";
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Listbox a comboboxtan  seçilen değere vtdan kayıtları yükleye  kodu yazınız
            string sql = "select * from cevaplar where soruno = @prm1 ";
            SqlDataAdapter da = new SqlDataAdapter( sql,bag);
            da.SelectCommand.Parameters.AddWithValue("@prm1", comboBox1.SelectedValue);
            DataTable dt = new DataTable();
            da.Fill(dt);
            listBox1.DataSource= dt;
            listBox1.DisplayMember = "cevap";
            listBox1.ValueMember = "cevapno";
        }

        private void btnOyVer_Click(object sender, EventArgs e)
        {
            try
            {
                //vt daki değeri güncelleyen kodu yazınız.
                string sql = "update cevaplar set oy=oy+1 where cevapno=@prm1";
                SqlCommand komut = new SqlCommand(sql, bag);
                komut.Parameters.AddWithValue("@prm1", listBox1.SelectedValue);
                bag.Open();
                komut.ExecuteNonQuery();
                bag.Close();

                MessageBox.Show("Oyunuz Alınmıştır.");
            }
            catch
            {
                MessageBox.Show("Opsss!!!!");
            }


        }
    }
}
