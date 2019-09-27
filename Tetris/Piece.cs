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

    class Piece
    {
        Vector2[,] rotateVectors = new Vector2[,] {
        {new Vector2(2, 0), new Vector2(1, 1), new Vector2(0, 2) },
        {new Vector2(1, -1), new Vector2(0, 0), new Vector2(-1, 1) },
        {new Vector2(0, -2), new Vector2(-1, -1), new Vector2(-2, 0) } };

        List<Square> squares;
        Form1 form;
        Random r;
        public Piece(Form1 form)
        {
            squares = new List<Square>();
            this.form = form;
            r = new Random();
        }

        

        public void generateNewPiece(Board b)
        {
            foreach(Square sq in squares)
            {
                b.insertSquare(sq);
            }
            squares.Clear();

            int[,] piece = new int[,] {
                { -1, -1, -1 },
                { -1, -1, -1 } ,
                { -1, -1, -1 }  };

            Stack<Vector2> stack = new Stack<Vector2>();
            stack.Push(new Vector2(r.Next(3), r.Next(3)));
            bool firstSquare = true;
            while(stack.Count != 0)
            {
                Vector2 pos = stack.Pop();
                if(piece[pos.y, pos.x] != -1)
                {
                    continue;
                }

                piece[pos.y, pos.x] = r.Next(2);
                if (firstSquare)
                {
                    firstSquare = false;
                    piece[pos.y, pos.x] = 1;
                }
                if(piece[pos.y, pos.x] == 0)
                {
                    continue;
                }

                List<Vector2> neighbours = new List<Vector2>();
                neighbours.Add(new Vector2(pos.x - 1, pos.y));
                neighbours.Add(new Vector2(pos.x + 1, pos.y));
                neighbours.Add(new Vector2(pos.x, pos.y - 1));
                neighbours.Add(new Vector2(pos.x, pos.y + 1));

                foreach(Vector2 neighbour in neighbours)
                {
                    if(neighbour.x < 0 || neighbour.x > 2 || neighbour.y < 0 || neighbour.y > 2 || piece[neighbour.y, neighbour.x] != -1)
                    {
                        continue;
                    }
                    stack.Push(neighbour);
                }

            }

            Color color = calculatePieceColor(piece);
            for(int y = 0; y < 3; y++)
            {
                for(int x = 0; x < 3; x++)
                {
                    if(piece[y, x] == 1)
                    {
                        squares.Add(new Square(x, y, color, form, rotateVectors[y, x]));
                    }
                }
            }

        }

        public void dropDown()
        {
            foreach(Square sq in squares)
            {
                sq.dropDown();
            }
        }

        public int getYDistanceUntilCollision(Board b)
        {
            int distance = 999;
            foreach(Square sq in squares)
            {
                int x = sq.getX();
                int y = sq.getY();

                int tempDist = b.getHighestSquareYPosition(x, y) - y;
                distance = Math.Min(distance, tempDist);
            }

            return distance;
        }

        public void forceDrop(Board b)
        {
            int minDistance = getYDistanceUntilCollision(b);
            foreach(Square sq in squares)
            {
                sq.increaseY(minDistance-1);
            }
        }

        private Color calculatePieceColor(int[,] piece)
        {
            int red = 0;
            int green = 0;
            int blue = 0;
            for (int x = 0; x < 3; x++)
            {
                

                if(piece[0, x] == 1)
                {
                    red += (x + 1) * 42;
                }
                if (piece[1, x] == 1)
                {
                    green += (x + 1) * 42;
                }
                if (piece[2, x] == 1)
                {
                    blue += (x + 1) * 42;
                }
            }
            return Color.FromArgb(red, green , blue);
        }

        public void moveLeft(Board b)
        {
            moveXDirection(b, -1);
        }

        public void moveRight(Board b)
        {
            moveXDirection(b, 1);
        }

        public void rotate(Board b)
        {
            foreach (Square sq in squares)
            {
                if (!sq.canRotate(b))
                {
                    return;
                }
            }

            foreach (Square sq in squares)
            {
                sq.rotate();
            }
        }

        private void moveXDirection(Board b, int increment)
        {
            foreach (Square sq in squares)
            {
                if (!b.spaceIsEmpty(sq.getX() + increment, sq.getY()))
                {
                    return;
                }
            }
            foreach (Square sq in squares)
            {
                sq.increaseX(increment);
            }
        }


    }
}
