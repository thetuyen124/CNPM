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
        private string StringConnect;
        private SqlConnection Connect = null;

        string ten;//lưu tên nhân viên đang đăng nhập
        public homenv(string t,string con):this()
        {
            ten = t;
            tennhanvien.Text = ten;
            StringConnect = con;
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
            updatecbtenhang();
            updatedgvsp();
        }

        private void btdangxuat_Click(object sender, EventArgs e)
        {
            Form a = new dangnhap();
            a.Show();
            this.Hide();
        }

        private void thêmNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form a = new nvthemkhachhang();
            a.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form a = new dangnhap();
            a.Show();
            this.Hide();
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
    }
}
