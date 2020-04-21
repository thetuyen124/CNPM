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
    public partial class hoadon : Form
    {
        public hoadon(string ten,string gia,string tongtien):this()
        {
            lbgia.Text = gia;
            lbsp.Text = ten;
            lbthanhtien.Text = tongtien;
        }
        public hoadon()
        {
            InitializeComponent();
        }

        private void btin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btok_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
