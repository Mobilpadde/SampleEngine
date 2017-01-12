using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Engine.Creatures
{
    public abstract class Creature
    {
        /// <summary>
        /// How much HP a <see cref="Creature"/> shoudl have
        /// </summary>
        public double Hp { get; internal set; }

        /// <summary>
        /// The Body of the <see cref="Creature"/>
        /// </summary>
        public Ellipse Body;

        /// <summary>
        /// The <see cref="Size"/> of the <see cref="Creature"/>
        /// </summary>
        internal Size Size;

        /// <summary>
        /// The <see cref="Position"/> of the <see cref="Creature"/>
        /// </summary>
        internal Position Position;

        private Canvas _c;

        /// <summary>
        /// Creates a new <see cref="Creature"/>
        /// </summary>
        /// <param name="width">Width of the <see cref="Creature"/></param>
        /// <param name="height">Height of the <see cref="Creature"/></param>
        /// <param name="x">X-coordinate of the <see cref="Creature"/></param>
        /// <param name="y">Y-coordinate of the <see cref="Creature"/></param>
        /// <param name="c">The canvas to draw the <see cref="Creature"/> on</param>
        public Creature(double width, double height, double x, double y, Canvas c)
        {
            _c = c;

            Body = new Ellipse();

            Size = new Size(width, height);

            Body.Width = width;
            Body.Height = height;

            Position = new Position(x, y);
            Position.PostionChanged += moveTo;

            Body.Margin = new Thickness(x, y, 0, 0);
        }

        private void moveTo(object s, Position e)
        {
            _c.Dispatcher.InvokeAsync(new Action(() =>
            {
                Body.Margin = new Thickness(e.X, e.Y, 0, 0);
            }));
        }
    }
}
