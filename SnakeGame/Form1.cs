using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        Snake snake;
        Apple apple;
        Borders borders;

        public Form1()
        {
            InitializeComponent();

            borders = new Borders(this);

            apple = new Apple(this);

            Point startLocation = new Point(200, 100);
            int snakeLength = 5;
            snake = new Snake(startLocation, snakeLength, this);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Point direction = GetNewDirection(e);
            snake.TryChangeDirection(direction);
        }

        private Point GetNewDirection(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    return new Point(0, -1);
                case Keys.S:
                case Keys.Down:
                    return new Point(0, 1);
                case Keys.A:
                case Keys.Left:
                    return new Point(-1, 0);
                case Keys.D:
                case Keys.Right:
                    return new Point(1, 0);
                default:
                    return new Point(0, 0);
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            snake.TryMove();
            if (snake.InteractsWithApple(apple.AppleLocation))
            {
                apple.ChangeLocation(this.Size);
                snake.AddOneCell();
            }
            if (snake.InteractsWithBorders(borders.GetBorderLocations()))
            {
                snake.StopMoving();
            }
        }
    }
}
