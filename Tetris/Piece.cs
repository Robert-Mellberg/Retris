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
 * The piece class handles all logic connected to the four pieces: current piece, next piece, swapped piece and hypo piece.
 * 
 * Logic includes swapping pieces, generating random pieces, rotating pieces and droping pieces.
 * A piece is represented by between 1-9 squares (see Square.cs)
 * The piece class uses the current board (see Board.cs) as board reference.
 * 
 */
namespace Tetris
{

    class Piece
    {
        Vector2[,] rotateVectors = new Vector2[,] {
        {new Vector2(2, 0), new Vector2(1, 1), new Vector2(0, 2) },
        {new Vector2(1, -1), new Vector2(0, 0), new Vector2(-1, 1) },
        {new Vector2(0, -2), new Vector2(-1, -1), new Vector2(-2, 0) } };

        List<Square> currentPiece;
        List<Square> nextPiece;
        List<Square> switchPiece;
        List<Square> hypoBrick;
        Form1 form;
        Random r;
        public Piece(Form1 form)
        {
            currentPiece = new List<Square>();
            hypoBrick = new List<Square>();
            this.form = form;
            r = new Random();
            generateNextPiece();
            for(int i = 0; i < 9; i++)
            {
                hypoBrick.Add(new Square(-10, -10, Color.Gray, form, null));
            }
        }

        public void clearPiece()
        {
            clearSquareLists(currentPiece);
            clearSquareLists(nextPiece);
            clearSquareLists(switchPiece);
            clearSquareLists(hypoBrick);

        }
        private void clearSquareLists(List<Square> squares)
        {
            if(squares == null)
            {
                return;
            }
            foreach (Square sq in squares)
            {
                sq.removeSquare();
            }
        }

        public void generateNewPiece(Board b)
        {
            foreach(Square sq in currentPiece)
            {
                b.insertSquare(sq);
            }
            currentPiece = nextPiece;
            foreach (Square sq in currentPiece)
            {
                sq.setRelativePosition(4, 0);
            }
            generateNextPiece();



        }

        public void swapPiece()
        {
            List<Square> temp = switchPiece;
            switchPiece = currentPiece;
            if (temp == null)
            {
                currentPiece = nextPiece;
                generateNextPiece();
            }
            else
            {
                currentPiece = temp;
            }
            foreach (Square sq in currentPiece)
            {
                sq.setRelativePosition(4, 0);
            }
            foreach (Square sq in switchPiece)
            {
                sq.setRelativePosition(-10, 3);
            }
        }

        private void generateNextPiece()
        {
            nextPiece = generateRandomPiece();
            foreach(Square sq in nextPiece)
            {
                sq.setRelativePosition(20, 3);
            }
        }

        private List<Square> generateRandomPiece()
        {
            List<Square> squares = new List<Square>();
            int[,] piece = new int[,] {
                { -1, -1, -1 },
                { -1, -1, -1 } ,
                { -1, -1, -1 }  };

            Stack<Vector2> stack = new Stack<Vector2>();
            stack.Push(new Vector2(r.Next(3), r.Next(3)));
            bool firstSquare = true;
            while (stack.Count != 0)
            {
                Vector2 pos = stack.Pop();
                if (piece[pos.y, pos.x] != -1)
                {
                    continue;
                }

                piece[pos.y, pos.x] = r.Next(2);
                if (firstSquare)
                {
                    firstSquare = false;
                    piece[pos.y, pos.x] = 1;
                }
                if (piece[pos.y, pos.x] == 0)
                {
                    continue;
                }

                List<Vector2> neighbours = new List<Vector2>();
                neighbours.Add(new Vector2(pos.x - 1, pos.y));
                neighbours.Add(new Vector2(pos.x + 1, pos.y));
                neighbours.Add(new Vector2(pos.x, pos.y - 1));
                neighbours.Add(new Vector2(pos.x, pos.y + 1));

                foreach (Vector2 neighbour in neighbours)
                {
                    if (neighbour.x < 0 || neighbour.x > 2 || neighbour.y < 0 || neighbour.y > 2 || piece[neighbour.y, neighbour.x] != -1)
                    {
                        continue;
                    }
                    stack.Push(neighbour);
                }

            }

            Color color = calculatePieceColor(piece);
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (piece[y, x] == 1)
                    {
                        squares.Add(new Square(x, y, color, form, rotateVectors[y, x]));
                        squares.Last().BringToFront();
                    }
                }
            }
            return squares;
        }

        public void dropDown()
        {
            foreach(Square sq in currentPiece)
            {
                sq.dropDown();
            }
        }

        public int getYDistanceUntilCollision(Board b)
        {
            int distance = 999;
            foreach(Square sq in currentPiece)
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
            foreach(Square sq in currentPiece)
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
            foreach (Square sq in currentPiece)
            {
                if (!sq.canRotate(b))
                {
                    return;
                }
            }

            foreach (Square sq in currentPiece)
            {
                sq.rotate();
            }
        }

        private void moveXDirection(Board b, int increment)
        {
            foreach (Square sq in currentPiece)
            {
                if (!b.spaceIsEmpty(sq.getX() + increment, sq.getY()))
                {
                    return;
                }
            }
            foreach (Square sq in currentPiece)
            {
                sq.increaseX(increment);
            }
        }

        public void updateHypoBrick(Board b)
        {
            foreach(Square sq in hypoBrick)
            {
                sq.setRelativePosition(0, 0);
            }
            int index = 0;
            int yDistance = getYDistanceUntilCollision(b);

            foreach(Square sq in currentPiece)
            {
                hypoBrick[index].setPosition(sq.getX(), sq.getY()+yDistance-1);
                index++;
            }
        }

        public bool isColliding(Board b)
        {
            foreach(Square sq in currentPiece)
            {
                if (sq.isColliding(b))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
