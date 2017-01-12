using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Engine.Creatures
{
    public abstract class Creature
    {
        public double Hp { get; internal set; }
        public Ellipse Body;

        internal Size Size;
        internal Position Position;

        private Canvas _c;

        public Creature(double width, double height, double x, double y, Canvas c)
        {
            _c = c;

            Body = new Ellipse();

            Size = new Size(width, height);

            Body.Width = width;
            Body.Height = height;

            Position = new Position(x, y);
            Position.PostionChanged += MoveTo;

            Body.Margin = new Thickness(x, y, 0, 0);
        }

        private void MoveTo(object s, Position e)
        {
            _c.Dispatcher.InvokeAsync(new Action(() =>
            {
                Body.Margin = new Thickness(e.X, e.Y, 0, 0);
            }));
        }
    }
}
