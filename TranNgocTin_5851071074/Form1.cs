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

namespace TranNgocTin_5851071074
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-JHRGK5N\SQLEXPRESS;Initial Catalog=DemoCrud;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getStuden();
        }

        private void reset()
        {
            txtFN.Text = "";
            txtName.Text = "";
            txtRoll.Text = "";
            txtAdd.Text = "";
            txtPhone.Text = "";
        }

        public void getStuden()
        {
            SqlCommand cmd = new SqlCommand("select * from student", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sqlData = cmd.ExecuteReader();
            dt.Load(sqlData);
            con.Close();
            dgvStu.DataSource = dt;
        }

        public bool isVali()
        {
            if (txtFN.Text == string.Empty
                || txtName.Text == string.Empty 
                || txtAdd.Text == string.Empty
                || string.IsNullOrEmpty(txtPhone.Text)
                ||string.IsNullOrEmpty(txtRoll.Text))
            {
                MessageBox.Show("Data null!!", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if(isVali())
            {
                SqlCommand cmd = new SqlCommand("insert into student values (@name, @fathername, @rollname, @addresss, @mobile)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtFN.Text);
                cmd.Parameters.AddWithValue("@fathername", txtName.Text);
                cmd.Parameters.AddWithValue("@rollname", txtRoll.Text);
                cmd.Parameters.AddWithValue("@addresss", txtAdd.Text);
                cmd.Parameters.AddWithValue("@mobile", txtPhone.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                getStuden();
            }
        }

        private int id;
        private void dgvStu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvStu.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dgvStu.CurrentRow.Selected = true;
                id = Convert.ToInt32(dgvStu.Rows[e.RowIndex].Cells[0].Value);
                txtFN.Text = dgvStu.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtName.Text = dgvStu.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtRoll.Text = dgvStu.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtAdd.Text = dgvStu.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtPhone.Text = dgvStu.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
            
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if(isVali())
            {
                    SqlCommand cmd = new SqlCommand("update student set name = @name" +
                        "fathername = @fathername, rollname = @rollname," +
                        "addresss = @addresss, mobile = @mobile where studenid = @id", con);
                    con.Open();
                cmd.Parameters.AddWithValue("@studentid", id);
                cmd.Parameters.AddWithValue("@name", txtFN.Text);
                cmd.Parameters.AddWithValue("@fathername", txtName.Text);
                cmd.Parameters.AddWithValue("@rollname", txtRoll.Text);
                cmd.Parameters.AddWithValue("@addresss", txtAdd.Text);
                cmd.Parameters.AddWithValue("@mobile", txtPhone.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                getStuden();
                reset();

            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }
            //if(id > 0)
            //{
            //    SqlCommand cmd = new SqlCommand("update student set name = @name, fathername = @fathername" +
            //        "rollname= @rollname, addresss= @addresss, mobile =@mobile," +
            //        "where studentid = @id", con);
            //    cmd.CommandType = CommandType.Text;
            //    cmd.Parameters.AddWithValue("@name", txtFN.Text);
            //    cmd.Parameters.AddWithValue("@fathername", txtName.Text);
            //    cmd.Parameters.AddWithValue("@rollname", txtRoll.Text);
            //    cmd.Parameters.AddWithValue("@addresss", txtAdd.Text);
            //    cmd.Parameters.AddWithValue("@mobile", txtPhone.Text);
            //    cmd.Parameters.AddWithValue("@id", this.id);

            //    con.Open();
            //    cmd.ExecuteNonQuery();
            //    con.Close();
            //    getStuden();


        }
    }
}
