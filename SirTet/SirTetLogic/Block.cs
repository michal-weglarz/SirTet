using System;
using System.Collections.Generic;
using System.Text;

namespace SirTetLogic
{
    public abstract class Block
    {
        protected abstract Point MiddlePoint{get;}
        protected abstract Point[] RestPoints{get;}
        protected abstract int RotationPose{get;}              

        public void Fall()
        {
            foreach(Point point in GetBlock())            
                point.Set(point.Get()[0], point.Get()[1] + 1);            
        }

        public void Move(bool toLeft)
        {
            if(toLeft)
                foreach(Point point in GetBlock())
                    point.Set(point.Get()[0] - 1, point.Get()[1]);
            else
                foreach(Point point in GetBlock())
                    point.Set(point.Get()[0] + 1, point.Get()[1]);
        }

        public abstract void Rotate();

        public bool IfTouchHardLayer(bool[,] HardLayer)
        {
            foreach(Point point in GetBlock())
            {
                if(HardLayer[point.Get()[0], point.Get()[1] + 1] == true)
                    return true;                                         
            }
            return false;
        }

        public abstract bool IfBlockOutOfGridOnRotate(int gridX);

        public abstract bool IfBlockOverrideOnRotate(bool[,] HardLayer);

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

        public Point[] GetBlock() => new Point[4] { MiddlePoint, RestPoints[0], RestPoints[1], RestPoints[2] };

        public int GetRotationPose => RotationPose;

        public  abstract string GetBlockType();
    }    
}
