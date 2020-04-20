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
    public partial class nvNCC : Form
    {
        private string StringConnect;
        private SqlConnection Connect = null;
        public nvNCC(string con) : this()
        {
            StringConnect = con;
        }
        public nvNCC()
        {
            InitializeComponent();
        }
        DataTable DTNCC;

        private void Form_C_NCC_Load(object sender, EventArgs e)
        {
            TBtenNCC.Enabled = false;
            TBdiachiNCC.Enabled = false;
            TBsdtNCC.Enabled = false;
            TBwebNCC.Enabled = false;
            LuuNCC.Enabled = false;
            LoadDGVNCC(); //Hiển thị danh sách nhà cung cấp
        }

        private void LoadDGVNCC()
        {
            ////select trên sql//
            //string query = "Select TEN_NCC as [Tên nhà cung cấp], DIACHI_NCC as [Địa chỉ], SDT_NCC as [Số điện thoại], WEB_NCC as [Website] from NCC";
            //DTNCC = CNPM.DataConnection.GetDataToTable(query);
            //BangNCC.DataSource = DTNCC;
        }

        private void BangNCC_Click(object sender, EventArgs e)
        {
            if (ThemNCC.Enabled == false)
            {
                MessageBox.Show("Bạn đang ở trạng thái thêm mới.", "Thống báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TBtenNCC.Focus();
                return;
            }

            if (DTNCC.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            TBtenNCC.Text = BangNCC.CurrentRow.Cells["Tên nhà cung cấp"].Value.ToString();
            TBdiachiNCC.Text = BangNCC.CurrentRow.Cells["Địa chỉ"].Value.ToString();
            TBsdtNCC.Text = BangNCC.CurrentRow.Cells["Số điện thoại"].Value.ToString();
            TBwebNCC.Text = BangNCC.CurrentRow.Cells["Website"].Value.ToString();

            TBdiachiNCC.Enabled = true;
            TBsdtNCC.Enabled = true;
            TBwebNCC.Enabled = true;
        }

        private void ResetValuesNCC()
        {
            TBtenNCC.Text = "";
            TBdiachiNCC.Text = "";
            TBsdtNCC.Text = "";
            TBwebNCC.Text = "";
        }

        private void ThemNCC_Click(object sender, EventArgs e)
        {
            LuuNCC.Enabled = true;
            ThemNCC.Enabled = false;
            ResetValuesNCC();
            TBtenNCC.Enabled = true;
            TBdiachiNCC.Enabled = true;
            TBsdtNCC.Enabled = true;
            TBwebNCC.Enabled = true;
            TBtenNCC.Focus();
        }

        private void LuuNCC_Click(object sender, EventArgs e)
        {
            //string sql;
            //if (TBtenNCC.Text.Trim().Length == 0)
            //{
            //    MessageBox.Show("Bạn chưa nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    TBtenNCC.Focus();
            //    return;
            //}
            ////select trên sql//
            //sql = "Select TEN_NCC from NCC where TEN_NCC = N'" + TBtenNCC.Text.Trim() + "'";

            //if (CNPM.DataConnection.CheckKey(sql))
            //{
            //    MessageBox.Show("Nhà cung cấp này đã có sẵn", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    TBtenNCC.Focus();
            //    return;
            //}
            ////insert vào csdl//
            //sql = "Insert into NCC(TEN_NCC, DIACHI_NCC, SDT_NCC, WebSupp) values (N'" + TBtenNCC.Text + "', N'" + TBdiachiNCC.Text + "', '" + TBsdtNCC.Text + "', '" + TBwebNCC.Text + "')";
            //CNPM.DataConnection.RunSql(sql);   //Thực hiện câu lệnh sql.
            //LoadDGVNCC(); //Cập nhật lại DataGridView.
            //ResetValuesNCC();
            //ThemNCC.Enabled = true;
            //TBtenNCC.Enabled = false;
            //TBdiachiNCC.Enabled = false;
            //TBsdtNCC.Enabled = false;
            //TBwebNCC.Enabled = false;
            //LuuNCC.Enabled = false;
        }
        private void SuaNCC_Click(object sender, EventArgs e)
        {
            //string sql;
            //if (DTNCC.Rows.Count == 0)
            //{
            //    MessageBox.Show("Không có dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            //if (TBtenNCC.Text == "")
            //{
            //    MessageBox.Show("Bạn phải chọn bản ghi cần sửa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            //if (TBtenNCC.Text.Trim().Length == 0)
            //{
            //    MessageBox.Show("Bạn phải nhập tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    TBtenNCC.Focus();
            //    return;
            //}
            ////chưa có update trong csdl//
            //sql = "Update NCC set DIACHI_NCC = N'" + TBdiachiNCC.Text.Trim().ToString() + "', SDT_NCC =  '" + TBsdtNCC.Text.Trim().ToString() + "', WebSupp = '"
            //    + TBwebNCC.Text.Trim().ToString() + "' where TEN_NCC = N'" + TBtenNCC.Text.Trim().ToString() + "'";
            //CNPM.DataConnection.RunSql(sql);
            //LoadDGVNCC();
            //ResetValuesNCC();
            //TBdiachiNCC.Enabled = false;
            //TBsdtNCC.Enabled = false;
            //TBwebNCC.Enabled = false;
        }

        private void ResetNCC_Click(object sender, EventArgs e)
        {
            ResetValuesNCC();
            TBtenNCC.Enabled = false;
            TBdiachiNCC.Enabled = false;
            TBwebNCC.Enabled = false;
            TBsdtNCC.Enabled = false;
            ResetNCC.Enabled = false;
            LuuNCC.Enabled = false;
            SuaNCC.Enabled = false;
            ThemNCC.Enabled = true;
        }
        private void DongNCC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nvNCC_Load(object sender, EventArgs e)
        {
            Connect = new SqlConnection(StringConnect); //Khởi tạo kết nối với đường dẫn StringConnect
            Connect.Open();
        }
    }
}
