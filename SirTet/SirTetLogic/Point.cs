using System;
using System.Collections.Generic;
using System.Text;

namespace SirTetLogic
{
    public class Point
    {
        int x;
        int y;

        public Point(int X, int Y)
        {
            x = X;
            y = Y;
        }

        public int[] Get()
        {
            return new int[2] { x, y };
        }

        public void Set(int X, int Y)
        {
            x = X;
            y = Y;
        }
    }
}
