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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class employeeHiring : Form
    {
        public employeeHiring()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void employeeHiring_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet2.HR_EMPLOYEES' table. You can move, or remove it, as needed.
            this.hR_EMPLOYEESTableAdapter.Fill(this.dataSet2.HR_EMPLOYEES);
            // TODO: This line of code loads data into the 'dataSet2.HR_JOBS' table. You can move, or remove it, as needed.
            this.hR_JOBSTableAdapter.Fill(this.dataSet2.HR_JOBS);
            OracleConnection con = new OracleConnection("User Id=COMP214_F22_ER_56;Password=password;Data Source=199.212.26.208:1521/SQLD");
            con.Open();
            // Create a command to retrieve data from the database
            OracleCommand cmd = new OracleCommand("SELECT job_id||',  '||job_title as a FROM hr_jobs", con);

            // Execute the command and retrieve the data
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataTable data = new DataTable();
            adapter.Fill(data);

            // Set the ComboBox's data source and display and value members
            comboBox2.DataSource = data;
            comboBox2.DisplayMember = "a";
            comboBox2.ValueMember = "a";

            // Close the connection
            con.Close();

            OracleConnection con2 = new OracleConnection("User Id=COMP214_F22_ER_56;Password=password;Data Source=199.212.26.208:1521/SQLD");
            con2.Open();
            OracleCommand cmd2 = new OracleCommand("SELECT department_id||',  '||department_name as b FROM hr_departments", con2);

            // Execute the command and retrieve the data
            OracleDataAdapter adapter2 = new OracleDataAdapter(cmd2);
            DataTable data2 = new DataTable();
            adapter2.Fill(data2);

            // Set the ComboBox's data source and display and value members
            comboBox1.DataSource = data2;
            comboBox1.DisplayMember = "b";
            comboBox1.ValueMember = "b";

            //no need to change since I'm just using keying's login info -cathy
            OracleConnection con3 = new OracleConnection("User Id=COMP214_F22_ER_56;Password=password;Data Source=199.212.26.208:1521/SQLD");
            con3.Open();
            OracleCommand cmd3 = new OracleCommand("SELECT employee_id||', '||first_name||', '||last_name as c FROM hr_employees WHERE employee_id IN" +
                "(SELECT DISTINCT manager_id FROM hr_employees)", con3);

            OracleDataAdapter adapter3 = new OracleDataAdapter(cmd3);
            DataTable data3 = new DataTable();
            adapter3.Fill(data3);

            cmbManager.DataSource = data3;
            cmbManager.DisplayMember = "c";
            cmbManager.DisplayMember = "c";


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("111");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtFname.Clear();
            txtLName.Clear();
            txtSalary.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtHireDate.Clear();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            cmbManager.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            employeeUpdate updatepage = new employeeUpdate();
            updatepage.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this is my oracle login, we can change this later - cathy
            OracleConnection con = new OracleConnection("User Id=COMP214_F22_ER_46;Password=password;Data Source=199.212.26.208:1521/SQLD");
            con.Open();

            string jobId = comboBox2.Text;
            string[] splitJob = jobId.Split(',');

            string departmentId = comboBox1.Text;
            string[] splitDepartment = departmentId.Split(',');

            string managerId = cmbManager.Text;
            string[] splitManager = managerId.Split(',');

            //just need to pull this procedure from the chat or ask me and I'll send it over -cathy
            OracleDataAdapter da = new OracleDataAdapter("employee_hire_sp", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = txtFname.Text;
            da.SelectCommand.Parameters.Add("p_last_name", OracleDbType.Varchar2).Value = txtLName.Text;
            da.SelectCommand.Parameters.Add("p_email", OracleDbType.Varchar2).Value = txtEmail.Text;
            da.SelectCommand.Parameters.Add("p_salary", OracleDbType.Int32).Value = int.Parse(txtSalary.Text);
            da.SelectCommand.Parameters.Add("p_hire_date", OracleDbType.Date).Value = DateTime.Parse(txtHireDate.Text);
            da.SelectCommand.Parameters.Add("p_phone", OracleDbType.Varchar2).Value = txtPhone.Text;
            da.SelectCommand.Parameters.Add("p_job_id", OracleDbType.Varchar2).Value = splitJob[0];
            da.SelectCommand.Parameters.Add("p_manager", OracleDbType.Int32).Value = int.Parse(splitManager[0]);
            da.SelectCommand.Parameters.Add("p_department_id", OracleDbType.Int32).Value = int.Parse(splitDepartment[0]);

            DataTable dt = new DataTable();
            da.Fill(dt);
            MessageBox.Show("Successfully Hired.");
            dt.AcceptChanges();

            con.Close();

            txtFname.Clear();
            txtLName.Clear();
            txtSalary.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtHireDate.Clear();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            cmbManager.SelectedIndex = 0;
        }
    }
}
