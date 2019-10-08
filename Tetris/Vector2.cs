using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * A simple class for a 2 dimensional vector
 * The vector can set two values and return those values.
 * 
 */
namespace Tetris
{
    public class Vector2
    {
        public int x { get; set; }
        public int y { get; set; }

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
