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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Piece p;
        Board board;

        private void timer1_Tick(object sender, EventArgs e)
        {
            dropPiece();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            p = new Piece(this);
            board = new Board();
            p.generateNewPiece(board);
            timer1.Start();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        bool swappedThisTurn = false;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                p.forceDrop(board);
            }
            else if(e.KeyCode == Keys.Left)
            {
                p.moveLeft(board);
            }
            else if (e.KeyCode == Keys.Right)
            {
                p.moveRight(board);
            }
            else if (e.KeyCode == Keys.Up)
            {
                p.rotate(board);
            }
            else if(e.KeyCode == Keys.Down)
            {
                dropPiece();
            }
            else if(e.KeyCode == Keys.C)
            {
                if (!swappedThisTurn)
                {
                    swappedThisTurn = true;
                    p.swapPiece();
                }
            }
        }

        private void dropPiece()
        {
            if (p.getYDistanceUntilCollision(board) == 1)
            {
                p.generateNewPiece(board);
                swappedThisTurn = false;
                board.clearLines();
            }
            else
            {
                p.dropDown();
            }
        }
    }
}
