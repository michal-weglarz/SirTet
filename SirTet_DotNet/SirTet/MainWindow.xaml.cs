using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using SirTetLogic; 

namespace SirTet
{
    public partial class MainWindow : Window
    {
        MainGameController game;
        Rectangle[,] blockTab = new Rectangle[10,24];
        Rectangle[,] nextBlockTab = new Rectangle[4, 15];
        Rectangle[,] holdBlockTab = new Rectangle[4, 3];




        public MainWindow()
        {
           
            InitializeComponent();
            SirTetLogic.Score.DrawRecords(ref Record);
            Music.MainTheme();
            GenerateBlockTable();            
            GenerateNextBlockTable();
            GenerateHoldBlockTable();
            
        }

        private void GenerateBlockTable ()
        {
            for ( int i = 0; i < 10; i++)
            {
                for ( int j = 0; j < 24; j++)
                {
                    Rectangle tile = new Rectangle();
                    tile.StrokeThickness = 1;
                    tile.Stroke = Brushes.White;
                    System.Windows.Controls.Grid.SetRow(tile, j);
                    System.Windows.Controls.Grid.SetColumn(tile, i);
                    sirtet_Grid_GameSpace.Children.Add(tile);
                    blockTab[i, j] = tile;
                }
            }
        }

        private void GenerateNextBlockTable()
        {
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 15; j++)
                {
                    Rectangle tile = new Rectangle();
                    tile.StrokeThickness = 1;
                    tile.Stroke = Brushes.White;
                    System.Windows.Controls.Grid.SetRow(tile, j);
                    System.Windows.Controls.Grid.SetColumn(tile, i);
                    sirtet_Grid_NextBlock.Children.Add(tile);
                    nextBlockTab[i, j] = tile;
                }
            }
        }

        private void GenerateHoldBlockTable()
        {
            for(int i = 0;i < 4;i++)
            {
                for(int j = 0;j < 3;j++)
                {
                    Rectangle tile = new Rectangle();
                    tile.StrokeThickness = 1;
                    tile.Stroke = Brushes.White;
                    System.Windows.Controls.Grid.SetRow(tile, j);
                    System.Windows.Controls.Grid.SetColumn(tile, i);
                    sirtet_Grid_HoldBlock.Children.Add(tile);
                    holdBlockTab[i, j] = tile;
                }
            }
        }

        void GetKey(object sender, KeyEventArgs e)
        {
            switch (e.Key.ToString())
            {
                case "Left":
                    game.MoveBlockHorizontal(true);
                    break;
                case "Right":
                    game.MoveBlockHorizontal(false);
                    break;
                case "Up":
                    game.RotateBlock();
                    break;
                case "Down":
                    game.BlockFall(2); 
                    break;
                case "Space":
                    game.BlockFall(20); 
                    break;
                case "LeftCtrl":
                    game.HoldBlock();
                    break;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            game = new MainGameController(ref blockTab, ref nextBlockTab, ref holdBlockTab, ref PlayerNick, ref Score, ref Combo, ref Record, ref DestroyedLines);

            KeyDown += GetKey;
            StartButton.IsEnabled = false;
            PlayerNick.IsEnabled = false;
        }
    }
}
