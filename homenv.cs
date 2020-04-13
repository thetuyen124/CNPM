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
    
    public partial class homenv : Form
    {
        string ten;
        public homenv(string x):this()
        {
            ten = x;
            tennhanvien.Text = ten;
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

        }

        private void btdangxuat_Click(object sender, EventArgs e)
        {
            Form a = new dangnhap();
            a.Show();
            this.Hide();
        }
<<<<<<< HEAD

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
=======
>>>>>>> b19b6197c3e04d08021fd88c53c357791782179c
    }
}
