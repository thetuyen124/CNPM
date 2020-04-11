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
        public dangnhap()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            quenmatkhau.Hide();
        }

        private void btdangnhap_Click(object sender, EventArgs e)
        {
            Form a = new homenv();
            a.Show();
            this.Hide();
        }
    }
}
