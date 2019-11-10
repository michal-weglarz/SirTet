using System;
using System.Collections.Generic;
using System.Text;

namespace SirTetLogic
{
    class O_Block : Block
    {
        Point middlePoint;
        Point[] restPoints;
        Point[] allPoints;
        int rotationPose = 0;

        protected override Point MiddlePoint{get => middlePoint;}
        protected override Point[] RestPoints{get => restPoints;}
        protected override int RotationPose{get => rotationPose;}

        public O_Block(int middleX, int middleY)
        {
            middlePoint = new Point(middleX, middleY);
            restPoints = new Point[3] { new Point(middleX, middleY - 1), new Point(middleX + 1, middleY - 1), new Point(middleX + 1, middleY)};
            allPoints = new Point[4] { middlePoint, restPoints[0], restPoints[1], restPoints[2]};
        }     

        public override void Rotate()
        {
            //W kontekście tego klocka ta metoda nie ma sensu
        }

        public override bool IfBlockOutOfGridOnRotate(int gridX)
        {
            //W kontekście tego klocka ta metoda nie ma sensu
            return false;
        }

        public override bool IfBlockOverrideOnRotate(bool[,] HardLayer)
        {
            //W kontekście tego klocka ta metoda nie ma sensu
            return false;
        }
    }
}
