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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Game
{
    public partial class YourScore : Form
    {
        private string score;
        public YourScore(string score)
        {
            InitializeComponent();
            this.CenterToScreen();
            this.textScore.Text = score;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string connectionString = null;
            string sql = null;
            connectionString = @"Data Source=DESKTOP-VMBA36A\SQLEXPRESS;Initial Catalog = 480; Integrated Security = True;";
            sql = "insert into records ([score], [nickname]) values(@score,@nick)";
            using(SqlConnection cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.Parameters.Add("@score", SqlDbType.NVarChar).Value = textScore.Text;
                        cmd.Parameters.Add("@nick", SqlDbType.NVarChar).Value = textName.Text;
                        int rowAdded = cmd.ExecuteNonQuery();
                        if (rowAdded > 0)
                            MessageBox.Show("Result recorded");
                        else
                            MessageBox.Show("No result recorded");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something wrong:" + ex.Message);
                }
                Form1 forma = new Form1();
                this.Close();
                forma.Show();
            }
        }
    }


}
