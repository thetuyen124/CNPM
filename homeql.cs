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
    public partial class homeql : Form
    {
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
            tabControl1.SelectedIndex=0;
        }
    }
}
