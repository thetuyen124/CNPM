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
    public partial class homeql : Form
    {
        private string StringConnect;
        private SqlConnection Connect = null;
        public homeql(string con):this()
        {
            StringConnect = con;
        }
        public homeql()
        {
            InitializeComponent();
        }

        private void homeql_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form a = new dangnhap();
            a.Show();
            this.Hide();
        }

        private void quảnLýNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcql.SelectedIndex=0;
        }

        private void homeql_Load(object sender, EventArgs e)
        {
            Connect = new SqlConnection(StringConnect); //Khởi tạo kết nối với đường dẫn StringConnect
            Connect.Open();
        }

        private void quảnLýNhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcql.SelectedIndex = 2;
        }

        private void quảnLýLoạiHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcql.SelectedIndex = 3;
        }

        private void quảnLýKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcql.SelectedIndex = 4;
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcql.SelectedIndex = 5;
        }

        private void báoCáoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcql.SelectedIndex = 6;
        }

        private void đăngXuấtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form a =new dangnhap();
            a.Show();
            this.Hide();
        }

        private void thêmKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form a = new nvKH();
            a.Show();
        }

        private void xóaKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form a = new nvNCC();
            a.Show();
        }

        private void quảnLýSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcql.SelectedIndex = 1;
        }

        private void tcql_Click(object sender, EventArgs e)
        {
            //**Code khởi chạy tab quản lý sản phẩm**//
            CapNhatLH();
            CapNhatNCC();
            tB_Ma.Enabled = false;
            tB_TenSP.Enabled = false;
            nUD_SP.Enabled = false;
            rTB_GhiChu.Enabled = false;
            cB_LH.Enabled = false;
            cB_NCC.Enabled = false;
            tB_GiaBan.Enabled = false;
            rTB_HinhAnh.Enabled = false;
            b_AnhSP.Enabled = false;
            b_SuaSP.Enabled = false;
            b_BoQuaSP.Enabled = false;
            b_XoaSP.Enabled = false;
            b_LuuSP.Enabled = false;
            pB_SP.Image = null;
            LoadDGVSP();
            ResetValuesSP();
            //**Kết thúc khởi tạo tab quản lý sản phẩm**//
        }

        //======= BĂT ĐẦU PHẦN HOÀNG LÀM :> =======//
        DataTable DTSP;

        //Hàm cập nhật combobox
        private static void UpdateCombobox(string query, ComboBox cbo, string ma, string ten, string con)
        {
            SqlDataAdapter DA = new SqlDataAdapter(query, con);
            DataTable DT = new DataTable();
            DA.Fill(DT);
            cbo.DataSource = DT;
            //Trường giá trị.
            cbo.ValueMember = ma;
            //Trường hiển thị.
            cbo.DisplayMember = ten;
        }

        private void CapNhatNCC()
        {
            string query = "Select MA_NCC, TEN_NCC from NCC";
            UpdateCombobox(query, cB_NCC, "MA_NCC", "TEN_NCC", StringConnect);
            cB_NCC.SelectedIndex = -1;
        }

        private void CapNhatLH()
        {
            string query = "Select MA_LH, TEN_LH from LH";
            UpdateCombobox(query, cB_LH, "MA_LH", "TEN_LH", StringConnect);
            cB_LH.SelectedIndex = -1;
        }

        //Hàm lấy dữ liệu ra DataTable
        private static DataTable LayDuLieuRaBang(String query, string con)
        {
            SqlDataAdapter DA = new SqlDataAdapter(query, con);
            DataTable DT = new DataTable();
            DA.Fill(DT);
            return DT;
        }

        private void LoadDGVSP()
        {
            string query = "Select MA_SP as [MÃ SẢN PHẨM], TEN_SP as [TÊN SẢN PHẨM], TEN_NCC as [TÊN NHÀ CUNG CẤP], TEN_LH as [TÊN LOẠI HÀNG], SIZE, GIABAN as [GIÁ BÁN], GIANHAP as [GIÁ NHẬP]," +
                " ANH as [ĐƯỜNG DẪN HÌNH ẢNH], SOLUONG as [SỐ LƯỢNG], GHICHU as [GHI CHÚ] from LH, NCC, SP where SP.MA_NCC = NCC.MA_NCC and LH.MA_LH = SP.MA_LH";
            DTSP = LayDuLieuRaBang(query, StringConnect);
            dGV_SP.DataSource = DTSP;
        }

        private void dGV_SP_Click(object sender, EventArgs e)
        {
            if (b_ThemSP.Enabled == false)
            {
                MessageBox.Show("Bạn đang ở trạng thái thêm mới.", "Thống báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tB_Ma.Focus();
                return;
            }

            if (DTSP.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            tB_Ma.Text = dGV_SP.CurrentRow.Cells["Mã sản phẩm"].Value.ToString();
            tB_TenSP.Text = dGV_SP.CurrentRow.Cells["Tên sản phẩm"].Value.ToString();
            cB_NCC.Text = dGV_SP.CurrentRow.Cells["Tên nhà cung cấp"].Value.ToString();
            cB_LH.Text = dGV_SP.CurrentRow.Cells["Tên loại hàng"].Value.ToString();
            nUD_SP.Text = dGV_SP.CurrentRow.Cells["Size"].Value.ToString();
            tB_GiaBan.Text = dGV_SP.CurrentRow.Cells["Giá bán"].Value.ToString();
            tB_GiaNhap.Text = dGV_SP.CurrentRow.Cells["Giá nhập"].Value.ToString();
            rTB_GhiChu.Text = dGV_SP.CurrentRow.Cells["Ghi chú"].Value.ToString();
            rTB_HinhAnh.Text = dGV_SP.CurrentRow.Cells["Đường dẫn hình ảnh"].Value.ToString();
            if (rTB_HinhAnh.Text.Length > 0)
                pB_SP.Image = Image.FromFile(rTB_HinhAnh.Text);
            else pB_SP.Image = null;

            b_LuuSP.Enabled = false;
            b_SuaSP.Enabled = true;
            b_XoaSP.Enabled = true;
            b_BoQuaSP.Enabled = true;
            b_AnhSP.Enabled = true;
            tB_TenSP.Enabled = true;
            rTB_GhiChu.Enabled = true;
            tB_GiaBan.Enabled = true;
            nUD_SP.Enabled = true;
            cB_NCC.Enabled = true;
            cB_LH.Enabled = true;
        }

        private void ResetValuesSP()
        {
            tB_Ma.Text = "";
            tB_TenSP.Text = "";
            cB_NCC.Text = "";
            cB_LH.Text = "";
            nUD_SP.Text = "16";
            rTB_HinhAnh.Text = "";
            rTB_GhiChu.Text = "";
            tB_SoLuong.Text = "0";
            tB_GiaBan.Text = "";
            tB_GiaNhap.Text = "";
            pB_SP.Image = null;
        }

        private void b_ThemSP_Click(object sender, EventArgs e)
        {
            b_TimKiemSP.Enabled = false;
            tB_TimKiemSP.Enabled = false;
            b_XoaSP.Enabled = false;
            b_SuaSP.Enabled = false;
            b_AnhSP.Enabled = true;
            b_BoQuaSP.Enabled = true;
            b_LuuSP.Enabled = true;
            b_ThemSP.Enabled = false;
            tB_TenSP.Enabled = true;
            rTB_GhiChu.Enabled = true;
            tB_Ma.Enabled = true;
            tB_GiaBan.Enabled = true;
            nUD_SP.Enabled = true;
            cB_NCC.Enabled = true;
            cB_LH.Enabled = true;
            ResetValuesSP();
        }

        //Hàm kiểm tra lỗi
        private static bool KiemTraMa(string query, string con)
        {
            SqlDataAdapter DA = new SqlDataAdapter(query, con);
            DataTable DT = new DataTable();
            DA.Fill(DT);
            if (DT.Rows.Count > 0)
                return true;
            else return false;
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

        private void b_LuuSP_Click(object sender, EventArgs e)
        {
            string query;
            if (tB_TenSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập tên sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tB_TenSP.Focus();
                return;
            }

            if (tB_Ma.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập mã sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tB_Ma.Focus();
                return;
            }

            if (cB_NCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn nhà cung cấp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cB_NCC.Focus();
                return;
            }

            if (cB_LH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn loại hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cB_LH.Focus();
                return;
            }

            if (tB_GiaBan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập giá bán cho sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tB_GiaBan.Focus();
                return;
            }

            if (nUD_SP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập kích cỡ của sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nUD_SP.Focus();
                return;
            }

            query = "Select MA_SP from SP where MA_SP = '" + tB_Ma.Text.Trim() + "_S" + nUD_SP.Text + "'";
            if (KiemTraMa(query, StringConnect))
            {
                MessageBox.Show("Sản phẩm này đã có sẵn.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tB_Ma.Focus();
                return;
            }

            query = "Insert into SP(MA_SP, TEN_SP, MA_NCC, MA_LH, SIZE, GIABAN, ANH, GHICHU) values('" + tB_Ma.Text.Trim() + "_S" + nUD_SP.Text + "', N'" + tB_TenSP.Text.Trim() + "_S" + nUD_SP.Text + "', "
                + cB_NCC.SelectedValue.ToString() + ", " + cB_LH.SelectedValue.ToString() + ", " + nUD_SP.Text.Trim() + ", " + tB_GiaBan.Text.Trim() + ", N'" + rTB_HinhAnh.Text + "', N'" + rTB_GhiChu.Text.Trim() + "')";

            ChayLenh(query, Connect);
            LoadDGVSP();
            ResetValuesSP();

            tB_TenSP.Enabled = false;
            rTB_GhiChu.Enabled = false;
            tB_Ma.Enabled = false;
            tB_GiaBan.Enabled = false;
            nUD_SP.Enabled = false;
            cB_NCC.Enabled = false;
            cB_LH.Enabled = false;
            b_AnhSP.Enabled = false;
            b_XoaSP.Enabled = true;
            b_SuaSP.Enabled = true;
            b_LuuSP.Enabled = false;
            b_BoQuaSP.Enabled = false;
            b_ThemSP.Enabled = true;
            b_TimKiemSP.Enabled = true;
            tB_TimKiemSP.Enabled = true;
        }

        private void b_AnhSP_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Bitmap (*.bmp)|*.bmp|JPEG(*.jpg)|*jpg|GIF(*gif)|*gif|All files(*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.Title = "Chọn ảnh minh họa cho sản phẩm";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pB_SP.Image = Image.FromFile(openFileDialog.FileName);
                rTB_HinhAnh.Text = openFileDialog.FileName;
            }
        }

        private void b_SuaSP_Click(object sender, EventArgs e)
        {
            string query;

            if (tB_Ma.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tB_Ma.Focus();
                return;
            }

            if (tB_TenSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tB_TenSP.Focus();
                return;
            }

            if (tB_GiaBan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập đơn giá cho sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tB_GiaBan.Focus();
                return;
            }

            if (nUD_SP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập kích thước của sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nUD_SP.Focus();
                return;
            }

            if (cB_NCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn nhà cung cấp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cB_NCC.Focus();
                return;
            }

            if (cB_LH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn loại hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cB_LH.Focus();
                return;
            }

            query = "Update SP set TEN_SP = N'" + tB_TenSP.Text.Trim().ToString() + "', MA_NCC = " + cB_NCC.SelectedValue.ToString() + ", MA_LH = " + cB_LH.SelectedValue.ToString() + ", SIZE = "
                + nUD_SP.Text.Trim() + ", GIABAN = " + tB_GiaBan.Text.Trim() + ", ANH = N'" + rTB_HinhAnh.Text.Trim() + "', GHICHU = N'" + rTB_GhiChu.Text + "' where MA_SP = '" + tB_Ma.Text.Trim() + "'";
            ChayLenh(query, Connect);
            LoadDGVSP();
            ResetValuesSP();
            tB_TenSP.Enabled = false;
            rTB_GhiChu.Enabled = false;
            tB_Ma.Enabled = false;
            rTB_GhiChu.Enabled = false;
            tB_GiaBan.Enabled = false;
            nUD_SP.Enabled = false;
            cB_NCC.Enabled = false;
            cB_LH.Enabled = false;
            b_AnhSP.Enabled = false;
            b_XoaSP.Enabled = false;
            b_SuaSP.Enabled = false;
            b_LuuSP.Enabled = false;
            b_BoQuaSP.Enabled = false;
            b_ThemSP.Enabled = true;
        }

        //Hàm chạy lệnh xóa
        private static void ChayLenhXoa(string query, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Dữ liệu đang được sử dụng, không thể xóa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();
            cmd = null;
        }

        private void b_XoaSP_Click(object sender, EventArgs e)
        {
            string sql;
            if (DTSP.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("bạn có muốn xóa hay không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "Delete SP where MA_SP = '" + tB_Ma.Text + "'";
                ChayLenhXoa(sql, Connect);
                LoadDGVSP();
                ResetValuesSP();
            }

            b_XoaSP.Enabled = false;
            b_SuaSP.Enabled = false;
            b_BoQuaSP.Enabled = false;
            tB_TenSP.Enabled = false;
            rTB_GhiChu.Enabled = false;
            tB_Ma.Enabled = false;
            rTB_HinhAnh.Enabled = false;
            tB_GiaBan.Enabled = false;
            nUD_SP.Enabled = false;
            cB_NCC.Enabled = false;
            cB_LH.Enabled = false;
            b_AnhSP.Enabled = false;
            b_ThemSP.Enabled = true;
        }

        private void b_BoQuaSP_Click(object sender, EventArgs e)
        {
            ResetValuesSP();
            b_XoaSP.Enabled = false;
            b_ThemSP.Enabled = true;
            b_SuaSP.Enabled = false;
            b_BoQuaSP.Enabled = false;
            b_LuuSP.Enabled = false;
            b_AnhSP.Enabled = false;
            tB_TenSP.Enabled = false;
            rTB_GhiChu.Enabled = false;
            tB_Ma.Enabled = false;
            rTB_HinhAnh.Enabled = false;
            tB_GiaBan.Enabled = false;
            nUD_SP.Enabled = false;
            cB_NCC.Enabled = false;
            cB_LH.Enabled = false;
            tB_TimKiemSP.Enabled = true;
            b_TimKiemSP.Enabled = true;
            LoadDGVSP();
        }

        private void tB_TimKiemSP_Click(object sender, EventArgs e)
        {
            tB_TimKiemSP.Text = "";
        }

        private void b_TimKiemSP_Click(object sender, EventArgs e)
        {
            string query = "Select MA_SP as [MÃ SẢN PHẨM], TEN_SP as [TÊN SẢN PHẨM], TEN_NCC as [TÊN NHÀ CUNG CẤP], TEN_LH as [TÊN LOẠI HÀNG], SIZE, GIABAN as [GIÁ BÁN], GIANHAP as [GIÁ NHẬP]," +
                " ANH as [ĐƯỜNG DẪN HÌNH ẢNH], SOLUONG as [SỐ LƯỢNG], GHICHU as [GHI CHÚ] from LH, NCC, SP " +
                "where SP.MA_NCC = NCC.MA_NCC and LH.MA_LH = SP.MA_LH and (MA_SP LIKE '" + tB_TimKiemSP.Text.Trim() + "%' or TEN_SP LIKE N'" + tB_TimKiemSP.Text.Trim() + "%')";
            DTSP = LayDuLieuRaBang(query, StringConnect);
            dGV_SP.DataSource = DTSP;
            b_BoQuaSP.Enabled = true;
        }

        //======= KẾT THÚC PHẦN HOÀNG LÀM :> =======//

        //======= PHẦN Tùng Làm //

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
            XoaKH.Enabled = false;
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
            XoaKH.Enabled = true;
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
            XoaKH.Enabled = false;
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
            XoaKH.Enabled = false;
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
            XoaKH.Enabled = false;
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
            XoaKH.Enabled = false;
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
            XoaKH.Enabled = false;
            ThemKH.Enabled = true;
        }

        //xong phần TÙng Làm//

        //Phần Thái Làm//
        DataTable DTNCC;

        private void Form_C_NCC_Load(object sender, EventArgs e)
        {
            TBtenNCC.Enabled = false;
            TBdiachiNCC.Enabled = false;
            TBsdtNCC.Enabled = false;
            TBwebNCC.Enabled = false;
            LuuNCC.Enabled = false;
            XoaNCC.Enabled = false;
            XoaNCC.Enabled = false;
            LoadDGVNCC(); //Hiển thị danh sách nhà cung cấp
        }

        private void LoadDGVNCC()
        {
            string query = "Select TEN_NCC as [Tên nhà cung cấp], DIACHI_NCC as [Địa chỉ], SDT_NCC as [Số điện thoại], WebSupp as [Website] from NCC";
            DTNCC = CNPM.DataConnection.GetDataToTable(query);
            BangNCC.DataSource = DTNCC;
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

            XoaNCC.Enabled = true;
            XoaNCC.Enabled = true;
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
            XoaNCC.Enabled = false;
            XoaNCC.Enabled = false;
        }

        private void ThemNCC_Click(object sender, EventArgs e)
        {
            XoaNCC.Enabled = false;
            XoaNCC.Enabled = false;
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
            string sql;
            if (TBtenNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TBtenNCC.Focus();
                return;
            }

            sql = "Select TEN_NCC from NCC where TEN_NCC = N'" + TBtenNCC.Text.Trim() + "'";

            if (CNPM.DataConnection.CheckKey(sql))
            {
                MessageBox.Show("Nhà cung cấp này đã có sẵn", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TBtenNCC.Focus();
                return;
            }
            // Insert trong sql //
            sql = "Insert into NCC(TEN_NCC, DIACHI_NCC, SDT_NCC, WebSupp) values (N'" + TBtenNCC.Text + "', N'" + TBdiachiNCC.Text + "', '" + TBsdtNCC.Text + "', '" + TBwebNCC.Text + "')";
            CNPM.DataConnection.RunSql(sql);   //Thực hiện câu lệnh sql.
            LoadDGVNCC(); //Cập nhật lại DataGridView.
            ResetValuesNCC();
            ThemNCC.Enabled = true;
            TBtenNCC.Enabled = false;
            TBdiachiNCC.Enabled = false;
            TBsdtNCC.Enabled = false;
            TBwebNCC.Enabled = false;
            LuuNCC.Enabled = false;
            XoaNCC.Enabled = false;
            XoaNCC.Enabled = false;
        }

        private void XoaNCC_Click(object sender, EventArgs e)
        {
            string sql;
            if (DTNCC.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Nếu chưa chọn bản ghi nào.
            if (TBtenNCC.Text == "")
            {
                MessageBox.Show("Không có bản ghi trong bộ nhớ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("bạn có muốn xóa hay không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Delete trong sql//
                sql = "Delete NCC where TEN_NCC = N'" + TBtenNCC.Text + "'";
                CNPM.DataConnection.RunSqlDel(sql);
                LoadDGVNCC();
                ResetValuesNCC();
            }
        }

        private void SuaNCC_Click(object sender, EventArgs e)
        {
            string sql;
            if (DTNCC.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (TBtenNCC.Text == "")
            {
                MessageBox.Show("Bạn phải chọn bản ghi cần sửa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (TBtenNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TBtenNCC.Focus();
                return;
            }
            //update trong sql//
            sql = "Update NCC set DIACHI_NCC = N'" + TBdiachiNCC.Text.Trim().ToString() + "', SDT_NCC =  '" + TBsdtNCC.Text.Trim().ToString() + "', WebSupp = '"
                + TBwebNCC.Text.Trim().ToString() + "' where TEN_NCC = N'" + TBtenNCC.Text.Trim().ToString() + "'";
            CNPM.DataConnection.RunSql(sql);
            LoadDGVNCC();
            ResetValuesNCC();
            TBdiachiNCC.Enabled = false;
            TBsdtNCC.Enabled = false;
            TBwebNCC.Enabled = false;
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

        private void SuaKH_Click_1(object sender, EventArgs e)
        {

        }

        private void ThemKH_Click_1(object sender, EventArgs e)
        {

        }

        private void XoaKH_Click_1(object sender, EventArgs e)
        {

        }

        private void LuuKH_Click_1(object sender, EventArgs e)
        {

        }

        private void ResetKH_Click_1(object sender, EventArgs e)
        {

        }

        private void ThemNCC_Click_1(object sender, EventArgs e)
        {

        }
        //hết THái Làm//
    }
}
