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

namespace Game
{
    public partial class ResultTable : Form
    {
        string connectionString = @"Data Source=DESKTOP-VMBA36A\SQLEXPRESS;Initial Catalog = 480; Integrated Security = True;";
        public ResultTable()
        {
            InitializeComponent();
            this.CenterToScreen();
            using(SqlConnection sqlCon = new SqlConnection(connectionString)) 
            { 
                sqlCon.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.records", sqlCon);  
                DataTable dtb = new DataTable();
                adapter.Fill(dtb);
                dgResults.DataSource = dtb;
                dgResults.Sort(this.dgResults.Columns["score"], ListSortDirection.Descending);
                sqlCon.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 forma = new Form1();
            this.Close();
            forma.Show();
        }
    }
}
