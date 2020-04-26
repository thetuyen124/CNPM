using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM
{
    public partial class hoadon : Form
    {
        private readonly Dictionary<string, int> dic;
        readonly string StringConnect = "";
        private SqlConnection Connect = null;
        public hoadon(string tongtien,string con,ref Dictionary<string,int>  d):this()
        {
            lbthanhtien.Text = tongtien;
            StringConnect = con+ "; MultipleActiveResultSets = True";
            dic = d;
        }
        public hoadon()
        {
            InitializeComponent();
        }

        private void Btin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Hoadon_Load(object sender, EventArgs e)
        {
            Connect = new SqlConnection(StringConnect); //Khởi tạo kết nối với đường dẫn StringConnect
            Connect.Open();
            LoadKH();
            Loadhd();
        }
        private void LoadKH()
        {
            SqlDataAdapter run;//lấy dữ liệu lấy từ CSDL
            DataSet bang = new DataSet();//luu du lieu lay tu csdl
            string query = "select TEN_KH from KH";//query sql
            run = new SqlDataAdapter(query, Connect);
            run.Fill(bang);
            cbkh.DataSource = bang.Tables[0];
            cbkh.DisplayMember = "TEN_KH";
        }
        private void Loadhd()
        {
            lbgia.Text = "\n \n";
            lbsp.Text = "\n \n";
            foreach (KeyValuePair<string, int> r in dic)
            {
                lbsp.Text += r.Key + "\n";
                lbgia.Text += Convert.ToString(r.Value) + " đ\n";
            }
        }

        private void Hoadon_FormClosing(object sender, FormClosingEventArgs e)
        {
            string query = "insert into DONBAN values(" + Laymakh(cbkh.Text) + "," + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "," + lbthanhtien.Text + ")";
            SqlCommand run = new SqlCommand(query, Connect);
            IAsyncResult result= run.BeginExecuteNonQuery();
            run.EndExecuteNonQuery(result);
            foreach (KeyValuePair<string, int> r in dic)
            {
                query = "insert into THONGTINDONBAN values(" + Laymadh() + ",'"+ Laymasp(r.Key) +"',"+ Laysoluong(r.Value,r.Key) +",0,"+r.Value+")";
                run = new SqlCommand(query, Connect);
                result = run.BeginExecuteNonQuery();
                run.EndExecuteNonQuery(result);
            }
            dic.Clear();
        }
        private string Laymakh(string a)
        {
            string id ;
            string query = "select MA_KH from KH where TEN_KH=N'" + a+"'";
            SqlDataReader run;
            SqlCommand cmd = new SqlCommand(query, Connect);
            IAsyncResult result = cmd.BeginExecuteReader();
            run =cmd.EndExecuteReader(result);
            run.Read();
            id = run.GetInt32(0).ToString();
            return id;
        }
        private string Laymadh()
        {
            string ma  ;
            string query = "select top 1 MA_DONBAN from DONBAN order by(MA_DONBAN) desc";
            SqlDataReader run;
            SqlCommand cmd = new SqlCommand(query, Connect);
            IAsyncResult result = cmd.BeginExecuteReader();
            run = cmd.EndExecuteReader(result);
            run.Read();
            ma = run.GetInt32(0).ToString();
            return ma;
        }
        private string Laymasp(string a)
        {
            string ma;
            string query = "select top 1 MA_SP from SP where TEN_SP=N'"+a+"'";
            using (SqlCommand cmd = new SqlCommand(query, Connect))
            {
                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                ma = dr["MA_SP"].ToString();
                dr.Close();
            }
            return ma;
        }
        private int Laysoluong(int tt,string ten)
        {
            int sl = tt;
            string query = "SELECT GIABAN FROM SP where TEN_SP=N'" + ten + "'";
            string giaban;
            using (SqlCommand cmd = new SqlCommand(query, Connect))
            {
                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                giaban = dr["GIABAN"].ToString();
                dr.Close();
            }
            sl /= Convert.ToInt32(giaban);
            return sl;
        }
    }
}
