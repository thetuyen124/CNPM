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
    public partial class nvKH : Form
    {
        private string StringConnect;
        SqlConnection Connect = null;
        public nvKH(string con) : this()
        {
            StringConnect = con;
        }
        public nvKH()
        {
            InitializeComponent();
        }

        private void btok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nvthemkhachhang_Load(object sender, EventArgs e)
        {
            Connect = new SqlConnection(StringConnect); //Khởi tạo kết nối với đường dẫn StringConnect
            Connect.Open();
            TBtenKH.Focus();
            Form_C_Customer_Load();
        }

        DataTable DTKH;

        private void Form_C_Customer_Load()
        {
            TBtenKH.Enabled = false;
            TBdiachiKH.Enabled = false;
            TBcmtKH.Enabled = false;
            TBsdtKH.Enabled = false;
            TBghichuKH.Enabled = false;
            b_HuyKH.Enabled = false;
            b_LuuKH.Enabled = false;
            b_SuaKH.Enabled = false;
            ResetValuesKH();
            LoadDGVKH();
        }

        private static DataTable LayDuLieuRaBang(String query, string con)
        {
            SqlDataAdapter DA = new SqlDataAdapter(query, con);
            DataTable DT = new DataTable();
            DA.Fill(DT);
            return DT;
        }

        private void LoadDGVKH()
        {
            string query = "Select MA_KH as [Mã khách hàng], TEN_KH as [Tên khách hàng], DIACHI_KH as [Địa chỉ], SDT_KH as [Số điện thoại], CMTND as [Chứng minh thư], GHICHU as [Ghi chú] from KH";
            DTKH = LayDuLieuRaBang(query, StringConnect);
            dGV_KH.DataSource = DTKH;
        }

        private void ResetValuesKH()
        {
            TBtenKH.Text = "";
            TBdiachiKH.Text = "";
            TBcmtKH.Text = "";
            TBsdtKH.Text = "";
            TBghichuKH.Text = "";
            TBmaKH.Text = "";
        }

        private string GiveNextMA_KHtomer()
        {
            //Select trong sql//
            string query;
            query = "Select Max(MA_KH) from KH";
            DTKH = LayDuLieuRaBang(query, StringConnect);
            try
            {
                return ((int)DTKH.Rows[0][0] + 1).ToString();
            }
            catch
            {
                return "0";
            }
        }

        private static void ChayLenh(string query, SqlConnection con)
        {
            SqlCommand Cmd;
            Cmd = new SqlCommand(query, con);

            try
            {
                Cmd.ExecuteNonQuery(); //Thực hiện câu lệnh SQL.
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Cmd.Dispose();             //Giải phóng bộ nhớ.
            Cmd = null;
        }

        private void ResetKH_Click(object sender, EventArgs e)
        {
            ResetValuesKH();
            TBtenKH.Enabled = false;
            TBdiachiKH.Enabled = false;
            TBcmtKH.Enabled = false;
            TBsdtKH.Enabled = false;
            TBghichuKH.Enabled = false;
            b_HuyKH.Enabled = false;
            b_LuuKH.Enabled = false;
            b_SuaKH.Enabled = false;
            b_ThemKH.Enabled = true;
        }

        private void dGV_KH_Click(object sender, EventArgs e)
        {
                if (b_ThemKH.Enabled == false)
                {
                    MessageBox.Show("Bạn đang ở trạng thái thêm mới.", "Thống báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TBtenKH.Focus();
                    return;
                }

                if (DTKH.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                TBmaKH.Text = dGV_KH.CurrentRow.Cells["Mã khách hàng"].Value.ToString();
                TBtenKH.Text = dGV_KH.CurrentRow.Cells["Tên khách hàng"].Value.ToString();
                TBcmtKH.Text = dGV_KH.CurrentRow.Cells["Chứng minh thư"].Value.ToString();
                TBsdtKH.Text = dGV_KH.CurrentRow.Cells["Số điện thoại"].Value.ToString();
                TBghichuKH.Text = dGV_KH.CurrentRow.Cells["Ghi chú"].Value.ToString();
                TBdiachiKH.Text = dGV_KH.CurrentRow.Cells["Địa chỉ"].Value.ToString();
                b_SuaKH.Enabled = true;
                b_HuyKH.Enabled = true;
                TBtenKH.Enabled = true;
                TBdiachiKH.Enabled = true;
                TBcmtKH.Enabled = true;
                TBsdtKH.Enabled = true;
                TBghichuKH.Enabled = true;
            }

        private static bool KiemTraMa(string query, string con)
        {
            SqlDataAdapter DA = new SqlDataAdapter(query, con);
            DataTable DT = new DataTable();
            DA.Fill(DT);
            if (DT.Rows.Count > 0)
                return true;
            else return false;
        }

        private void b_LuuKH_Click(object sender, EventArgs e)
        {
            string sql;
            if (TBtenKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập tên khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TBtenKH.Focus();
                return;
            }

            if (TBsdtKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại của khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TBsdtKH.Focus();
                return;
            }
            //Select trong sql//
            sql = "Select TEN_KH, SDT_KH from KH where TEN_KH = N'" + TBtenKH.Text.Trim() + "' and SDT_KH = '" + TBsdtKH.Text.Trim() + "'";
            if (KiemTraMa(sql, StringConnect))
            {
                MessageBox.Show("Tên khách hàng này đã có sẵn", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Insert trong sql//
            sql = "Insert into KH(TEN_KH, DIACHI_KH, GHICHU, CMTND, SDT_KH) values(N'" + TBtenKH.Text.Trim() + "', N'" + TBdiachiKH.Text.Trim() + "', N'" + TBghichuKH.Text.Trim() + "', '"
                + TBcmtKH.Text.Trim() + "', '" + TBsdtKH.Text.Trim() + "')";
            ChayLenh(sql, Connect);
            LoadDGVKH();

            ResetValuesKH();
            TBtenKH.Enabled = false;
            TBdiachiKH.Enabled = false;
            TBcmtKH.Enabled = false;
            TBsdtKH.Enabled = false;
            TBghichuKH.Enabled = false;
            b_HuyKH.Enabled = false;
            b_LuuKH.Enabled = false;
            b_SuaKH.Enabled = false;
            b_ThemKH.Enabled = true;
        }

        private void b_SuaKH_Click(object sender, EventArgs e)
        {
            string sql;
            if (TBtenKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập tên khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TBtenKH.Focus();
                return;
            }

            if (TBsdtKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại của khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TBcmtKH.Focus();
                return;
            }
            //update kh chưa có trong csdl//
            sql = "Update KH set TEN_KH = N'" + TBtenKH.Text.Trim() + "', DIACHI_KH = N'" + TBdiachiKH.Text.Trim() + "', GHICHU = N'" + TBghichuKH.Text.Trim() + "', CMTND = '"
                + TBcmtKH.Text.Trim() + "', SDT_KH = '" + TBsdtKH.Text.Trim() + "' WHERE MA_KH = " + TBmaKH.Text + "";
            ChayLenh(sql, Connect);

            LoadDGVKH();
            ResetValuesKH();
            TBtenKH.Enabled = false;
            TBdiachiKH.Enabled = false;
            TBcmtKH.Enabled = false;
            TBsdtKH.Enabled = false;
            TBghichuKH.Enabled = false;
            b_LuuKH.Enabled = false;
            b_SuaKH.Enabled = false;
        }

        private void b_ThemKH_Click(object sender, EventArgs e)
        {
            TBtenKH.Enabled = true;
            TBdiachiKH.Enabled = true;
            TBcmtKH.Enabled = true;
            TBsdtKH.Enabled = true;
            TBghichuKH.Enabled = true;
            b_HuyKH.Enabled = true;
            b_SuaKH.Enabled = false;
            b_LuuKH.Enabled = true;
            ResetValuesKH();
            TBmaKH.Text = GiveNextMA_KHtomer();
        }
    }
}
