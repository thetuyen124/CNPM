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
        private void update()
        {

        }
    }
}
