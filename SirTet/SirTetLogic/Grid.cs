using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SirTetLogic
{
    /// <summary>
    /// The main Grid class
    /// Contains all method for performing Grid function
    /// </summary>
    public class Grid
    {
        Rectangle[,] grid;
        Color gridColor;
        bool[,] hardLayer;
        int sizeX;
        int sizeY;
        /// <summary>
        /// Initialize a new instance of the <see cref="Grid"/> class
        /// </summary>
        /// <param name="RectangleTab">Variable contains reference to two dimensional table contains canvas elements</param>
        /// <param name="color">Variable contains information about grid color</param>
        /// <param name="sizeGridX">Number of columns</param>
        /// <param name="sizeGridY">Number of rows</param>
        public Grid(ref Rectangle[,] RectangleTab, Color color, int sizeGridX = 10, int sizeGridY = 24)
        {
            grid = RectangleTab;
            sizeX = sizeGridX;
            sizeY = sizeGridY ;
            gridColor = color;
            hardLayer = new bool[sizeX, sizeY + 1]; // Dodanie jednego wiersza jest konieczne by pierwsze bloki miały się na czym rozbijać
            InitializeHardLayer(gridColor);
            
        }
        /// <summary>
        /// Method responsible for making the last row visible as used
        /// </summary>
        /// <param name="color">Variable contains information about color</param>
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
        /// <summary>
        /// Method responsible for drawing the block on grid
        /// </summary>
        /// <param name="Block">Table that contains informations about points in block</param>
        /// <param name="color">Variable contains information about grid color</param>
        public void DrawBlock(Point[] Block, Color color)
        {
            foreach(Point point in Block)
                grid[point.Get()[0], point.Get()[1]].Fill = new SolidColorBrush(color);
        }
        /// <summary>
        /// Method responsible for clearing the given line
        /// </summary>
        /// <param name="lineToClear">Variable that contains index of row</param>
        /// <param name="color">Variable contains information about grid color</param>
        public void ClearLine(int lineToClear, Color color)
        {
            for(int i = 0;i < sizeX;i++)
            {
                grid[i, lineToClear].Fill = new SolidColorBrush(color);
                hardLayer[i, lineToClear] = false;
            }

        }
        /// <summary>
        /// Method responsible for clearing whole grid
        /// </summary>
        /// <param name="color">Variable contains information about grid color</param>
        public void ClearAllGrid(Color color)
        {
            for(int i = 0; i < sizeY; i++)            
                ClearLine(i, color);
        }
        /// <summary>
        /// Method responsible for acknowledging that given space is used
        /// </summary>
        /// <param name="IndurateBlock">Table contains information about block</param>
        /// <param name="rowToCheck">Variable that contains index of row</param>
        /// <returns></returns>
        public bool Indurate(Point[] IndurateBlock, int rowToCheck)
        {
            foreach(Point point in IndurateBlock)
            {
                hardLayer[point.Get()[0], point.Get()[1]] = true;
            }
            return IfGameOver(rowToCheck, sizeX); // Zwraca czy nie koniec gry
        }
        /// <summary>
        /// List of lines to destroy
        /// </summary>
        /// <param name="IndurateBlock">Table contains information about block</param>
        /// <returns></returns>
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
        /// <summary>
        /// Method responsible for falling blocks after cleaning line
        /// </summary>
        /// <param name="whereStartFall">Number of from which line block should fall</param>
        /// <param name="fallLenght">Number of how many lines should block fall</param>
        /// <param name="whereEndFall">Number of to which line should block fall. Default value = 0</param>
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
        /// <summary>
        /// Method responsible for checking is given line clear
        /// </summary>
        /// <param name="line">Variable contains information about index of row</param>
        /// <param name="color">Variable contains information about grid color</param>
        /// <returns>Return information about if the line cleared</returns>
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
        /// <summary>
        /// Method responsible for getting two dimensional table containing information about used space
        /// </summary>
        public bool[,] GetHardLayer => hardLayer;
        /// <summary>
        /// Method responsible for checking if game is over
        /// </summary>
        /// <param name="rowToCheck">Variable contains information about index of row</param>
        /// <param name="xSize">Number of columns</param>
        /// <returns>Return information about whether game is over or not</returns>
        bool IfGameOver(int rowToCheck, int xSize)
        {
            for(int i = 0;i < xSize;i++)
                if(hardLayer[i, rowToCheck])
                {
                    return true;
                }
                              
            return false;
        }
        /// <summary>
        /// Method responsible for checking if line is complete
        /// </summary>
        /// <param name="rowToCheck">Variable contains information about index of row</param>
        /// <param name="xSize">Number of columns</param>
        /// <returns>Return information about whether line is complete or not</returns>
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
