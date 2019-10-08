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
 * Main class for the tetris game.
 * The board and the game pieces are referenced from this class.
 * 
 * This class handles checking user input and calling the appropriate method for the piece based on the input.
 * 
 * 
 */
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
        int score = 0;
        bool gameActive = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            dropPiece();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            p = new Piece(this);
            board = new Board();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            this.ActiveControl = label1;
        }

        bool swappedThisTurn = false;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!gameActive)
            {
                return;
            }
            if (e.KeyCode == Keys.Space)
            {
                p.forceDrop(board);
                dropPiece();
                return;
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
                return;
            }
            else if(e.KeyCode == Keys.C)
            {
                if (!swappedThisTurn)
                {
                    swappedThisTurn = true;
                    p.swapPiece();
                }
            }
            p.updateHypoBrick(board);
            
        }

        private void dropPiece()
        {
            if (p.getYDistanceUntilCollision(board) == 1)
            {
                p.generateNewPiece(board);
                swappedThisTurn = false;
                score += board.clearLines();
                label1.Text = "Score: " + score;
                timer1.Interval = 20000 / (20+score);

                if (p.isColliding(board))
                {
                    gameActive = false;
                    timer1.Stop();
                }

            }
            else
            {
                p.dropDown();
            }
            p.updateHypoBrick(board);
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            board.clearBoard();
            board = new Board();
            p.clearPiece();
            p = new Piece(this);
            p.generateNewPiece(board);
            this.ActiveControl = label1;
            gameActive = true;
            timer1.Start();
            score = 0;
            label1.Text = "Score: " + score;
        }
    }
}
