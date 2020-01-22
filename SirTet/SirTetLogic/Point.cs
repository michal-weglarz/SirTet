using System;
using System.Collections.Generic;
using System.Text;

namespace SirTetLogic
{
    /// <summary>
    /// The main Point class
    /// Contains all method for performing Point function
    /// </summary>
    public class Point
    {
        int x;
        int y;
        /// <summary>
        /// Initialize a new instance of the <see cref="Point"/> class
        /// </summary>
        /// <param name="X">Index of column</param>
        /// <param name="Y">Index of row</param>
        public Point(int X, int Y)
        {
            x = X;
            y = Y;
        }
        /// <summary>
        /// Method responsible for getting coordinates of point
        /// </summary>
        /// <returns>Returns table containing coordinates of point</returns>
        public int[] Get()
        {
            return new int[2] { x, y };
        }
        /// <summary>
        /// Method responsible for setting coordinates of point
        /// </summary>
        /// <param name="X">Index of column</param>
        /// <param name="Y">Index of row</param>
        public void Set(int X, int Y)
        {
            x = X;
            y = Y;
        }
    }
}
