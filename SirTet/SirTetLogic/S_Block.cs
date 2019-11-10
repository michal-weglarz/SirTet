using System;
using System.Collections.Generic;
using System.Text;

namespace SirTetLogic
{
    class S_Block: Block
    {
        Point middlePoint;
        Point[] restPoints;
        int rotationPose = 0;

        protected override Point MiddlePoint{get => middlePoint;}
        protected override Point[] RestPoints{get => restPoints;}
        protected override int RotationPose{get => rotationPose;}

        public S_Block(int middleX, int middleY)
        {
            middlePoint = new Point(middleX, middleY);
            restPoints = new Point[3] { new Point(middleX - 1, middleY), new Point(middleX, middleY - 1), new Point(middleX + 1, middleY - 1) };
        }

        public override void Rotate()
        {
            switch(rotationPose)
            {
                case 0:
                    restPoints[0] = new Point(restPoints[0].Get()[0] + 1, restPoints[0].Get()[1] - 1);
                    restPoints[1] = new Point(restPoints[1].Get()[0] + 1, restPoints[1].Get()[1] + 1);
                    restPoints[2] = new Point(restPoints[2].Get()[0], restPoints[2].Get()[1] + 2);
                    rotationPose = 1;
                    break;
                case 1:
                    restPoints[0] = new Point(restPoints[0].Get()[0] + 1, restPoints[0].Get()[1] + 1);
                    restPoints[1] = new Point(restPoints[1].Get()[0] - 1, restPoints[1].Get()[1] + 1);
                    restPoints[2] = new Point(restPoints[2].Get()[0] - 2, restPoints[2].Get()[1]);
                    rotationPose = 2;
                    break;
                case 2:
                    restPoints[0] = new Point(restPoints[0].Get()[0] - 1, restPoints[0].Get()[1] + 1);
                    restPoints[1] = new Point(restPoints[1].Get()[0] - 1, restPoints[1].Get()[1] - 1);
                    restPoints[2] = new Point(restPoints[2].Get()[0], restPoints[2].Get()[1] - 2);
                    rotationPose = 3;
                    break;
                case 3:
                    restPoints[0] = new Point(restPoints[0].Get()[0] - 1, restPoints[0].Get()[1] - 1);
                    restPoints[1] = new Point(restPoints[1].Get()[0] + 1, restPoints[1].Get()[1] - 1);
                    restPoints[2] = new Point(restPoints[2].Get()[0] + 2, restPoints[2].Get()[1]);
                    rotationPose = 0;
                    break;
            }
        }

        public override bool IfBlockOutOfGridOnRotate(int gridX)
        {
            switch(rotationPose)
            {
                case 0:
                    if(restPoints[0].Get()[0] + 1 >= gridX - 1 || restPoints[1].Get()[0] + 1 >= gridX - 1)
                        return true;
                    break;
                case 1:
                    if(restPoints[0].Get()[0] + 1 >= gridX - 1 || restPoints[1].Get()[0] - 1 <= 0 || restPoints[2].Get()[0] - 2 <= 0)
                        return true;
                    break;
                case 2:
                    if(restPoints[0].Get()[0] - 1 <= 0 || restPoints[1].Get()[0] - 1 <= 0 )
                        return true;
                    break;
                case 3:
                    if(restPoints[0].Get()[0] - 1 <= 0 || restPoints[1].Get()[0] + 1 >= gridX - 1 || restPoints[2].Get()[0] + 2 >= gridX - 1)
                        return true;
                    break;
            }
            return false;
        }

        public override bool IfBlockOverrideOnRotate(bool[,] HardLayer)
        {
            switch(rotationPose)
            {
                case 0:
                    if(HardLayer[restPoints[0].Get()[0]+1, restPoints[0].Get()[1]-1] || HardLayer[restPoints[1].Get()[0]+1, restPoints[1].Get()[1]+1] || HardLayer[restPoints[2].Get()[0], restPoints[2].Get()[1]+2])
                        return true;
                    break;
                case 1:
                    if(HardLayer[restPoints[0].Get()[0]+1, restPoints[0].Get()[1]+1] || HardLayer[restPoints[1].Get()[0]-1, restPoints[1].Get()[1]+1] || HardLayer[restPoints[2].Get()[0]-2, restPoints[2].Get()[1]])
                        return true;
                    break;
                case 2:
                    if(HardLayer[restPoints[0].Get()[0]-1, restPoints[0].Get()[1]+1] || HardLayer[restPoints[1].Get()[0]-1, restPoints[1].Get()[1]-1] || HardLayer[restPoints[2].Get()[0], restPoints[2].Get()[1]-2])
                        return true;
                    break;
                case 3:
                    if(HardLayer[restPoints[0].Get()[0]-1, restPoints[0].Get()[1]-1] || HardLayer[restPoints[1].Get()[0]+1, restPoints[1].Get()[1]-1] || HardLayer[restPoints[2].Get()[0]+2, restPoints[2].Get()[1]])
                        return true;
                    break;
            }
            return false;
        }
    }
}
