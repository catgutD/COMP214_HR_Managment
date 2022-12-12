using Oracle.ManagedDataAccess.Client;
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

namespace WindowsFormsApp1
{
    public partial class CreateJobForm : Form
    {

        
        public CreateJobForm()
        {
            InitializeComponent();
            OracleConnection con = new OracleConnection("User Id=COMP214_F22_ER_56;Password=password;Data Source=199.212.26.208:1521/SQLD");
            con.Open();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        //load the datagrid
        private void CreateJobForm_Load(object sender, EventArgs e)
        {
           
            OracleConnection con = new OracleConnection("User Id=COMP214_F22_ER_56;Password=password;Data Source=199.212.26.208:1521/SQLD");
            con.Open();
            /*
             OracleCommand cmd = new OracleCommand("SELECT * FROM hr_jobs", con);
             OracleDataReader reader = cmd.ExecuteReader();
             DataTable dt = new DataTable();
             dt.Load(reader);
             dataGridView1.DataSource = dt;
            
            con.Close();

            dataGridView1.Refresh();*/
            OracleDataAdapter AdapterSelect = new OracleDataAdapter("SELECT * FROM hr_jobs", con);

            DataTable dt = new DataTable();

            AdapterSelect.Fill(dt);

            dataGridView1.DataSource = dt.DefaultView;
            dataGridView1.Refresh();





        }

      


        //create new job 
        private void button1_Click(object sender, EventArgs e)
        {

            OracleConnection con = new OracleConnection("User Id=COMP214_F22_ER_56;Password=password;Data Source=199.212.26.208:1521/SQLD");
            con.Open();
            //string sql = "EXECUTE new_job(888, 'test2', 300);commit";
            // Create a new OracleDataAdapter object and specify the stored procedure to execute
            OracleDataAdapter da = new OracleDataAdapter("new_job", con);
            // Set the command type to "StoredProcedure"
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("p_jobid", OracleDbType.Varchar2).Value = textBox1.Text;
            da.SelectCommand.Parameters.Add("p_title", OracleDbType.Varchar2).Value = textBox2.Text;
            da.SelectCommand.Parameters.Add("v_minsal", OracleDbType.Int32).Value =int.Parse(textBox3.Text);


            // Create a new DataTable object to hold the results of the stored procedure
            DataTable dt = new DataTable();
            // Execute the stored procedure and fill the DataTable with the results
            da.Fill(dt);
            MessageBox.Show("Record created successfully.");
            dt.AcceptChanges();
            dataGridView1.Refresh();
            //this.hR_JOBSTableAdapter.Fill(this.dataSet2.HR_JOBS);
            OracleCommand cmd = new OracleCommand("SELECT * FROM hr_jobs", con);
            OracleDataReader reader = cmd.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Load(reader);
            dataGridView1.DataSource = dt2;

            

            con.Close();
            dataGridView1.Refresh();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != " "&& textBox3.Text != null) { 
                textBox4.Text = (int.Parse(textBox3.Text) * 2).ToString();
            }
           
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = " ";
            textBox2.Text = " ";
            textBox3.Text = " ";
            textBox4.Text = " ";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox5.Text = " ";
            textBox6.Text = " ";
            textBox7.Text = " ";
            textBox8.Text = " ";
        }
        
        //update job records
        private void button3_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection("User Id=COMP214_F22_ER_56;Password=password;Data Source=199.212.26.208:1521/SQLD");
            con.Open();
            OracleDataAdapter da = new OracleDataAdapter("update_job", con);
            // Set the command type to "StoredProcedure"
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("p_jobid", OracleDbType.Varchar2).Value = textBox5.Text;
            da.SelectCommand.Parameters.Add("p_title", OracleDbType.Varchar2).Value = textBox6.Text;
            da.SelectCommand.Parameters.Add("p_minsal", OracleDbType.Int32).Value = int.Parse(textBox7.Text);
            da.SelectCommand.Parameters.Add("p_minsal", OracleDbType.Int32).Value = int.Parse(textBox8.Text);

            // Create a new DataTable object to hold the results of the stored procedure
            DataTable dt = new DataTable();
            // Execute the stored procedure and fill the DataTable with the results
            da.Fill(dt);
            MessageBox.Show("Record updated successfully.");
            dt.AcceptChanges();

            //this.hR_JOBSTableAdapter.Fill(this.dataSet2.HR_JOBS);
            OracleCommand cmd = new OracleCommand("SELECT * FROM hr_jobs", con);
            OracleDataReader reader = cmd.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Load(reader);
            dataGridView1.DataSource = dt2;
            con.Close();

        }
        //auto fill
        private void button5_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection("User Id=COMP214_F22_ER_56;Password=password;Data Source=199.212.26.208:1521/SQLD");
            con.Open();
            // Create an OracleCommand object for the search query
            string queryString = "SELECT * FROM hr_jobs WHERE job_id='"+textBox5.Text+"'";
            OracleCommand cmd = new OracleCommand(queryString, con);

            // Execute the search query and retrieve the results as a DataTable
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataTable searchResults = new DataTable();
            adapter.Fill(searchResults);

            // Close the connection
            con.Close();
            if (searchResults.Rows.Count > 0)
            {

                textBox6.Text = searchResults.Rows[0]["job_title"].ToString();
                textBox7.Text = searchResults.Rows[0]["min_salary"].ToString();
                textBox8.Text = searchResults.Rows[0]["max_salary"].ToString();
            }
            else {
                textBox5.Text = " ";
                textBox6.Text = " ";
                textBox7.Text = " ";
                textBox8.Text = " ";
                MessageBox.Show("no record!");
            }
           

        }
    }
}
