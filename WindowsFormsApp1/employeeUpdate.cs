using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class employeeUpdate : Form
    {
        public employeeUpdate()
        {
            InitializeComponent();
            LoadEmployees();
            LoadJobIds();
        }

        private void LoadEmployees()
        {
            //again using my database, we can change -cathy
            OracleConnection con = new OracleConnection("User Id=COMP214_F22_ER_46;Password=password;Data Source=199.212.26.208:1521/SQLD");
            con.Open();

            OracleDataAdapter AdapterSelect = new OracleDataAdapter("SELECT * from hr_employees", con);

            DataTable dt = new DataTable();

            AdapterSelect.Fill(dt);

            dgvEmployee.DataSource = dt.DefaultView;
            dgvEmployee.Refresh();
        }

        private void LoadJobIds()
        {
            OracleConnection con = new OracleConnection("User Id=COMP214_F22_ER_56;Password=password;Data Source=199.212.26.208:1521/SQLD");
            con.Open();

            OracleCommand cmd = new OracleCommand("SELECT job_id as a FROM hr_jobs", con);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataTable data = new DataTable();
            adapter.Fill(data);

            comboBox1.DataSource = data;
            comboBox1.DisplayMember = "a";
            comboBox1.ValueMember = "a";
        }

        //declaring this outside so it can be passed to another method
        int employee_id;

        private void dgvEmployee_RowHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            employee_id = int.Parse(dgvEmployee.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtEmail.Text = dgvEmployee.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtPhone.Text = dgvEmployee.Rows[e.RowIndex].Cells[4].Value.ToString();
            comboBox1.SelectedIndex = comboBox1.FindStringExact(dgvEmployee.Rows[e.RowIndex].Cells[6].Value.ToString());
            txtSalary.Text = dgvEmployee.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            employee_id = 0;
            txtEmail.Clear();
            txtPhone.Clear();
            txtSalary.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //this is my oracle connection -cathy
            OracleConnection con = new OracleConnection("User Id=COMP214_F22_ER_46;Password=password;Data Source=199.212.26.208:1521/SQLD");
            con.Open();
            OracleDataAdapter da = new OracleDataAdapter("upd_employee", con);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("p_emp_id", OracleDbType.Int32).Value = employee_id;
            da.SelectCommand.Parameters.Add("p_salary", OracleDbType.Int32).Value = int.Parse(txtSalary.Text);
            da.SelectCommand.Parameters.Add("p_phone", OracleDbType.Varchar2).Value = txtPhone.Text;
            da.SelectCommand.Parameters.Add("p_email", OracleDbType.Varchar2).Value = txtEmail.Text;
            da.SelectCommand.Parameters.Add("p_job_id", OracleDbType.Varchar2).Value = comboBox1.Text;

            DataTable dt = new DataTable();

            try
            {
                da.Fill(dt);
                MessageBox.Show("Employee updated successfully.");
                dt.AcceptChanges();

                LoadEmployees();
                con.Close();
            }
            catch (OracleException ex) when (ex.Message.Contains("ORA-20100"))
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
