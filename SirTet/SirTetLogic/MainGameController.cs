using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Controls;

namespace SirTetLogic
{
    public class MainGameController
    {        
        int startX;
        int startY;
        int sizeX;
        int sizeY; 

        Grid grid;        
        Block block;
        Color blockColor;        

        Grid nextBlockGrid;       
        Color nextBlockColor;
        Block nextBlock;
        int nextBlockType;
        byte[] nextBlockColorArray;

        Score score; 

        float gameSpeed;
        DateTime currentd_Time = DateTime.Now;
        DispatcherTimer timer;

        Random random = new Random();

        public MainGameController(ref Rectangle[,] Grid, ref Rectangle[,] NextBlockGrid, ref TextBlock ScoreText, ref TextBlock ComboText, ref TextBlock RecordText, ref TextBlock DestroyLinesText, float GameSpeed = 0.7f, int blockGenerateX = 4, int blockGenerateY=3, int sizeGridX = 10, int sizeGridY = 24)
        {            
            grid = new Grid(ref Grid, Colors.Black);
            nextBlockGrid = new Grid(ref NextBlockGrid, Colors.Black, 4, 3);
            startX = blockGenerateX;
            startY = blockGenerateY;
            sizeX = sizeGridX;
            sizeY = sizeGridY;
            score = new Score(ref ScoreText, ref ComboText,ref RecordText, ref DestroyLinesText);
            gameSpeed = GameSpeed; 
            CrateBlock();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(gameSpeed);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        public void BlockFall(int fallLenght)
        {
            int counter = 0;           
            while(!block.IfTouchHardLayer(grid.GetHardLayer) && fallLenght > counter)
            {
                grid.DrawBlock(block.GetBlock(), Colors.Black);
                block.Fall();
                grid.DrawBlock(block.GetBlock(), blockColor);
                counter++;
            }
            
            if( block.IfTouchHardLayer(grid.GetHardLayer))
            {
                if(grid.Indurate(block.GetBlock(), 4))
                {
                    GameOver();
                    return;
                }
                else
                {
                    List<int> lineToClear = grid.LinesToDestroy(block.GetBlock());
                    if(lineToClear.Count > 0)
                    {  
                        score.AddMainScore(lineToClear.Count * 1000);
                        score.AddLineCombo(lineToClear.Count);
                        score.AddDestroyLineScore(lineToClear.Count);
                        lineToClear.Sort();
                        grid.RestBlockFall(lineToClear[lineToClear.Count - 1], lineToClear.Count, startY + 2);
                    }
                    else
                    {
                        if(score.GetLineCombo() > 0)
                            score.AddUpLineCombo(2000);//Podlicznie combo za linie
                    }
                    CrateBlock();
                }
            }                  
        }

        void timer_Tick(object sender, EventArgs e)
        {
            BlockFall(1);
            if((float)score.GetMainScore() > (10000f / gameSpeed) && (gameSpeed -  0.05f) > 0) // uposledzony poziom trudności
            {
                gameSpeed -= 0.05f;
                timer.Interval = TimeSpan.FromSeconds(gameSpeed);
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
            if(block == null)
            {
                block = GenerateBlock();
                nextBlock = GenerateBlock();
                blockColor = GenerateColor();
                nextBlockColor = GenerateColor();
            }
            else
            {
                block = GenerateBlock(nextBlockType);               
                blockColor = GenerateColor(nextBlockColorArray[0], nextBlockColorArray[1], nextBlockColorArray[2]);
                nextBlock = GenerateBlock();
                nextBlockColor = GenerateColor();
            }
            grid.DrawBlock(block.GetBlock(),blockColor);
            DrawNextBlock(nextBlock.GetBlockType(), nextBlockColor);

        }

        Block GenerateBlock(int blockType = 0)
        {            
            if(blockType == 0)
                blockType = random.Next(1, 7);
            switch(blockType)
            {
                case 1:
                    return new I_Block(startX,startY);
                case 2:
                    return new J_Block(startX, startY);
                case 3:
                    return new L_Block(startX, startY);
                case 4:
                    return new O_Block(startX, startY);
                case 5:
                    return new S_Block(startX, startY);
                case 6:
                    return new T_Block(startX, startY);
                case 7:
                    return new Z_Block(startX, startY);
                    
            }
            throw new Exception();
        }

        Color GenerateColor(byte r = 0, byte g = 0, byte b = 0)
        {            
            Color color;
            if(r == (byte)0 && g == (byte)0 && b == (byte)0)
                do
                     color = Color.FromArgb(255, (byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));
                while(color == Color.FromArgb(255, 0, 0, 0) || blockColor == Color.FromArgb(255, 255, 255, 255));
            else
                color = Color.FromArgb(255, r, g, b);
            return color;            
        }

        void DrawNextBlock(string blockType, Color blockColor)
        {
            Block preparedBlock;
            switch(blockType)
            {
                case "I_Block":
                    preparedBlock = new I_Block(1, 1);
                    nextBlockType = 1;
                    nextBlockColorArray = new byte[3] { nextBlockColor.R, nextBlockColor.G, nextBlockColor.B };
                    break;
                case "J_Block":
                    preparedBlock = new J_Block(1, 1);
                    nextBlockType = 2;
                    nextBlockColorArray = new byte[3] { nextBlockColor.R, nextBlockColor.G, nextBlockColor.B };
                    break;
                case "L_Block":
                    preparedBlock = new L_Block(1, 1);
                    nextBlockType = 3;
                    nextBlockColorArray = new byte[3] { nextBlockColor.R, nextBlockColor.G, nextBlockColor.B };
                    break;
                case "O_Block":
                    preparedBlock = new O_Block(1, 1);
                    nextBlockType = 4;
                    nextBlockColorArray = new byte[3] { nextBlockColor.R, nextBlockColor.G, nextBlockColor.B };
                    break;
                case "S_Block":
                    preparedBlock = new S_Block(1, 1);
                    nextBlockType = 5;
                    nextBlockColorArray = new byte[3] { nextBlockColor.R, nextBlockColor.G, nextBlockColor.B };
                    break;
                case "T_Block":
                    preparedBlock = new T_Block(1, 1);
                    nextBlockType = 6;
                    nextBlockColorArray = new byte[3] { nextBlockColor.R, nextBlockColor.G, nextBlockColor.B };
                    break;
                case "Z_Block":
                    preparedBlock = new Z_Block(1, 1);
                    nextBlockType = 7;
                    nextBlockColorArray = new byte[3] { nextBlockColor.R, nextBlockColor.G, nextBlockColor.B };
                    break;
                default:
                    throw new Exception();               
            }
            nextBlockGrid.ClearAllGrid(Colors.Black);
            nextBlockGrid.DrawBlock(preparedBlock.GetBlock(), blockColor);
        }

        void GameOver() //Tutaj co się dzieje po przegranej // Narazie tylko restart
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
