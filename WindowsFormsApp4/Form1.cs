using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        snake mySnake;
        direction myDirection;
        PictureBox[] pb_snakeParts;
        bool anyMeal = false;
        Random random = new Random();
        PictureBox pb_meal = new PictureBox();
        int score = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            New_Game();
        }
        private void New_Game()
        {
            mySnake = new snake();
            myDirection = new direction(-10, 0);
            pb_snakeParts = new PictureBox[0];
            for (int i = 0; i < 3; i++)
            {
                Array.Resize(ref pb_snakeParts, pb_snakeParts.Length + 1);
                pb_snakeParts[i] = pb_add();
            }
            timer1.Start();
            button1.Enabled = false;
        }
        private PictureBox pb_add()
        {
            PictureBox pb = new PictureBox();
            pb.Size = new Size(10, 10);
            pb.BackColor = Color.White;
            pb.Location = mySnake.GetPosition(pb_snakeParts.Length-1);
            panel1.Controls.Add(pb);
            return pb;
        }
        private void pb_update()
        {
            for (int i = 0; i < pb_snakeParts.Length; i++)
            {
                pb_snakeParts[i].Location = mySnake.GetPosition(i);
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                if (myDirection._y != 10)
                {
                    myDirection = new direction(0, -10);
                }
        }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                if (myDirection._y != -10)
                {
                    myDirection = new direction(0, 10);
                }
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                if (myDirection._x != 10)
                {
                    myDirection = new direction(-10, 0);
                }
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                if (myDirection._x != -10)
                {
                    myDirection = new direction(10, 0);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "SCORE : " + score.ToString();
            mySnake.Move(myDirection);
            pb_update();
            newMeal();
            Did_Snake_Eat_Meal();
            Snake_Hit_Oneself();
            Snake_Hit_Walls();
            
        }
        public void newMeal()
        {
            if (!anyMeal)
            {

                PictureBox pb = new PictureBox();
                pb.BackColor = Color.Red;
                pb.Size = new Size(10, 10);
                pb.Location = new Point(random.Next(panel1.Width / 10) * 10, random.Next(panel1.Height / 10) * 10);
                pb_meal = pb;
                anyMeal = true;
                panel1.Controls.Add(pb);
            }
        }
        public void Did_Snake_Eat_Meal()
        {
            if (mySnake.GetPosition(0) == pb_meal.Location)
            {
                score += 10;
                mySnake.GetBigger();
                Array.Resize(ref pb_snakeParts, pb_snakeParts.Length + 1);
                pb_snakeParts[pb_snakeParts.Length - 1] = pb_add();
                anyMeal = false;
                panel1.Controls.Remove(pb_meal);
            }
        }
        public void Snake_Hit_Oneself()
        {
            for (int i = 1; i < mySnake.Snake_Size; i++)
            {
                if (mySnake.GetPosition(0) == mySnake.GetPosition(i))
                {
                    Game_Over();
                }
            }
        }
        public void Snake_Hit_Walls()
        {
            Point p = mySnake.GetPosition(0);
            if (p.X < 0 || p.X > panel1.Width - 10 || p.Y < 0 || p.Y > panel1.Height - 10)
            {
                Game_Over();
            }
        }
        public void Game_Over()
        {
            timer1.Stop();
            MessageBox.Show("GAME OVER!" + "\nYOUR SCORE:" + score.ToString());
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            New_Game();
            anyMeal=false;
        }
    }
}
