using System;
using System.Collections.Generic;
using System.Text;

namespace SirTetLogic
{
    /// <summary>
    /// The main O_Block class
    /// Contains all method for performing O_Block function
    /// </summary>
    class O_Block : Block
    {
        Point middlePoint;
        Point[] restPoints;
        Point[] allPoints;
        int rotationPose = 0;
        /// <value>Gets middle point of block</value>
        protected override Point MiddlePoint{get => middlePoint;}
        /// <value>Gets table contains of rest of points excluding middle point</value>
        protected override Point[] RestPoints{get => restPoints;}
        /// <value>Gets index of actual rotation position</value>
        protected override int RotationPose{get => rotationPose;}
        /// <summary>
        /// Initialize a new instance of the <see cref="O_Block"/> class
        /// </summary>
        /// <param name="middleX">Index of column</param>
        /// <param name="middleY">Index of row</param>
        public O_Block(int middleX, int middleY)
        {
            middlePoint = new Point(middleX, middleY);
            restPoints = new Point[3] { new Point(middleX, middleY - 1), new Point(middleX + 1, middleY - 1), new Point(middleX + 1, middleY)};
            allPoints = new Point[4] { middlePoint, restPoints[0], restPoints[1], restPoints[2]};
        }
        /// <summary>
        /// Method responsible for rotation of blocks
        /// </summary>
        public override void Rotate()
        {
            //W kontekście tego klocka ta metoda nie ma sensu
        }
        /// <summary>
        /// Method responsible for checking if the block is out of the grid x after rotation
        /// </summary>
        /// <param name="gridX">Index of column</param>
        /// <returns>Return information about if the block after rotation is out off the grid x</returns>
        public override bool IfBlockOutOfGridOnRotate(int gridX)
        {
            //W kontekście tego klocka ta metoda nie ma sensu
            return false;
        }
        /// <summary>
        /// Method responsible for checking if the block collide with used space after rotation
        /// </summary>
        /// <param name="HardLayer">Two dimensional pool Table that contains of information about state of the block</param>
        /// <returns>Return information about if the block after rotation collide with used space</returns>
        public override bool IfBlockOverrideOnRotate(bool[,] HardLayer)
        {
            //W kontekście tego klocka ta metoda nie ma sensu
            return false;
        }
        /// <summary>
        /// Method responsible for getting active block type
        /// </summary>
        /// <returns>Return information about the block type</returns>
        public override string GetBlockType() => "O_Block";

    }
}
