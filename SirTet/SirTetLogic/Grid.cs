using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SirTetLogic
{
    public class Grid
    {
        Rectangle[,] grid;
        bool[,] hardLayer;
        int sizeX;
        int sizeY;

        public Grid(ref Rectangle[,] RectangleTab, int sizeGridX = 10, int sizeGridY = 24)
        {
            grid = RectangleTab;
            sizeX = sizeGridX;
            sizeY = sizeGridY + 1; // Dodanie jednego wiersza jest konieczne by pierwsze bloki miały się na czym rozbijać
            hardLayer = new bool[sizeX, sizeY];
            InitializeHardLayer();
            
        }

        private void InitializeHardLayer()
        {
            for(int i = 0; i < sizeX; i++)
            {
                for(int j = 0; j < sizeY; j++)
                {                    
                    if(j == sizeY-1)
                        hardLayer[i, j] = true;
                    else
                        hardLayer[i, j] = false;
                }
            }                
        }

        public void DrawBlock(Point[] Block, Color color)
        {
            foreach(Point point in Block)
                grid[point.Get()[0], point.Get()[1]].Fill = new SolidColorBrush(color);
        }

        public void Indurate(Point[] IndurateBlock)
        {
            foreach(Point point in IndurateBlock)
            {
                hardLayer[point.Get()[0], point.Get()[1]] = true;
            }
        }

        public bool[,] GetHardLayer => hardLayer;
    }
}
