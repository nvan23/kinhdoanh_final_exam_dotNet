using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kinhdoanh
{
    public partial class Home : Form
    {
        int index = 0;
        int tong_tien = 0;
        SqlDataAdapter da;
        DataTable dt = new DataTable();
        public Home()
        {
            InitializeComponent();
        }

        public void load_data()
        {
            try
            {
                clsDatabase.OpenConnection();
                string strSelect = "Select chi_tiet.MASD, LOAISD, DONGIA, SoKW, ThanhTien=su_dung.DONGIA*chi_tiet.SoKW, chi_tiet.MAKH, khach_hang.TENKH, khach_hang.DIACHI from khach_hang inner join chi_tiet on khach_hang.MAKH = chi_tiet.MAKH inner join su_dung on chi_tiet.MASD = su_dung.MASD";
                SqlCommand com = new SqlCommand(strSelect, clsDatabase.con);
                com.ExecuteNonQuery();
                da = new SqlDataAdapter(com);
                da.Fill(dt);
                dgvKinhDoanh.DataSource = dt;
                dgvKinhDoanh.Columns[5].Visible = false;
                dgvKinhDoanh.Columns[6].Visible = false;
                dgvKinhDoanh.Columns[7].Visible = false;
                showData(0);
                foreach (DataRow dr in dt.Rows)
                {
                    tong_tien += Convert.ToInt32(dr["ThanhTien"].ToString());
                }
                txtTongTien.Text = tong_tien.ToString();
                clsDatabase.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void showData(int index)
        {
            txtMAKH.Text = dt.Rows[index][5].ToString();
            txtTenKH.Text = dt.Rows[index][6].ToString();
            txtDiaChi.Text = dt.Rows[index][7].ToString();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            load_data();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            index++;
            if (index < dt.Rows.Count)
            {
                showData(index);
            }
            else
            {
                MessageBox.Show("You are in the last data", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                index = dt.Rows.Count - 1;
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            index--;
            if (index >= 0)
            {
                showData(index);
            }
            else
            {
                MessageBox.Show("You are in the first data", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
