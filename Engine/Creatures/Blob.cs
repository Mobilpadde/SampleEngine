using System.Windows.Controls;
using System.Windows.Media;

namespace Engine.Creatures
{
    public class Blob : Creature
    {
        /// <summary>
        /// A <see cref="Position"/> which the <see cref="Creatures.Blob"/> wishes to go
        /// </summary>
        internal Position Wish { get; private set; }

        private double _width;
        private double _height;

        /// <summary>
        /// Creates a new <see cref="Creatures.Hero"/>
        /// </summary>
        /// <param name="width">Width of the <see cref="Creatures.Hero"/></param>
        /// <param name="height">Height of the <see cref="Creatures.Hero"/></param>
        /// <param name="x">X-coordinate of the <see cref="Creatures.Hero"/></param>
        /// <param name="y">Y-coordinate of the <see cref="Creatures.Hero"/></param>
        /// <param name="c">The canvas to draw the <see cref="Creatures.Hero"/> on</param>
        public Blob(double lWidth, double lHeight, double width, double height, double x, double y, Canvas c) : base(width, height, x, y, c)
        {
            _width = lWidth;
            _height = lHeight;

            Body.Fill = Brushes.ForestGreen;

            Hp = 10;

            Wish = new Position(0, 0);
            checkWish(null, null);

            Position.PostionChanged += checkWish;
        }

        /// <summary>
        /// Moves the <see cref="Creatures.Blob"/> around
        /// </summary>
        public void Move()
        {
            if (Position.X < Wish.X)
                Position.Set(Position.X + 1, Position.Y);

            if (Position.X > Wish.X)
                Position.Set(Position.X - 1, Position.Y);

            if (Position.Y < Wish.Y)
                Position.Set(Position.X, Position.Y + 1);

            if (Position.Y > Wish.Y)
                Position.Set(Position.X, Position.Y - 1);
        }

        /// <summary>
        /// Checks if the wish has come to life
        /// </summary>
        /// <param name="s">The sender; not important</param>
        /// <param name="pos">The new <see cref="Position"/> of the <see cref="Creatures.Blob"/></param>
        private void checkWish(object s, Position pos)
        {
            if (Position.X == Wish.X && Position.Y == Wish.Y)
                Wish = new Position(
                ThreadSafeRandom.NextDouble(0, _width),
                ThreadSafeRandom.NextDouble(0, _height));
        }
    }
}
