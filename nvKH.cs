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
        }

        DataTable DTKH;

        private void Form_C_Customer_Load(object sender, EventArgs e)
        {
            TBtenKH.Enabled = false;
            TBdiachiKH.Enabled = false;
            TBcmtKH.Enabled = false;
            TBsdtKH.Enabled = false;
            TBghichuKH.Enabled = false;
            ResetKH.Enabled = false;
            LuuKH.Enabled = false;
            SuaKH.Enabled = false;
            ResetValuesKH();
            LoadDGVKH();
        }

        private void LoadDGVKH()
        {
            string query = "Select MA_KH as [Mã khách hàng], TEN_KH as [Tên khách hàng], DIACHI_KH as [Địa chỉ], SDT_KH as [Số điện thoại], CMTND as [Chứng minh thư], GHICHU as [Ghi chú] from KH";
            DTKH = CNPM.DataConnection.GetDataToTable(query);
            BangKH.DataSource = DTKH;
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

        private void BangKH_Click(object sender, EventArgs e)
        {
            if (ThemKH.Enabled == false)
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

            TBmaKH.Text = BangKH.CurrentRow.Cells["Mã khách hàng"].Value.ToString();
            TBtenKH.Text = BangKH.CurrentRow.Cells["Tên khách hàng"].Value.ToString();
            TBcmtKH.Text = BangKH.CurrentRow.Cells["Chứng minh thư"].Value.ToString();
            TBsdtKH.Text = BangKH.CurrentRow.Cells["Số điện thoại"].Value.ToString();
            TBghichuKH.Text = BangKH.CurrentRow.Cells["Ghi chú"].Value.ToString();
            TBdiachiKH.Text = BangKH.CurrentRow.Cells["Địa chỉ"].Value.ToString();
            SuaKH.Enabled = true;
            ResetKH.Enabled = true;
            TBtenKH.Enabled = true;
            TBdiachiKH.Enabled = true;
            TBcmtKH.Enabled = true;
            TBsdtKH.Enabled = true;
            TBghichuKH.Enabled = true;
        }

        private string GiveNextMA_KHtomer()
        {
            //Select trong sql//
            string query;
            query = "Select Max(MA_KH) from KH";
            DTKH = CNPM.DataConnection.GetDataToTable(query);
            try
            {
                return ((int)DTKH.Rows[0][0] + 1).ToString();
            }
            catch
            {
                return "0";
            }
        }

        private void ThemKH_Click(object sender, EventArgs e)
        {
            TBtenKH.Enabled = true;
            TBdiachiKH.Enabled = true;
            TBcmtKH.Enabled = true;
            TBsdtKH.Enabled = true;
            TBghichuKH.Enabled = true;
            ResetKH.Enabled = true;
            SuaKH.Enabled = false;
            LuuKH.Enabled = true;
            ResetValuesKH();
            TBmaKH.Text = GiveNextMA_KHtomer();
        }

        private void SuaKH_Click(object sender, EventArgs e)
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
                + TBcmtKH.Text.Trim() + "', SDT_KH = '" + TBsdtKH.Text.Trim() + "'";
            CNPM.DataConnection.RunSql(sql);
            LoadDGVKH();
            ResetValuesKH();
            TBtenKH.Enabled = false;
            TBdiachiKH.Enabled = false;
            TBcmtKH.Enabled = false;
            TBsdtKH.Enabled = false;
            TBghichuKH.Enabled = false;
            ResetKH.Enabled = false;
            LuuKH.Enabled = false;
            SuaKH.Enabled = false;
        }

        private void LuuKH_Click(object sender, EventArgs e)
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
            if (CNPM.DataConnection.CheckKey(sql))
            {
                MessageBox.Show("Tên khách hàng này đã có sẵn", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Insert trong sql//
            sql = "Insert into KH(TEN_KH, DIACHI_KH, GHICHU, CMTND, SDT_KH) values(N'" + TBtenKH.Text.Trim() + "', N'" + TBdiachiKH.Text.Trim() + "', N'" + TBghichuKH.Text.Trim() + "', '"
                + TBcmtKH.Text.Trim() + "', '" + TBsdtKH.Text.Trim() + "')";

            CNPM.DataConnection.RunSql(sql);
            LoadDGVKH();
            ResetValuesKH();
            TBtenKH.Enabled = false;
            TBdiachiKH.Enabled = false;
            TBcmtKH.Enabled = false;
            TBsdtKH.Enabled = false;
            TBghichuKH.Enabled = false;
            ResetKH.Enabled = false;
            LuuKH.Enabled = false;
            SuaKH.Enabled = false;
            ThemKH.Enabled = true;
        }

        private void XoaKH_Click(object sender, EventArgs e)
        {
            string sql;

            if (MessageBox.Show("bạn có muốn xóa khách hàng này hay không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //xoa KH trong sql//
                sql = "Delete KH where MA_KH = '" + TBmaKH.Text + "'";
                CNPM.DataConnection.RunSqlDel(sql);
                LoadDGVKH();
                ResetValuesKH();
            }

            TBtenKH.Enabled = false;
            TBdiachiKH.Enabled = false;
            TBcmtKH.Enabled = false;
            TBsdtKH.Enabled = false;
            TBghichuKH.Enabled = false;
            ResetKH.Enabled = false;
            LuuKH.Enabled = false;
            SuaKH.Enabled = false;
        }

        private void ResetKH_Click(object sender, EventArgs e)
        {
            ResetValuesKH();
            TBtenKH.Enabled = false;
            TBdiachiKH.Enabled = false;
            TBcmtKH.Enabled = false;
            TBsdtKH.Enabled = false;
            TBghichuKH.Enabled = false;
            ResetKH.Enabled = false;
            LuuKH.Enabled = false;
            SuaKH.Enabled = false;
            ThemKH.Enabled = true;
        }

        private void DongKH_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
