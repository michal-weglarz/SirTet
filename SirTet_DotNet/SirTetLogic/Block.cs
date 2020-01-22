using System;
using System.Collections.Generic;
using System.Text;

namespace SirTetLogic
{
    /// <summary>
    /// The main Block abstract class
    /// Contains all method for performing Block function
    /// </summary>
    public abstract class Block
    {
        /// <value>Gets middle point of block</value>
        protected abstract Point MiddlePoint{get;}
        /// <value>Gets table contains of rest of points excluding middle point</value>
        protected abstract Point[] RestPoints{get;}
        /// <value>Gets index of actual rotation position</value>
        protected abstract int RotationPose{get;}              
        /// <summary>
        /// Method responsible for blocks moving one row lower
        /// </summary>
        public void Fall()
        {
            foreach(Point point in GetBlock())            
                point.Set(point.Get()[0], point.Get()[1] + 1);            
        }
        /// <summary>
        /// Method responsible for moving blocks to left and right
        /// </summary>
        /// <param name="toLeft">Bool contains information if key to move left is pressed</param>
        public void Move(bool toLeft)
        {
            if(toLeft)
                foreach(Point point in GetBlock())
                    point.Set(point.Get()[0] - 1, point.Get()[1]);
            else
                foreach(Point point in GetBlock())
                    point.Set(point.Get()[0] + 1, point.Get()[1]);
        }
        /// <summary>
        /// Abstract method responsible for rotation of blocks
        /// </summary>
        public abstract void Rotate();
        /// <summary>
        /// Method responsible for checking if blocks touch hard layer below
        /// </summary>
        /// <param name="HardLayer">Two dimensional pool Table that contains of information about state of the block</param>
        /// <returns>Return information about is the block below used or not</returns>
        public bool IfTouchHardLayer(bool[,] HardLayer)
        {
            foreach(Point point in GetBlock())
            {
                if(HardLayer[point.Get()[0], point.Get()[1] + 1] == true)
                    return true;                                         
            }
            return false;
        }
        /// <summary>
        /// Abstract method responsible for checking if the block is out of the grid x after rotation
        /// </summary>
        /// <param name="gridX">Number of columns</param>
        /// <returns>Return information about if the block after rotation is out off the grid x</returns>
        public abstract bool IfBlockOutOfGridOnRotate(int gridX);
        /// <summary>
        /// Abstract method responsible for checking if the block collide with used space after rotation
        /// </summary>
        /// <param name="HardLayer">Two dimensional pool Table that contains of information about state of the block</param>
        /// <returns>Return information about if the block after rotation collide with used space</returns>
        public abstract bool IfBlockOverrideOnRotate(bool[,] HardLayer);
        /// <summary>
        /// Method responsible for checking if the block is out of the grid x after moving left or right
        /// </summary>
        /// <param name="gridX">Number of columns</param>
        /// <param name="toLeft">Bool contains information if key to move left is pressed</param>
        /// <returns>Return information about if the block is out of grid x after moving left or right</returns>
        public bool IfOutOfGrid(int gridX, bool toLeft)
        {
            foreach(Point point in GetBlock())
            {
                if(toLeft)
                {
                    if(point.Get()[0] <= 0)
                        return true;
                }                    
                else
                    if(point.Get()[0] >= gridX - 1)
                        return true;             
            }
            return false;
        }
        /// <summary>
        /// Method responsible for checking if block collide with used space after moving left or right
        /// </summary>
        /// <param name="HardLayer">Two dimensional pool Table that contains of information about state of the block</param>
        /// <param name="toLeft">Bool contains information if key to move left is pressed</param>
        /// <returns>Return information about if the block collide with used space after moving left or right</returns>
        public bool IfBlockOverride(bool[,] HardLayer, bool toLeft)
        {
            foreach(Point point in GetBlock())
            {
                if(toLeft)
                {
                    if(HardLayer[point.Get()[0] - 1, point.Get()[1]])
                        return true;
                }
                else
                    if(HardLayer[point.Get()[0] + 1, point.Get()[1]])
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Method responsible for getting a table consisting of single points out of the whole block
        /// </summary>
        /// <returns></returns>
        public Point[] GetBlock() => new Point[4] { MiddlePoint, RestPoints[0], RestPoints[1], RestPoints[2] };
        /// <summary>
        /// Method responsible for getting index of used rotation pose
        /// </summary>
        public int GetRotationPose => RotationPose;
        /// <summary>
        /// Abstract method responsible for getting active block type
        /// </summary>
        /// <returns>Return information about the block type</returns>
        public  abstract string GetBlockType();
    }    
}
