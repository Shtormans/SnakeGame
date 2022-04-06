using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    class Snake
    {
        private List<SnakeBody> body = new List<SnakeBody>();
        private SnakeHead snakeHead;
        private Point direction = new Point(0, 0);
        private Point nextDirection = new Point(0, 0);
        private const int width = 20;
        private readonly Size bodySize = new Size(width, width);
        private readonly Form form;

        public Snake(Point headLocation, int bodyLength, Form form)
        {
            this.form = form;
            InitializeSnake(headLocation, bodyLength);
        }

        public void AddOneCell()
        {
            AddBodySell();
        }

        public bool InteractsWithApple(Point appleLocation)
        {
            return snakeHead.Location == appleLocation;
        }

        public bool InteractsWithBorders(Point[] borderLocations)
        {
            Point location = snakeHead.Location;

            return location.X <= 0
                || location.X >= borderLocations[1].X
                || location.Y <= 0
                || location.Y >= borderLocations[2].Y;
        }

        public void TryMove()
        {
            if (!(direction.X == 0 && direction.Y == 0))
            {
                Move();

                if (direction != nextDirection)
                {
                    direction = nextDirection;
                }
            }
        }

        private void Move()
        {
            BodyMove();

            snakeHead.Move(direction);
        }

        public void TryChangeDirection(Point newDirection)
        {
            if (CanChangeDirection(newDirection))
            {
                nextDirection = newDirection;

                if (!(snakeHead.Location.X - nextDirection.X == body[1].Location.X
                    || snakeHead.Location.Y - nextDirection.Y == body[1].Location.Y)
                    || direction.IsEmpty)
                {
                    direction = nextDirection;
                }
            }
        }

        public void StopMoving()
        {
            direction = new Point(0, 0);
        }

        private bool CanChangeDirection(Point newDirection)
        {
            return Math.Abs(newDirection.X) == Math.Abs(direction.Y) || direction.IsEmpty;
        }

        private void BodyMove()
        {
            for (int i = body.Count - 1; i >= 1; i--)
            {
                Point newLocation = body[i - 1].Location;
                body[i].Move(newLocation);
            }
        }

        public void InitializeSnake(Point headLocation, int bodyLength)
        {
            DeleteSnake();

            snakeHead = new SnakeHead(headLocation, bodySize);
            body.Add(snakeHead);

            for (int i = 1; i < bodyLength; i++)
            {
                AddBodySell();
            }
        }

        private void DeleteSnake()
        {
            foreach (var item in body)
            {
                form.Controls.Remove(item.Cell);
            }
            body.Clear();
        }

        private void AddBodySell()
        {
            int Length = body.Count;
            SnakeBody lastCell = body[Length - 1];
            int locationX = lastCell.Location.X - bodySize.Width;
            Point currectLocation = new Point(locationX, lastCell.Location.Y);

            SnakeBody snakeBody = new SnakeBody(currectLocation, bodySize);
            snakeBody.ShowCell(form);
            body.Add(snakeBody);
        }

        public Point GetHeadLocation()
        {
            return body[0].Location;
        }
    }

    class SnakeBody
    {
        protected Button cell;
        protected readonly Size size;
        private readonly Color cellColor = Color.Green;

        public virtual void Move(Point newLocation)
        {
            cell.Location = newLocation;
        }

        public Button Cell
        {
            get { return cell; }
        }

        public Point Location
        {
            get { return cell.Location; }
            set { cell.Location = value; }
        }

        public SnakeBody(Point cellLocation, Size bodySize)
        {
            cell = new Button();
            this.size = bodySize;
            cell.Location = cellLocation;
            InitializeCell();
        }

        protected void InitializeCell()
        {
            cell.Enabled = false;
            cell.Size = size;
            cell.BackColor = cellColor;
            cell.FlatStyle = FlatStyle.Flat;
            cell.FlatAppearance.BorderSize = 0;
        }

        internal void ShowCell(Form form)
        {
            form.Controls.Add(cell);
            cell.BringToFront();
        }
    }

    class SnakeHead : SnakeBody
    {
        public SnakeHead(Point headLocation, Size headSize)
            : base(headLocation, headSize)
        {

        }

        public override void Move(Point distance)
        {
            int newX = cell.Location.X + distance.X * size.Width;
            int newY = cell.Location.Y + distance.Y * size.Width;
            cell.Location = new Point(newX, newY);
        }
    }
}
