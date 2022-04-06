using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    class Apple
    {
        private Button body;
        private Color appleColor = Color.Red;
        private readonly Size size = new Size(20, 20);
        public Point AppleLocation
        {
            get { return body.Location; }
        }

        public Apple(Form form)
        {
            body = new Button();
            body.BackColor = appleColor;
            body.FlatStyle = FlatStyle.Flat;
            body.FlatAppearance.BorderSize = 0;
            body.Size = size;
            body.Enabled = false;
            ChangeLocation(form.Size);

            form.Controls.Add(body);
        }

        public void ChangeLocation(Size fieldSize)
        {
            Random random = new Random();

            int minX = 1;
            int maxX = fieldSize.Width / size.Width - 1;
            int x = random.Next(minX, maxX) * size.Width;

            int minY = 1;
            int maxY = fieldSize.Height / size.Height - 2;
            int y = random.Next(minY, maxY) * size.Width;

            body.Location = new Point(x, y);
        }
    }
}
