using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * A square is a component of a piece and is referenced by the Piece class.
 * This class handles the graphics connected with the square by using the inherited class PictureBox
 * It has methods for setting positions, changing positions, checking if it can rotate and checking if it is colliding with another square. 
 * 
 * 
 */
namespace Tetris
{
    class Square : PictureBox
    {
        const int SIZE = 50;
        const int XOFFSET = 600;
        int x = 0;
        int y = 0;
        int startX;
        int startY;

        Vector2 startRotateVector;
        Vector2 rotateVector;
        Form1 form;

        public Square(int x, int y, Color color, Form1 form, Vector2 rotateVector)
        {
            BackColor = color;
            this.x = x;
            this.y = y;
            startX = x;
            startY = y;
            this.form = form;
            Location = new Point(x*SIZE+ XOFFSET, y*SIZE+50);
            Size = new Size(SIZE-2, SIZE-2);
            this.rotateVector = rotateVector;
            startRotateVector = rotateVector;
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
            Location = new Point(x * SIZE + XOFFSET, y * SIZE + 50);
        }

        public void increaseX(int increment)
        {
            x += increment;
            Location = new Point(x * SIZE + XOFFSET, y * SIZE + 50);
        }

        public void setRelativePosition(int x, int y)
        {
            this.x = x+startX;
            this.y = y+startY;
            Location = new Point(this.x * SIZE + XOFFSET, this.y * SIZE + 50);
            rotateVector = startRotateVector;
        }

        public void setPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
            Location = new Point(this.x * SIZE + XOFFSET, this.y * SIZE + 50);
            rotateVector = startRotateVector;
        }

        public bool canRotate(Board b)
        {
            return b.spaceIsEmpty(x + rotateVector.x, y + rotateVector.y);
        }

        public bool isColliding(Board b)
        {
            return !b.spaceIsEmpty(x, y);
        }

        public void rotate()
        {
            increaseX(rotateVector.x);
            increaseY(rotateVector.y);

            rotateVector = new Vector2(-rotateVector.y, rotateVector.x);
        }

        public void removeSquare()
        {
            form.Controls.Remove(this);
            
        }
    }
}
