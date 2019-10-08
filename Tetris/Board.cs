using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * The board class is where the board is represented as a 2 dimensional array of squares
 * 
 * It has methods for inserting a square, clearing full lines on the board and calculating the y position of the highest square in a column. 
 * 
 */
namespace Tetris
{
    class Board
    {

        Square[,] board = new Square[,] {
            {null, null, null, null, null, null, null, null, null, null },
            {null, null, null, null, null, null, null, null, null, null },
            {null, null, null, null, null, null, null, null, null, null },
            {null, null, null, null, null, null, null, null, null, null },
            {null, null, null, null, null, null, null, null, null, null },
            {null, null, null, null, null, null, null, null, null, null },
            {null, null, null, null, null, null, null, null, null, null },
            {null, null, null, null, null, null, null, null, null, null },
            {null, null, null, null, null, null, null, null, null, null },
            {null, null, null, null, null, null, null, null, null, null },
            {null, null, null, null, null, null, null, null, null, null },
            {null, null, null, null, null, null, null, null, null, null },
            {null, null, null, null, null, null, null, null, null, null },
            {null, null, null, null, null, null, null, null, null, null },
            {null, null, null, null, null, null, null, null, null, null } };


        public int getHighestSquareYPosition(int x, int maxHeight)
        {
            int maxY = getYSize();
            for(int yPos = maxHeight; yPos < maxY; yPos++)
            {
                if(board[yPos, x] != null)
                {
                    return yPos;
                }
            }
            
            return maxY;
        }

        public void insertSquare(Square sq)
        {
            board[sq.getY(), sq.getX()] = sq;
        }

        public bool spaceIsEmpty(int x, int y)
        {
            if(x < 0 || y < 0 || x >= getXSize() || y > getYSize())
            {
                return false;
            }

            return board[y, x] == null;
        }

        public int getYSize()
        {
            return board.GetLength(0);
        }

        public int getXSize()
        {
            return board.GetLength(1);
        }

        public int clearLines()
        {
            int amountOfClearedLines = 0;
            for(int y = 14; y >= 0; y--)
            {
                int countSquares = 0;
                for(int x = 0; x <= 9; x++)
                {
                    if(board[y, x] != null)
                    {
                        countSquares++;
                    }
                }

                if (countSquares == 10)
                {
                    amountOfClearedLines++;
                    for (int x = 0; x <= 9; x++)
                    {
                        board[y, x].removeSquare();
                        board[y, x] = null;
                    }
                }
                else
                {
                    if(amountOfClearedLines == 0)
                    {
                        continue;
                    }
                    for (int x = 0; x <= 9; x++)
                    {
                        //swap
                        board[y+amountOfClearedLines, x] = board[y, x];
                        if (board[y, x] != null)
                        {
                            board[y, x].increaseY(amountOfClearedLines);
                            board[y, x] = null;
                        }
                    }
                }
            }
            return amountOfClearedLines;
        }

        public void clearBoard()
        {
            for(int y = 0; y <= 14; y++)
            {
                for(int x = 0; x <= 9; x++)
                {
                    if (board[y, x] != null)
                    {
                        board[y, x].removeSquare();
                    }
                }
            }
        }
    }
}
