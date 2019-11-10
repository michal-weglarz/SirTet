using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;

namespace SirTetLogic
{
    public class MainGameController
    {        
        int startX;
        int startY;
        int sizeX;
        int sizeY;
        
        Block block;
        Color blockColor;
        Grid grid;

        public MainGameController(ref Rectangle[,] Grid, int blockGenerateX = 4, int blockGenerateY=2, int sizeGridX = 10, int sizeGridY = 24)
        {            
            grid = new Grid(ref Grid);            
            startX = blockGenerateX;
            startY = blockGenerateY;
            sizeX = sizeGridX;
            sizeY = sizeGridY;
            CrateBlock();            
        }        

        public void BlockFall()
        {
            if(!block.IfTouchHardLayer(grid.GetHardLayer))
            {
                grid.DrawBlock(block.GetBlock(), Colors.Black);
                block.Fall();
                grid.DrawBlock(block.GetBlock(), blockColor);
            }                
            else
            {
                grid.Indurate(block.GetBlock());
                CrateBlock();
            }
                
        }

        public void MoveBlockHorizontal(bool toLeft)
        {
            if(!block.IfOutOfGrid(sizeX, toLeft) && !block.IfBlockOverride(grid.GetHardLayer, toLeft))
            {
                grid.DrawBlock(block.GetBlock(), Colors.Black);
                block.Move(toLeft);
                grid.DrawBlock(block.GetBlock(), blockColor);
            }                
        }

        public void RotateBlock()
        {
            if(!block.IfBlockOutOfGridOnRotate(sizeX) && !block.IfBlockOverrideOnRotate(grid.GetHardLayer))
            {
                grid.DrawBlock(block.GetBlock(), Colors.Black);
                block.Rotate();
                grid.DrawBlock(block.GetBlock(), blockColor);
            }                       
        }

        void CrateBlock()
        {
            GenerateBlock();
            GenerateColor();
            grid.DrawBlock(block.GetBlock(),blockColor);
        }

        void GenerateBlock()
        {
            Random random = new Random();
            switch(random.Next(1, 7))
            {
                case 1:
                    block = new I_Block(startX,startY);
                    break;
                case 2:
                    block = new J_Block(startX, startY);
                    break;
                case 3:
                    block = new L_Block(startX, startY);
                    break;
                case 4:
                    block = new O_Block(startX, startY);
                    break;
                case 5:
                    block = new S_Block(startX, startY);
                    break;
                case 6:
                    block = new T_Block(startX, startY);
                    break;
                case 7:
                    block = new Z_Block(startX, startY);
                    break;
            }
        }

        void GenerateColor()
        {
            Random random = new Random();
            do
                blockColor = Color.FromArgb(255, (byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));
            while(blockColor == Color.FromArgb(255, 0, 0, 0) || blockColor == Color.FromArgb(255, 255, 255, 255));
            
        }
    }
}
