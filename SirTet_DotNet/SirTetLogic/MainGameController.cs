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
    /// <summary>
    /// The main MainGameController class
    /// Contains all method for performing MainGameController function
    /// </summary>
    public class MainGameController
    {        
        int startX;
        int startY;
        int sizeX;
        int sizeY; 

        Grid grid;        
        Block block;
        Color blockColor;
        byte[] blockColorArray;
        int blockType;

        Grid nextBlockGrid;       
        Color[] nextBlockColor;
        Block[] nextBlock;
        int blockToPreview;
        int[] nextBlockType;
        byte[][] nextBlockColorArray;

        Grid holdBlockGrid;
        Color holdBlockColor;
        Block holdBlock;
        int holdBlockType;
        byte[] holdBlockColorArray;

        Score score; 

        float gameSpeed;
        DateTime currentd_Time = DateTime.Now;
        DispatcherTimer timer;

        Random random = new Random();
        /// <summary>
        /// Initialize a new instance of the <see cref="MainGameController"/> class
        /// </summary>
        /// <param name="Grid">Variable contains reference to two dimensional table contains canvas elements</param>
        /// <param name="NextBlockGrid">Variable contains reference to two dimensional table contains canvas elements</param>
        /// <param name="HoldBlockGrid">Variable contains reference to two dimensional table contains canvas elements</param>
        /// <param name="PlayerNick">Variable contains reference to canvas element</param>
        /// <param name="ScoreText">Variable contains reference to canvas element</param>
        /// <param name="ComboText">Variable contains reference to canvas element</param>
        /// <param name="RecordText">Variable contains reference to canvas element</param>
        /// <param name="DestroyLinesText">Variable contains reference to canvas element</param>
        /// <param name="GameSpeed">Variable contains float number</param>
        /// <param name="BlockToPreview">Variable contains</param>
        /// <param name="blockGenerateX">Variable contains index of row</param>
        /// <param name="blockGenerateY">Variable contains index of row</param>
        /// <param name="sizeGridX">Variable contains number of columns</param>
        /// <param name="sizeGridY">Variable contains number of rows</param>
        public MainGameController(ref Rectangle[,] Grid, ref Rectangle[,] NextBlockGrid, ref Rectangle[,] HoldBlockGrid, ref TextBox PlayerNick, ref TextBlock ScoreText, ref TextBlock ComboText, ref TextBlock RecordText, ref TextBlock DestroyLinesText, float GameSpeed = 0.7f, int BlockToPreview = 5, int blockGenerateX = 4, int blockGenerateY=3, int sizeGridX = 10, int sizeGridY = 24)
        {            
            grid = new Grid(ref Grid, Colors.Black);
            nextBlockGrid = new Grid(ref NextBlockGrid, Colors.Black, 4, 15);
            holdBlockGrid = new Grid(ref HoldBlockGrid, Colors.Black, 4, 3);
            startX = blockGenerateX;
            startY = blockGenerateY;
            sizeX = sizeGridX;
            sizeY = sizeGridY;
            blockToPreview = BlockToPreview;
            nextBlockColor = new Color[BlockToPreview];
            nextBlock = new Block[BlockToPreview];
            nextBlockType = new int[BlockToPreview];
            nextBlockColorArray = new byte[BlockToPreview][];
            score = new Score(ref ScoreText, ref ComboText,ref RecordText, ref DestroyLinesText ,ref PlayerNick);
            gameSpeed = GameSpeed; 
            CrateBlock();            
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(gameSpeed);
            timer.Tick += Timer_Tick;
            timer.Start();
            Music.GameStartSFX();
        }
        /// <summary>
        /// Method responsible for blocks moving one row lower
        /// </summary>
        /// <param name="fallLenght">Number of how many lines should block fall</param>
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
                    Music.GameOverSFX();
                    GameOver();
                    return;
                }
                else
                {
                    List<int> lineToClear = grid.LinesToDestroy(block.GetBlock());
                    if(lineToClear.Count > 0)
                    {
                        Music.LineClearSFX();
                        score.AddMainScore(lineToClear.Count * 1000);
                        score.AddLineCombo(lineToClear.Count);
                        score.AddDestroyLineScore(lineToClear.Count);
                        lineToClear.Sort();
                        grid.RestBlockFall(lineToClear[lineToClear.Count - 1], lineToClear.Count, startY + 2);
                    }
                    else
                    {
                        if(score.GetLineCombo() > 0)
                        {
                            score.AddUpLineCombo(2000);
                            Music.ComboSFX();
                        }
                        Music.FallSFX();
                    }
                    CrateBlock();
                }
            }                  
        }
        /// <summary>
        /// Method responsible for timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Timer_Tick(object sender, EventArgs e)
        {
            BlockFall(1);
            if((float)score.GetMainScore() > (10000f / gameSpeed) && (gameSpeed -  0.05f) > 0) 
            {
                gameSpeed -= 0.05f;
                timer.Interval = TimeSpan.FromSeconds(gameSpeed);
            }
        }
        /// <summary>
        /// Method responsible for moving blocks to left and right
        /// </summary>
        /// <param name="toLeft">Bool contains information if key to move left is pressed</param>
        public void MoveBlockHorizontal(bool toLeft)
        {
            if(!block.IfOutOfGrid(sizeX, toLeft) && !block.IfBlockOverride(grid.GetHardLayer, toLeft))
            {
                Music.MoveSFX();
                grid.DrawBlock(block.GetBlock(), Colors.Black);
                block.Move(toLeft);
                grid.DrawBlock(block.GetBlock(), blockColor);
            }                
        }
        /// <summary>
        /// Method responsible for rotation of blocks
        /// </summary>
        public void RotateBlock()
        {
            if(!block.IfBlockOutOfGridOnRotate(sizeX) && !block.IfBlockOverrideOnRotate(grid.GetHardLayer))
            {
                Music.RotateSFX();
                grid.DrawBlock(block.GetBlock(), Colors.Black);
                block.Rotate();
                grid.DrawBlock(block.GetBlock(), blockColor);
            }                       
        }
        /// <summary>
        /// Method responsible for creating and drawing block
        /// </summary>
        void CrateBlock()
        {
            if(block == null)
            {
                block = GenerateBlock(0, true);
                blockColor = GenerateColor();
                blockColorArray = new byte[3] { blockColor.R, blockColor.G, blockColor.B };
                for(int i = 0; i < blockToPreview; i++)
                {
                    nextBlock[i] = GenerateBlock();
                    nextBlockColor[i] = GenerateColor();
                }                
            }
            else
            {
                block = GenerateBlock(nextBlockType[0], true);
                blockColor = GenerateColor(nextBlockColorArray[0][0], nextBlockColorArray[0][1], nextBlockColorArray[0][2]);
                blockColorArray = new byte[3] { blockColor.R, blockColor.G, blockColor.B };
                for(int i = 0;i < blockToPreview - 1;i++)
                {
                    nextBlock[i] = GenerateBlock(nextBlockType[i + 1]);
                    nextBlockColor[i] = GenerateColor(nextBlockColorArray[i + 1][0], nextBlockColorArray[i + 1][1], nextBlockColorArray[i + 1][2]);
                }
                nextBlock[blockToPreview - 1] = GenerateBlock();
                nextBlockColor[blockToPreview - 1] = GenerateColor();
            }
            grid.DrawBlock(block.GetBlock(),blockColor);
            nextBlockGrid.ClearAllGrid(Colors.Black);
            for(int i = 0; i < blockToPreview; i++)
            {
                DrawNextBlock(nextBlock[i].GetBlockType(), nextBlockColor[i], i, 1 + (3 * i));
            }
        }
        /// <summary>
        /// Method for generating block
        /// </summary>
        /// <param name="BlockType">Variable contains index of block type</param>
        /// <param name="ifMainBlock">Variable contains a bool about the block whether it is main or not</param>
        /// <param name="x">Variable contains index of column. Default value = -1</param>
        /// <param name="y">Variable contains index of row. Default value = -1</param>
        /// <returns>Return generated block</returns>
        Block GenerateBlock(int BlockType = 0, bool ifMainBlock = false, int x = -1, int y = -1)
        {            
            if(BlockType == 0)
                BlockType = random.Next(1, 7);
            if(x < 0 || y < 0)
            {
                x = startX;
                y = startY;
            }
            if(ifMainBlock)
                blockType = BlockType;
            
            switch(BlockType)
            {
                case 1:
                    return new I_Block(x, y);
                case 2:
                    return new J_Block(x, y);
                case 3:
                    return new L_Block(x, y);
                case 4:
                    return new O_Block(x, y);
                case 5:
                    return new S_Block(x, y);
                case 6:
                    return new T_Block(x, y);
                case 7:
                    return new Z_Block(x, y);
            }
            throw new Exception();
        }
        /// <summary>
        /// Method for generating color
        /// </summary>
        /// <param name="r">Variable contains value of red color</param>
        /// <param name="g">Variable contains value of green color</param>
        /// <param name="b">Variable contains value of blue color</param>
        /// <returns>Return generated color</returns>
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
        /// <summary>
        /// Method for drawing next block
        /// </summary>
        /// <param name="blockType">Variable contains name of block</param>
        /// <param name="blockColor">Variable contains color of block</param>
        /// <param name="blockNumber">Variable contains index of block</param>
        /// <param name="y">Variable contains index of row. Default value = 1</param>
        /// <param name="x">Variable contains index of column. Default value = 1</param>
        void DrawNextBlock(string blockType, Color blockColor,int blockNumber, int y = 1, int x = 1)
        {
            Block preparedBlock;
            switch(blockType)
            {
                case "I_Block":
                    preparedBlock = new I_Block(x, y);
                    nextBlockType[blockNumber] = 1;
                    nextBlockColorArray[blockNumber] = new byte[3] { nextBlockColor[blockNumber].R, nextBlockColor[blockNumber].G, nextBlockColor[blockNumber].B };
                    break;
                case "J_Block":
                    preparedBlock = new J_Block(x, y);
                    nextBlockType[blockNumber] = 2;
                    nextBlockColorArray[blockNumber] = new byte[3] { nextBlockColor[blockNumber].R, nextBlockColor[blockNumber].G, nextBlockColor[blockNumber].B };
                    break;
                case "L_Block":
                    preparedBlock = new L_Block(x, y);
                    nextBlockType[blockNumber] = 3;
                    nextBlockColorArray[blockNumber] = new byte[3] { nextBlockColor[blockNumber].R, nextBlockColor[blockNumber].G, nextBlockColor[blockNumber].B };
                    break;
                case "O_Block":
                    preparedBlock = new O_Block(x, y);
                    nextBlockType[blockNumber] = 4;
                    nextBlockColorArray[blockNumber] = new byte[3] { nextBlockColor[blockNumber].R, nextBlockColor[blockNumber].G, nextBlockColor[blockNumber].B };
                    break;
                case "S_Block":
                    preparedBlock = new S_Block(x, y);
                    nextBlockType[blockNumber] = 5;
                    nextBlockColorArray[blockNumber] = new byte[3] { nextBlockColor[blockNumber].R, nextBlockColor[blockNumber].G, nextBlockColor[blockNumber].B };
                    break;
                case "T_Block":
                    preparedBlock = new T_Block(x, y);
                    nextBlockType[blockNumber] = 6;
                    nextBlockColorArray[blockNumber] = new byte[3] { nextBlockColor[blockNumber].R, nextBlockColor[blockNumber].G, nextBlockColor[blockNumber].B };
                    break;
                case "Z_Block":
                    preparedBlock = new Z_Block(x, y);
                    nextBlockType[blockNumber] = 7;
                    nextBlockColorArray[blockNumber] = new byte[3] { nextBlockColor[blockNumber].R, nextBlockColor[blockNumber].G, nextBlockColor[blockNumber].B };
                    break;
                default:
                    throw new Exception();               
            }            
            nextBlockGrid.DrawBlock(preparedBlock.GetBlock(), blockColor);
        }
        /// <summary>
        /// Method responsible for holding block
        /// </summary>
        public void HoldBlock()
        { 
            Color tempBlockColor;
            Block tempBlock;
            int tempBlockType;
            byte[] tempBlockColorArray;

            Music.MoveSFX();

            if(holdBlock == null)
            {
                holdBlockType = blockType;
                holdBlock = GenerateBlock(holdBlockType, false, 1, 1);
                holdBlockColor = GenerateColor(blockColorArray[0], blockColorArray[1], blockColorArray[2]);
                holdBlockColorArray = new byte[3] { blockColor.R, blockColor.G, blockColor.B };
                holdBlockGrid.DrawBlock(holdBlock.GetBlock(), holdBlockColor);
                grid.DrawBlock(block.GetBlock(), Colors.Black);
                CrateBlock();
            }
            else
            {
                tempBlockType = blockType;
                tempBlock = GenerateBlock(tempBlockType, false, 1, 1);
                tempBlockColor = GenerateColor(blockColorArray[0], blockColorArray[1], blockColorArray[2]);
                tempBlockColorArray = new byte[3] { blockColor.R, blockColor.G, blockColor.B };
                
                grid.DrawBlock(block.GetBlock(), Colors.Black);
                block = GenerateBlock(holdBlockType, true);
                blockColor = GenerateColor(holdBlockColorArray[0], holdBlockColorArray[1], holdBlockColorArray[2]);
                blockColorArray = new byte[3] { blockColor.R, blockColor.G, blockColor.B };

                holdBlockType = tempBlockType;
                holdBlock = GenerateBlock(holdBlockType, false, 1, 1);
                holdBlockColor = GenerateColor(tempBlockColorArray[0], tempBlockColorArray[1], tempBlockColorArray[2]);
                holdBlockColorArray = new byte[3] { holdBlockColor.R, holdBlockColor.G, holdBlockColor.B };
                
                holdBlockGrid.ClearAllGrid(Colors.Black);               
                holdBlockGrid.DrawBlock(holdBlock.GetBlock(), holdBlockColor);                
                grid.DrawBlock(block.GetBlock(), blockColor);
            }           
            
        }
        /// <summary>
        /// Method responsible for game over
        /// </summary>
        void GameOver() 
        {
            score.SetRecords();
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
