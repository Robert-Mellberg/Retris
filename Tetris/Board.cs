using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
