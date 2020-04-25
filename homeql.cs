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
using COMExcel = Microsoft.Office.Interop.Excel; 

namespace CNPM
{
    public partial class homeql : Form
    {
        private string StringConnect = "Data Source=DESKTOP-R9IA4BP\\SQLEXPRESS;Initial Catalog=QUANLYCUAHANGGIAY;Integrated Security=True";
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
            getData();
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
            Form a = new nvKH(StringConnect);
            a.Show();
        }

        private void xóaKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form a = new nvNCC(StringConnect);
            a.Show();
        }

        private void quảnLýSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcql.SelectedIndex = 1;
        }

        private void tcql_Click(object sender, EventArgs e)
        {
            Load_NV();
            Load_SP();
            Load_KH();
            LoadNCC();
            ResetValuesBC();
        }

        private void Load_NV()
        {
            tB_MANV.Enabled = false;
            tB_MANV.Enabled = false;
            tB_TENNV.Enabled = false;
            tb_DIACHINV.Enabled = false;
            tB_CMNDNV.Enabled = false;
            tB_SDTNV.Enabled = false;
            tB_MATKHAU.Enabled = false;
            rTB_CHUCVUNV.Enabled = false;
            tB_MATKHAU.Enabled = false;
            bt_MoAnhNV.Enabled = false;
            bt_LUUNV.Enabled = false;
            rB_NAM.Enabled = false;
            rB_NU.Enabled = false;
            bt_SUANV.Enabled = false;
            bt_XOANV.Enabled = false;
            bt_HUYNV.Enabled = false;
            bt_THEMNV.Enabled = true;
            ResetValuesNV();
            LoadDGVNV();
        }

        private void Load_SP()
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

        private void Load_KH()
        {
            TBtenKH.Enabled = false;
            TBdiachiKH.Enabled = false;
            TBcmtKH.Enabled = false;
            TBsdtKH.Enabled = false;
            TBghichuKH.Enabled = false;
            b_HuyKH.Enabled = false;
            b_LuuKH.Enabled = false;
            b_SuaKH.Enabled = false;
            b_XoaKH.Enabled = false;
            ResetValuesKH();
            LoadDGVKH();
        }

        private void LoadNCC()
        {
            TBtenNCC.Enabled = false;
            TBdiachiNCC.Enabled = false;
            TBsdtNCC.Enabled = false;
            TBwebNCC.Enabled = false;
            b_LuuNCC.Enabled = false;
            b_HuyNCC.Enabled = false;
            b_XoaNCC.Enabled = false;
            b_SuaNCC.Enabled = false;
            LoadDGVNCC(); //Hiển thị danh sách nhà cung cấp
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

        //Hàm chạy lệnh
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

        // phần vinh làm//
        private void bt_HuyNV_Click(object sender, EventArgs e)
        {
            ResetValuesNV();
            tB_TENNV.Enabled = false;
            tb_DIACHINV.Enabled = false;
            tB_CMNDNV.Enabled = false;
            tB_SDTNV.Enabled = false;
            tB_MATKHAU.Enabled = false;
            rTB_CHUCVUNV.Enabled = false;
            tB_TENDANGNHAP.Enabled = false;
            bt_MoAnhNV.Enabled = false;
            bt_LUUNV.Enabled = false;
            rB_NAM.Enabled = false;
            rB_NU.Enabled = false;
            bt_SUANV.Enabled = false;
            bt_XOANV.Enabled = false;
            bt_THEMNV.Enabled = true;
            LoadDGVSP();
        }

        DataTable DTNV;

        private void LoadDGVNV()
        {
            string query;
            query = "Select MA_NV as [Mã nhân viên], TEN_NV as [Tên nhân viên], DIACHI_NV as [Địa chỉ], GIOITINH as [Giới tính], CMTND as [Chứng minh thư], ANH as [Hình ảnh], SDT_NV as [Số điện thoại], USERNAME as [Tên đăng nhập], PASS as [Mật khẩu], CHUCVU as [Chức vụ] from NV";
            DTNV = LayDuLieuRaBang(query, StringConnect);
            dGV_NV.DataSource = DTNV;
        }

        private string LayIDNhanVienTiepTheo()
        {
            string query;
            query = "Select Max(MA_NV) from NV";
            DTNV = LayDuLieuRaBang(query, StringConnect);
            try
            {
                return ((int)DTNV.Rows[0][0] + 1).ToString();
            }
            catch
            {
                return "0";
            }
        }

        private void ResetValuesNV()
        {
            tb_DIACHINV.Text = "";
            tB_CMNDNV.Text = "";
            tB_MANV.Text = "";
            tB_TENNV.Text = "";
            tB_SDTNV.Text = "";
            tB_MATKHAU.Text = "";
            rTB_HINHANHNV.Text = "";
            rTB_CHUCVUNV.Text = "";
            tB_TENDANGNHAP.Text = "";
            pB_NV = null;
            rB_NAM.Checked = false;
            rB_NU.Checked = false;
        }

        private void bt_THEMNV_Click(object sender, EventArgs e)
        {
            tB_TENNV.Enabled = true;
            tb_DIACHINV.Enabled = true;
            tB_CMNDNV.Enabled = true;
            tB_SDTNV.Enabled = true;
            tB_TENDANGNHAP.Enabled = true;
            tB_MATKHAU.Enabled = true;
            rTB_CHUCVUNV.Enabled = true;
            bt_MoAnhNV.Enabled = true;
            bt_LUUNV.Enabled = true;
            rB_NAM.Enabled = true;
            rB_NU.Enabled = true;
            bt_SUANV.Enabled = false;
            bt_XOANV.Enabled = false;
            bt_THEMNV.Enabled = false;
            bt_HUYNV.Enabled = true;
            ResetValuesNV();
            tB_MANV.Text = LayIDNhanVienTiepTheo();
        }

        private void bt_LUUNV_Click(object sender, EventArgs e)
        {
            string query, gt;
            if (tB_TENNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập tên nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tB_TENNV.Focus();
                return;
            }

            if (tB_CMNDNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập số CMTND của nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tB_CMNDNV.Focus();
                return;
            }

            if (rB_NAM.Checked == false && rB_NU.Checked == false)
            {
                MessageBox.Show("Bạn chưa chọn giới tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (rB_NAM.Checked == true && rB_NU.Checked == false)
            {
                gt = "Nam";
            }
            else gt = "Nữ";

            query = "Select CMTND from NV where CMTND = '" + tB_CMNDNV.Text.Trim() + "'";
            if (KiemTraMa(query, StringConnect))
                {
                MessageBox.Show("Nhân viên này đã có sẵn.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            query = "Select USERNAME from NV where USERNAME = '" + tB_TENDANGNHAP.Text.Trim() + "'";
            if (KiemTraMa(query, StringConnect))
            {
                MessageBox.Show("Tài khoản này đã có sẵn.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tB_TENDANGNHAP.Focus();
                return;
            }

            query = "Insert into NV(TEN_NV, DIACHI_NV, GIOITINH, CMTND, ANH, SDT_NV, USERNAME, PASS, CHUCVU) values(N'" + tB_TENNV.Text.Trim() + "', N'" + tb_DIACHINV.Text.Trim() + "', N'" + gt + "', '"
                + tB_CMNDNV.Text.Trim() + "', N'" + rTB_HINHANHNV.Text + "', '" + tB_SDTNV.Text.Trim() + "', '" + tB_TENDANGNHAP.Text.Trim() + "', '" + tB_MATKHAU.Text.Trim() + "', N'" + rTB_CHUCVUNV.Text.Trim() + "')";
            ChayLenh(query, Connect);
            LoadDGVNV();
            ResetValuesNV();
            tB_TENNV.Enabled = false;
            tb_DIACHINV.Enabled = false;
            tB_CMNDNV.Enabled = false;
            tB_SDTNV.Enabled = false;
            tB_MATKHAU.Enabled = false;
            rTB_CHUCVUNV.Enabled = false;
            tB_TENDANGNHAP.Enabled = false;
            bt_MoAnhNV.Enabled = false;
            bt_LUUNV.Enabled = false;
            rB_NAM.Enabled = false;
            rB_NU.Enabled = false;
            bt_SUANV.Enabled = true;
            bt_XOANV.Enabled = true;
            bt_THEMNV.Enabled = true;
        }

        private void bt_MoAnhNV_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Bitmap (*.bmp)|*.bmp|JPEG(*.jpg)|*jpg|GIF(*gif)|*gif|All files(*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.Title = "Chọn ảnh minh họa cho sản phẩm";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //pB_NV.Image = Image.FromFile(openFileDialog.FileName);
                rTB_HINHANHNV.Text = openFileDialog.FileName;
            }
        }

        private void bt_SUANV_Click(object sender, EventArgs e)
        {
            string query, gt;

            if (tB_TENNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tB_TENNV.Focus();
                return;
            }

            if (tB_CMNDNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập chứng minh thư của nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tB_CMNDNV.Focus();
                return;
            }

            if (rB_NAM.Checked == true && rB_NU.Checked == false)
            {
                gt = "Nam";
            }
            else gt = "Nữ";

            query = "Update NV set TEN_NV = N'" + tB_TENNV.Text.Trim() + "', DIACHI_NV = N'" + tb_DIACHINV.Text.Trim() + "', GIOITINH = N'" + gt + "', CMTND = '"
                + tB_CMNDNV.Text.Trim() + "', ANH = N'" + rTB_HINHANHNV.Text.Trim() + "', SDT_NV = '" + tB_SDTNV.Text.Trim() + "', USERNAME = '" + tB_TENDANGNHAP.Text.Trim() + "', PASS = '"
                + tB_MATKHAU.Text.Trim() + "', CHUCVU = N'" + rTB_CHUCVUNV.Text.Trim() + "' where MA_NV = " + tB_MANV.Text + "";
            ChayLenh(query, Connect);
            LoadDGVNV();
            ResetValuesNV();
            tB_TENNV.Enabled = false;
            tb_DIACHINV.Enabled = false;
            tB_CMNDNV.Enabled = false;
            tB_SDTNV.Enabled = false;
            tB_MATKHAU.Enabled = false;
            rTB_CHUCVUNV.Enabled = false;
            tB_TENDANGNHAP.Enabled = false;
            bt_MoAnhNV.Enabled = false;
            bt_LUUNV.Enabled = false;
            rB_NAM.Enabled = false;
            rB_NU.Enabled = false;
            bt_SUANV.Enabled = true;
            bt_XOANV.Enabled = true;
            bt_THEMNV.Enabled = true;
            bt_HUYNV.Enabled = false;
        }

        private void bt_XOANV_Click(object sender, EventArgs e)
        {
            string query;

            if (MessageBox.Show("bạn có muốn xóa nhân viên này hay không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                query = "Delete NV where MA_NV = '" + tB_MANV.Text + "'";
                ChayLenh(query, Connect);
                LoadDGVNV();
                ResetValuesNV();
            }

            tB_TENNV.Enabled = false;
            tb_DIACHINV.Enabled = false;
            tB_CMNDNV.Enabled = false;
            tB_SDTNV.Enabled = false;
            tB_MATKHAU.Enabled = false;
            rTB_CHUCVUNV.Enabled = false;
            tB_TENDANGNHAP.Enabled = false;
            bt_MoAnhNV.Enabled = false;
            bt_LUUNV.Enabled = false;
            rB_NAM.Enabled = false;
            rB_NU.Enabled = false;
            bt_SUANV.Enabled = true;
            bt_XOANV.Enabled = true;
            bt_HUYNV.Enabled = false;
            bt_THEMNV.Enabled = true;
        }

        private void dGV_NV_Click_1(object sender, EventArgs e)
        {
            if (bt_THEMNV.Enabled == false)
            {
                MessageBox.Show("Bạn đang ở trạng thái thêm mới.", "Thống báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tB_TENNV.Focus();
                return;
            }

            if (DTNV.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            tB_MANV.Text = dGV_NV.CurrentRow.Cells["Mã nhân viên"].Value.ToString();
            tB_TENNV.Text = dGV_NV.CurrentRow.Cells["Tên nhân viên"].Value.ToString();
            if (dGV_NV.CurrentRow.Cells["Giới tính"].Value.ToString() == "Nam")
                rB_NAM.Checked = true;
            else rB_NU.Checked = true;
            tb_DIACHINV.Text = dGV_NV.CurrentRow.Cells["Địa chỉ"].Value.ToString();
            rTB_CHUCVUNV.Text = dGV_NV.CurrentRow.Cells["Chức vụ"].Value.ToString();
            tB_SDTNV.Text = dGV_NV.CurrentRow.Cells["Số điện thoại"].Value.ToString();
            tB_CMNDNV.Text = dGV_NV.CurrentRow.Cells["Chứng minh thư"].Value.ToString();
            rTB_HINHANHNV.Text = dGV_NV.CurrentRow.Cells["Hình ảnh"].Value.ToString();
            tB_TENDANGNHAP.Text = dGV_NV.CurrentRow.Cells["Tên đăng nhập"].Value.ToString();
            tB_MATKHAU.Text = dGV_NV.CurrentRow.Cells["Mật khẩu"].Value.ToString();
            //if (rTB_HINHANHNV.Text.Length > 0)
            //    pB_NV.Image = Image.FromFile(rTB_HINHANHNV.Text);
            //else pB_NV.Image = null;
            bt_SUANV.Enabled = true;
            bt_XOANV.Enabled = true;
            bt_MoAnhNV.Enabled = true;
            tB_TENNV.Enabled = false;
            tb_DIACHINV.Enabled = true;
            tB_CMNDNV.Enabled = false;
            tB_SDTNV.Enabled = true;
            tB_MATKHAU.Enabled = true;
            rTB_CHUCVUNV.Enabled = true;
            tB_TENDANGNHAP.Enabled = true;
            rB_NAM.Enabled = true;
            rB_NU.Enabled = true;
        }

        public void getData()
        {
            string query = "Select TEN_NCC, SP.TEN_SP,GIANHAP,SOLUONG from NCC JOIN SP ON NCC.MA_NCC = SP.MA_NCC;";
            SqlDataAdapter apt = new SqlDataAdapter(query, Connect);
            DataTable tb = new DataTable();
            apt.Fill(tb);
            dataGridView1.DataSource = tb;

            for (int i = 0; i < 50; i++)
            {
                DomainUpDown.DomainUpDownItemCollection collect = this.txtSL.Items;
                collect.Add(i);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "update SP set SOLUONG  +='" + txtSL.SelectedItem + "' where TEN_SP ='" + txtSP.Text + "'";
            SqlCommand cmd = new SqlCommand(query, Connect);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Update!");
            getData();
        }
        // string extension = ".jpg";

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                txtNCC.Text = row.Cells["TEN_NCC"].Value.ToString();
                txtSP.Text = row.Cells["TEN_SP"].Value.ToString();
                txtGN.Text = row.Cells["GIANHAP"].Value.ToString();
                txtSL.Text = row.Cells["SOLUONG"].Value.ToString();
            }
        //    pictureBox1.Image = new Bitmap(Application.StartupPath + "\\Resources\\" + txtSP.Text + extension);
        }

        public double getMoney(double giaNhap, int soluong)
        {
            double tongTien;
            tongTien = giaNhap * soluong;
            return tongTien;
        }

        private void txtSL_SelectedItemChanged(object sender, EventArgs e)
        {
            txtTien.Text = Convert.ToString(getMoney(Convert.ToDouble(txtGN.Text), Convert.ToInt32(txtSL.Text)));
        }

        //Phần Tùng làm//
        DataTable DTKH;

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
            b_XoaKH.Enabled = true;
            b_HuyKH.Enabled = true;
            TBtenKH.Enabled = true;
            TBdiachiKH.Enabled = true;
            TBcmtKH.Enabled = true;
            TBsdtKH.Enabled = true;
            TBghichuKH.Enabled = true;
        }

        private string Lay_ma_KH_Tiep_Theo()
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

        private void b_ThemKH_Click(object sender, EventArgs e)
        {
            b_ThemKH.Enabled = false;
            TBtenKH.Enabled = true;
            TBdiachiKH.Enabled = true;
            TBcmtKH.Enabled = true;
            TBsdtKH.Enabled = true;
            TBghichuKH.Enabled = true;
            b_HuyKH.Enabled = true;
            b_SuaKH.Enabled = false;
            b_XoaKH.Enabled = false;
            b_LuuKH.Enabled = true;
            ResetValuesKH();
            TBmaKH.Text = Lay_ma_KH_Tiep_Theo();
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
            b_XoaKH.Enabled = false;
        }

        private void b_XoaKH_Click(object sender, EventArgs e)
        {
            string sql;

            if (MessageBox.Show("bạn có muốn xóa khách hàng này hay không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //xoa KH trong sql//
                sql = "Delete KH where MA_KH = '" + TBmaKH.Text + "'";
                ChayLenhXoa(sql, Connect);
                LoadDGVKH();
                ResetValuesKH();
            }

            TBtenKH.Enabled = false;
            TBdiachiKH.Enabled = false;
            TBcmtKH.Enabled = false;
            TBsdtKH.Enabled = false;
            TBghichuKH.Enabled = false;
            b_HuyKH.Enabled = false;
            b_LuuKH.Enabled = false;
            b_SuaKH.Enabled = false;
            b_XoaKH.Enabled = false;
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
            b_XoaKH.Enabled = false;
            b_ThemKH.Enabled = true;
        }

        private void b_HuyKH_Click(object sender, EventArgs e)
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
            b_XoaKH.Enabled = false;
            b_ThemKH.Enabled = true;
        }

        //Xong phần Tùng làm//

        //Phần thái làm//
        DataTable DTNCC;

        private void LoadDGVNCC()
        {
            string query = "Select TEN_NCC as [Tên nhà cung cấp], DIACHI_NCC as [Địa chỉ], SDT_NCC as [Số điện thoại], WEB_NCC as [Website] from NCC";
            DTNCC = LayDuLieuRaBang(query, StringConnect);
            dGV_NCC.DataSource = DTNCC;
        }

        private void dGV_NCC_Click(object sender, EventArgs e)
        {
            if (b_ThemNCC.Enabled == false)
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

            TBtenNCC.Text = dGV_NCC.CurrentRow.Cells["Tên nhà cung cấp"].Value.ToString();
            TBdiachiNCC.Text = dGV_NCC.CurrentRow.Cells["Địa chỉ"].Value.ToString();
            TBsdtNCC.Text = dGV_NCC.CurrentRow.Cells["Số điện thoại"].Value.ToString();
            TBwebNCC.Text = dGV_NCC.CurrentRow.Cells["Website"].Value.ToString();

            b_XoaNCC.Enabled = true;
            b_SuaNCC.Enabled = true;
            b_HuyNCC.Enabled = true;
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
            b_XoaNCC.Enabled = false;
            b_SuaNCC.Enabled = false;
        }

        private void b_HuyNCC_Click(object sender, EventArgs e)
        {
            ResetValuesNCC();
            TBtenNCC.Enabled = false;
            TBdiachiNCC.Enabled = false;
            TBwebNCC.Enabled = false;
            TBsdtNCC.Enabled = false;
            b_HuyNCC.Enabled = false;
            b_LuuNCC.Enabled = false;
            b_SuaNCC.Enabled = false;
            b_ThemNCC.Enabled = true;
        }

        private void b_ThemNCC_Click(object sender, EventArgs e)
        {

            b_XoaNCC.Enabled = false;
            b_SuaNCC.Enabled = false;
            b_LuuNCC.Enabled = true;
            b_ThemNCC.Enabled = false;
            ResetValuesNCC();
            TBtenNCC.Enabled = true;
            TBdiachiNCC.Enabled = true;
            TBsdtNCC.Enabled = true;
            TBwebNCC.Enabled = true;
            TBtenNCC.Focus();
            b_HuyNCC.Enabled = true;
        }

        private void b_SuaNCC_Click(object sender, EventArgs e)
        {
            string query;
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
            query = "Update NCC set DIACHI_NCC = N'" + TBdiachiNCC.Text.Trim().ToString() + "', SDT_NCC =  '" + TBsdtNCC.Text.Trim().ToString() + "', WEB_NCC = '"
                + TBwebNCC.Text.Trim().ToString() + "' where TEN_NCC = N'" + TBtenNCC.Text.Trim().ToString() + "'";
            ChayLenh(query, Connect);
            LoadDGVNCC();
            ResetValuesNCC();
            TBdiachiNCC.Enabled = false;
            TBsdtNCC.Enabled = false;
            TBwebNCC.Enabled = false;
            b_HuyNCC.Enabled = false;
            b_SuaNCC.Enabled = false;
        }

        private void b_XoaNCC_Click(object sender, EventArgs e)
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
                ChayLenhXoa(sql, Connect);
                LoadDGVNCC();
                ResetValuesNCC();
                b_SuaNCC.Enabled = false;
                b_HuyNCC.Enabled = false;
            }
        }

        private void b_LuuNCC_Click(object sender, EventArgs e)
        {
            string query;
            if (TBtenNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TBtenNCC.Focus();
                return;
            }

            query = "Select TEN_NCC from NCC where TEN_NCC = N'" + TBtenNCC.Text.Trim() + "'";

            if (KiemTraMa(query, StringConnect))
            {
                MessageBox.Show("Nhà cung cấp này đã có sẵn", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TBtenNCC.Focus();
                return;
            }
            // Insert trong sql //
            query = "Insert into NCC(TEN_NCC, DIACHI_NCC, SDT_NCC, WEB_NCC) values (N'" + TBtenNCC.Text + "', N'" + TBdiachiNCC.Text + "', '" + TBsdtNCC.Text + "', '" + TBwebNCC.Text + "')";
            ChayLenh(query, Connect);   //Thực hiện câu lệnh sql.
            LoadDGVNCC(); //Cập nhật lại DataGridView.
            ResetValuesNCC();
            b_ThemNCC.Enabled = true;
            TBtenNCC.Enabled = false;
            TBdiachiNCC.Enabled = false;
            TBsdtNCC.Enabled = false;
            TBwebNCC.Enabled = false;
            b_LuuNCC.Enabled = false;
            b_XoaNCC.Enabled = false;
            b_HuyNCC.Enabled = false;
            b_XoaNCC.Enabled = false;
        }

        private void tB_THANG_TextChanged(object sender, EventArgs e)
        {
            ResetValuesBC();
        }
        public static bool IsNumber(string txt)
        {
            foreach (Char c in txt)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        private void ResetValuesBC()
        {
            bt_INBAOCAO.Enabled = false;
            bt_LAYBAOCAO.Enabled = true;
            bt_BCSP_PBN.Enabled = false;
            bt_BCSP_DTCN.Enabled = false;
            bt_BCSP_BRNN.Enabled = false;
            dGV_BAOCAO.DataSource = null;
            tB_TONGDOANHTHU.Text = "";
            tB_SODONHANG.Text = "";
        }
        DataTable DTBC;
        private void bt_LAYBAOCAO_Click(object sender, EventArgs e)
        {
            try
            {
                bt_BCSP_BRNN.Enabled = true;
                bt_BCSP_DTCN.Enabled = true;
                bt_BCSP_PBN.Enabled = true;
                bt_INBAOCAO.Enabled = true;
                IsNumber(tB_THANG.Text);
                IsNumber(tB_NAM.Text);

                if (tB_THANG.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập tháng cần báo cáo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tB_THANG.Focus();
                    ResetValuesBC();
                    return;
                }

                if (tB_NAM.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập năm cần báo cáo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tB_NAM.Focus();
                    ResetValuesBC();
                    return;
                }

                string sql;
                sql = "Select NGAYBAN as [Ngày], TotalOrder as [Số đơn đặt hàng], TotalMoney as [Tổng danh thu ngày] from f_R_InfoMonthly('" + tB_THANG.Text.Trim() + "', '" + tB_NAM.Text.Trim() + "')";
                DTBC = LayDuLieuRaBang(sql, StringConnect);
                dGV_BAOCAO.DataSource = DTBC;

                sql = "Select * from f_R_TotalMonthly('" + tB_THANG.Text.Trim() + "', '" + tB_NAM.Text.Trim() + "')";
                DTBC = LayDuLieuRaBang(sql, StringConnect);
                if (DTBC != null)
                {
                    foreach (DataRow DR in DTBC.Rows)
                    {
                        tB_SODONHANG.Text = DR["TotalOrder"].ToString();
                        tB_TONGDOANHTHU.Text = DR["TotalMoney"].ToString();
                    }
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Bạn nhập sai kiểu ký tự", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bt_BCSP_PBN_Click(object sender, EventArgs e)
        {

            bt_BCSP_PBN.Enabled = false;
            bt_BCSP_DTCN.Enabled = true;
            bt_BCSP_BRNN.Enabled = true;

            if (tB_THANG.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tháng cần báo cáo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tB_THANG.Focus();
                ResetValuesBC();
                return;
            }

            if (tB_NAM.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập năm cần báo cáo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tB_NAM.Focus();
                ResetValuesBC();
                return;
            }

            string sql;
            sql = "select Distinct IDP as [Mã sản phẩm], left(trim(TEN_SP), charindex('_', TRIM(TEN_SP))-1) as [Tên sản phẩm], TotalOrder_P as [Số đơn hàng] from f_R_PMO('" + tB_THANG.Text + "','" + tB_NAM.Text + "'), SP " +
                "where left(trim(MA_SP), charindex('_', TRIM(MA_SP))-1) = IDP";
            DTBC = LayDuLieuRaBang(sql, StringConnect);
            dGV_BAOCAO.DataSource = DTBC;
        }

        private void bt_BCSP_BRNN_Click(object sender, EventArgs e)
        {
            bt_BCSP_BRNN.Enabled = false;
            bt_BCSP_PBN.Enabled = true;
            bt_BCSP_DTCN.Enabled = true;

            if (tB_THANG.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tháng cần báo cáo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tB_THANG.Focus();
                ResetValuesBC();
                return;
            }

            if (tB_NAM.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập năm cần báo cáo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tB_NAM.Focus();
                ResetValuesBC();
                return;
            }

            string sql;
            sql = "Select Distinct IDP as [Mã sản phẩm], left(trim(TEN_SP), charindex('_', TRIM(TEN_SP))-1) as [Tên sản phẩm], TotalON_P as [Số lượng bán] from f_R_PMON('" + tB_THANG.Text + "', '" + tB_NAM.Text + "'), SP " +
                "where left(trim(MA_SP), charindex('_', TRIM(MA_SP))-1) = IDP";
            DTBC = LayDuLieuRaBang(sql, StringConnect);
            dGV_BAOCAO.DataSource = DTBC;
        }

        private void bt_BCSP_DTCN_Click(object sender, EventArgs e)
        {
            bt_BCSP_DTCN.Enabled = false;
            bt_BCSP_BRNN.Enabled = true;
            bt_BCSP_PBN.Enabled = true;

            if (tB_THANG.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tháng cần báo cáo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tB_THANG.Focus();
                ResetValuesBC();
                return;
            }

            if (tB_NAM.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập năm cần báo cáo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tB_NAM.Focus();
                ResetValuesBC();
                return;
            }

            string sql;
            sql = "Select Distinct IDP as [Mã sản phẩm], left(trim(TEN_SP), charindex('_', TRIM(TEN_SP))-1) as [Tên sản phẩm], TotalMoney_P as [Doanh thu] from f_R_PMM('" + tB_THANG.Text + "', '" + tB_NAM.Text + "'), SP " +
                "where left(trim(MA_SP), charindex('_', TRIM(MA_SP))-1) = IDP";
            DTBC = LayDuLieuRaBang(sql, StringConnect);
            dGV_BAOCAO.DataSource = DTBC;
        }

        private void bt_INBAOCAO_Click(object sender, EventArgs e)
        {
            if (tB_THANG.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tháng cần báo cáo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tB_THANG.Focus();
                ResetValuesBC();
                return;
            }

            if (tB_NAM.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập năm cần báo cáo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tB_NAM.Focus();
                ResetValuesBC();
                return;
            }

            COMExcel.Application ExApp = new COMExcel.Application();
            COMExcel.Workbook ExBook;
            COMExcel.Worksheet ExSheet;
            COMExcel.Range ExRange;
            string sql;
            int hang = 0, cot = 0;
            ExBook = ExApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            ExSheet = ExBook.Worksheets[1];

            //Định dạng chung.
            ExRange = ExSheet.Cells[1, 1];
            ExRange.Range["A1:Z300"].Font.Name = "Times new roman";
            ExRange.Range["A1:B3"].Font.Size = 10;
            ExRange.Range["A1:B3"].Font.Bold = true;
            ExRange.Range["A1:B3"].Font.ColorIndex = 5;
            ExRange.Range["A1:A1"].ColumnWidth = 15;
            ExRange.Range["B1:B1"].ColumnWidth = 15;
            ExRange.Range["A1:B1"].MergeCells = true;
            ExRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            ExRange.Range["A1:B1"].Value = "CỬA HÀNG GIÀY DÉP";
            ExRange.Range["A2:B2"].MergeCells = true;
            ExRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            ExRange.Range["A2:B2"].Value = "Địa chỉ: Tây Hồ - Hà Nội";
            ExRange.Range["A3:B3"].MergeCells = true;
            ExRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            ExRange.Range["A3:B3"].Value = "Số điện thoại : 0989999999";
            ExRange.Range["C5:F5"].Font.Size = 18;
            ExRange.Range["C5:F5"].Font.Bold = true;
            ExRange.Range["C5:F5"].Font.ColorIndex = 3;
            ExRange.Range["C5:F5"].MergeCells = true;
            ExRange.Range["C5:F5"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            ExRange.Range["C5:F5"].Value = "BÁO CÁO DOANH THU THÁNG " + tB_THANG.Text + " NĂM " + tB_NAM.Text + "";

            //Thông tin báo cáo
            ExRange.Range["B7:D7"].Font.Size = 14;
            ExRange.Range["B7:D7"].MergeCells = true;
            ExRange.Range["B7:D7"].Value = "Danh sách sản phẩm phổ biến nhất";
            ExRange.Range["B7:D7"].Font.Bold = true;
            sql = sql = "select Distinct IDP, left(trim(TEN_SP), charindex('_', TRIM(TEN_SP))-1), TotalOrder_P from f_R_PMO('" + tB_THANG.Text + "','" + tB_NAM.Text + "'), SP " +
                "where left(trim(MA_SP), charindex('_', TRIM(MA_SP))-1) = IDP";
            DTBC = LayDuLieuRaBang(sql, StringConnect);
            ExRange.Range["A9:D9"].Font.Bold = true;
            ExRange.Range["A9:D9"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            ExRange.Range["C9:D9"].ColumnWidth = 14;
            ExRange.Range["C9:C9"].ColumnWidth = 40;
            ExRange.Range["A9:A9"].Value = "STT";
            ExRange.Range["B9:B9"].Value = "Mã sản phẩm";
            ExRange.Range["C9:C9"].Value = "Tên sản phẩm";
            ExRange.Range["D9:D9"].Value = "Số đơn hàng";
            for (hang = 0; hang < DTBC.Rows.Count; hang++)
            {
                ExSheet.Cells[1][hang + 10] = hang + 1;
                for (cot = 0; cot < DTBC.Columns.Count; cot++)
                {
                    ExSheet.Cells[cot + 2][hang + 10] = DTBC.Rows[hang][cot].ToString();
                }
            }

            ExRange = ExSheet.Cells[2][hang + 11];
            ExRange.Font.Bold = true;
            ExRange.Font.Size = 14;
            ExRange.Value2 = "Danh sách sản phẩm có số lượng bán ra nhiều nhất nhất";
            sql = "Select Distinct IDP as [Mã sản phẩm], left(trim(TEN_SP), charindex('_', TRIM(TEN_SP))-1) as [Tên sản phẩm], TotalON_P as [Số lượng bán] from f_R_PMON('" + tB_THANG.Text + "', '" + tB_NAM.Text + "'), SP " +
                "where left(trim(MA_SP), charindex('_', TRIM(MA_SP))-1) = IDP";
            DTBC = LayDuLieuRaBang(sql, StringConnect);
            ExRange = ExSheet.Cells[1][hang + 13];
            ExRange.ColumnWidth = 14;
            ExRange.Range["C1:C1"].ColumnWidth = 40;
            ExRange.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            ExRange.Range["A1:D1"].Font.Bold = true;
            ExRange.Range["A1:A1"].Value = "STT";
            ExRange.Range["B1:B1"].Value = "Mã sản phẩm";
            ExRange.Range["C1:C1"].Value = "Tên sản phẩm";
            ExRange.Range["D1:D1"].Value = "Số lượng bán ra";
            for (hang = 0; hang < DTBC.Rows.Count; hang++)
            {
                ExSheet.Cells[1][hang + 15] = hang + 1;
                for (cot = 0; cot < DTBC.Columns.Count; cot++)
                {
                    ExSheet.Cells[cot + 2][hang + 15] = DTBC.Rows[hang][cot].ToString();
                }
            }

            ExRange = ExSheet.Cells[2][hang + 16];
            ExRange.Font.Bold = true;
            ExRange.Font.Size = 14;
            ExRange.Value2 = "Danh sách sản phẩm có doanh thu nhiều nhất nhất";
            sql = "Select Distinct IDP as [Mã sản phẩm], left(trim(TEN_SP), charindex('_', TRIM(TEN_SP))-1) as [Tên sản phẩm], TotalMoney_P as [Doanh thu] from f_R_PMM('" + tB_THANG.Text + "', '" + tB_NAM.Text + "'), SP " +
                "where left(trim(MA_SP), charindex('_', TRIM(MA_SP))-1) = IDP";
            DTBC = LayDuLieuRaBang(sql, StringConnect);
            ExRange = ExSheet.Cells[1][hang + 18];
            ExRange.ColumnWidth = 14;
            ExRange.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            ExRange.Range["A1:D1"].Font.Bold = true;
            ExRange.Range["A1:A1"].Value = "STT";
            ExRange.Range["B1:B1"].Value = "Mã sản phẩm";
            ExRange.Range["C1:C1"].Value = "Tên sản phẩm";
            ExRange.Range["D1:D1"].Value = "Doanh thu";
            for (hang = 0; hang < DTBC.Rows.Count; hang++)
            {
                ExSheet.Cells[1][hang + 21] = hang + 1;
                for (cot = 0; cot < DTBC.Columns.Count; cot++)
                {
                    ExSheet.Cells[cot + 2][hang + 21] = DTBC.Rows[hang][cot].ToString();
                }
            }

            ExRange = ExSheet.Cells[2][hang + 22];
            ExRange.Font.Bold = true;
            ExRange.Font.Size = 14;
            ExRange.Value2 = "Tổng số lượng đơn hàng tháng " + tB_THANG.Text + " là: " + tB_SODONHANG.Text + "";

            ExRange = ExSheet.Cells[2][hang + 24];
            ExRange.Font.Bold = true;
            ExRange.Font.Size = 14;
            ExRange.Value2 = "Tổng doanh thu tháng " + tB_THANG.Text + " là: " + tB_TONGDOANHTHU.Text + "";

            ExSheet.Name = "Báo cáo doanh thu tháng";
            ExApp.Visible = true;
        }

        private void tB_NAM_TextChanged(object sender, EventArgs e)
        {
            ResetValuesBC();
        }

        private void bt_DONGBC_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

