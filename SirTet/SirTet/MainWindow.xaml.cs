using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using SirTetLogic;
using System.Windows.Threading;

namespace SirTet
{
    public partial class MainWindow : Window
    {
        MainGameController game;
        Rectangle[,] rectangleTab = new Rectangle[10,24];

        float game_Speed = 1f;
        DateTime currentd_Time = DateTime.Now;
        DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            Tiles();
            game = new MainGameController(ref rectangleTab);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(game_Speed);
            timer.Tick += timer_Tick;
            KeyDown += GetKey;
            timer.Start();
        }

        private void Tiles ()
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
                    sirtet_Grid.Children.Add(tile);
                    rectangleTab[i, j] = tile;
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
                    game.BlockFall();
                    break;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            game.BlockFall();
        }
    }
}
