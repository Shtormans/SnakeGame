using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    class Borders
    {
        private Border[] borders;
        private const int borderSize = 20;
        private const int verticalError = 39;
        private const int horizontalError = 16;

        public Point[] GetBorderLocations()
        {
            Point[] locations = new Point[borders.Length];

            for (int i = 0; i < borders.Length; i++)
            {
                locations[i] = borders[i].Location;
            }

            return locations;
        }

        public Borders(Form form)
        {
            borders = new Border[4];
            for (int i = 0; i < borders.Length; i++)
            {
                borders[i] = new Border();
            }

            Point buttonLocation = new Point(0, 0);
            Size buttonSize = new Size(form.Width - horizontalError, borderSize);
            borders[0].InitializeBorder(buttonLocation, buttonSize, form);

            buttonLocation = new Point(form.Width - borderSize - horizontalError, 0);
            buttonSize = new Size(borderSize, form.Height - verticalError);
            borders[1].InitializeBorder(buttonLocation, buttonSize, form);

            buttonLocation = new Point(0, form.Height - borderSize - verticalError);
            buttonSize = new Size(form.Width - horizontalError, borderSize);
            borders[2].InitializeBorder(buttonLocation, buttonSize, form);

            buttonLocation = new Point(0, 0);
            buttonSize = new Size(borderSize, form.Height - verticalError);
            borders[3].InitializeBorder(buttonLocation, buttonSize, form);

            foreach (var item in borders)
            {
                item.ShowBorder(form);
            }
        }
    }
    class Border
    {
        private Color borderColor = Color.Brown;
        private Button borderBody;

        public Point Location
        {
            get { return borderBody.Location; }
        }

        public void InitializeBorder(Point borderLocation, Size borderSize, Form form)
        {
            borderBody = new Button();
            borderBody.Enabled = false;
            borderBody.Size = borderSize;
            borderBody.Location = borderLocation;
            borderBody.BackColor = borderColor;
            borderBody.FlatStyle = FlatStyle.Flat;
            borderBody.FlatAppearance.BorderSize = 0;
        }

        public void ShowBorder(Form form)
        {
            form.Controls.Add(borderBody);
        }
    }
}
