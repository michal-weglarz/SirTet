using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SirTetLogic
{
    public class Grid
    {
        Rectangle[,] grid;
        Color gridColor;
        bool[,] hardLayer;
        int sizeX;
        int sizeY;

        public Grid(ref Rectangle[,] RectangleTab, Color color, int sizeGridX = 10, int sizeGridY = 24)
        {
            grid = RectangleTab;
            sizeX = sizeGridX;
            sizeY = sizeGridY ;
            gridColor = color;
            hardLayer = new bool[sizeX, sizeY + 1]; // Dodanie jednego wiersza jest konieczne by pierwsze bloki miały się na czym rozbijać
            InitializeHardLayer(gridColor);
            
        }

        public void InitializeHardLayer(Color color)
        {
            for(int i = 0; i < sizeX; i++)
            {
                for(int j = 0; j <= sizeY; j++)
                {
                    if(j == sizeY)
                        hardLayer[i, j] = true;
                    else
                    {
                        hardLayer[i, j] = false;
                        grid[i, j].Fill = new SolidColorBrush(color);
                    }                        
                }
            }                
        }

        public void DrawBlock(Point[] Block, Color color)
        {
            foreach(Point point in Block)
                grid[point.Get()[0], point.Get()[1]].Fill = new SolidColorBrush(color);
        }

        public void ClearLine(int lineToClear, Color color)
        {
            for(int i = 0;i < sizeX;i++)
            {
                grid[i, lineToClear].Fill = new SolidColorBrush(color);
                hardLayer[i, lineToClear] = false;
            }

        }

        public void ClearAllGrid(Color color)
        {
            for(int i = 0; i < sizeY; i++)            
                ClearLine(i, color);
        }

        public bool Indurate(Point[] IndurateBlock, int rowToCheck)
        {
            foreach(Point point in IndurateBlock)
            {
                hardLayer[point.Get()[0], point.Get()[1]] = true;
            }
            return IfGameOver(rowToCheck, sizeX); // Zwraca czy nie koniec gry
        }

        public List<int> LinesToDestroy(Point[] IndurateBlock)
        {
            List<int> LinesToDestroy = new List<int>();
            foreach(Point point in IndurateBlock)
            {
                if(!LinesToDestroy.Contains(point.Get()[1]))
                    if(IfLineComplete(point.Get()[1], sizeX))
                        LinesToDestroy.Add(point.Get()[1]);
            }
            return LinesToDestroy;
        }

        public void RestBlockFall(int whereStartFall, int fallLenght , int whereEndFall = 0)
        {
            for(int j = whereStartFall - fallLenght; j >= whereEndFall; j--)
            {
                if(IfLineClear(j + fallLenght, gridColor)) break;
                else
                {
                    for(int i = 0;i < sizeX;i++)                    
                    {                    
                        hardLayer[i, j + fallLenght] = hardLayer[i, j];
                        grid[i, j + fallLenght].Fill = grid[i, j].Fill.CloneCurrentValue();
                    }                    
                }
            }
        }

        bool IfLineClear(int line, Color color)
        {
            for(int i = 0;i < sizeX;i++)
            {
                if(((SolidColorBrush)grid[i, line].Fill).ToString() != (new SolidColorBrush(color)).ToString())
                {
                    return false;
                }                    
            }
            return true;
        }

        public bool[,] GetHardLayer => hardLayer;

        bool IfGameOver(int rowToCheck, int xSize)
        {
            for(int i = 0;i < xSize;i++)
                if(hardLayer[i, rowToCheck])
                {
                    return true;
                }
                              
            return false;
        }

        bool IfLineComplete(int rowToCheck, int xSize)
        {
            for(int i = 0;i < xSize;i++)
                if(!hardLayer[i, rowToCheck])
                {
                    return false;
                }

            return true;
        }
    }
}
