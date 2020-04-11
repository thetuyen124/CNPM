using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM
{
    public partial class dangnhap : Form
    {
        int dem;
        
        public dangnhap()
        {
            InitializeComponent();
        }
        private void loadtkmk()
        {
            tbmatkhau.Text = "";
            tbtaikhoan.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            quenmatkhau.Hide();
            dem = 0;
        }

        private void btdangnhap_Click(object sender, EventArgs e)
        {
            if (tbtaikhoan.Text == "quanly")
            {
                Form a = new homeql();
                loadtkmk();
                a.Show();
                this.Hide();
            }
            else if (tbtaikhoan.Text == "nhanvien")
            {
                Form b = new homenv();
                loadtkmk();
                b.Show();
                this.Hide();
            }
            else
            {
                dem++;
                loadtkmk();
                MessageBox.Show("Tài khoản mật khẩu không chính xác","Warning",MessageBoxButtons.OK);
                tbtaikhoan.Focus();

                if (dem >= 3)
                    quenmatkhau.Show();


                if (dem >= 5)
                    btdangnhap.Enabled=false;
            }
        }

        private void dangnhap_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void quenmatkhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form a = new quenmk();
            a.Show();
            this.Hide();
        }
    }
}
