using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp4
{
    class snake
    {
        snake_parts[] snake_part;
        int snake_size;
        direction direct;
        public snake()
        {
            snake_part = new snake_parts[3];
            snake_size = 3;
            snake_part[0] = new snake_parts(150, 150);
            snake_part[1] = new snake_parts(160, 150);
            snake_part[2] = new snake_parts(170, 150);
        }
        public void Move(direction direction)
        {
            direct = direction;
            if (direction._x == 0 && direction._y == 0)
            {

            }
            else
            {
                for (int i = snake_part.Length - 1; i > 0; i--)
                {
                    snake_part[i] = new snake_parts(snake_part[i - 1].x_, snake_part[i - 1].y_);
                }
                snake_part[0] = new snake_parts(snake_part[0].x_ + direction._x, snake_part[0].y_ + direction._y);
            }
        }
        public void GetBigger()
        {
            Array.Resize(ref snake_part, snake_part.Length + 1);
            snake_part[snake_part.Length - 1] = new snake_parts(snake_part[snake_part.Length - 2].x_ - direct._x, snake_part[snake_part.Length - 2].y_ - direct._y); ; //will snake separete 2 parts and move 
            snake_size++;
        }
        public Point GetPosition(int number)
        {
            return new Point(snake_part[number].x_, snake_part[number].y_);
        }
        public int Snake_Size
        {
            get
            {
                return snake_size;
            }
        }
    }
    class snake_parts
    {
        public int x_; 
        public int y_;
        public readonly int size_x;
        public readonly int size_y;
        public snake_parts(int x, int y)
        {
            x_ = x;
            y_ = y;
            size_x = 10;
            size_y = 10;

        }
    }
    class direction
    {
        public readonly int _x;
        public readonly int _y;
        public direction(int x,int y)
        {
            _x = x;
            _y = y;
        }
    }
}
