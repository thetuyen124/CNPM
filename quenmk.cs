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
    public partial class quenmk : Form
    {
        public quenmk()
        {
            InitializeComponent();
        }

        private void quenmk_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form a = new dangnhap();
            a.Show();
        }
    }
}
