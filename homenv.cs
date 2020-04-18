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
    
    public partial class homenv : Form
    {
        double tt=0;
        private string StringConnect;
        private SqlConnection Connect = null;
        string tensphd="\n\n";
        string giahd = "\n\n";

        string ten;//lưu tên nhân viên đang đăng nhập
        string ma; //Lưu mã nhân viên dùng bên dưới :>
        public homenv(string t,string con,string max):this()
        {
            ten = t;
            tennhanvien.Text = ten;
            StringConnect = con;
            ma = max;
        }
        public homenv()
        {
            InitializeComponent();
        }

        private void homenv_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void homenv_Load(object sender, EventArgs e)
        {
            Connect = new SqlConnection(StringConnect); //Khởi tạo kết nối với đường dẫn StringConnect
            Connect.Open();
            cbsize.Enabled = false;
            updatecbtenhang();
            updatedgvsp();
            LoadNV();
        }

        private void btdangxuat_Click(object sender, EventArgs e)
        {
            Form a = new dangnhap();
            a.Show();
            this.Hide();
        }

        private void thêmNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form a = new nvKH(StringConnect);
            a.Show();
        }
        private void updatecbtenhang()
        {
            SqlDataAdapter run;//lấy dữ liệu lấy từ CSDL
            DataSet bang = new DataSet();//luu du lieu lay tu csdl
            string query = "select TEN_SP from SP where SOLUONG >0";//query sql
            run = new SqlDataAdapter(query, Connect);
            run.Fill(bang);
            cbtenhang.DataSource = bang.Tables[0];
            cbtenhang.DisplayMember = "TEN_SP";
        }
        private void updatedgvsp()
        {
            SqlDataAdapter run;
            DataTable bang = new DataTable();
            string query = "select TEN_SP,TEN_NCC,SIZE,GIABAN,SOLUONG,GHICHU from SP, NCC where SP.MA_NCC=NCC.MA_NCC";
            run = new SqlDataAdapter(query, Connect);
            run.Fill(bang);
            dgvsp.DataSource = bang;
        }

        private void dgvsp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        DataTable DTNV;

        private static DataTable LayDuLieuRaBang(String query, string con)
        {
            SqlDataAdapter DA = new SqlDataAdapter(query, con);
            DataTable DT = new DataTable();
            DA.Fill(DT);
            return DT;
        }

        private void LoadNV()
        {
            rTB_DiaChi.Enabled = false;
            tB_CMTND.Enabled = false;
            tB_SDTNV.Enabled = false;
            tB_Pass.Enabled = false;
            b_HuyTK.Enabled = false;
            b_HuyTT.Enabled = false;
            b_Anh.Enabled = false;
            b_LuuTK.Enabled = false;
            b_LuuTT.Enabled = false;
            rB_2.Enabled = false;
            rB_1.Enabled = false;
            tB_UN.Enabled = false;
            cB_CH1.Enabled = false;
            cB_CH2.Enabled = false;
            tB_TL1.Enabled = false;
            tB_TL2.Enabled = false;

            string sql = "Select TEN_NV,CHUCVU, USERNAME, DIACHI_NV, SDT_NV, CMTND, PASS, ANH, GIOITINH from NV where MA_NV = " + ma + "";
            DTNV = LayDuLieuRaBang(sql, StringConnect);
            if (DTNV != null)
            {
                foreach (DataRow DR in DTNV.Rows)
                {
                    l_Ten.Text = DR["TEN_NV"].ToString();
                    l_ChucVu.Text = DR["CHUCVU"].ToString();
                    tB_UN.Text = DR["USERNAME"].ToString();
                    rTB_DiaChi.Text = DR["DIACHI_NV"].ToString();
                    tB_SDTNV.Text = DR["SDT_NV"].ToString();
                    tB_CMTND.Text = DR["CMTND"].ToString();
                    tB_Pass.Text = DR["PASS"].ToString();
                    rTB_Anh.Text = DR["ANH"].ToString();
                    if (DR["GIOITINH"].ToString() == "Nam")
                        rB_1.Checked = true;
                    else rB_2.Checked = true;
                    if (rTB_Anh.Text.Length > 0)
                        pB_NV.Image = Image.FromFile(rTB_Anh.Text);
                    else pB_NV.Image = null;
                }
            }

            string query = "Select MA_CAUHOI from NV_CAUHOI where MA_NV = " + ma + "";
            DTNV = LayDuLieuRaBang(query, StringConnect);
            if (DTNV != null)
            {
                string a = DTNV.Rows[0]["MA_CAUHOI"].ToString();
                query = "Select CAUHOI, TRALOI from NV_CAUHOI, CAUHOIBAOMAT where NV_CAUHOI.MA_CAUHOI = CAUHOIBAOMAT.MA_CAUHOI and MA_NV = " + ma + " and CAUHOIBAOMAT.MA_CAUHOI = " + a + "";
                DTNV = LayDuLieuRaBang(query, StringConnect);
                if (DTNV != null)
                {
                    foreach (DataRow DR in DTNV.Rows)
                    {
                        cB_CH1.Text = DR["CAUHOI"].ToString();
                        tB_TL1.Text = DR["TRALOI"].ToString();
                    }
                }
            }

            string query1 = "Select MA_CAUHOI from NV_CAUHOI where MA_NV = " + ma + "";
            DTNV = LayDuLieuRaBang(query1, StringConnect);
            if (DTNV != null)
            {
                string a = DTNV.Rows[0]["MA_CAUHOI"].ToString();
                query = "Select CAUHOI, TRALOI from NV_CAUHOI, CAUHOIBAOMAT where NV_CAUHOI.MA_CAUHOI = CAUHOIBAOMAT.MA_CAUHOI and MA_NV = " + ma + " and NV_CAUHOI.MA_CAUHOI = " + a + "";
                DTNV = LayDuLieuRaBang(query, StringConnect);
                if (DTNV != null)
                {
                    foreach (DataRow DR in DTNV.Rows)
                    {
                        cB_CH2.Text = DR["CAUHOI"].ToString();
                        tB_TL2.Text = DR["TRALOI"].ToString();
                    }
                }
            }
        }

        private void b_CapNhatThongTin_Click(object sender, EventArgs e)
        {
            rTB_DiaChi.Enabled = true;
            tB_CMTND.Enabled = true;
            tB_SDTNV.Enabled = true;
            b_HuyTT.Enabled = true;
            b_Anh.Enabled = true;
            b_LuuTT.Enabled = true;
            b_CapNhatThongTin.Enabled = false;
            b_CapNhatTK.Enabled = false;
            rB_1.Enabled = true;
            rB_2.Enabled = true;
        }

        private void b_Anh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Bitmap (*.bmp)|*.bmp|JPEG(*.jpg)|*jpg|GIF(*gif)|*gif|All files(*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.Title = "Chọn ảnh minh họa cho sản phẩm";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pB_NV.Image = Image.FromFile(openFileDialog.FileName);
                rTB_Anh.Text = openFileDialog.FileName;
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

        private void b_LuuTT_Click(object sender, EventArgs e)
        {
            string gt, sql;

            if (tB_CMTND.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập chứng minh thư của bạn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tB_CMTND.Focus();
                return;
            }

            if (rB_1.Checked == true && rB_2.Checked == false)
            {
                gt = "Nam";
            }
            else gt = "Nữ";

            sql = "Update NV set DIACHI_NV = N'" + rTB_DiaChi.Text.Trim() + "', GIOITINH = N'" + gt + "', CMTND = '" + tB_CMTND.Text.Trim() + "', ANH = N'"
                + rTB_Anh.Text.Trim() + "', SDT_NV = '" + tB_SDTNV.Text.Trim() + "' where MA_NV = " + ma + "";
            ChayLenh(sql, Connect);
            rTB_DiaChi.Enabled = false;
            tB_CMTND.Enabled = false;
            tB_SDTNV.Enabled = false;
            tB_Pass.Enabled = false;
            b_HuyTT.Enabled = false;
            b_Anh.Enabled = false;
            b_CapNhatTK.Enabled = true;
            b_CapNhatThongTin.Enabled = true;
            b_LuuTT.Enabled = false;
            rB_1.Enabled = false;
            rB_2.Enabled = false;
        }

        private void b_HuyTT_Click(object sender, EventArgs e)
        {
            LoadNV();
            rTB_DiaChi.Enabled = false;
            tB_CMTND.Enabled = false;
            tB_SDTNV.Enabled = false;
            tB_Pass.Enabled = false;
            b_HuyTK.Enabled = false;
            b_HuyTT.Enabled = false;
            b_Anh.Enabled = false;
            b_LuuTK.Enabled = false;
            b_LuuTT.Enabled = false;
            rB_2.Enabled = false;
            rB_1.Enabled = false;
            b_CapNhatThongTin.Enabled = true;
            b_CapNhatTK.Enabled = true;
        }

        private void b_HuyTK_Click(object sender, EventArgs e)
        {
            LoadNV();
            rTB_DiaChi.Enabled = false;
            tB_CMTND.Enabled = false;
            tB_SDTNV.Enabled = false;
            tB_Pass.Enabled = false;
            b_HuyTK.Enabled = false;
            b_HuyTT.Enabled = false;
            b_Anh.Enabled = false;
            b_LuuTK.Enabled = false;
            b_LuuTT.Enabled = false;
            rB_2.Enabled = false;
            rB_1.Enabled = false;
            b_CapNhatThongTin.Enabled = true;
            b_CapNhatTK.Enabled = true;
        }

        private void b_CapNhatTK_Click(object sender, EventArgs e)
        {
            tB_UN.Enabled = false;
            tB_Pass.Enabled = true;
            b_CapNhatThongTin.Enabled = false;
            b_CapNhatTK.Enabled = false;
            b_LuuTK.Enabled = true;
            b_HuyTK.Enabled = true;
        }

        private void b_LuuTK_Click(object sender, EventArgs e)
        {
            string sql;

            if (tB_Pass.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu của bạn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tB_Pass.Focus();
                return;
            }

            sql = "Update NV set Pass = '" + tB_Pass.Text.Trim() + "' where MA_NV = " + ma + "";
            ChayLenh(sql,Connect);
            tB_Pass.Enabled = false;
            b_CapNhatThongTin.Enabled = true;
            b_CapNhatTK.Enabled = true;
            b_LuuTK.Enabled = false;
            b_HuyTK.Enabled = false;
        }

        private void nhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form a = new nvNCC(StringConnect);
            a.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form a = new dangnhap();
            a.Show();
            this.Hide();
        }

        private void btthanhtoan_Click(object sender, EventArgs e)
        {
            
            DialogResult res = MessageBox.Show("Bạn có chắc chắn muốn thanh toán?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(res==DialogResult.Yes)
            {
                check_sp(ref tensphd, ref giahd);
                Form a = new hoadon(tensphd, giahd, lbtongtien.Text);
                a.Show();
                reset();
            }

        }

        private void btthem_Click(object sender, EventArgs e)
        {
            string query="SELECT GIABAN FROM SP where TEN_SP='" + cbtenhang.Text + "'";
            string giaban;
            using (SqlCommand cmd = new SqlCommand(query, Connect))
            {
                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                giaban = dr["GIABAN"].ToString();
                dr.Close();
            }
            dgvgiohang.Rows.Add(cbtenhang.Text,cbsize.Text, giaban);
            
            tt += Convert.ToDouble(giaban);
            lbtongtien.Text =tt.ToString();
        }

        private void cbtenhang_SelectedValueChanged(object sender, EventArgs e)
        {
            cbsize.Enabled = true;
            SqlDataAdapter run;//lấy dữ liệu lấy từ CSDL
            DataSet bang = new DataSet();//luu du lieu lay tu csdl
            string query = "select SIZE from SP where TEN_SP ='"+cbtenhang.Text+"'";//query sql
            run = new SqlDataAdapter(query, Connect);
            run.Fill(bang);
            cbsize.DataSource = bang.Tables[0];
            cbsize.DisplayMember = "SIZE";
        }
        private void check_sp(ref string tensp,ref string giaban)
        {
            Dictionary<string, int> a = new Dictionary<string, int>();
            //lbtongtien.Text = Convert.ToString(dgvgiohang.Rows[0].Cells[0].Value);
            int dem = dgvgiohang.RowCount;
            for (int i=0; i<dem;i++)
            {
                string b = "";
                
                b = Convert.ToString(dgvgiohang.Rows[i].Cells[0].Value);
                int gia = Convert.ToInt32(dgvgiohang.Rows[i].Cells[2].Value);
                if (a.ContainsKey(b))
                    a[b]+=gia;
                else
                    a[b] = gia;
            }
            foreach(KeyValuePair<string,int> r in a)
            {
                tensp += r.Key + "\n \n";
                giaban += Convert.ToString(r.Value) + " đ\n \n";
            }
        }
        private void reset()
        {
            dgvgiohang.Rows.Clear();
        }
    }
}
