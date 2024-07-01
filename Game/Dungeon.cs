using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Game
{
    public partial class Dungeon : Form
    {
        public Dungeon()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        DataTable table = new DataTable();
        int _x, _y;
        double HealthBar = 5;
        bool LastStand = false;
        bool SnakeEyes = false;
        int score = 0;

        private void buttonSurrender_Click(object sender, EventArgs e)
        {
            YourScore forma = new YourScore(textScore.Text);
            forma.Show();
        }


        private void Dungeon_Load(object sender, EventArgs e)
        {
            int _size = 20;
            for(int i = 0; i < _size+1; i++)
            {
                table.Columns.Add(System.Convert.ToString(i), typeof(char));
            }
            for (int i = 0; i < _size+1; i++)//generates walls
            {
               table.Rows.Add('#','#','#','#','#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#');
            }

            Random rnd = new Random();//generates empty space
            int room1x = rnd.Next(1, 10);   int room1y = rnd.Next(1, 10);
            for (int i = room1x - 2; i <= room1x + 2; i++)
            {
                for (int j = room1y - 2; j <= room1y + 2; j++)
                {
                    if(i > 0 && j >0 && i < _size - 1 && j < _size - 1)
                        table.Rows[j].SetField(i, " ");
                }
            }

            int room2y = rnd.Next(11, 20);

            for (int i = room1x - 2; i <= room1x + 2; i++)
            {
                for (int j = room2y - 2; j <= room2y + 2; j++)
                {
                    if (i > 0 && j > 0 && i < _size - 1 && j < _size - 1)
                        table.Rows[j].SetField(i, " ");
                }
            }
            for (int i = room1y; i < room2y; i++)//connects rooms
                table.Rows[i].SetField(room1x, " ");

            int room2x = rnd.Next(11, 20);
            for (int i = room2x - 2; i <= room2x + 2; i++)
            {
                for (int j = room2y - 2; j <= room2y + 2; j++)
                {
                    if (i > 0 && j > 0 && i < _size - 1 && j < _size - 1)
                        table.Rows[j].SetField(i, " ");
                }
            }
            for (int i = room1x; i < room2x; i++)
                table.Rows[room2y].SetField(i, " ");

            _x = room1x; _y = room1y;//place for player
            table.Rows[_y].SetField(_x, "W");
            table.Rows[room2y].SetField(room1x, "R");
            table.Rows[room2y].SetField(room2x, "S");
            //table.Rows[i].SetField(columnIndex, value);

            dataGridView1.DataSource = table;
            dataGridView1.RowHeadersWidth = 15;
            dataGridView1.ColumnHeadersHeight = 15;
            dataGridView1.RowTemplate.Height = 15;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;


            this.KeyPreview = true;
            }


        private void _action(char direction)
        {
            HealthBar += 0.1;
            int xshift = 0;
            int yshift = 0;
            if(direction == 'd')
                xshift = 1;

            if (direction == 'w')
                yshift = -1;

            if (direction == 'a')
                xshift = -1;

            if (direction == 's')
                yshift = 1;

            if (System.Convert.ToString(table.Rows[_y+yshift][_x + xshift]) == " ")
            {
                table.Rows[_y].SetField(_x, " ");
                table.Rows[_y + yshift].SetField(_x + xshift, "W");
                _x += xshift;   _y += yshift;
            } else if (System.Convert.ToString(table.Rows[_y + yshift][_x + xshift]) == "R")
            {
                HealthBar -= 3;
                check_health(HealthBar);
                table.Rows[_y + yshift].SetField(_x + xshift, " "); 
                LastStand = true;   score += 300;   textScore.Text = System.Convert.ToString(score);
                if (SnakeEyes == true)
                {
                    MessageBox.Show("You won");
                    YourScore forma = new YourScore(textScore.Text);
                    this.Close();
                    forma.Show();
                }
            }
            else if (System.Convert.ToString(table.Rows[_y + yshift][_x + xshift]) == "S")
            {
                HealthBar -= 8;
                check_health(HealthBar);
                table.Rows[_y + yshift].SetField(_x + xshift, " ");
                SnakeEyes = true;   score += 500;   textScore.Text = System.Convert.ToString(score);
                if (LastStand == true)
                {
                    MessageBox.Show("Game over");
                    YourScore forma = new YourScore(textScore.Text);
                    this.Close();
                    forma.Show();
                }
            }
            textHealth.Text = System.Convert.ToString(HealthBar) + "/10";
        }

        private void check_health(double hp)
        {
            if (HealthBar <= 0)
            {
                MessageBox.Show("You have been defeated");
                YourScore forma = new YourScore(textScore.Text);
                this.Close();
                forma.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _action('s');
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _action('a');
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _action('w');
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _action('d');
        }
    }
    }