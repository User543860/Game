using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Dungeon dung = new Dungeon();
            this.Hide();
            dung.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            ResultTable resul = new ResultTable();
            this.Hide();
            resul.Show();
        }
    }
}
