using System;
using System.Collections.Generic;
using System.Text;

namespace SirTetLogic
{
    /// <summary>
    /// The main Z_Block class
    /// Contains all method for performing Z_Block function
    /// </summary>
    class Z_Block : Block
    {
        Point middlePoint;
        Point[] restPoints;
        int rotationPose = 0;
        /// <value>Gets middle point of block</value>
        protected override Point MiddlePoint{get => middlePoint;}
        /// <value>Gets table contains of rest of points excluding middle point</value>
        protected override Point[] RestPoints{get => restPoints;}
        /// <value>Gets index of actual rotation position</value>
        protected override int RotationPose{get => rotationPose;}
        /// <summary>
        /// Initialize a new instance of the <see cref="Z_Block"/> class
        /// </summary>
        /// <param name="middleX">Index of column</param>
        /// <param name="middleY">Index of row</param>
        public Z_Block(int middleX, int middleY)
        {
            middlePoint = new Point(middleX, middleY);
            restPoints = new Point[3] { new Point(middleX - 1, middleY - 1), new Point(middleX, middleY - 1), new Point(middleX + 1, middleY)};
        }
        /// <summary>
        /// Method responsible for rotation of blocks
        /// </summary>
        public override void Rotate()
        {
            switch(rotationPose)
            {
                case 0:
                    restPoints[0] = new Point(restPoints[0].Get()[0] + 2, restPoints[0].Get()[1]);
                    restPoints[1] = new Point(restPoints[1].Get()[0] + 1, restPoints[1].Get()[1] - 1);
                    restPoints[2] = new Point(restPoints[2].Get()[0] - 1, restPoints[2].Get()[1] + 1);
                    rotationPose = 1;
                    break;
                case 1:
                    restPoints[0] = new Point(restPoints[0].Get()[0], restPoints[0].Get()[1] + 2);
                    restPoints[1] = new Point(restPoints[1].Get()[0] - 1, restPoints[1].Get()[1] + 1);
                    restPoints[2] = new Point(restPoints[2].Get()[0] - 1, restPoints[2].Get()[1] - 1);
                    rotationPose = 2;
                    break;
                case 2:
                    restPoints[0] = new Point(restPoints[0].Get()[0] - 2, restPoints[0].Get()[1]);
                    restPoints[1] = new Point(restPoints[1].Get()[0] - 1, restPoints[1].Get()[1] - 1);
                    restPoints[2] = new Point(restPoints[2].Get()[0] + 1, restPoints[2].Get()[1] - 1);
                    rotationPose = 3;
                    break;
                case 3:
                    restPoints[0] = new Point(restPoints[0].Get()[0], restPoints[0].Get()[1] - 2);
                    restPoints[1] = new Point(restPoints[1].Get()[0] + 1, restPoints[1].Get()[1] - 1);
                    restPoints[2] = new Point(restPoints[2].Get()[0] + 1, restPoints[2].Get()[1] + 1);
                    rotationPose = 0;
                    break;
            }
        }
        /// <summary>
        /// Method responsible for checking if the block is out of the grid x after rotation
        /// </summary>
        /// <param name="gridX">Index of column</param>
        /// <returns>Return information about if the block after rotation is out off the grid x</returns>
        public override bool IfBlockOutOfGridOnRotate(int gridX)
        {
            switch(rotationPose)
            {
                case 0:
                    if(restPoints[0].Get()[0] + 2 >= gridX - 1 || restPoints[1].Get()[0] + 1 >= gridX - 1 || restPoints[2].Get()[0] - 1 <= 0)
                        return true;
                    break;
                case 1:
                    if(restPoints[1].Get()[0] - 1 <= 0 || restPoints[2].Get()[0] - 1 <= 0)
                        return true;
                    break;
                case 2:
                    if(restPoints[0].Get()[0] - 2 <= 0 || restPoints[1].Get()[0] - 1 <= 0 || restPoints[2].Get()[0] + 1 >= gridX - 1)
                        return true;
                    break;
                case 3:
                    if(restPoints[1].Get()[0] + 1 >= gridX - 1 || restPoints[2].Get()[0] + 1 >= gridX - 1)
                        return true;
                    break;
            }
            return false;
        }
        /// <summary>
        /// Method responsible for checking if the block collide with used space after rotation
        /// </summary>
        /// <param name="HardLayer">Two dimensional pool Table that contains of information about state of the block</param>
        /// <returns>Return information about if the block after rotation collide with used space</returns>
        public override bool IfBlockOverrideOnRotate(bool[,] HardLayer)
        {
            switch(rotationPose)
            {
                case 0:
                    if(HardLayer[restPoints[0].Get()[0] + 2, restPoints[0].Get()[1]] || HardLayer[restPoints[1].Get()[0] + 1, restPoints[1].Get()[1] - 1] || HardLayer[restPoints[2].Get()[0] - 1, restPoints[2].Get()[1] + 1])
                        return true;
                    break;
                case 1:
                   if(HardLayer[restPoints[0].Get()[0] , restPoints[0].Get()[1] +2] || HardLayer[restPoints[1].Get()[0] -1, restPoints[1].Get()[1] +1] || HardLayer[restPoints[2].Get()[0] -1, restPoints[2].Get()[1] -1])
                        return true;
                    break;
                case 2:
                   if(HardLayer[restPoints[0].Get()[0] -2, restPoints[0].Get()[1] ] || HardLayer[restPoints[1].Get()[0] -1, restPoints[1].Get()[1] -1] || HardLayer[restPoints[2].Get()[0] +1, restPoints[2].Get()[1] -1])                 
                        return true;
                    break;
                case 3:
                    if(HardLayer[restPoints[0].Get()[0] , restPoints[0].Get()[1] -2] || HardLayer[restPoints[1].Get()[0] +1, restPoints[1].Get()[1] -1] || HardLayer[restPoints[2].Get()[0] +1, restPoints[2].Get()[1] +1])
                        return true;
                    break;
            }
            return false;
        }
        /// <summary>
        /// Method responsible for getting active block type
        /// </summary>
        /// <returns>Return information about the block type</returns>
        public override string GetBlockType() => "Z_Block";

    }
}
