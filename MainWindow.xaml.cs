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
using System.Windows.Threading;

namespace _319481GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        Rectangle[,] ChessBoard = new Rectangle[20, 20];
        Point location;
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Rectangle r = new Rectangle();
                    r.Width = 20;
                    r.Height = 20;
                    r.Fill = Brushes.White;
                    r.StrokeThickness = 1;
                    r.Stroke = Brushes.Black;
                    Canvas.SetLeft(r, r.Width * j);
                    Canvas.SetTop(r, r.Height * i);
                    canvas.Children.Add(r);
                    ChessBoard[j, i] = r;
                }
            }
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.Tick += tick;
        }
        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            location = e.GetPosition(canvas);
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Rectangle r = ChessBoard[j, i];
                    if ((location.X > Canvas.GetLeft(r) && location.X < Canvas.GetLeft(r) + 20)
                        && (location.Y > Canvas.GetTop(r) && location.Y < Canvas.GetTop(r) + 20))
                    {
                        if (r.Fill == Brushes.White)
                        {
                            r.Fill = Brushes.Black;
                        }
                        else
                        {
                            r.Fill = Brushes.White;
                        }
                    }
                }
            }
        }
        private void tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    int minX = -1;
                    int maxX = 1;
                    int minY = -1;
                    int maxY = 1;
                    Rectangle r = ChessBoard[j, i];
                    int an = 0;
                    bool alive;
                    Rectangle[,] neighbours = new Rectangle[3, 3];
                    if (j == 19)
                    {
                        maxX = 0;
                    }
                    else if (j == 0)
                    {
                        minX = 0;
                    }
                    if (i == 19)
                    {
                        maxY = 0;
                    }
                    else if (i == 0)
                    {
                        minY = 0;
                    }
                    for (int x = minX; x <= maxX; x++)
                    {
                        for (int y = minY; y <= maxY; y++)
                        {
                            Rectangle neighbour = ChessBoard[j + x, i + y];
                            neighbours[x + 1, y + 1] = neighbour;
                            if (neighbours[x + 1, y + 1].Fill == Brushes.Black)
                            {
                                an++;
                            }
                        }
                    }
                    if (r.Fill == Brushes.Black)
                    {
                        alive = true;
                        if (an < 2 || an > 3)
                        {
                            alive = false;
                        }
                        else if (an == 2 || an == 3)
                        {
                            alive = true;
                        }
                    }
                    else
                    {
                        alive = false;
                        if (an == 3)
                        {
                            alive = true;
                        }
                    }
                    if (alive == true)
                    {
                        r.StrokeThickness = 3;
                    }
                    if (alive == false)
                    {
                        r.StrokeThickness = 2;
                    }
                }
            }
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Rectangle r = ChessBoard[j, i];
                    if (r.StrokeThickness == 3)
                    {
                        r.Fill = Brushes.Black;
                    }
                    else
                    {
                        r.Fill = Brushes.White;
                    }
                }
            }
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    ChessBoard[i, j].Fill = Brushes.White;
                    ChessBoard[i, j].StrokeThickness = 1;
                }
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}