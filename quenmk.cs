using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace CNPM
{
    public partial class quenmk : Form
    {
        public quenmk()
        {
            InitializeComponent();
            lbM.Hide();
        }

        private void quenmk_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form a = new dangnhap();
            a.Show();
        }
        void fillCombo()
        {
            string connectString = "Data Source=DESKTOP-R9IA4BP\\SQLEXPRESS;Initial Catalog=QUANLYCUAHANGGIAY;Integrated Security=True";
            string query = " select NV.USERNAME,CAUHOIBAOMAT.CAUHOI ,NV_CAUHOI.TRALOI from NV JOIN NV_CAUHOI ON NV.MA_NV = NV_CAUHOI.MA_NV JOIN CAUHOIBAOMAT ON CAUHOIBAOMAT.MA_CAUHOI = NV_CAUHOI.MA_CAUHOI WHERE USERNAME = '"+txtUser.Text+"'";
           
            SqlConnection connect = new SqlConnection(connectString);
            SqlCommand cmd = new SqlCommand(query,connect);
            SqlDataReader myReader;

            try
            {
                connect.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    string cauhoi = myReader.GetString(1);
                    comboBox1.Items.Add(cauhoi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            fillCombo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectString = "Data Source=DESKTOP-R9IA4BP\\SQLEXPRESS;Initial Catalog=QUANLYCUAHANGGIAY;Integrated Security=True";
            string query = " select NV_CAUHOI.TRALOI from NV JOIN NV_CAUHOI ON NV.MA_NV = NV_CAUHOI.MA_NV JOIN CAUHOIBAOMAT ON CAUHOIBAOMAT.MA_CAUHOI = NV_CAUHOI.MA_CAUHOI WHERE nv.USERNAME = '" + txtUser.Text + "' AND CAUHOI = N'" + comboBox1.SelectedItem.ToString() + "';";
            SqlConnection connect = new SqlConnection(connectString);
            SqlCommand cmd = new SqlCommand(query, connect);
            SqlDataReader myReader;
            try
            {
                connect.Open();
                myReader = cmd.ExecuteReader();
                myReader.Read();
                string traloi = myReader.GetString(0);
                if (String.Equals(textBox2.Text, traloi))
                {
                    lbM.Show();
                    newP.ReadOnly = false;
                    conP.ReadOnly = false;
                }
                else
                {
                    MessageBox.Show("Sai câu trả lời hoặc câu hỏi bảo mật!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(String.Equals(newP.Text, conP.Text))
            {
                string connectString = "Data Source=DESKTOP-R9IA4BP\\SQLEXPRESS;Initial Catalog=QUANLYCUAHANGGIAY;Integrated Security=True";
                string query = " update NV set PASS = '"+conP.Text+"' where USERNAME = '"+txtUser.Text+"';";
                SqlConnection connect = new SqlConnection(connectString);
                connect.Open();
                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated");
            }
            else
            {
                MessageBox.Show("Mật Khẩu không trùng khớp");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form a = new dangnhap();
            a.Show();
            this.Hide();
        }
    }
}
