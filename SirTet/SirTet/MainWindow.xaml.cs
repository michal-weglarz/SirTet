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
using System.Windows.Threading;

namespace SirTet
{
    public partial class MainWindow : Window
    {
        bool game_Start = true;
        float game_Speed = 1f;
        DateTime currentd_Time = DateTime.Now;
        int x = 4;
        int y = 0;
        Rectangle[,] r = new Rectangle[10,24];
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            Tiles();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(game_Speed);
            timer.Tick += timer_Tick;
            timer.Start();
            KeyDown += Move_Key;
        }
        void timer_Tick(object sender, EventArgs e)
        {
            Move_Down();
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
                    Grid.SetRow(tile, j);
                    Grid.SetColumn(tile, i);
                    sirtet_Grid.Children.Add(tile);
                    r[i, j] = tile;
                }
            }
        }
        void Move_Down()
        {
            r[x, y].Fill = new SolidColorBrush(Colors.White);
            if (y > 0)
            r[x, y - 1].Fill = new SolidColorBrush(Colors.Black);
            y++;
        }
        void Move_Key(object sender, KeyEventArgs e)
        {
            switch (e.Key.ToString())
            {
                case "Left":
                    r[x, y-1].Fill = new SolidColorBrush(Colors.Black);//y-1 ponieważ na końcu funckji Move_Down y jest zwiększany o 1 przez co wskazywane jest pole o jedno niżej niż pole aktywne(pokolorowane)
                    x--;
                    r[x, y-1].Fill = new SolidColorBrush(Colors.White);
                    break;
                case "Right":
                    r[x, y-1].Fill = new SolidColorBrush(Colors.Black);
                    x++;
                    r[x, y-1].Fill = new SolidColorBrush(Colors.White);
                    break;
            }
        }
    }
}
