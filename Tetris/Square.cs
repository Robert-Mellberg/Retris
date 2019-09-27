using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    class Square : PictureBox
    {
        const int SIZE = 25;
        int x = 0;
        int y = 0;
        Vector2 rotateVector;

        public Square(int x, int y, Color color, Form1 form, Vector2 rotateVector)
        {
            BackColor = color;
            this.x = x;
            this.y = y;
            Location = new Point(x*SIZE+50, y*SIZE+50);
            Size = new Size(SIZE-2, SIZE-2);
            this.rotateVector = rotateVector;
            form.Controls.Add(this);
        }

        public void dropDown()
        {
            increaseY(1);
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public void increaseY(int increment)
        {
            y += increment;
            Location = new Point(x * SIZE + 50, y * SIZE + 50);
        }

        public void increaseX(int increment)
        {
            x += increment;
            Location = new Point(x * SIZE + 50, y * SIZE + 50);
        }

        public bool canRotate(Board b)
        {
            return b.spaceIsEmpty(x + rotateVector.x, y + rotateVector.y);
        }

        public void rotate()
        {
            increaseX(rotateVector.x);
            increaseY(rotateVector.y);

            rotateVector = new Vector2(-rotateVector.y, rotateVector.x);
        }
    }
}
