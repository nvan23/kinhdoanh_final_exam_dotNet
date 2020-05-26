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
        SqlDataAdapter da;
        DataTable dt = new DataTable();
        BindingSource bs;
        DataSet dset;

        public Home()
        {
            InitializeComponent();
            load_data();
        }
        

        public void load_data()
        {
            try
            {
                string strSelect = "SELECT * FROM khach_hang";
                clsDatabase.OpenConnection();
                SqlCommand con = new SqlCommand(strSelect, clsDatabase.con);
                SqlDataAdapter da = new SqlDataAdapter(con);
                da.Fill(dt);
                clsDatabase.CloseConnection();

                //Lay khach hang dau tien
                txtMAKH.Text = dt.Rows[index]["MAKH"].ToString();
                txtTenKH.Text = dt.Rows[index]["TENKH"].ToString();
                txtDiaChi.Text = dt.Rows[index]["DIACHI"].ToString();
                load_chitiet(txtMAKH.Text);
                btnPrevious.Visible = false;

            }
            catch (Exception ex)
            {

            }
            
        }

        //Lay chi tiet khach hang 
        public void load_chitiet(string ma)
        {
            try
            {
                string strSelect = "SELECT sd.MASD, sd.LOAISD, ct.SoKW, sd.DONGIA, ct.SoKW*sd.DONGIA as THANHTIEN FROM khach_hang kh join chi_tiet ct on kh.MAKH = ct.MAKH join su_dung sd on ct.MASD = sd.MASD WHERE ct.MAKH= '" + ma + "'";
                clsDatabase.OpenConnection();
                SqlCommand con = new SqlCommand(strSelect, clsDatabase.con);
                SqlDataAdapter da = new SqlDataAdapter(con);
                DataTable dt2 = new DataTable();
                da.Fill(dt2);
                dgvKinhDoanh.DataSource = dt2;
                clsDatabase.CloseConnection();
                double total = 0;
                total = dgvKinhDoanh.Rows.Cast<DataGridViewRow>()
                .Sum(t => Convert.ToDouble(t.Cells[4].Value));

                txtTongTien.Text = total.ToString();

                //paging
                dset = new DataSet();
                da.Fill(dset);
                bs = new BindingSource();
                bs.DataSource = dset.Tables[0].DefaultView;
                bindingNavigator_kinh_doanh.BindingSource = bs;
                dgvKinhDoanh.DataSource = bs;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                index++;
                if (index < dt.Rows.Count - 1)
                {
                    btnPrevious.Visible = true;
                    txtMAKH.Text = dt.Rows[index]["MAKH"].ToString();
                    txtTenKH.Text = dt.Rows[index]["TENKH"].ToString();
                    txtDiaChi.Text = dt.Rows[index]["DIACHI"].ToString();
                    load_chitiet(txtMAKH.Text);
                }
                else
                {
                    btnNext.Visible = false;
                    MessageBox.Show("You are in the last data", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                index--;
                if (index >= 0)
                {
                    btnNext.Visible = true;
                    txtMAKH.Text = dt.Rows[index]["MAKH"].ToString();
                    txtTenKH.Text = dt.Rows[index]["TENKH"].ToString();
                    txtDiaChi.Text = dt.Rows[index]["DIACHI"].ToString();
                    load_chitiet(txtMAKH.Text);
                }
                else
                {
                    btnPrevious.Visible = false;
                    MessageBox.Show("You are in the first data", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
