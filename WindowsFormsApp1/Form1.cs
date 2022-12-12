using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            OracleConnection con = new OracleConnection("User Id=COMP214_F22_ER_56;Password=password;Data Source=199.212.26.208:1521/SQLD");
            con.Open();
            Console.WriteLine("Connected to the oracle db");

        }

        private void job_btn_Click(object sender, EventArgs e)
        {
            CreateJobForm createJob = new CreateJobForm();
            createJob.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            employeeHiring hiringPage = new employeeHiring();
            hiringPage.Show();
        }



        //using connection string attributes to connect to Oracle Database

        //  connection.Open();
        // Console.WriteLine("Connected to Oracle" + con.ServerVersion);
        //

    }
}
