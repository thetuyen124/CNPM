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
    public partial class nvthemkhachhang : Form
    {
        public nvthemkhachhang()
        {
            InitializeComponent();
        }

        private void btok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nvthemkhachhang_Load(object sender, EventArgs e)
        {
            tbtenkhachhang.Focus();
        }
    }
}
