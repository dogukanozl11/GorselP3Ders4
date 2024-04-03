using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GorselP3Ders4
{
    
    internal class Class1
    {
        SqlConnection bag = new SqlConnection("Data Source=DESKTOP-MJGGV3B;Initial Catalog=anket;Integrated Security=True;Encrypt=False;");
        public void cbyukle(ComboBox cb )
        {
            //combobox a vt den kayıtları yükleyen kodu yazınız
            string sql = "select * from sorular";
            SqlDataAdapter da = new SqlDataAdapter(sql, bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cb.DataSource = dt;
            cb.DisplayMember = "soru";
            cb.ValueMember = "soruno";
        }
        public void lboxayukle(ComboBox cb1 , ListBox listb)
        {

        }
    }
}
