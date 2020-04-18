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
    public partial class homeql : Form
    {
        private string StringConnect = "Data Source=DESKTOP-R9IA4BP\\SQLEXPRESS;Initial Catalog=QUANLYCUAHANGGIAY;Integrated Security=True";
        private SqlConnection Connect = null;
        public homeql(string con):this()
        {
            StringConnect = con;
        }
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

        private void homeql_Load(object sender, EventArgs e)
        {
            Connect = new SqlConnection(StringConnect); //Khởi tạo kết nối với đường dẫn StringConnect
            Connect.Open();
            getData();
        }

        private void quảnLýNhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void quảnLýLoạiHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void quảnLýKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 4;
        }

        private void báoCáoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 5;
        }

        private void đăngXuấtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form a =new dangnhap();
            a.Show();
            this.Hide();
        }

        private void thêmKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form a = new nvthemkhachhang();
            a.Show();
        }

        private void xóaKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form a = new qlkhachhang();
            a.Show();
        }
        public void getData()
        {
            string query = "Select TEN_NCC, SP.TEN_SP,GIANHAP,SOLUONG from NCC JOIN SP ON NCC.MA_NCC = SP.MA_NCC;";
            SqlDataAdapter apt = new SqlDataAdapter(query, Connect);
            DataTable tb = new DataTable();
            apt.Fill(tb);
            dataGridView2.DataSource = tb;
            
            for(int i = 0; i < 50; i++)
            {
                DomainUpDown.DomainUpDownItemCollection collect = this.txtSL.Items;
                collect.Add(i);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string query = "update SP set SOLUONG  +='"+ txtSL.SelectedItem + "' where TEN_SP ='"+txtSP.Text+"'";
            SqlCommand cmd = new SqlCommand(query ,Connect);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Update!");
            getData();
        }
        string extension = ".jpg";
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];

                txtNCC.Text = row.Cells["TEN_NCC"].Value.ToString();
                txtSP.Text = row.Cells["TEN_SP"].Value.ToString();
                txtGN.Text = row.Cells["GIANHAP"].Value.ToString();
                //txtSL.Text = row.Cells["SOLUONG"].Value.ToString();
            }
            pictureBox2.Image = new Bitmap(Application.StartupPath + "\\Resources\\" + txtSP.Text + extension);

        }
        public double getMoney(double giaNhap, int soluong)
        {
            double tongTien;
            tongTien = giaNhap * soluong;
            return tongTien;
        }
        private void txtSL_SelectedItemChanged(object sender, EventArgs e)
        {
            txtTien.Text = Convert.ToString(getMoney(Convert.ToDouble(txtGN.Text) , Convert.ToInt32(txtSL.SelectedItem)));

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void txtGN_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void txtSP_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void txtTien_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNCC_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
