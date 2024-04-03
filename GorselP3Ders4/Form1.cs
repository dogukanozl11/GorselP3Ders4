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
using System.Windows.Forms.DataVisualization.Charting;

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

                grafikciz();
            }
            catch
            {
                MessageBox.Show("Opsss!!!!");
            }


        }
        public void grafikciz()
        {
            //combobox da seçili elemanın tablodaki kayıtlarını toplayan ve bir değişkene atayan kodu yazınız.

            string sql = "select sum(oy) from cevaplar where soruno=@prm1";
            SqlDataAdapter da = new SqlDataAdapter(sql, bag);
            da.SelectCommand.Parameters.AddWithValue("@prm1", comboBox1.SelectedValue);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int toplamoy = Convert.ToInt32(dt.Rows[0][0]);

            //vt daki kayıtları chartta gösteren (grafiğini çizen) kodu yazınız.

            string sql1 = "select cevap , oy , (oy*100/@prm3) as yuzde from cevaplar where soruno =@prm2";
            SqlDataAdapter da1 = new SqlDataAdapter(sql1,bag);
            da1.SelectCommand.Parameters.AddWithValue("@prm2", comboBox1.SelectedValue);
            da1.SelectCommand.Parameters.AddWithValue("@prm3", toplamoy);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            chart1.DataSource = dt1;
            chart1.Series[0].XValueMember = "cevap";
            chart1.Series[0].YValueMembers = "yuzde";
            chart1.DataBind();
            chart1.Series[0].ChartType = SeriesChartType.Pyramid;
        }
    }
}
